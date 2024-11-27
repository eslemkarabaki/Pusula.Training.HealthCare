# Base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Çalışma dizinini ayarla
WORKDIR /app

# Proje dosyasını kopyala
COPY src/Pusula.Training.HealthCare.DbMigrator/ ./

# Build işlemi
RUN dotnet restore
RUN dotnet build -c Release -o out

# Uygulamayı çalıştır
ENTRYPOINT ["dotnet", "Pusula.Training.HealthCare.DbMigrator.dll"]
