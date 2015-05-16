using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoToCO.Models;

namespace GoToCO.Controllers
{
    public class NodesController : Controller
    {
        private NodeEntities _db = new NodeEntities();

        // GET: Nodes
        public ActionResult Index()
        {
            return View(_db.Node.ToList());
        }

        // GET: Nodes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Node node = _db.Node.Find(id);
            if (node == null)
            {
                return HttpNotFound();
            }
            return View(node);
        }

        // GET: Nodes/Create
        public ActionResult Create()
        {
            Node model = new Node() 
            { 
                CreatedDate = DateTime.Now,
                Active = true
            };
            return View(model);
        }

        // POST: Nodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Category,Question,Direction,ParentId,ImagePath,Active,CreatedDate")] Node node)
        {
            if (ModelState.IsValid)
            {
                _db.Node.Add(node);
                _db.SaveChanges();
                return RedirectToAction("Create");
            }

            return View(node);
        }

        // GET: Nodes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Node node = _db.Node.Find(id);
            if (node == null)
            {
                return HttpNotFound();
            }
            return View(node);
        }

        // POST: Nodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Category,Question,Direction,ParentId,ImagePath,Active,CreatedDate")] Node node)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(node).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(node);
        }

        // GET: Nodes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Node node = _db.Node.Find(id);
            if (node == null)
            {
                return HttpNotFound();
            }
            return View(node);
        }

        // POST: Nodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Node node = _db.Node.Find(id);
            _db.Node.Remove(node);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult GetNextNode( int currentNodeId, string category = "", string direction = "")
        {
            Node node = new Node();
            if( currentNodeId != 0 )
            {
                //.Include("NodeItem")
                node = _db.Node.Where(item => item.ParentId == currentNodeId && item.Direction == direction).SingleOrDefault();
                node.NodeItem = null;
            }
            else
            {
                //.Include("NodeItem")
                node = _db.Node.Where(item => item.Category == category && item.Direction == null).SingleOrDefault();
                node.NodeItem = null;
            }
            return Json(node, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult GetPreviousNode(int previousNodeId)
        {
            Node node = new Node();
            
            node = _db.Node.Where(item => item.Id == previousNodeId).SingleOrDefault();
                
            return Json(node, JsonRequestBehavior.AllowGet);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
