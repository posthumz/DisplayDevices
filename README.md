Download the library from the [DisplayDevices.dll](./bin/Release/net4.8.1/DisplayDevices.dll)

To use in PowerShell, load the library with

```pwsh
Add-Type -Path "./path/to/DisplayDevices.dll"
```

To list all display devices (`getInterfaceName` is either 0 or 1, defaults to 0):

```pwsh
[DisplayDevices]::GetAll(getInterfaceName)
```

To get display devices by its instance ID:

```pwsh
[DisplayDevices]::FromID("LNX0000", getInterfaceName)
```