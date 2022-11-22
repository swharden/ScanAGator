rmdir /s /q X:\Software\Scan-A-Gator\v4
rmdir /s /q ..\src\ScanAGator\bin
dotnet publish --configuration Release ..\src
robocopy ..\src\ScanAGator\bin\Release\net48\publish X:\Software\Scan-A-Gator\v4 /E /NJH /NFL /NDL
pause