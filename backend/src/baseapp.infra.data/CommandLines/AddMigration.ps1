cd ..

dotnet ef migrations add $("BaseApp_$([int64](([datetime]::UtcNow)-(get-date "1/1/1970")).TotalSeconds)") --startup-project ..\baseapp.api --verbose