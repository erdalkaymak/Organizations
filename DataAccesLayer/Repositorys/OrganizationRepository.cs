using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Repositorys
{
    public class OrganizationRepository:BaseGenericRepository<Organization>
    {
        public OrganizationRepository(MyOrganizationEntities db) : base(db)
        {

        }
    }
}
