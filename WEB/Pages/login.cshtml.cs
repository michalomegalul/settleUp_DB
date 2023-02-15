using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using dluznik3;
//call function from different project

namespace Webapp.Pages
{
    public class loginModel : PageModel
    {
        public void OnGet()
        {
            string username = Request.Form["username"];
            string password = Request.Form["password"];
            SettleUpApp app = new SettleUpApp();


        }
        
    }
}
