using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HD.EFCore.MySqlGenerator.Models;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using System.IO;

namespace HD.EFCore.MySqlGenerator.Controllers
{
    public class HomeController : Controller
    {
        IModelScaffolder _modelScaffolder;
        public HomeController(IModelScaffolder modelScaffolder)
        {
            _modelScaffolder = modelScaffolder;
        }

        public IActionResult Index()
        {
            var filePaths = _modelScaffolder.Generate(
                    "Server=localhost;Port=3306;Database=golang;Uid=root;Pwd=870224;",
                    Enumerable.Empty<string>(),
                    Enumerable.Empty<string>(),
                    "Data",
                    "",
                    "HD.Data",
                    contextName: "HDDbContext",
                    useDataAnnotations: false,
                    overwriteFiles: true,
                    useDatabaseNames: false);

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
