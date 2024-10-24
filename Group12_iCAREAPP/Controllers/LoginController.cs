using Group12_iCAREAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Group12_iCAREAPP.Controllers
{
    public class LoginController : Controller
    {
        private Group12_iCAREDBEntities db = new Group12_iCAREDBEntities();

        // GET: Login
        public ActionResult Index()
        {
            //if password is right and id in admin (get UserRoles) -> AdminView
            //if password is right and id in nurse -> NurseView
            //if password is right and id in doctor -> DoctorView
            //else "credentials are incorrect try again"
    
            return View();
        }
    }
}