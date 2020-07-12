#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Web/SimpleSocial.Web.API/SimpleSocial.Web.API.csproj", "Web/SimpleSocial.Web.API/"]
RUN dotnet restore "Web/SimpleSocial.Web.API/SimpleSocial.Web.API.csproj"
COPY . .
WORKDIR "/src/Web/SimpleSocial.Web.API"
RUN dotnet build "SimpleSocial.Web.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleSocial.Web.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleSocial.Web.API.dll"]