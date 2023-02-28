rmdir /s /q X:\Software\ImageRatioTool
rmdir /s /q ..\src\ImageRatioTool\bin
dotnet publish --configuration Release ..\src\ImageRatioTool
robocopy ..\src\ImageRatioTool\bin\Release\net7.0-windows\publish X:\Software\ImageRatioTool /E /NJH /NFL /NDL
pause