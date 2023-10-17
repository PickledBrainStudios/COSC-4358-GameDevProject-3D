# The Cult of the Crows 3D

## Troubleshooting

#### Error 1
Failed to load project.
The project file `\COSC-4358-GameDevProject-3D\Assembly-CSharp.csproj` is in unsupported format (for example, a traditional .NET Framework project). It need be converted to new SDK style to work in C# Dev Kit.

#### Solution 1
Add `<TargetFramework>net7.0</TargetFramework>` below `<TargetFrameworkVersion>v4.7.1</TargetFrameworkVersion>`


