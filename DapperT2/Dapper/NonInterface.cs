using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DapperT2.Dapper
{
    public class NonInterface
    {
        //private IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
        public List<DapperKategori> GetAll()
        {
            using (IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
            {
                return _db.Query<DapperKategori>("SELECT * FROM DapperKategori").ToList();
            }
            
        }

        public List<AracModel> GetAllSP()
        {
            using (IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
            {

                var p = new DynamicParameters();              
                p.Add("@MARKA", "ALFA ROMEO");              
                 var katter=_db.Execute("ap_AracModel", p, commandType: CommandType.StoredProcedure);             
                 var yorumlar = _db.Query<AracModel>("ap_AracModel",p, commandType: System.Data.CommandType.StoredProcedure);               
                List<AracModel> dapkat = new List<AracModel>();
                foreach (var item in yorumlar) { dapkat.Add(new AracModel(item.MODEL)); }               
                return dapkat;
            }

        }


        public List<CARILISTE> GetAllSP2()
        {
            using (IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
            {               
                var p = new DynamicParameters();
                //parametre ekleme seklı
                p.Add("@CARIKOD", "M 27-016");
                p.Add("@BORC", dbType: DbType.Double, direction: ParameterDirection.Output);
                p.Add("@ALACAK", dbType: DbType.Double, direction: ParameterDirection.Output);                
                var yorumlar = _db.Query<CARILISTE>("ap_GetCariHareket", p, commandType: System.Data.CommandType.StoredProcedure);
                List<CARILISTE> dapkat = new List<CARILISTE>();
                foreach (var item in yorumlar) { dapkat.Add(new CARILISTE(item.belge_no,item.vade_tarihi,item.tarih,item.borc,item.alacak,item.aciklama,item.bakiye)); }

                double b = p.Get<double>("@BORC");
                double c = p.Get<double>("@ALACAK");
                return dapkat;
            }

        }
    }
}