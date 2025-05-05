Download the library from the [DisplayDevices.dll](./bin/Release/net4.8.1/DisplayDevices.dll)

To use in PowerShell, load the library with

```pwsh
Add-Type -Path "./path/to/DisplayDevices.dll"
```

To retrieve the device interface name, set `$getInterfaceName=1`, otherwise it defaults to 0.

To list all display devices:
```pwsh
[DisplayDevices]::GetAll($getInterfaceName)
# or simply
[DisplayDevices]::GetAll()
```

To get display devices by its instance ID `$deviceID` (for example, **`LNX0000`** in `MONITOR\LNX0000\{UUID}\0000`):
```pwsh
[DisplayDevices]::FromID($deviceID, $getInterfaceName)
# or simply
[DisplayDevices]::FromID($deviceID)
```