using SMART_Business.DatabaseContext;

namespace SMART_Business.Models.Repositories
{
    public class ProductionRoomRepository
    {
        public static List<ProductionRoom> GetAllRooms()
        {
            using (ContractContext db = new ContractContext())
            {
                return db.Rooms.ToList();
            }
        }

        public static ProductionRoom GetOneRoom(int id)
        {
            ContractContext db = new ContractContext();
            return db.Rooms.FirstOrDefault(a => a.Id == id);
        }
    }
}
