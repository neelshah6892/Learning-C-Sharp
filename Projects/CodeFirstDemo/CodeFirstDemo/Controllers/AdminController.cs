using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeFirstDemo.Controllers
{
    public class AdminController : Controller
    {
        //  
        // GET: /Admin/  
        public ActionResult Index()
        {
            List<ProjectModels> lstProject = new List<ProjectModels>();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("~/XML/ProjectList.xml"));
            DataView dvPrograms;
            dvPrograms = ds.Tables[0].DefaultView;
            dvPrograms.Sort = "Id";
            foreach (DataRowView dr in dvPrograms)
            {
                ProjectModels model = new ProjectModels();
                model.Id = Convert.ToInt32(dr[0]);
                model.ProjectName = Convert.ToString(dr[1]);
                model.Location = Convert.ToString(dr[2]);
                lstProject.Add(model);
            }
            if (lstProject.Count > 0)
            {
                return View(lstProject);
            }
            return View();
            return View();
        }
        ProjectModels model = new ProjectModels();
        public ActionResult AddEditProject(int? id)
        {
            int Id = Convert.ToInt32(id);
            if (Id > 0)
            {
                GetDetailsById(Id);
                model.IsEdit = true;
                return View(model);
            }
            else
            {
                model.IsEdit = false;
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult AddEditProject(ProjectModels mdl)
        {
            if (mdl.Id > 0)
            {
                XDocument xmlDoc = XDocument.Load(Server.MapPath("~/XML/ProjectList.xml"));
                var items = (from item in xmlDoc.Descendants("Project") select item).ToList();
                XElement selected = items.Where(p => p.Element("Id").Value == mdl.Id.ToString()).FirstOrDefault();
                selected.Remove();
                xmlDoc.Save(Server.MapPath("~/XML/ProjectList.xml"));
                xmlDoc.Element("Projects").Add(new XElement("Project", new XElement("Id", mdl.Id), new XElement("ProjectName", mdl.ProjectName), new XElement("Location", mdl.Location)));
                xmlDoc.Save(Server.MapPath("~/XML/ProjectList.xml"));
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                XmlDocument oXmlDocument = new XmlDocument();
                oXmlDocument.Load(Server.MapPath("~/XML/ProjectList.xml"));
                XmlNodeList nodelist = oXmlDocument.GetElementsByTagName("Project");
                var x = oXmlDocument.GetElementsByTagName("Id");
                int Max = 0;
                foreach (XmlElement item in x)
                {
                    int EId = Convert.ToInt32(item.InnerText.ToString());
                    if (EId > Max)
                    {
                        Max = EId;
                    }
                }
                Max = Max + 1;
                XDocument xmlDoc = XDocument.Load(Server.MapPath("~/XML/ProjectList.xml"));
                xmlDoc.Element("Projects").Add(new XElement("Project", new XElement("Id", Max), new XElement("ProjectName", mdl.ProjectName), new XElement("Location", mdl.Location)));
                xmlDoc.Save(Server.MapPath("~/XML/ProjectList.xml"));
                return RedirectToAction("Index", "Admin");
            }
        }
        public ActionResult Delete(int Id)
        {
            if (Id > 0)
            {
                XDocument xmlDoc = XDocument.Load(Server.MapPath("~/XML/ProjectList.xml"));
                var items = (from item in xmlDoc.Descendants("Project") select item).ToList();
                XElement selected = items.Where(p => p.Element("Id").Value == Id.ToString()).FirstOrDefault();
                selected.Remove();
                xmlDoc.Save(Server.MapPath("~/XML/ProjectList.xml"));
            }
            return RedirectToAction("Index", "Admin");
        }
        public void GetDetailsById(int Id)
        {
            XDocument oXmlDocument = XDocument.Load(Server.MapPath("~/XML/ProjectList.xml"));
            var items = (from item in oXmlDocument.Descendants("Project")
                         where Convert.ToInt32(item.Element("Id").Value) == Id
                         select new projectItems
                         {
                             Id = Convert.ToInt32(item.Element("Id").Value),
                             ProjectName = item.Element("ProjectName").Value,
                             Location = item.Element("Location").Value,
                         }).SingleOrDefault();
            if (items != null)
            {
                model.Id = items.Id;
                model.ProjectName = items.ProjectName;
                model.Location = items.Location;
            }
        }
        public class projectItems
        {
            public int Id
            {
                get;
                set;
            }
            public string ProjectName
            {
                get;
                set;
            }
            public string Location
            {
                get;
                set;
            }
            public projectItems() { }
        }
    }  
}
