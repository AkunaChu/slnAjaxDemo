using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjAjaxDemo.Models;
using prjAjaxDemo.ViewModels;

namespace prjAjaxDemo.Controllers
{
    public class ApiController : Controller
    {
        private readonly IWebHostEnvironment _host;
        private readonly DemoContext _context;
        public ApiController(IWebHostEnvironment host, DemoContext context)
        {
            _host = host;
            _context = context;
        }
        
        
        public IActionResult Index(string? name,int age = 20)
        {
            Thread.Sleep(2000);
            if (string.IsNullOrEmpty(name))
                name = "guest";
            return Content($"Hellow! {name}, age = {age}");
            

        }

        public IActionResult HW1()
        {

            return View();
        }

        public IActionResult HW2() 
        {
            return View();


            
        }
        public IActionResult Check(string name)
        {
            if (_context.Members.Any(n => n.Name == name))
                return Content("有");

            return Content("沒");



        }


        public IActionResult register(MemberViewModel member, IFormFile formFile)
        {
            //實際路徑
            //C:\Users\User\Documents\workspace\MSIT153Site\wwwroot\uploads\abc.jpg
            //string strPath = _host.ContentRootPath; //C:\Users\User\Documents\workspace\MSIT153Site
            //tring strPath = _host.WebRootPath; //C:\Users\User\Documents\workspace\MSIT153Site\wwwroot
            string strPath = Path.Combine(_host.WebRootPath, "uploads", formFile.FileName);
            using(var data = new FileStream(strPath,FileMode.Create))
            {
               formFile.CopyTo(data);
            }
            return Content(strPath);

            //string fileInfo = $"{formFile?.FileName} - {formFile?.Length} - {formFile?.ContentType}";
            //return Content(fileInfo);
            //return Content("<h2>Ajax 你好 !!</h2>","text/html", System.Text.Encoding.UTF8);
            //return Content($"Hello {member.name}，{member.email},  You are {member.age} years old.");
        }
    }
}
