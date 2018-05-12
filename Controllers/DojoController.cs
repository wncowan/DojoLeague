using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DojoLeague.Factory;
using DojoLeague.Models;

namespace DojoLeague.Controllers
{
    public class DojoController : Controller
    {
        private readonly DojoFactory dojoFactory;
        public DojoController() {
            dojoFactory = new DojoFactory();
        }

        [HttpGet]
        [Route("dojos")]
        public IActionResult DojoHome()
        {
            ViewBag.dojos = dojoFactory.GetAll();
            return View("dojos");
        }

        [HttpPost]
        [Route("add_dojo")]
        public IActionResult addDojo(Dojo item){
            if(ModelState.IsValid){
                dojoFactory.Add(item);
                return RedirectToAction("DojoHome");
            }
            ViewBag.dojos = dojoFactory.GetAll();

            return View("dojos");
        }

        [HttpGet]
        [Route("dojos/{dojo_id}")]
        public IActionResult showDojo(int dojo_id){
            System.Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~");
            System.Console.WriteLine(dojo_id);
            ViewBag.dojo = dojoFactory.GetNinjasById(dojo_id);
            ViewBag.rogues = dojoFactory.GetRogueNinjas();

            return View("show_dojo");
        }

        [HttpGet]
        [Route("dojos/{Dojo_Id}/banish/{ninja_id}")]
        public IActionResult BanishNinja(int Dojo_Id, int ninja_id){
            dojoFactory.BanishNinja(ninja_id);
            return RedirectToAction("showDojo", new { dojo_id = Dojo_Id});
        }

        [HttpGet]
        [Route("dojos/{Dojo_Id}/recruit/{ninja_id}")]
        public IActionResult RecruitNinja(int Dojo_Id, int ninja_id){
            dojoFactory.RecruitNinja(Dojo_Id,ninja_id);
            return RedirectToAction("showDojo", new { dojo_id = Dojo_Id});
        }
    }
}