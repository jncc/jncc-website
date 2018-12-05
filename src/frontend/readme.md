# Frontend Build

The following readme will guide a developer through using the Frontend Build.


## Getting Started

If you not previously used the Frontend Build ensure you have [NPM installed](https://www.npmjs.com/get-npm).

Once NPM is installed.

 1. Navigate to this folder in your Command Line Interface (CLI) of choice (e.g.  Command Prompt/Terminal/PowerShell).
 2. Run the following command `npm install`. 


## Performing a build
Once all NPM packages are installed run one of the following commands:


### 1. Development Build

A development build is useful to make changes and have those changes sync which automatically builds & synchronises files with a local development server running on your browser on localhost. This version is useful for experimental changes before they are ready to be used in production.

To run a development build enter the following command `grunt dev` (or simply `grunt`) in the frontend build folder.


### 2. Production Build

A production build simply performs a number of changes which builds & optimises the source files without creating a local development server.

To run a production build enter the following command `grunt dist` in the frontend build folder.


## Important

Currently frontend changes are managed in two places:

 1. src/frontend
 2. src/JNCC.PublicWebsite

Any changes made in the frontend build must be ran through a Production Build first even if a Development build was ran before it.

The frontend folder will be updated by the Production Build then the following folders must be mirrored to the JNCC.PublicWebsite folder:

 - /css
 - /fonts
 - /images
 - /js

Frontend changes should not be made directly in the JNCC.PublicWebsite folder.