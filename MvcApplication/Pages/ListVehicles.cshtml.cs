using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MvcApplication.Pages
{
    public class ListVehiclesModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Your application description page.";
        }
    }
}
