function ApplyPermissions
{
    param([string]$path, [string]$users, [string]$permission)
 
    if(!(Test-Path $path ))
    {
        throw "$path does not exist"
    }
    else
    {
        $splitUsers = $users.Split(",")
        foreach($user in $splitUsers)
        {
            Write-Host "Adding write permissions for $user on path $path"
            $acl = (Get-Item $path).GetAccessControl('Access')
            $acl.SetAccessRuleProtection($False, $False)
            $rule = New-Object System.Security.AccessControl.FileSystemAccessRule($user, $permission, "ContainerInherit, ObjectInherit", "None", "Allow")
            $acl.AddAccessRule($rule)
            Set-Acl $path $acl
        }
    }
}

Write-Host "== Octopus PostDeploy script started. =="

$octopusActionName = $OctopusParameters["Octopus.Action.Name"]
$previousDestination = $OctopusParameters["Octopus.Tentacle.PreviousSuccessfulInstallation.OriginalInstalledPath"]
$destination = $OctopusParameters["Octopus.Action[" + $octopusActionName + "].Output.Package.InstallationDirectoryPath"]
$permissionsUsers = "IIS_IUSRS,NETWORK SERVICE,IUSR";

Write-Host "Copying Umbraco License files.`n From: \"$UmbracoLicensesPath\"`n To: \"$destination\"."
Copy-Item -Path $UmbracoLicensesPath -Destination $destination -Recurse -Force -Verbose;

Write-Host "Applying permissions."
ApplyPermissions "$destination\views" $permissionsUsers "Modify";

Write-Host "Copy Courier App_Data folder data from previous app to new app."
Copy-Item -Path "$previousDestination\App_Data\Courier" -Destination "$destination\App_Data\Courier" -Recurse -Force -Verbose;

Write-Host "== Octopus PostDeploy script completed. =="