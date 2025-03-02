
# HealthCare

Pusula.Training.HealthCare modern bir muayenehane yönetim sistemi geliştirmeyi amaçlayan kapsamlı bir projedir. ABP Framework ve Blazor teknolojilerini temel alarak, projenin çeşitli modülleri entegre edilmiş ve her biri ayrı bir işlevsel alanı yönetmek üzere tasarlanmıştır. 



## Kullanılan Teknolojiler

**ABP Framework:**  Katmanlı mimari, modüler yapı ve çoklu dil desteği sağlamanın yanı sıra, dinamik API oluşturma, gelişmiş izin yönetimi ve modül yaşam döngüsü yönetimi gibi özellikler sunar. Bu sayede proje daha genişletilebilir ve yönetilebilir bir hale getirilmiştir.

**Blazor:** Modern ve interaktif web arayüzleri oluşturmak için kullanılmıştır. Syncfusion UI bileşenleri ve SignalR entegrasyonları ile zengin ve dinamik bir kullanıcı arayüzü sağlanmıştır. Blazor’un bileşen tabanlı yapısı ve hızlı geliştirme süreçleri, proje bakımını ve geliştirilmesini kolaylaştırmıştır.

**Syncfusion Components:** Blazor tabanlı gelişmiş UI bileşenleri kullanılmıştır. Özellikle veri tabloları, belge görüntüleyiciler ve takvim bileşenleri, kullanıcı arayüzünü daha işlevsel ve kullanıcı dostu hale getirmiştir.

**SignalR:** Gerçek zamanlı bildirimler sağlamak için kullanılmıştır. Örneğin, Radyoloji modülünde, doktorlar tarafından yapılan tetkik taleplerinin teknisyenlere iletilmesi ve sonuçların anında doktorlara bildirilmesi SignalR ile gerçekleştirilmiştir.

**Elasticsearch ve Kibana:** Loglama ve arama süreçlerinde kritik rol oynamaktadır. Elasticsearch, uygulama loglarının merkezi bir şekilde depolanmasını ve aranabilir hale getirilmesini sağlar. Kibana ise bu logları görselleştirerek sistemin genel durumu hakkında detaylı analizler yapılmasına olanak tanır.

**PostgreSQL:**  Projede kullanılan güçlü veritabanı çözümüdür. Büyük veri setlerini işleyebilme ve veri tutarlılığını sağlama konusunda yüksek performans sunmaktadır.

**Redis:** Performansı artırmak ve mesajlaşma altyapısını yönetmek için kullanılmıştır. Redis, sık kullanılan verilerin cache'lenmesini sağlayarak veritabanı sorgularını azaltır ve hızlı veri erişimi sunar. Ayrıca mesaj kuyruğu görevlerini yöneterek asenkron iletişimi destekler.

**Docker:** Projede kullanılan PostgreSQL, Redis, RabbitMQ ve Elasticsearch gibi hizmetlerin kolayca yapılandırılmasını ve çalıştırılmasını sağlamıştır. Docker, geliştirme ve dağıtım süreçlerinde zaman tasarrufu sağlarken, hata riskini de azaltmıştır.



  
## Projenin Kurulum Aşamaları 

Bu projeyi çalıştırmak için aşağıdaki adımları takip etmelisiniz.
  
### .NET 8 Kurulumu
En güncel .NET 8 sürümünü [buradan](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) indirebilirsiniz.

### ABP Framework Kurulumu
Terminalde şu komutu çalıştırarak ABP Framework CLI'ı yükleyin:

```bash
  dotnet tool install -g Volo.Abp.Cli
```

### Visual Studio 2022 Community Edition Kurulumu
[Bu linkten](https://visualstudio.microsoft.com/tr/downloads/) Visual Studio 2022'yi indirip kurabilirsiniz.
- ASP.NET ve Web Geliştirme
- Veri Depolama ve İşleme paketlerini yüklemeniz yeterli olacaktır.

### Docker Desktop Kurulumu
Docker Desktop' [buradan](https://www.docker.com/products/docker-desktop/) kurabilirsiniz.

### Projenin Çalıştırılması
Terminalde proje dizininde şu komutları çalıştırın:

```bash
  Set-ExecutionPolicy -ExecutionPolicy RemoteSigned
  .\run-docker.ps1
```

PostgreSQL, Redis, RabbitMQ, Elasticsearch, Kibana ve PgAdmin uygulamalarına erişim sağlanacaktır.

### Veritabanı Bağlantısı
Visual Studio'da `DBMigrator` projesini çalıştırarak gerekli veritabanı tablolarını oluşturabilirsiniz.

### Proje Ön Yüzü
Blazor projesini çalıştırarak uygulamanın arayüzüne erişebilirsiniz.

`Kullanıcı Adı: admin`

`Şifre: 1q2w3E*`
## Modüller Hakkında

Bu proje 5 ana modülden oluşmaktadır:

#### Hasta Kabul Modülü
#### Randevu Modülü
#### Tedavi Modülü
#### Laboratuvar Modülü
#### Radyoloji Modülü

## Test (Laboratuvar) Modülü
Test (Laboratuvar) Modülü, kliniklerdeki laboratuvar işlemlerinin yönetimini sağlayan ve bu süreçlerin her aşamasını dijital ortamda takip etmeye olanak tanıyan bir modüldür. Bu modül, doktorlar ve laboratuvar teknisyenleri için kapsamlı bir test yönetim sistemi sunarak, hasta başvurusundan sonuçların raporlanmasına kadar olan tüm süreçleri verimli bir şekilde yönetmeyi amaçlar. Test modülü, projenin gereksinimleri tarafımca geliştirilmiştir.

#### Kullanıcı Rolleri
**Doktorlar:** Hastaların laboratuvar test sonuçlarını görüntüleyebilir, test talepleri oluşturabilir.
**Laboratuvar Teknisyenleri:** Test taleplerini görüntüleyebilir, numune girişlerini yapabilir, test sonuçlarını sisteme ekleyebilir ve belgeleri yükleyebilir.
Özellikler
**Tanım ve Rapor Sayfaları:** Test grupları ve test tanımları yapılabilir. Ayrıca, filtrelenmiş raporlar görüntülenebilir, bu da kullanıcıların belirli kriterlere göre arama yapmasını kolaylaştırır.

**Yetkilendirme:** Kullanıcı rolleri ve erişim yetkileri belirlenmiştir. Bu sayede her kullanıcı yalnızca yetkili olduğu verilere erişebilir.

#### Veritabanı Tabloları
TestProcesses: Laboratuvar test belgelerinin saklandığı tablo.
TestGroups: Test gruplarının tanımlandığı ve sınıflandırıldığı tablo.
Tests: Gerçekleştirilen laboratuvar testlerinin saklandığı tablo.
TestRequests: Laboratuvar test taleplerinin bulunduğu tablo.
TestRequestItems: Laboratuvar test taleplerine ait ayrıntılı öğelerin saklandığı tablo.
