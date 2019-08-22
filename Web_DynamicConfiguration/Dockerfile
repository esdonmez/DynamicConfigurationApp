FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY Web_DynamicConfiguration/Web_DynamicConfiguration.csproj Web_DynamicConfiguration/
RUN dotnet restore Web_DynamicConfiguration/Web_DynamicConfiguration.csproj
COPY . .
WORKDIR /src/Web_DynamicConfiguration
RUN dotnet build Web_DynamicConfiguration.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Web_DynamicConfiguration.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Web_DynamicConfiguration.dll"]
