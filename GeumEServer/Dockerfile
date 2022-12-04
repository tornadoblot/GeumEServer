#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["GeumEServer/GeumEServer.csproj", "GeumEServer/"]
RUN dotnet restore "GeumEServer/GeumEServer.csproj"
COPY . .
WORKDIR "/src/GeumEServer"
RUN dotnet build "GeumEServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GeumEServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GeumEServer.dll"]