﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalifornianHealthNewPortal.Controllers
{
    public class BookingController : Controller
    {
        public ActionResult GetConsultantCalendar() 
        {
            return View();
        }
        // GET: Booking
        //TODO: Change this method to display the consultant calendar. Ensure that the user can have a real time view of 
        //the consultant's availability;
        //public ActionResult GetConsultantCalendar()
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

        //TODO: Change this method to ensure that members do not have to wait endlessly. 
        //public ActionResult ConfirmAppointment(Appointment model)
        //{
        //    CHDBContext dbContext = new CHDBContext();

        //    //Code to create appointment in database
        //    //This needs to be reassessed. Before confirming the appointment, should we check if the consultant calendar is still available?
        //    Repository repo = new Repository();
        //    var result = repo.CreateAppointment(model, dbContext);

        //    return View();
        //}

        public ActionResult ConfirmAppointment() 
        {
            return View();
        }
    }
}