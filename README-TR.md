dapper_t2 dapper ile yapılacak işlemleri kısaltmak için yazılmış ufak bir kütüphanedir.(DapperT2 klasörü test için yazılmış basit bir MVC projesi içermektedir.)

DapperT2=> Test için yazılmış basit bir MVC projesi içermektedir.

DapperDataLayer=>Dapper ile etkileşime giren ve plugini oluşturan dosyalar buradadır.

Başlamadan önce -yok ise- dapper nuget'i yüklenmelidir

dapper_t2 DapperDataLayer sınıf kütüphanesinden ibarettir. Beraberinde kullanılacağı projeye ayrı bir proje olarak eklenmesi yeterlidir. Helper Class'ı ile yanına eklendiği projenin web.config dosyasındaki bağlantı cümleciğini çekerek veritabanına bağlanır. Dolayısıyla yanına eklendiği projenin config dosyasına şu satırlar eklenmelidir.

``` <add name="DapperDefault" connectionString="Data Source=**;Initial Catalog=**;User ID=**;Password=**" providerName="System.Data.SqlClient" /> ```

DapperDefault ismi sabit değildir DapperDataLayer/Access/Helper.cs sınıfında değiştirilebilir. 
  
Eğer veritabanından veri çekilecekse gerekli data classı ve o class'ın propertyleri doğru sekilde yaratılmalıdır. Veritabanındaki kolon ismi ile property ismi uyuşmalıdır. Bütün kolonlar için property yaratmak gerekmez.Ama yaratılan propertyler kolon isimleri ile uyuşmalıdır.
 
 Bu projede DDLayer/Core klasörü içinde AracModel sınıfı kullanılmıştır.
 
 Öncelikle kullanılacak yerde DB nesnesi yaratılır. DB nesnesi yok edilebilir olduğu için using ile kullanılabilir
 ```
 using(DDLayer.Access.DB _db = new DDLayer.Access.DB())
  {
      //db kodları
  } 
 ``` 
  
  _db nesneleri son parametre olarak bool bir değer alabilirler. Eğer bu değer false ise sorguya ait parametre listesi boşaltılmayacaktır. Default değer true olduğu için herhangi bir parametre verilmezse parametreler sıfırlanacaktır. Manuel olarak bu değere false verilirse iki metod arasında mutlaka aşağıdaki metod kullanılmalıdır.
  
  ```_db.FlushParameter()``` 
  
Yoksa hata verecektir.
  
 # Örnek Select Sorgusu
  
  ``` 
  _db.SelectQuery<dynamic>("UPDATE AT_ANASAYFAMETINLER SET BASLIK = 'Lorem İpsum17', EKLEYENISIM = 'bhg' where ID=7",false); 
  _db.FlushParameter()//sadece eğer parametre olarak manuel olarak false verildiyse gereklidir.
  ```
  
  # Örnek Parametre ile Stored Procedure (Update,Insert)
  ```
   _db.AddParameter("@ID",221831); //Parametre  
   _db.AddParameter("@ADET",0);    //Parametre
   _db.AddParameter("@NOT","Gene"); //Parametre           
   _db.SelectSP("ap_Sepet_Guncelle_Web"); //stored proc
  ```
 # Örnek parametreli geriye tablo dönen Stored Procedure (Select)
 
 ```
  //SelectSP generic liste döndüren bir modeldir. Bu yüzden bu noktada daha önce yaratılmış data class'ı kullanılmalıdır
  
   _db.AddParameter("@MARKA", "ALFA ROMEO");
   var list= _db.SelectSP<AracModel>("ap_AracModel");        //Geri AracModel tipinden liste dönecektir.      
  ```
 # Output parametreli geriye tablo dönen Stored Procedure(Select)
 ```
  List<DDLayer.Access.Dparam> outputs = new List<DDLayer.Access.Dparam>();
  _db.AddParameter("@CARIKOD", "M 00000");
  _db.AddOutputParameter("@BORC", DbType.Double); //Output parametrelerin DBType'ı önden verilmelidir
  _db.AddOutputParameter("@ALACAK", DbType.Double);
  var list2 = _db.SelectSP<CARILISTE>("ap_GetCariHareket",out outputs);    //Onceden yaratılmıs liste out anahtar sözcüğü ile metoda gönderilmelidir 
  ```
  
