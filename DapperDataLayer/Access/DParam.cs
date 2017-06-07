using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDLayer.Access
{
    public class Dparam
    {
        public string name { get; set; }
        public object value { get; set; }
        public DbType dbtype { get; set; }      
        public int size { get; set; }
        /// <summary>
        /// Default boş parametre. Kullanmamakta yarar var. Kaşınmaya lüzüm yok.
        /// </summary>
        public Dparam() { }
        /// <summary>
        /// OUTPUT OLMAYAN PARAMETRELER ICIN. SADECE NAME VE VALUE
        /// </summary>
        public Dparam(string name, object value)
        {
            this.name = name;
            this.value = value;

        }
        /// <summary>
        /// OUTPUTLAR İCİN. PARAMETERDİRECTİON OTOMATİK OLARAK OUTPUT VERİLDİ.
        /// </summary>
        public Dparam(string name, DbType dbtype)
        {
            this.name = name;
            this.value = null;
            this.dbtype = dbtype;                    
        }

        public static Dparam typecaster(DynamicParameters p, DbType dbtype, string name)
        {
            try
            {
                Dparam param = new Dparam();
                var x = (dynamic)null;
                switch (dbtype)
                {
                    case DbType.Boolean:
                        x = p.Get<bool>(name);
                        param = new Dparam(name, x);
                        break;
                    case DbType.Byte:
                        x = p.Get<byte>(name);
                        param = new Dparam(name, x);
                        break;
                    case DbType.Binary:
                        x = p.Get<byte[]>(name);
                        param = new Dparam(name, x);
                        break;
                    case DbType.DateTime:
                        x = p.Get<DateTime>(name);
                        param = new Dparam(name, x);
                        break;
                    case DbType.DateTimeOffset:
                        x = p.Get<DateTimeOffset>(name);
                        param = new Dparam(name, x);
                        break;
                    case DbType.Decimal:
                        x = p.Get<decimal>(name);
                        param = new Dparam(name, x);
                        break;
                    case DbType.Double:
                        x = p.Get<double>(name);
                        param = new Dparam(name, x);
                        break;
                    case DbType.Single:
                        x = p.Get<float>(name);
                        param = new Dparam(name, x);
                        break;
                    case DbType.Guid:
                        x = p.Get<Guid>(name);
                        param = new Dparam(name, x);
                        break;
                    case DbType.Int16:
                        x = p.Get<short>(name);
                        param = new Dparam(name, x);
                        break;
                    case DbType.Int32:
                        x = p.Get<int>(name);
                        param = new Dparam(name, x);
                        break;
                    case DbType.Int64:
                        x = p.Get<long>(name);
                        param = new Dparam(name, x);
                        break;
                    case DbType.Object:
                        x = p.Get<object>(name);
                        param = new Dparam(name, x);
                        break;
                    case DbType.String:
                        x = p.Get<string>(name);
                        param = new Dparam(name, x);
                        break;
                    case DbType.Time:
                        x = p.Get<TimeSpan>(name);
                        param = new Dparam(name, x);
                        break;
                    case DbType.UInt16:
                        x = p.Get<ushort>(name);
                        param = new Dparam(name, x);
                        break;
                    case DbType.UInt32:
                        x = p.Get<uint>(name);
                        param = new Dparam(name, x);
                        break;
                    case DbType.UInt64:
                        x = p.Get<ulong>(name);
                        param = new Dparam(name, x);
                        break;
                    default:
                        //bos param
                        param = new Dparam();
                        break;
                }
                return param;
            }
            catch (Exception e)
            {
                e = e;
                throw;
            }
        }
    }
}
