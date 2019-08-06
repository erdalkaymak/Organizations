using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Repositorys
{
   public class ExceptionRepository:BaseGenericRepository<Exception>
    {
        public ExceptionRepository(MyOrganizationEntities db):base(db)
        {

        }
    }
}
