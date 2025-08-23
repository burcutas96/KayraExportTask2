# KayraExportApi

Bu proje, **Onion Architecture** kullanılarak geliştirilmiş bir **.NET Core Web API** uygulamasıdır. Projede **Swagger dokümantasyonu**, **JWT tabanlı kimlik doğrulama** ve **Redis Cache** entegrasyonu uygulanmıştır.

<br>

##  Özellikler

-    Ürün CRUD işlemleri
-    Kullanıcı Register ve Login işlemleri (JWT tabanlı kimlik doğrulama)
-    Onion Architecture ile katmanlı yapı
-    Redis Cache ile veri önbellekleme
-    Asenkron programlama ile performans optimizasyonu
-    CQRS pattern kullanımı
-    Global Exception Handling ile merkezi hata yönetimi
-    Swagger/OpenAPI dokümantasyonu

<br>

## Kullanılan Teknolojiler

-   .NET 7+
-   Entity Framework Core
-   PostgreSQL
-   Swagger / Swashbuckle
-   Redis

<br>

##  Kurulum

### 1. Projeyi Klonlayın

```bash
git clone <repository-url>
```

<br>

### 2. Proje Dizine Gidin ve Açın
Projeyi açmanın iki yolu vardır:

a) Terminal üzerinden Visual Studio ile açmak:

```bash
cd KayraExportTask2

start KayraExportTask2.sln
```
Bu komut Visual Studio’yu açar ve çözüm dosyasını yükler.

b) Manuel olarak açmak:
- Windows Gezgini ile KayraExportTask2 klasörüne gidin.

- KayraExportTask2.sln dosyasına çift tıklayarak Visual Studio’da açın.

<br>

### 3. Startup Projesini Ayarlayın
Visual Studio’da birden fazla proje varsa, **hangi projenin çalıştırılacağını** belirtmek önemlidir.

1. Solution Explorer’da `KayraExport.API` projesine sağ tıklayın. 
2. Açılan menüden **Set as Startup Project** seçeneğini seçin. 
3. Artık projeyi çalıştırdığınızda doğru proje (KayraExport.API) başlatılacaktır.

<br>

### 4. Veritabanı Bağlantısını Ayarlayın
`appsettings.json` dosyasındaki connection string'i güncelleyin:

```json
{
  "ConnectionStrings": {
    "PostgreSql": "User ID=YOUR_SERVER;Password=YOUR_PASSWORD;Host=localhost;Port=5432;Database=KayraExportDB;"
  }
}
```

<br>

### 5. Veritabanını Oluşturun

Migration dosyaları proje içinde yer almaktadır.  
Bu nedenle yalnızca aşağıdaki komutla veritabanını güncellemeniz yeterlidir:

```bash
# Package Manager Console (Visual Studio)
Update-Database


# Veya CLI
dotnet ef database update
```

<br>

### 6. Bağımlılıkları Yükleyin

```bash
dotnet restore
```

<br>


### 7. API'yi Çalıştırma

```bash
# Visual Studio'da:
Ctrl + F5

# CLI üzerinden:
dotnet run
```

<br>

##  Swagger Dokümantasyonu

Projeyi çalıştırdıktan sonra Swagger arayüzüne erişmek için:

👉 <http://localhost:5267/swagger/index.html>

Swagger üzerinden tüm endpointleri test edebilir, response tiplerini
görebilirsiniz.


