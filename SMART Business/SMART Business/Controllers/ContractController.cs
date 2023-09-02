using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMART_Business.DatabaseContext;
using SMART_Business.Filters;
using SMART_Business.Models;
using SMART_Business.Models.Repositories;
using System.Diagnostics.Contracts;

namespace SMART_Business.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractController : ControllerBase
    {
        private ContractContext db { get; set; }
        [HttpGet]
        [TypeFilter(typeof(ApiKeyAuthorizationFilterAttribute))] //Перевірка авторизації. Ключ зберігається у секретах

        public IActionResult GetContracts()
        {
            db = new ContractContext();
            var list =db.Contracts.ToList();
            if (list.Count == 0)
                return Ok("There are any contracts"); //Якщо список порожній, значить контрактів немає
            List<ContractToGet> contracts = new List<ContractToGet>();
            foreach(var contract in list) //Трішки перероблюю об'єкти для легкого читання користовачу
            {
                contracts.Add(new ContractToGet
                {
                    Id = contract.Id,
                    RoomName = db.Rooms.Single(r=>r.Id==contract.RoomId).RoomName,
                    TypeName = db.Types.Single(t => t.Id == contract.RoomId).TypeName,
                    Count = contract.Count
                });
            }
            return Ok(contracts);
        }

        [HttpPost]
        [TypeFilter(typeof(ApiKeyAuthorizationFilterAttribute))]
        public IActionResult CreateContract(int RoomId, int TypeId, int Count)
        {
            if (RoomId == 0 || TypeId == 0 || Count == 0) //Якщо користувач щось не ввів
                return NotFound("Please fill all the neccessary informaion");
            ProductionRoom? room = ProductionRoomRepository.GetAllRooms().FirstOrDefault(r => r.Id == RoomId);
            TypeOfTE? type = TypeOfTERepository.GetAllTypes().FirstOrDefault(t => t.Id == TypeId);
            if (room == null || type == null) //Якщо повертає null, то такого приміщення або такого типу обладнання не існує
                return NotFound("You entered invalid information");
            double cur_sq = room.Square - (type.Square * Count); //Рахую введені дані
            
            if (cur_sq < 0) //Якщо менше нуля, то приміщення заповнене або туди не влізуть введені типи обладання
                return NotFound("Sorry, the room is full");
            else
            {
                room.Square = cur_sq; //Змінюю актуальну площу і додаю дані до таблиці у бд
                ContractRepository.AddNewContract(room, type, Count);
                return Ok("A new contract was successfully added");
            }
        }
    }
}
