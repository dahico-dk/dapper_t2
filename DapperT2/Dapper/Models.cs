using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DapperT2.Dapper
{
    public class DapperKategori
    {
        public int id { get; set; }
        public string Ad { get; set; }

        public DapperKategori(int id,string Ad)
        {
            this.id = id;
            this.Ad = Ad;
        }
    }
}