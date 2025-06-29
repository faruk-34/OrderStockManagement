📌   Proje Raporu

Proje Açıklaması ve Nasıl Çalıştığı
Bu proje, mağazalardaki ürün stok seviyelerini takip eden bir sistemdir. Amaç, kritik seviyenin altına düşen ürünleri tespit etmek ve bu ürünleri Fake Store API ile eşleştirerek otomatik sipariş oluşturmaktır. Sistem, ürünleri bellek içi (in-memory) tutar ve HTTP üzerinden API istekleriyle çalışır.

 🔧 Kullanılan Teknolojiler
- Backend:  .NET 8  Web API
 - ORM: Entity Framework Core
- Veritabanı: Microsoft SQL Server
- Mimari: Domain-Driven Design (DDD) yaklaşımıyla

📐 Tasarım
Proje, DDD prensiplerine uygun şekilde inşa edilmiştir. `Domain`, `Application`, `Infrastructure`, `WebAPI olarak ayrılmıştır. Her katman kendi sorumlulukları çerçevesinde yapılandırılmıştır:
 - Domain  katmanı,   temel varlıkları barındırır.
 - Application  katmanı, servis arayüzleri ve iş mantığı uygulamalarını içerir.
 - Infrastructure  katmanı, veritabanı erişimi ve dış kaynaklara bağlanmak için kullanılır.
 - WebAPI  katmanı, API endpoint’lerini barındırır ve HTTP taleplerini karşılar.


API Uç Noktalarının Örnek İstek/Yanıtları

Post/Products
{
  "id": 1,   "name": "test",   "productCode": "1",   "threshold": 10,   "stock": 5
}

Get/ Products/low-stock
Response body
[ "Product: test, Stock: II Adet"  ]

Post/orders/ check-and-place
Response Body
[   {     "product": "Fjallraven - Foldsack No. 1 Backpack, Fits 15 Laptops",     "orderedQuantity": 8,     "pricePerUnit": 109.95   } ]

Fake Store API ile Nasıl Eşleştiği
Her ürün, ProductCode alanı aracılığıyla Fake Store API ürünleriyle eşleştirilir. Fake Store’dan gelen ürün listesi içinden en ucuz eşleşen ürün seçilir ve siparişe dahil edilir. Eşleştirme id == ProductCode üzerinden yapılır.

Bonus Görev Çözümü
•  Amaç: Verilen sayıyı Roma rakamına çevirmek.
•  Bir sözlük (map) ile sayı–Roma karşılıkları tutulur (örneğin 1000 = M, 900 = CM).
•  Sayı büyükten küçüğe doğru kontrol edilir.
•  Sayı, listedeki değerden büyükse:
•	Roma harfi sonuca eklenir.
•	Sayıdan bu değer çıkarılır.
•  Bu işlem sayı sıfır olana kadar devam eder.
•  En son, birleştirilen Roma rakamı string olarak döndürülür
