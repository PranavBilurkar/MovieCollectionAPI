FROM mcr.microsoft.com/dotnet/framework/sdk:4.8 AS build

WORKDIR /app

COPY /bin/Release/net48/ .

ENTRYPOINT ["MovieCollection.API.exe"]
