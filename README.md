# JNCC Website
The JNCC website. Redesigned in 2018 by Carbon Six Digital.

# Building
You can make a local build like this:

    msbuild src\JNCC.PublicWebsite.sln /p:Configuration=Release /p:RunOctoPack=true

On the build server, several more OctoPack options are passed to create a package suitable for Octopus Deploy.

# Hosting
The HTTP redirects list uses the IIS rewrite module. Unfortunately the size of the list now requires that the MaxWebConfigFileSizeInKB registry key be set to a higher-than-default limit.
