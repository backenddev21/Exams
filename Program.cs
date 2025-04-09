using System.Collections.Generic;
using System.Threading.Tasks;
using elevator.Services;

namespace elevator
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            // Initialize elevators
            var elevators = new List<Elevator>(){
                new(1),
                new(2),
                new(3),
                new(4)
            };
            var controller = new ElevatorController(elevators);
             controller.RequestElevator(3, 1, Direction.Down); 
             controller.RequestElevator(1, 3, Direction.Up);
             controller.RequestElevator(5, 1, Direction.Down);
             controller.RequestElevator(2, 7, Direction.Down);
             await controller.StartElevators();
        }
    }
}
