using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using System.Threading.Tasks;
using DapperT2.Dapper;
using System.Data;

namespace DapperT2.Controllers
{
	public class HomeController : Controller
	{
	   
		//NonInterface non;
		//private string connectionString = ConfigurationManager.ConnectionStrings["DapperDefault"].ConnectionString;
		public ActionResult Index()
		{                      
			//non = new NonInterface();
			using (DDLayer.Access.DB _db = new DDLayer.Access.DB())
			{
				try
				{
					//Ornek queryler
					//1. select sorgusu
					//2. parametreli dönüş değersiz stored procedure(parametresizi de var)
					//3. parametreli donus degerli stored procedure
					//4. parametreli output degerli donus degerli stored procedure

					//****!!!******* 1
					_db.SelectQuery<dynamic>("UPDATE AT_ANASAYFAMETINLER SET BASLIK = 'Lorem İpsum17', EKLEYENISIM = 'hbr' where ID=7",false);//No 1
					_db.FlushParameter();
					//****!!!******* 2                        
					_db.AddParameter("@ID", 221831);
					_db.AddParameter("@ADET", 0);
					_db.AddParameter("@NOT", "hb");
					_db.SelectSP("ap_Sepet_Guncelle_Web",false);//2. numaralı
					_db.FlushParameter();
					//****!!!*******3
					_db.AddParameter("@MARKA", "ALFA ROMEO");
					var list = _db.SelectSP<AracModel>("ap_AracModel",false);
					_db.FlushParameter();
					//****!!!*******4
					List<DDLayer.Access.Dparam> outputs = new List<DDLayer.Access.Dparam>();
					_db.AddParameter("@CARIKOD", "M000");
					_db.AddOutputParameter("@BORC", DbType.Double);
					_db.AddOutputParameter("@ALACAK", DbType.Double);
					var list2 = _db.SelectSP<CARILISTE>("ap_GetCariHareket", out outputs,false);
					//outputs dönüş değerleridir

					//otomatik flush parameter olanlar										
					_db.SelectQuery<dynamic>("UPDATE AT_ANASAYFAMETINLER SET BASLIK = 'Lorem İpsum17', EKLEYENISIM = 'hbr' where ID=7", true); 
					_db.AddParameter("@ID", 221831);
					_db.AddParameter("@ADET", 0);
					_db.AddParameter("@NOT", "hb");
					_db.SelectSP("ap_Sepet_Guncelle_Web");//2. numaralı										
					_db.AddParameter("@MARKA", "ALFA ROMEO");
					var list3 = _db.SelectSP<AracModel>("ap_AracModel");										
					List<DDLayer.Access.Dparam> outputs2 = new List<DDLayer.Access.Dparam>();
					_db.AddParameter("@CARIKOD", "M000");
					_db.AddOutputParameter("@BORC", DbType.Double);
					_db.AddOutputParameter("@ALACAK", DbType.Double);
					var list4 = _db.SelectSP<CARILISTE>("ap_GetCariHareket", out outputs);
					//outputs2 dönüş değerleridir

				}
				catch (Exception E)
				{
					
					throw;
				}
			}
					 
			return View();
		}
		 public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}
