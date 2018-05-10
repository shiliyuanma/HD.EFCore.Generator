﻿using HD.EFCore.MySqlGenerator.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace HD.EFCore.MySqlGenerator.Controllers
{
    public class HomeController : Controller
    {
        IHostingEnvironment _env;
        IModelScaffolder _modelScaffolder;
        public HomeController(IHostingEnvironment env, IModelScaffolder modelScaffolder)
        {
            _env = env;
            _modelScaffolder = modelScaffolder;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new SqlGenerateViewModel());
        }

        [HttpPost]
        public IActionResult Index(SqlGenerateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ErrorMsg = string.Join("/", ModelState.Where(q => q.Value.Errors != null && q.Value.Errors.Count > 0).SelectMany(q => q.Value.Errors).Select(q => q.ErrorMessage));
                return View(model);
            }

            var dir = Path.Combine(_env.ContentRootPath, "Data");
            var subDir = Path.Combine(dir, DateTime.Now.ToString("yyyyMMdd-HHmmss") + "-" + new Random().Next(100, 999).ToString());
            var filePaths = _modelScaffolder.Generate(
                    model.ConnectionString,
                    !string.IsNullOrWhiteSpace(model.TableNames)
                        ? model.TableNames.Split(new[] { ',', '，', ' ' }, System.StringSplitOptions.RemoveEmptyEntries).ToList()
                        : Enumerable.Empty<string>(),
                    Enumerable.Empty<string>(),
                    dir,
                    subDir,
                    !string.IsNullOrWhiteSpace(model.NamespaceName) ? model.NamespaceName.Trim() : "HD.EFCore.MySqlGenerator",
                    contextName: !string.IsNullOrWhiteSpace(model.DbContextName) ? model.DbContextName.Trim() : "HDDbContext",
                    useDataAnnotations: false,
                    overwriteFiles: true,
                    useDatabaseNames: false);

            var zipPath = $"{Path.GetFileName(subDir)}.zip";
            ZipFile.CreateFromDirectory(subDir, zipPath);
            var data = System.IO.File.ReadAllBytes(zipPath);
            Directory.Delete(subDir, true);

            return File(data, "application/x-zip-compressed", zipPath);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
