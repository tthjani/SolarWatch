﻿FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base
WORKDIR /app
EXPOSE 5262

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SolarWatch.csproj", "."]
RUN dotnet restore "SolarWatch.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "SolarWatch.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SolarWatch.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SolarWatch.dll"]

ENV ASPNETCORE_URLS=http://+:5262/