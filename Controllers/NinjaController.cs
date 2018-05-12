using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DojoLeague.Factory;
using DojoLeague.Models;
using Newtonsoft.Json;

namespace DojoLeague.Controllers
{
    public class NinjaController : Controller
    {
        private readonly NinjaFactory ninjaFactory;
        private readonly DojoFactory dojoFactory;
        public NinjaController() {
            ninjaFactory = new NinjaFactory();
            dojoFactory = new DojoFactory();
        }
        [HttpGet]
        [Route("ninjas")]
        public IActionResult NinjaHome()
        {
            ViewBag.ninjas = ninjaFactory.GetAll();
            ViewBag.dojos = dojoFactory.GetAll();
            return View("ninjas");
        }

        [HttpPost]
        [Route("add_ninja")]
        public IActionResult addNinja(Ninja item, int dojo_id){
            if(ModelState.IsValid){
                Dojo dojo = dojoFactory.GetById(dojo_id);
                item.Dojo = dojo;
                System.Console.WriteLine(JsonConvert.SerializeObject(item));
                ninjaFactory.Add(item);
                return RedirectToAction("NinjaHome");
            }
            ViewBag.ninjas = ninjaFactory.GetAll();
            ViewBag.dojos = dojoFactory.GetAll(); 
            return View("ninjas");
        }

        [HttpGet]
        [Route("ninjas/{id}")]
        public IActionResult showNinja(int id){
            Ninja ninja = ninjaFactory.GetById(id);
            ViewBag.ninja = ninja;
            return View("show_ninja");
        }

    }
}