Set-Location -Path .\POS\client-app
Remove-Item -Path .\build\* -Recurse
npm run build
Remove-Item -Path ..\wwwroot\* -Recurse
Copy-Item ".\build\*" -Destination ..\wwwroot -Recurse -Force
Set-Location -Path ..
dotnet build
electronize build /target win
explorer .\bin\desktop\electron.net.host-win32-x64
