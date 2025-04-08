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
            // Simulate multiple requests from different passengers
            var request = new List<PassengerRequest>{
                // new(1, 3, Direction.Down), //Passenger from floor 3 requests neareset elevator to go to floor 3 to go down to floor 1
                // new(3, 1, Direction.Up), //Passenger from floor 1 requests to go Up to floor 3
                // new(1, 3, Direction.Down),
                // new(5, 1, Direction.Up),
                new(7, 2, Direction.Down)
            };
            
            foreach (var req in request)
            {
                var controller = new ElevatorController(elevators);
                controller.RequestElevator(req.FromFloor, req.ToFloor, req.Direction); 

                await controller.StartElevators();
            }
        }
    }
}
