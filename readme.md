# csh-io-copy-file

A command-line program for copying files with flags. This program mimics basic file copy (`cp`) functionality in Windows with options for verbose logging, overwriting, and quiet mode.

## features

File Copy: 
- Copies files from a source to a destination. If the destination path is a directory, the original filename is preserved.

Flags:
- `-v` or `--verbose`: Enables verbose mode, displaying detailed information about the copy process.
- `-o` or `--overwrite`: Allows overwriting an existing file in the destination path.
- `-q` or `--quiet`: Suppresses all output except error messages.

Error Handling:
- Checks if the source file exists.
- Handles common exceptions.

## how to build and test

To build and test this program as `cp.exe`, follow these steps:

1. buils as `cp.exe` 

1.1 the `csh-io-copy-file.csproj` file has `<AssemblyName>cp</AssemblyName>` line inside the `<PropertyGroup>` section

1.2 build config has custom Build Event which creates temporary alias 
```powershell
powershell -Command "Remove-Item Alias:cp -Force; New-Alias -Name cp -Value '$(TargetPath)'"
```

1.3 build config has custom Build Event which creates temporary test file 
```powershell
echo This is a test file. > "$(TargetDir)testfile.txt"
```

2. for CMD, 'cp' is not recognized as an internal or external command, run safely `C:\Users\ag\source\repos\csh-io-copy-file\bin\Debug\net8.0\cp.exe`

3. for PowerShell, manually create a temporary alias for your cp.exe program and override the built-in cp alias, this alias will only be active in the current PowerShell session 
```powershell
Remove-Item Alias:cp -Force
New-Alias -Name cp -Value "C:\Users\ag\source\repos\csh-io-copy-file\bin\Debug\net8.0\cp.exe"
```

## usage

```powershell
cp <source> <destination> [-v | --verbose] [-o | --overwrite] [-q | --quiet]
```
