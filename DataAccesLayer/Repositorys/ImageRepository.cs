using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Repositorys
{
    public class ImageRepository:BaseGenericRepository<Images>
    {
        public ImageRepository(MyOrganizationEntities db) : base(db)
        {
                
        }
    }
}
