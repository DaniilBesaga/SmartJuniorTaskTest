using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMART_Business.Models
{
    public class Contract
    {
        public int Id { get; set; }

        public int RoomId { get; set; }
        public ProductionRoom Room { get; set; }

        public int TypeId { get; set; }
        public TypeOfTE Type { get; set; }

        public int Count { get; set; }
    }
}
