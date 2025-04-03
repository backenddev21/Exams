using System.Collections.Generic;
using System.Threading.Tasks;
using elevator.Services;

namespace elevator
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var elevators = new List<Elevator>(){
                new(1),
                new(2),
                new(3),
                new(4)
            };
            var controller = new ElevatorController(elevators);
            controller.RequestElevator(5); 
            controller.RequestElevator(2);
            controller.RequestElevator(4);
            controller.RequestElevator(7);

            await controller.StartElevators();
        }
    }
}
