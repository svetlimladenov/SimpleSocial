FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

COPY . ./
RUN dotnet restore "SimpleSocial.sln"

WORKDIR /src/Web/Mvc/SimpleSocial.Web
#Publish in /app/build
RUN dotnet publish -c Release -o /app/build

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app/build .
EXPOSE 80
ENTRYPOINT ["dotnet", "SimpleSocial.Web.dll"]