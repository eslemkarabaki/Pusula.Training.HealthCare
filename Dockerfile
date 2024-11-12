FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 80

RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS publish
WORKDIR /src

COPY . .
WORKDIR /src/src/Pusula.Training.HealthCare.Blazor
RUN dotnet publish "Pusula.Training.HealthCare.Blazor.csproj" -c Release -o /app/publish

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Pusula.Training.HealthCare.Blazor.dll"]
