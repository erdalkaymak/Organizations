using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Repositorys
{
   public class CommentRepository:BaseGenericRepository<Comments>
    {
        public CommentRepository(MyOrganizationEntities db):base(db)
        {
        
        }
        public List<Comments> getAllById(int id)
        {
            var list = _db.Comments.Where(c => c.OrganizationId == id).ToList();
            return list;
        }
    }
}
