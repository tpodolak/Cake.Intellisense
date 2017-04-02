## Cake.Intellisense
Cake and Cake addins metadata generator which provides partial intellisense support for Cake build scripts for omnisharp editors.

[![Build status](https://ci.appveyor.com/api/projects/status/5q5b7f3r6dkiie20?svg=true)](https://ci.appveyor.com/project/tpodolak/cake-intellisense)

## Code Coverage

[![Coverage Status](https://coveralls.io/repos/github/tpodolak/Cake.Intellisense/badge.svg?branch=master)](https://coveralls.io/github/tpodolak/Cake.Intellisense?branch=master)

## Usage

````
Cake.Intellisense.exe --help
````
### Examples
Generate metadata for ``Cake.Common 0.19.2`` targeting ``.NETFramework,Version=v4.5`` and push the result to ``C:\Output``
````
Cake.Intellisense.exe --Package Cake.Common --PackageVersion 0.19.2 --TargetFramework .NETFramework,Version=v4.5 --OutputFolder C:\Output
````
Generate metadata for newest version of ``Cake.Common`` targeting ``.NETFramework,Version=v4.5`` and push the result to ``C:\Output``
````
Cake.Intellisense.exe --Package Cake.Common --TargetFramework .NETFramework,Version=v4.5 --OutputFolder C:\Output
````
Generate metadata for newest version of ``Cake.Common`` targeting ``.NETFramework,Version=v4.5`` and push the result to current executing directory
````
Cake.Intellisense.exe --Package Cake.Common --TargetFramework .NETFramework,Version=v4.5
````
Generate metadata for newest version of ``Cake.Common`` and let me choose framework version 
````
Cake.Intellisense.exe --Package Cake.Common
````

## Enabling intellisense in VSCode
 * Add project.json file with empty JSON object into your build directory. 
 * Rename your ``build.cake`` script to ``build.csx``.
 * Copy metadata libraries somewhere into Build directory
 * Create ``imports.csx`` file. This is the file which holds all original namespace imports. It may look as follows
 ````c#
#load "./metadataImports.csx"
using Cake.Core;
using Cake.Core.IO;
using Cake.Common;
...
...
````
 * Create ``metadataimports.csx`` file. This is the file which holds metadata imports and loads cake and metadata references.  
 ````c#
 #r "./tools/Cake/Cake.Core.dll"
 #r "./tools/Cake/Cake.Common.dll"
 #r "./tools/Cake.Metadata/Cake.Core.Metadata.dll"
 #r "./tools/Cake.Metadata/Cake.Common.Metadata.dll"
 using static Cake.Common.ArgumentAliasesMetadata;
 using static Cake.Common.EnvironmentAliasesMetadata;
 ...
 ...
 ...
 ```` 
 * Load ``imports.csx`` to your ``build.csx`` file via ``#load "./imports.csx"``
 * Run ``VSCode`` install [ms-vscode.csharp](https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp) 1.7.0, open Build directory and write your build script with intellisense support
 ![](http://i.imgur.com/ZrdxFXG.gif)
 * Before running the script remember to comment out ``#load "./metadataImports.csx"`` from ``imports.csx`` file
 * Run build pointing to build.csx file ``build.ps1 -script build.csx -target Your-Target``
 
Refer to [my build scripts](https://github.com/tpodolak/Cake.Intellisense/tree/master/Build) if you have any troubles

 ## Known issues
  * Cake.Intellisense can only generate metadata libraries for standard .NET frameworks, it will fail if you try to create metadata targeting .NETStandard or .NET core framework
  * Due to some breaking changes in Omnisharp-Roslyn scripting support, intellisense will work only with Omnisharp 1.7.0
