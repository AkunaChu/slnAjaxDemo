using Microsoft.AspNetCore.Mvc;

namespace prjAjaxDemo.Controllers
{
    public class ApiController : Controller
    {
        public IActionResult Index(string? name,int age = 20)
        {
            if (string.IsNullOrEmpty(name))
                name = "guest";
            return Content($"Hellow! {name}, age = {age}");

        }

        public IActionResult HW1()
        {

            return View();
        }
    }
}
