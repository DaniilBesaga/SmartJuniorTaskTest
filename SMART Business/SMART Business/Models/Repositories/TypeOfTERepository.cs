using SMART_Business.DatabaseContext;

namespace SMART_Business.Models.Repositories
{
    public class TypeOfTERepository
    {
        
        public static List<TypeOfTE> GetAllTypes()
        {
            using (ContractContext db = new ContractContext())
            {
                return db.Types.ToList();
            }
        }

        public static TypeOfTE GetOneType(int id)
        {
            ContractContext db = new ContractContext();
            return db.Types.FirstOrDefault(a => a.Id == id);
        }
    }
}
