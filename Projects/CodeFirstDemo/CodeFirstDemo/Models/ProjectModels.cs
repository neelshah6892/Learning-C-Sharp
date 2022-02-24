using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeFirstDemo.Models
{
    public class ProjectModels : Controller
    {
        public int Id
        {
            get;
            set;
        }
        //[Required]
        public string ProjectName
        {
            get;
            set;
        }
        //[Required]
        public string Location
        {
            get;
            set;
        }
        public bool IsEdit
        {
            get;
            set;
        }  

        public ActionResult Index()
        {
            return View();
        }

    }
}
