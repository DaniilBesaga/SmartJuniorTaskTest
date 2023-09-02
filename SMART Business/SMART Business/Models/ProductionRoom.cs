using System.ComponentModel.DataAnnotations;

namespace SMART_Business.Models
{
    public class ProductionRoom
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public double Square { get; set; }
        public ICollection<Contract>? Contracts { get; set; } //Для відносин один до багатьох
    }
}
