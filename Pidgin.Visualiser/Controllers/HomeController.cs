using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pidgin.Visualiser.Models;

namespace Pidgin.Visualiser.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            AssemblyList.Load("/Users/ben/Developer/Pidgin/Pidgin.Examples/bin/Debug/netstandard1.4/Pidgin.Examples.dll");

            var assemblyNames = AssemblyList
                .LoadedAssemblies
                .Select(a => a.FullName)
                .ToList();
            var vm = new IndexViewModel(
                assemblyNames,
                ParserProcess.Instance?.Name,
                ParserProcess.Instance?.Input,
                ParserProcess.Instance?.Pos
            );
            return View(vm);
        }

        [Route("load-assembly")]
        [HttpPost]
        public IActionResult LoadAssembly(string assemblyFile)
        {
            AssemblyList.Load(assemblyFile);
            return RedirectToAction("Index");
        }

        [Route("load-parser")]
        [HttpPost]
        public IActionResult LoadParser(string expr)
        {
            ParserProcess.Initialise(expr);
            return RedirectToAction("Index");
        }

        [Route("run-parser")]
        [HttpPost]
        public IActionResult RunParser(string input)
        {
            ParserProcess.Instance.SetInput(input);
            return RedirectToAction("Index");
        }

        [Route("continue-parser")]
        [HttpPost]
        public IActionResult ContinueParser()
        {
            if (!ParserProcess.Instance.IsRunning)
            {
                ParserProcess.Instance.Run();
            }
            else
            {
                ParserProcess.Instance.Continue();
            }
            return RedirectToAction("Index");
        }
    }
}
