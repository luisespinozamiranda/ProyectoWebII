﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoCRM.Models;

namespace ProyectoCRM.Controllers
{
    public class ReunionController : Controller
    {
        private CRMDB db = new CRMDB();

        // GET: Reunion
        public ActionResult Index()
        {
            var reunion = db.Reunion.Include(r => r.cliente).Include(r => r.usuarios);
            return View(reunion.ToList());
        }

        // GET: Reunion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reunion reunion = db.Reunion.Find(id);
            if (reunion == null)
            {
                return HttpNotFound();
            }
            return View(reunion);
        }

        // GET: Reunion/Create
        public ActionResult Create()
        {
            ViewBag.id_cliente = new SelectList(db.Cliente, "id", "nombre");
            ViewBag.id_user = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        /// <summary>
        /// Permite crear una reunion
        /// </summary>
        /// <param name="reunion">Propiedades de la reunion</param>
        /// <returns>Vista</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_user,id_cliente,titulo,fecha,_virtual")] Reunion reunion)
        {
            if (ModelState.IsValid)
            {
                db.Reunion.Add(reunion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_cliente = new SelectList(db.Cliente, "id", "nombre", reunion.id_cliente);
            ViewBag.id_user = new SelectList(db.AspNetUsers, "Id", "Email", reunion.id_user);
            return View(reunion);
        }

        // GET: Reunion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reunion reunion = db.Reunion.Find(id);
            if (reunion == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_cliente = new SelectList(db.Cliente, "id", "nombre", reunion.id_cliente);
            ViewBag.id_user = new SelectList(db.AspNetUsers, "Id", "Email", reunion.id_user);
            return View(reunion);
        }

        // POST: Reunion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_user,id_cliente,titulo,fecha,_virtual")] Reunion reunion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reunion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_cliente = new SelectList(db.Cliente, "id", "nombre", reunion.id_cliente);
            ViewBag.id_user = new SelectList(db.AspNetUsers, "Id", "Email", reunion.id_user);
            return View(reunion);
        }

        // GET: Reunion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reunion reunion = db.Reunion.Find(id);
            if (reunion == null)
            {
                return HttpNotFound();
            }
            return View(reunion);
        }

        // POST: Reunion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reunion reunion = db.Reunion.Find(id);
            db.Reunion.Remove(reunion);
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
