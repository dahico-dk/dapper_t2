dapper_t2 is a tiny plugin written for shortening the dapper processes. (DapperT2 folder includes a simple MVC project written for testing.) 

DapperT2=> includes a simple MVC project written for testing. 

DapperDataLayer=> The main files of the plugin consists here.

Before starting  dapper nuget should be installed. - if not installed -

dapper_t2 consists of DapperDataLayer class library. It is enough to add it as a separate project to the project it will be used with. Plugin gets the connection string from config file. Thus, the following lines must be added to the config file of the project it is added. 

``` <add name="DapperDefault" connectionString="Data Source=**;Initial Catalog=**;User ID=**;Password=**" providerName="System.Data.SqlClient" /> ```

DapperDefault name is not fixed.


If data will be retrieved from database, necessary data class and properties of that class must be created correctly. Column name in the database and the property name must match. It is not necessary to create property for all columns, but created properties must match the column names. 

In this project, AracModel class was used in DDLayer/Core folder. 

DB Object is disposable. It can used with using phrase.

```
 using(DDLayer.Access.DB _db = new DDLayer.Access.DB())
  {
      //db codes
  } 
 ``` 

_db objects can get a bool value as a final parameter. If this value is false, the parameter list of the query will not be cleared. Default value for this parameter is true. If this value is manually given false, the following method must be used between two methods. 

 ```_db.FlushParameter()```


Otherwise, it will throw an error. 


# Select Query 

 ``` 
  _db.SelectQuery<dynamic>("UPDATE AT_ANASAYFAMETINLER SET BASLIK = 'Lorem İpsum17', EKLEYENISIM = 'bhg' where ID=7",false); 
  _db.FlushParameter()//Only necessary if final parameter is false
  ```

# Stored Procedure with Parameter (Update, Insert) 

```
   _db.AddParameter("@ID",221831); //Parameter  
   _db.AddParameter("@ADET",0);    //Parameter
   _db.AddParameter("@NOT","Gene"); //Parameter           
   _db.SelectSP("ap_Sepet_Guncelle_Web"); //stored proc
  ```
  
# Stored Procedure with Parameter which returns table values (Select)

```
  //SelectSP is a generic method.
  
   _db.AddParameter("@MARKA", "ALFA ROMEO");
   var list= _db.SelectSP<AracModel>("ap_AracModel");    
  ```
  
# Stored Procedure with Output Parameter which returns table values (Select) 

```
  List<DDLayer.Access.Dparam> outputs = new List<DDLayer.Access.Dparam>();
  _db.AddParameter("@CARIKOD", "M 00000");
  _db.AddOutputParameter("@BORC", DbType.Double); //Output parameters return type must be passed as DbType parameter
  _db.AddOutputParameter("@ALACAK", DbType.Double);
  var list2 = _db.SelectSP<CARILISTE>("ap_GetCariHareket",out outputs);  
  ```
