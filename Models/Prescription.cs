using System.ComponentModel.DataAnnotations;

namespace PrescriptionApp.Models
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }

        [Required(ErrorMessage = "Please enter a medication name.")]
        public string MedicationName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please choose a fill status.")]
        public string FillStatus { get; set; } = string.Empty; // "New", "Filled", "Pending"

        [Required(ErrorMessage = "Please enter a cost.")]
        [Range(0.01, 1000000, ErrorMessage = "Cost must be greater than 0.")]
        public double? Cost { get; set; }

        [Required(ErrorMessage = "Request time is required.")]
        public DateTime? RequestTime { get; set; }

        // Generates slug for URL
        public string Slug =>
            MedicationName?.Trim().ToLower().Replace(' ', '-') ?? string.Empty;
    }
}
