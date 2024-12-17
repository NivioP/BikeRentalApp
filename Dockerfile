FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY BikeRentalApp.Api/BikeRentalApp.Api.sln ./
COPY BikeRentalApp.Api/BikeRentalApp.Api/*.csproj ./BikeRentalApp.Api/
COPY BikeRentalApp.Api/BikeRentalApp.Application/*.csproj ./BikeRentalApp.Application/
COPY BikeRentalApp.Api/BikeRentalApp.Domain/*.csproj ./BikeRentalApp.Domain/
COPY BikeRentalApp.Api/BikeRentalApp.Infrastructure/*.csproj ./BikeRentalApp.Infrastructure/

RUN dotnet restore

COPY BikeRentalApp.Api/BikeRentalApp.Api ./BikeRentalApp.Api
COPY BikeRentalApp.Api/BikeRentalApp.Application ./BikeRentalApp.Application
COPY BikeRentalApp.Api/BikeRentalApp.Domain ./BikeRentalApp.Domain
COPY BikeRentalApp.Api/BikeRentalApp.Infrastructure ./BikeRentalApp.Infrastructure

RUN dotnet publish ./BikeRentalApp.Api/BikeRentalApp.Api.csproj -c Release -o /app --no-restore -warnaserror:0

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app .

EXPOSE 80

ENTRYPOINT ["dotnet", "BikeRentalApp.Api.dll"]
