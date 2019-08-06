using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Repositorys
{
    public interface IUserRepository:IBaseInterface<User>
    {
        bool Filter(string u, string p);
        bool Filter(string u);
        User FilerWithUser(string u, string p);
        
    }
}
