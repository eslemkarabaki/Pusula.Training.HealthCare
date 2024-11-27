# Base image olarak .NET 8 SDK kullanıyoruz
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Çalışma dizinini ayarla
WORKDIR /app

# Proje dosyasını kopyala ve restore işlemini yap
COPY ["Pusula.Training.HealthCare.DbMigrator/", "./"]

RUN dotnet restore

# Tüm kaynak dosyaları kopyala ve build yap
COPY . .
RUN dotnet publish -c Release -o /publish

# Final image: .NET 8 Runtime kullanarak uygulamayı çalıştır
FROM mcr.microsoft.com/dotnet/runtime:8.0

# Çalışma dizinini ayarla
WORKDIR /app

# Yayınlanmış dosyaları kopyala
COPY --from=build /publish .

# Appsettings dosyasını kopyala (Production ortamı için)
COPY appsettings.Production.json .

# Uygulamayı çalıştır
ENTRYPOINT ["dotnet", "Pusula.Training.HealthCare.DbMigrator.dll"]
