using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Repositorys
{
    public class UserRepository : BaseGenericRepository<User>, IUserRepository
    {
        public UserRepository(MyOrganizationEntities db):base(db)
        {
                
        }

        public User FilerWithUser(string u, string p)
        {
            var entity = _db.User.Where(c => c.UserName == u && c.Password == p).FirstOrDefault();
            return entity;
        }

        public bool Filter(string u, string p)
        {
            var entity=_db.Set<User>().Where(c => c.UserName == u && c.Password == p).FirstOrDefault();
            if (entity == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool Filter(string u)
        {
            var entity =_db.Set<User>().Where(c => c.UserName == u).FirstOrDefault();
            if (entity == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
