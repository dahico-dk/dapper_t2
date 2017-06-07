using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperT2.Dapper
{
    public interface IUserRepository
    {
        List<DapperKategori> GetAll();
        User Find(int id);
        User Add(User user);
        User Update(User user);
        void Remove(int id);
        User GetUserInformatiom(int id);
    }
}
