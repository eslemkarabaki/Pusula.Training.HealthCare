FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Proje dosyalarını kopyala
COPY . .

# Gerekli workload'ları yükle
RUN dotnet workload restore

# Projeyi restore et
RUN dotnet restore

# Blazor projesine odaklan ve derle
WORKDIR /src/src/Pusula.Training.HealthCare.Blazor
RUN dotnet publish "Pusula.Training.HealthCare.Blazor.csproj" -c Release -o /app/blazor

# Runtime için image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Derlenmiş dosyaları kopyala
COPY --from=build /app/blazor .

# Uygulamayı başlat
ENTRYPOINT ["dotnet", "Pusula.Training.HealthCare.Blazor.dll"]
