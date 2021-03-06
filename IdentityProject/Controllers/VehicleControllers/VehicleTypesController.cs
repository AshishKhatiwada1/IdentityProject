﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IdentityProject.Models;
using IdentityProject.Models.Vehicle;
using IdentityProject.ViewModels;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace IdentityProject.Controllers.VehicleControllers
{
    public class VehicleTypesController : Controller
    {
        private MainApplicationDBContext db = new MainApplicationDBContext();

        // GET: VehicleTypes
        public async Task<ActionResult> Index()
        {


            return View("~/Views/Vehicle/VehicleTypes/Index.cshtml", await db.VehicleTypes.ToListAsync());
        }

        // GET: VehicleTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleType vehicleType = await db.VehicleTypes.FindAsync(id);
            if (vehicleType == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Vehicle/VehicleTypes/Details.cshtml", vehicleType);
        }

        // GET: VehicleTypes/Create
        public ActionResult Create()
        {

            AddVehicleTypeViewModel addVehicleTypeViewModel = new AddVehicleTypeViewModel
            {
                ManufacturerList = db.Manufacturers.ToList()
            };
            return View("~/Views/Vehicle/VehicleTypes/Create.cshtml", addVehicleTypeViewModel);
    }

    // POST: VehicleTypes/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create( AddVehicleTypeViewModel vehicleTypeVM)
    {
            VehicleType vehicle = new VehicleType();
            if (ModelState.IsValid)
        {
                ApplicationUser appuser = db.Users.Find(User.Identity.GetUserId());
                vehicle = vehicleTypeVM.VehicleType;
                vehicle.ManufacturerId = vehicleTypeVM.VehicleType.ManufacturerId;
                vehicle.AddedDate = DateTime.Now;
                vehicle.Added_User = appuser;
                vehicle.IsActive = true;
                
                //vehicleType.AddedDate = DateTime.Now;
                //vehicleType.Added_User = appuser;
                //vehicleType.IsActive = true;
                db.VehicleTypes.Add(vehicle);
                await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        return View("~/Views/Vehicle/VehicleTypes/Create.cshtml", vehicle);
    }

    // GET: VehicleTypes/Edit/5
    public async Task<ActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        VehicleType vehicleType = await db.VehicleTypes.FindAsync(id);
        if (vehicleType == null)
        {
            return HttpNotFound();
        }
        return View("~/Views/Vehicle/VehicleTypes/Edit.cshtml", vehicleType);
    }

    // POST: VehicleTypes/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit([Bind(Include = "Id,Name,AddedDate,IsActive")] VehicleType vehicleType)
    {
        if (ModelState.IsValid)
        {
                ApplicationUser appuser = db.Users.Find(User.Identity.GetUserId());
                vehicleType.Added_User = appuser;
                //vehicleType.AddedDate = DateTime.Now;
                db.Entry(vehicleType).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return View("~/Views/Vehicle/VehicleTypes/Edit.cshtml", vehicleType);
    }

    // GET: VehicleTypes/Delete/5
    public async Task<ActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        VehicleType vehicleType = await db.VehicleTypes.FindAsync(id);
        if (vehicleType == null)
        {
            return HttpNotFound();
        }
        return View("~/Views/Vehicle/VehicleTypes/Delete.cshtml", vehicleType);
    }

    // POST: VehicleTypes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(int id)
    {
        VehicleType vehicleType = await db.VehicleTypes.FindAsync(id);
        db.VehicleTypes.Remove(vehicleType);
        await db.SaveChangesAsync();
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
