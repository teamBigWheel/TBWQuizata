using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoToCO.Models;

namespace WebApplication1.Controllers
{
    public class NodeItemsController : Controller
    {
        private NodeEntities db = new NodeEntities();

        // GET: NodeItems
        public ActionResult Index()
        {
            
            var nodeItem = db.NodeItem.Include(n => n.Node);
            return View(nodeItem.ToList());
        }

        // GET: NodeItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NodeItem nodeItem = db.NodeItem.Find(id);
            if (nodeItem == null)
            {
                return HttpNotFound();
            }
            return View(nodeItem);
        }

        // GET: NodeItems/Create
        public ActionResult Create()
        {
            ViewBag.NodeId = new SelectList(db.Node, "Id", "Question");
            NodeItem model = new NodeItem()
            {
                CreatedDate = DateTime.Now
            };
            return View(model);
        }

        // POST: NodeItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NodeId,ItemName,ItemDescription,Url,CreatedDate")] NodeItem nodeItem)
        {
            if (ModelState.IsValid)
            {
                db.NodeItem.Add(nodeItem);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            ViewBag.NodeId = new SelectList(db.Node, "Id", "Question", nodeItem.NodeId);
            return View(nodeItem);
        }

        // GET: NodeItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NodeItem nodeItem = db.NodeItem.Find(id);
            if (nodeItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.NodeId = new SelectList(db.Node, "Id", "Question", nodeItem.NodeId);
            return View(nodeItem);
        }

        // POST: NodeItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NodeId,ItemName,ItemDescription,Url,CreatedDate")] NodeItem nodeItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nodeItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NodeId = new SelectList(db.Node, "Id", "Category", nodeItem.NodeId);
            return View(nodeItem);
        }

        // GET: NodeItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NodeItem nodeItem = db.NodeItem.Find(id);
            if (nodeItem == null)
            {
                return HttpNotFound();
            }
            return View(nodeItem);
        }

        // POST: NodeItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NodeItem nodeItem = db.NodeItem.Find(id);
            db.NodeItem.Remove(nodeItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
