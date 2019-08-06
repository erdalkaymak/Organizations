using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizationMvc.Models
{
    public class OrganizationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime OrganizationDate { get; set; }
        public int MainImageId { get; set; }
        public RegisterModel Organizer { get; set; }
        public List<ImageModel> Images { get; set; }
        public virtual ICollection<OrgImageModel> OrgImagemodel { get; set; }
    }
}