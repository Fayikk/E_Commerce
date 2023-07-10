# E_Commerce
ForUdemy
Projeyi Ayağa Kaldırmak için yapmanız gerekenler;

https://visualstudio.microsoft.com/vs/community/ adresinde ilgili ide'yi indirin Community Versiyonu uygundur.
Projeyi Clone'layın ve Solution ile projeyi açın.
Projede öncellikler Kendi veritabanı bağlantılarınız yapmanız gerekmektedir.Local'de SqlServer kullanılmaktadır.
Server ve api katmanında appsetting.json dosyalarında ConnectionStrings içerisindeki server ismini kendi local Sql isminizi yapmanız yeterli olacaktır.
Aynı zamanda Ide'de package manager console yardımı ile yada cmd ile projenin temel dizinine gidip;
drop-database komutunu çalıştırın(package manager console'da Katman olarak DataAccess seçili olmalı aynı zamanda SERVER katmanına sağ tık yapıp SetAStartup seçeneği seçilmelidir).
Ardından Data_Access katmanından Migration Klasörünü silip.
Tekrardan Konsola add-migration Init ile tabloları codefirst ile oluşturabilirsiniz.
Son olarak solution katmanına sağ tıklayıp properties seçerek.API,SERVER VE CLİENT  katmanlarını SetAStartup'a konumlandırın.
Ve projeyi IIS Express ile ayağa kaldırabilirsiniz.
