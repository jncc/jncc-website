$octopusActionName = $OctopusParameters["Octopus.Action.Name"]
$path = "C:\UmbracoLicenses\*"
$destination = $OctopusParameters["Octopus.Action[" + $octopusActionName + "].Output.Package.InstallationDirectoryPath"]

Copy-Item -path $path -Destination $destination -recurse -Force -Verbose