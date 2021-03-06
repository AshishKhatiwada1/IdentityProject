﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IdentityProject.Models;
using IdentityProject.Models.Address;
using IdentityProject.ViewModels;
using Microsoft.AspNet.Identity;

namespace IdentityProject.Controllers.AddressControllers
{
    public class CitiesController : Controller
    {
        private MainApplicationDBContext db = new MainApplicationDBContext();

        // GET: Cities
        public ActionResult Index()
        {
            return View("~/Views/Address/Cities/Index.cshtml",db.Cities.ToList());
        }

        // GET: Cities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Address/Cities/Details.cshtml", city);
        }

        // GET: Cities/Create
        public ActionResult Create()
        {
            AddressViewModel addressViewModel = new AddressViewModel
            {
                ContinentList = db.Continents.ToList(),
                CountryList = db.Countries.ToList(),
                StateList=db.States.ToList()
            };
            return View("~/Views/Address/Cities/Create.cshtml", addressViewModel);
        }

        // POST: Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,AddedDate,IsActive")] City city)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = db.Users.Find(User.Identity.GetUserId());
                city.Added_User = applicationUser;
                city.AddedDate = DateTime.Now;
                city.IsActive = true;
                db.Cities.Add(city);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("~/Views/Address/Cities/Create.cshtml", city);
        }

        // GET: Cities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Address/Cities/Edit.cshtml", city);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,AddedDate,IsActive")] City city)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = db.Users.Find(User.Identity.GetUserId());
                city.Added_User = applicationUser;
                city.AddedDate = DateTime.Now;
                db.Entry(city).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("~/Views/Address/Cities/Edit.cshtml", city);
        }

        // GET: Cities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Address/Cities/Delete.cshtml", city);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            City city = db.Cities.Find(id);
            db.Cities.Remove(city);
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
