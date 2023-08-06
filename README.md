# Dapper_Big_Data

## Description

Dapper Big Data, büyük veri işleme için Dapper ORM (Object-Relational Mapping) kütüphanesini kullanmayı amaçlayan bir projedir. Bu projede, Dapper ORM'in sağladığı hız ve performans avantajlarından yararlanarak büyük veri kümesini etkili bir şekilde sorgulama ve işleme imkanı sağlanmaktadır.



## Tecnologies

- Dapper(ORM)
- .Net Core 6.0
- Mssql(Optional)
- Chart.js
- Caching



## Screenshots

![dapper_Trailer](https://github.com/goktugfevzi/Dapper_Big_Data/assets/64567701/5d572dd4-7882-44ce-9fc9-4f13fd519f44)

<img width="947" alt="ss1" src="https://github.com/goktugfevzi/Dapper_Big_Data/assets/64567701/cab3e0ad-f356-4ead-bc45-65ca91840467">

<img width="947" alt="ss2" src="https://github.com/goktugfevzi/Dapper_Big_Data/assets/64567701/7a04b94a-4479-4f73-b2c4-3e86e6abab29">

<img width="950" alt="ss3" src="https://github.com/goktugfevzi/Dapper_Big_Data/assets/64567701/c43ec9a8-f0bb-4168-810f-f1c982a3d3dd">




## Instalation

Bu repoyu kendi bilgisayarınıza klonlayın veya indirin:

```sh
git clone https://github.com/goktugfevzi/Dapper_Big_Data.git
```

Veri Seti ve Veritabanı Kurulumu: Aşağıdaki Veri setini indirin ve yedeğinizi bir SQL Server veritabanına kurun.

https://www.kaggle.com/datasets/omercolakoglu/turkish-car-plate-dataset-with-fake-data


Daha İyi Performans İçin Dizinleme: Veri alım performansını optimize etmek için, aşağıdaki SQL komutlarını veritabanınızdaki yönetim aracında (örneğin, SQL Server Management Studio) çalıştırın. Belirli sütunlarda dizin oluşturarak veri alım sorgularının hızını artırır:

```sh
CREATE NONCLUSTERED INDEX IX_PLATE ON dbo.PLATES (PLATE);
CREATE NONCLUSTERED INDEX IX_SHIFTTYPE ON dbo.PLATES (SHIFTTYPE);
CREATE NONCLUSTERED INDEX IX_BRAND ON dbo.PLATES (BRAND);
CREATE NONCLUSTERED INDEX IX_FUEL ON dbo.PLATES (FUEL);`
```
Veritabanı configurasyonlarınızı tamamlayın.

Yukarıdaki adımları tamamladıktan sonra projeyi Visual Studio'da çalıştırabilirsiniz.

## License
Thanks My Mom
