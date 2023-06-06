using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
using System.IO;

namespace YourNamespace.Controllers
{
    public class FallbackController : Controller
    {
        public IActionResult Index()
        {
            var fileInfo = new PhysicalFileInfo(new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "dist", "index.html")));
            return PhysicalFile(fileInfo.PhysicalPath, "text/html");
        }
    }
}