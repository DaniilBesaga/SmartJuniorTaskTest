using SMART_Business.Controllers;
using SMART_Business.DatabaseContext;
using System.Transactions;

namespace SMART_Business.Models.Repositories
{
    public class ContractRepository
    {
        public static List<Contract> GetAllContracts() 
        {
            using (ContractContext db = new ContractContext())
            {
                return db.Contracts.ToList();
            }
        }
        public static void AddNewContract(ProductionRoom room, TypeOfTE type, int Count)
        {
            Contract contract = new Contract { Room = room, Type = type, Count = Count };
            using (ContractContext db = new ContractContext())
            {
                db.Contracts.Add(contract);
                db.Rooms.Update(room);
                db.Types.Update(type);
                db.SaveChanges();
            }
        }
    }
}
