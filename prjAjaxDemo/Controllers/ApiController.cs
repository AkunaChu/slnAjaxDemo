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

        public IActionResult HW3()
        {
            return View();



        }
        public IActionResult Check(string name)
        {
            if (_context.Members.Any(n => n.Name == name))
                return Content("有");

            return Content("沒");



        }


        public IActionResult register(Members member, IFormFile formFile)
        {
            //實際路徑
            //C:\Users\User\Documents\workspace\MSIT153Site\wwwroot\uploads\abc.jpg
            //string strPath = _host.ContentRootPath; //C:\Users\User\Documents\workspace\MSIT153Site
            //tring strPath = _host.WebRootPath; //C:\Users\User\Documents\workspace\MSIT153Site\wwwroot
            string strPath = Path.Combine(_host.WebRootPath, "uploads", formFile.FileName);
            //using(var data = new FileStream(strPath,FileMode.Create))
            //{
            //   formFile.CopyTo(data);
            //}
            //return Content(strPath);

            //string fileInfo = $"{formFile?.FileName} - {formFile?.Length} - {formFile?.ContentType}";
            //return Content(fileInfo);
            //return Content("<h2>Ajax 你好 !!</h2>","text/html", System.Text.Encoding.UTF8);
            //return Content($"Hello {member.name}，{member.email},  You are {member.age} years old.");

            using (var fileStream = new FileStream(strPath, FileMode.Create))
            {
                formFile.CopyTo(fileStream);
            }

            member.FileName = formFile.FileName;
            //將上傳的圖轉成二進位
            byte[]? imgByte = null;
            using (var memoryStream = new MemoryStream())
            {
                formFile.CopyTo(memoryStream);
                imgByte = memoryStream.ToArray();
            }
            member.FileData = imgByte;

            //將資料寫進資料庫中
            _context.Members.Add(member);
            _context.SaveChanges();

            return Content("新增成功");
        }


        public IActionResult Cities()
        {
            var cities = _context.Address.Select(c => c.City).Distinct();
            return Json(cities);
        }

        public IActionResult districts(string city)
        {
            var districts = _context.Address.Where(a => a.City == city).Select(a => a.SiteId).Distinct();
            return Json(districts);
        }

        public IActionResult Road(string siteid)
        {
            var roads = _context.Address.Where(a => a.SiteId == siteid).Select(a => a.Road).Distinct();
            return Json(roads);
        }
    }
}
