DapperT2 dapper ile yapılacak işlemleri kısaltmak için yazılmış ufak bir kütüphanedir.

Başlamadan önce -yok ise- dapper nuget'i yüklenmelidir

DapperT2 DDLayer sınıf kütüphanesinden ibarettir. Helper Class'ı ile yanına eklendiği projenin web.config dosyasındaki bağlantı cümleciğini çekerek veritabanına bağlanır. Dolayısıyla yanına eklendiği projenin config dosyasına şu satırlar eklenmelidir.

``` <add name="DapperDefault" connectionString="Data Source=**;Initial Catalog=**;User ID=**;Password=**" providerName="System.Data.SqlClient" /> ```
  
Eğer veritabanından veri çekilecekse gerekli data classı ve o class'ın propertyleri doğru sekilde yaratılmalıdır. Veritabanındaki kolon ismi ile property ismi uyuşmalıdır. Bütün kolonlar için property yaratmak gerekmez.Ama yaratılan propertyler kolon isimleri ile uyuşmalıdır.
 
 Bu projede DDLayer/Core klasörü içinde AracModel sınıfı kullanılmıştır.
 
 Öncelikle kullanılacak yerde DB nesnesi yaratılır
 
``` DDLayer.Access.DB _db = new DDLayer.Access.DB(); ```
  
  eğer _db nesnesi tekrar tekrar aynı yerde kullanılacaksa veritabanı işlemleri arasında
  ```_db.FlushParameter()``` 
  metodu kullanılmalıdır. Yoksa hata verecektir.
  
 # Örnek Select Sorgusu
  
  ``` _db.SelectQuery<dynamic>("UPDATE AT_ANASAYFAMETINLER SET BASLIK = 'Lorem İpsum17', EKLEYENISIM = 'bhg' where ID=7");
  _db.FlushParameter();   //bu noktada tekrar kullanılacağı için flush metodu kullanılıyor.
  ```
  
  # Örnek Parametre ile Stored Procedure (Update,Insert)
  ```
   _db.AddParameter("@ID",221831); //Parametre  
   _db.AddParameter("@ADET",0);    //Parametre
   _db.AddParameter("@NOT","Gene"); //Parametre           
   _db.SelectSP("ap_Sepet_Guncelle_Web"); //stored proc
  _db.FlushParameter();
  ```
 # Örnek parametreli geriye tablo dönen Stored Procedure (Select)
 
 ```
  //SelectSP generic liste döndüren bir modeldir. Bu yüzden bu noktada daha önce yaratılmış data class'ı kullanılmalıdır
  
   _db.AddParameter("@MARKA", "ALFA ROMEO");
   var list= _db.SelectSP<AracModel>("ap_AracModel");        //Geri AracModel tipinden liste dönecektir.      
   _db.FlushParameter();
  ```
 # Output parametreli geriye tablo dönen Stored Procedure(Select)
 ```
  List<DDLayer.Access.Dparam> outputs = new List<DDLayer.Access.Dparam>();
  _db.AddParameter("@CARIKOD", "M 00000");
  _db.AddOutputParameter("@BORC", DbType.Double); //Output parametrelerin DBType'ı önden verilmelidir
  _db.AddOutputParameter("@ALACAK", DbType.Double);
  var list2 = _db.SelectSP<CARILISTE>("ap_GetCariHareket",out outputs);    //Onceden yaratılmıs liste out anahtar sözcüğü ile metoda gönderilmelidir 
  ```
  
