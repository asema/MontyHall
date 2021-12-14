using System.ComponentModel.DataAnnotations;

namespace MonteyHall.Web.Model
{
    public class MontyHallModel
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please only numbers 1 and above are allowed")]
        public int NumberOfSimulation { get; set; }
        public bool IsDoorChanged { get; set; }
    }
}
