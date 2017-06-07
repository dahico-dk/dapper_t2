using System.Configuration;

namespace DDLayer.Access
{
    public class Helper
    {
      
        public static string connectionstring() { return ConfigurationManager.ConnectionStrings["DapperDefault"].ConnectionString; }
      
    }
}
