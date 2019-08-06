using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizationMvc.Models
{
    public class OrgImageModel
    {
        public int Id { get; set; }
        public int OrgId { get; set; }
        public int ImageId { get; set; }

        public virtual ImageModel Images { get; set; }
        public virtual OrganizationModel Organization { get; set; }
    }
}