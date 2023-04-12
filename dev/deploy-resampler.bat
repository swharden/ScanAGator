rmdir /s /q X:\Software\Resampler
rmdir /s /q ..\src\Resampler\bin
dotnet publish --configuration Release ..\src\Resampler
robocopy ..\src\Resampler\bin\Release\net7.0-windows\publish X:\Software\Resampler /E /NJH /NFL /NDL
pause