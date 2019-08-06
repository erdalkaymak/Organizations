using OrganizationMvc.CustomExceptionFilter;
using System.Web;
using System.Web.Mvc;

namespace OrganizationMvc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
           
        }
    }
}
