FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

RUN dotnet workload restore

RUN dotnet restore

WORKDIR /src/src/Pusula.Training.HealthCare.Blazor
RUN dotnet publish "Pusula.Training.HealthCare.Blazor.csproj" -c Release -o /app/blazor

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/blazor .

ENTRYPOINT ["dotnet", "Pusula.Training.HealthCare.Blazor.dll"]
