using Microsoft.AspNetCore.Mvc;
using PrescriptionApp.Models;
namespace PrescriptionApp.Controllers
{
    public class HomeController : Controller
    {
        private PrescriptionContext context { get; }
        public HomeController(PrescriptionContext ctx) => context = ctx;
        public IActionResult Index()
        {
            var prescriptions = context.Prescriptions
            .OrderBy(p => p.MedicationName)
            .ToList();
            return View(prescriptions);
        }
    }
}