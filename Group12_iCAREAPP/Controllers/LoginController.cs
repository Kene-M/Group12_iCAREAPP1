using System;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Group12_iCAREAPP.Models;

namespace Group12_iCAREAPP.Controllers
{
    public class LoginController : Controller
    {
        private Group12_iCAREDBEntities db = new Group12_iCAREDBEntities();

        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
                var user =  db.iCAREUser.Where(u => u.name.Equals(username)).FirstOrDefault();

                if (user != null)
                {
                    //find the password
                    var userPassword = db.UserPassword.Where(p => p.ID.Equals(user.passwordID)).FirstOrDefault();

                    if (userPassword != null)
                    {
                        //compare passwords
                        if (password == userPassword.password) 
                        {
                            Session["userID"] = user.ID;
                            if(user.ID=="admin")
                            {
                                return RedirectToAction("Index", "iCAREWorkers");
                            }
                            return RedirectToAction("Index", "PatientRecords");
                        }
                    }
                }

            return RedirectToAction("Index", "TreatmentRecords");
        }
    }
}
