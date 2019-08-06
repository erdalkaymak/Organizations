using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Repositorys
{
    public class OrgImageRepository:BaseGenericRepository<OrgImage>
    {
        public OrgImageRepository(MyOrganizationEntities db) : base(db)
        {

        }
    }
}
