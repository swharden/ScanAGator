rmdir /s /q X:\Software\Scan-A-Gator
rmdir /s /q ..\src\ScanAGator\bin
dotnet publish --configuration Release ..\src
robocopy ..\src\ScanAGator\bin\Release\net48\publish X:\Software\Scan-A-Gator /E /NJH /NFL /NDL
pause