using CalifornianHealthPortal.Code;
using CalifornianHealthPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace CalifornianHealthPortal.Controllers
{
    public class HomeController : Controller
    {
        //public ActionResult Index()
        //{
        //    ConsultantModelList conList = new ConsultantModelList();
        //    CHDBContext dbContext = new CHDBContext();
        //    Repository repo = new Repository();
        //    List<Consultant> cons = new List<Consultant>();
        //    cons = repo.FetchConsultants(dbContext);
        //    conList.ConsultantsList = new SelectList(cons, "Id", "FName");
        //    conList.consultants = cons;
        //    return View(conList);
        //}

        public ActionResult index()
        {
            return View();  
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
