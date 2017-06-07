using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DapperT2.Models
{
    public class DataAccessLayer
    {
        public List<T> GetAllWithQueryGeneric<T>(string query)
        {
            using (IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
            {
                return _db.Query<T>(query).ToList();
            }

        }

        public List<DapperKategori> GetAllSP()
        {
            using (IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
            {

                var p = new DynamicParameters();
                //p.Add("@a", 11);
                //p.Add("@b", dbType: DbType.Int32, direction: ParameterDirection.Output);
                //p.Add("@c", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                var katter = _db.Execute("SP_DAPPERPROCTEST", p, commandType: CommandType.StoredProcedure);
                //int b = p.Get<int>("@b");
                //int c = p.Get<int>("@c");
                return _db.Query<DapperKategori>("SELECT * FROM DapperKategori").ToList();
            }

        }
    }
}