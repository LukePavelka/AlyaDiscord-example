FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

COPY *.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/runtime:5.0
WORKDIR /app
COPY config.json .
COPY Cide/ActiveChannel.json .
COPY config.example.json .
COPY --from=build /app .
ENTRYPOINT ["dotnet", "AlyaDiscord.dll"]