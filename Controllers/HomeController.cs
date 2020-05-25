using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestDemo.Models;

namespace TestDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RecordDbContext recordDbContext;

        public HomeController(ILogger<HomeController> logger,RecordDbContext _recordDbContext)
        {
            _logger = logger;
            this.recordDbContext = _recordDbContext;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DataList()
        {
            return View();
        }

        public IActionResult EditView()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public JsonResult PostData(FormPost formPost)
        {
            var parent = new Parent
            {
                FatherName = formPost.FirstName,
                ContactNo = formPost.ContactNo,
                EmailID = formPost.Email
            };
            this.recordDbContext.parents.Add(parent);
            this.recordDbContext.SaveChanges();
            if (parent.ParentID > 0)
            {
                var children = new List<Child>();
                for (int i = 0; i < formPost.childNames.Length; i++)
                {
                    children.Add(new Child
                    {
                        ChildName = formPost.childNames[i],
                        Dob = Convert.ToDateTime(formPost.childDobs[i]),
                        ParentID= parent.ParentID
                    });
                }
                this.recordDbContext.children.AddRange(children);
                this.recordDbContext.SaveChanges();
            }
            return Json(new { 
              Msg="Record Inserted successfully",
              Status=HttpStatusCode.Created
            });
        }

        public JsonResult GetChildren()
        {
            return Json(new { 
              Data=this.recordDbContext.parents.Select(x=>new {
                 x.ParentID,
                 x.FatherName,
                 x.EmailID,
                 x.ContactNo,
                 x.children
                   
              })
             // Status=HttpStatusCode.OK
            });
        }

        public JsonResult parentwithChildDetail(int ParentID)
        {
            return Json(new { 
              data=this.recordDbContext.parents.Where(x=>x.ParentID==ParentID).Select(y=>new { 
                 y.FatherName,
                 y.EmailID,
                 y.ContactNo,
                 y.children
               })
             });
        }

        public JsonResult UpdateData(FormUpdate formPost)
        {
            var parent=this.recordDbContext.parents.Find(Convert.ToInt32(formPost.ParentID));
            parent.FatherName = formPost.FirstName;
            parent.EmailID = formPost.Email;
            parent.ContactNo = formPost.ContactNo;
            this.recordDbContext.parents.Update(parent);
            var i=this.recordDbContext.SaveChanges();
            if (i > 0)
            {
                var children =new List<Child>();
               for(int j = 0; j < formPost.childIDs.Length; j++)
                {
                    var child = this.recordDbContext.children.SingleOrDefault(x => x.ChildID == Convert.ToInt32(formPost.childIDs[j]));
                    child.ChildName = formPost.childNames[j];
                    child.Dob = Convert.ToDateTime(formPost.childDobs[j]);
                    children.Add(child);
                }

                this.recordDbContext.children.UpdateRange(children);
                this.recordDbContext.SaveChanges();
            }
            return Json(new { 
              Data="Data updated successfully."
            });
        }
    }
}
