using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace PrescriptionApp.Models.Controllers
{
    public class PrescriptionController : Controller
    {
        private PrescriptionContext context { get; set; }

        public PrescriptionController(PrescriptionContext ctx) => context = ctx;

        // Fill dropdown list for FillStatus
        private void PopulateFillStatuses(string? selected = null)
        {
            ViewBag.FillStatuses = new List<SelectListItem>
            {
                new SelectListItem("New", "New") { Selected = selected == "New" },
                new SelectListItem("Filled", "Filled") { Selected = selected == "Filled" },
                new SelectListItem("Pending", "Pending") { Selected = selected == "Pending" }
            };
        }

        // GET: Add new prescription
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            PopulateFillStatuses("New");

            var model = new Prescription
            {
                FillStatus = "New",
                RequestTime = DateTime.Now
            };

            return View("Edit", model); // reuse Edit view
        }

        // GET: Edit prescription
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Home");

            ViewBag.Action = "Edit";
            var prescription = context.Prescriptions.Find(id);
            if (prescription == null) return RedirectToAction("Index", "Home");

            PopulateFillStatuses(prescription.FillStatus);
            return View(prescription);
        }

        // POST: Add or Edit prescription
        [HttpPost]
        public IActionResult Edit(Prescription prescription)
        {
            PopulateFillStatuses(prescription.FillStatus);

            if (ModelState.IsValid)
            {
                if (prescription.PrescriptionId == 0) // Add new prescription
                {
                    prescription.RequestTime ??= DateTime.Now;
                    prescription.FillStatus ??= "New";

                    context.Prescriptions.Add(prescription);
                }
                else // Edit existing prescription
                {
                    var existing = context.Prescriptions.Find(prescription.PrescriptionId);
                    if (existing == null) return RedirectToAction("Index", "Home");

                    existing.MedicationName = prescription.MedicationName;
                    existing.FillStatus = prescription.FillStatus;
                    existing.Cost = prescription.Cost;
                }

                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Action = (prescription.PrescriptionId == 0) ? "Add" : "Edit";
            return View("Edit", prescription);
        }

        // GET: Delete confirmation
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Home");

            var prescription = context.Prescriptions.Find(id);
            if (prescription == null) return RedirectToAction("Index", "Home");

            return View(prescription);
        }

        // POST: Delete prescription
        [HttpPost]
        public IActionResult Delete(Prescription prescription)
        {
            var existing = context.Prescriptions.Find(prescription.PrescriptionId);
            if (existing != null)
            {
                context.Prescriptions.Remove(existing);
                context.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
