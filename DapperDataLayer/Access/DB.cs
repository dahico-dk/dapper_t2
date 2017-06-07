using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DDLayer.Access
{
   
    public class DB
    {
        private List<Dparam> parametreler;
        private List<Dparam> outputparametreler;
        
        public DB()
        {
            parametreler = new List<Dparam>();
            outputparametreler = new List<Dparam>();
        }

        /// <summary>connection string ve query alır. Geri dönüş tipi olmayan querylerde generic tipi olarak dynamic kullanılabilir.<para />
        /// Generic metod oldugu için tiplerin baştan belirtilmesi gerekir.<para />
        /// TEKRAR TEKRAR AYNI DB NESNESI KULLANILACAKSA ARALARINDA FLUSHPARAMETER() METODU KULLANILMALI <para />
        /// </summary>
        public List<T> SelectQuery<T>(string query)
        {
            try {using (IDbConnection _db = new SqlConnection(Helper.connectionstring())) { return _db.Query<T>(query).ToList(); } }         
            catch (Exception e) 
            {
                e = e;
                throw;
            }        
        }

        /// <summary>**!!!Sadece Düz parametre alan procedureler için. Output parametre almaz.!!!***<para />
        /// Generic metod oldugu için tiplerin baştan belirtilmesi gerekir.       <para />
        /// TEKRAR TEKRAR AYNI DB NESNESI KULLANILACAKSA ARALARINDA FLUSHPARAMETER() METODU KULLANILMALI <para />
        /// </summary>
        public List<T> SelectSP<T>(string procadi)
        {
            try
            {
                using (IDbConnection _db = new SqlConnection(Helper.connectionstring()))
                {
                    var p = new DynamicParameters();
                    foreach (var item in parametreler) { p.Add(item.name, item.value); }
                    var donenliste = _db.Query<T>(procadi, p, commandType: System.Data.CommandType.StoredProcedure);
                    //var yorumlar = _db.Query<AracModel>("ap_AracModel", p, commandType: System.Data.CommandType.StoredProcedure);
                    List<T> sonliste = new List<T>();
                    foreach (var item in donenliste) { sonliste.Add(item); }
                    return sonliste;
                }
            }
            catch (Exception e)
            {
                e = e;
                throw;
            }
        }
        /// <summary>***!!!OUTPUTLU PROCLAR ICIN.!!!***<para />
        /// Dönüş değeri generictir. outputs nesnesi geriye name-value ikilisi olarak output degerleri doner.<para />
        /// ÇOK ÖNEMLİ NOT: OUTPUT OLMAYAN PARAMETRELERDE KESİNLİKLE DBTYPE GİRİLMEMELİ. DBTYPE OLANLAR SADECE OUTPUT PARAMETRELERİ<para />
        /// TEKRAR TEKRAR AYNI DB NESNESI KULLANILACAKSA ARALARINDA FLUSHPARAMETRE() METODU KULLANILMALI <para />
        /// Generic metod oldugu için tiplerin baştan belirtilmesi gerekir. <para />
        /// outputs=> gerıye donen output parametreleri<para />
        /// Ek-Not: Float==>Single ama Float değerleri doublela karsılamak gerekiyor nedense.<para />
        /// </summary>
        public List<T> SelectSP<T>(string procadi,out List<Dparam> outputs)
        {
            try
            {
                var p = new DynamicParameters(); List<T> donenliste = new List<T>();
                using (IDbConnection _db = new SqlConnection(Helper.connectionstring()))
                {
                                      
                    foreach (var item in parametreler) { p.Add(item.name, item.value); }
                    foreach (var item in outputparametreler) {p.Add(item.name, dbType: item.dbtype, direction: ParameterDirection.Output); }
                    var returner = _db.Query<T>(procadi, p, commandType: System.Data.CommandType.StoredProcedure);                     
                    foreach (var item in returner) { donenliste.Add(item); }                  
                    outputs = new List<Dparam>();
                    foreach (var item in outputparametreler) { outputs.Add(Dparam.typecaster(p, item.dbtype, item.name)); }
                         
                }
                //double a = p.Get<double>("@BORC");
                //double b = p.Get<double>("@ALACAK");
                return donenliste;
            }
            catch (Exception e) 
            {
                e = e;
                throw;
            }

        }


        /// <summary>***Geri dönüş yapmayan procedureler için***<para />
        /// ÇOK ÖNEMLİ NOT: OUTPUT OLMAYAN PARAMETRELERDE KESİNLİKLE DBTYPE GİRİLMEMELİ. DBTYPE OLANLAR SADECE OUTPUT PARAMETRELERİ<para />
        /// TEKRAR TEKRAR AYNI DB NESNESI KULLANILACAKSA ARALARINDA FLUSHPARAMETRE() METODU KULLANILMALI <para />      
        /// outputs=> gerıye donen output parametreleri<para />
        /// Ek-Not: Float==>Single ama Float değerleri doublela karsılamak gerekiyor nedense.<para />
        /// </summary>
        public void SelectSP(string procadi, out List<Dparam> outputs)
        {
            try
            {
                var p = new DynamicParameters(); 
                using (IDbConnection _db = new SqlConnection(Helper.connectionstring()))
                {
                    foreach (var item in parametreler) { p.Add(item.name, item.value); }
                    foreach (var item in outputparametreler) { p.Add(item.name, dbType: item.dbtype, direction: ParameterDirection.Output); }
                    _db.Query(procadi, p, commandType: System.Data.CommandType.StoredProcedure);
                    outputs = new List<Dparam>();
                    foreach (var item in outputparametreler) { outputs.Add(Dparam.typecaster(p, item.dbtype, item.name)); }

                }
                
            }
            catch (Exception e)
            {
                e = e;
                throw;
            }

        }

        /// <summary>***Geri dönüş yapmayan ve outputu olmayan procedureler için***<para />
        /// ÇOK ÖNEMLİ NOT: OUTPUT OLMAYAN PARAMETRELERDE KESİNLİKLE DBTYPE GİRİLMEMELİ. DBTYPE OLANLAR SADECE OUTPUT PARAMETRELERİ<para />
        /// TEKRAR TEKRAR AYNI DB NESNESI KULLANILACAKSA ARALARINDA FLUSHPARAMETRE() METODU KULLANILMALI <para />      
        /// outputs=> gerıye donen output parametreleri<para />
        /// Ek-Not: Float==>Single ama Float değerleri doublela karsılamak gerekiyor nedense.<para />
        /// </summary>
        public void SelectSP(string procadi)
        {
            try
            {
                var p = new DynamicParameters();
                using (IDbConnection _db = new SqlConnection(Helper.connectionstring()))
                {
                    foreach (var item in parametreler) { p.Add(item.name, item.value); }                 
                    _db.Query(procadi, p, commandType: System.Data.CommandType.StoredProcedure);                 
                }

            }
            catch (Exception e)
            {
                e = e;
                throw;
            }

        }


        /// <summary>
        /// Class based parametreler listesine sadece name-value ikilisi ile parametre ekler. Sadece gonderılen parametreler ıcın.Outputları kapsamaz. Aynı DB ile sorgularda FlushParameters kullanılmalı<para />
        /// </summary>
        public Dparam AddParameter(string name, object value)
        {
            try
            {
                Dparam param = new Dparam();
                param.name = name; param.value = value;
                this.parametreler.Add(param);
                return param;//return cokta gereklı degıl aslında
            }
            catch (Exception e)
            {
                e = e;
                throw;
            }
        }
        /// <summary>
        /// Class based parametreler listesine sadece name-value ikilisi ile parametre ekler. Sadece gonderılen parametreler ıcın.Outputları kapsamaz. Aynı DB ile sorgularda FlushParameters kullanılmalı<para />
        /// </summary>
        public Dparam AddOutputParameter(string name, DbType dbtype)
        {
            try
            {               
                Dparam param = new Dparam();
                param.name = name; param.dbtype = dbtype;
                this.outputparametreler.Add(param);
                return param;
            }
            catch (Exception e)
            {
                e = e;
                throw;
            }
        }
        /// <summary>
        /// İKİ İNSTANCE ARASINDA MUTLAKA KULLANILMALI. YOKSA PARAMETRELER KARISIR<para />
        /// </summary>
        public void FlushParameter()
        {
            parametreler = new List<Dparam>();
            outputparametreler = new List<Dparam>();
        }
    }
}
