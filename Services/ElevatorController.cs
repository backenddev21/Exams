using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace elevator.Services
{
    public class ElevatorController
    {
        private List<Elevator> elevators;
        private const int MinFloor = 1;
        private const int MaxFloor = 10; //building has 10 floors

        public ElevatorController(List<Elevator> elevators)
        {
            this.elevators = elevators;
        }

        public void RequestElevator(int fromFloor, int toFloor, Direction direction)
        {
            //Check the request first then return a message if there is a problem with the request
            FloorRequestChecker(fromFloor, toFloor, direction);
            AssignRequestToNearestElevator(fromFloor, toFloor, direction);
        }
        private void FloorRequestChecker(int fromFloor, int toFloor, Direction direction)
        {
            //check if request to and from is with in the range of the building floors
            if (toFloor < MinFloor || toFloor > MaxFloor)
            {
                Console.WriteLine($"There is no floor {toFloor}. Select floors from 1 to 10.");
            }

            if (fromFloor < MinFloor || fromFloor > MaxFloor)
            {
                Console.WriteLine($"There is no floor {fromFloor}. Select floors from 1 to 10.");
            }
            if(fromFloor > toFloor && direction == Direction.Down)
            {
                Console.WriteLine($"Going {direction.ToString().ToLower()} selected by a passenger from floor {fromFloor}.");
            }
            if(fromFloor < toFloor && direction == Direction.Up)
            {
                Console.WriteLine($"Going {direction.ToString().ToLower()} selected by a passenger from floor {fromFloor}.");
            }
        }
        private void AssignRequestToNearestElevator(int fromFloor, int toFloor, Direction direction)
        {
            Elevator nearestElevator = null;
            int minDistance = int.MaxValue;

            // Loop through all elevators to find the nearest one that is idle
            foreach (var elevator in elevators)
            {
                // Check if the elevator is idle and ready to move
                if (!elevator.IsMoving)
                {
                    int distanceToFloorAndCurrentFloor = Math.Abs(elevator.CurrentFloor - toFloor);
                    int distanceFromFloorAndCurrentFloor = Math.Abs(elevator.CurrentFloor - fromFloor);
                    
                    if (distanceToFloorAndCurrentFloor == 0 && distanceFromFloorAndCurrentFloor >= 1 && distanceFromFloorAndCurrentFloor < minDistance && direction == Direction.Down)
                    {
                        minDistance = distanceFromFloorAndCurrentFloor;
                        nearestElevator = elevator;
                        toFloor = fromFloor;
                    }
                    
                    if (distanceToFloorAndCurrentFloor > 1 && distanceFromFloorAndCurrentFloor == 0 && distanceToFloorAndCurrentFloor < minDistance && direction == Direction.Down)
                    {
                        minDistance = distanceToFloorAndCurrentFloor;
                        nearestElevator = elevator;
                    } 

                    if (distanceToFloorAndCurrentFloor > 1 && distanceFromFloorAndCurrentFloor == 0 && distanceToFloorAndCurrentFloor < minDistance && direction == Direction.Up)
                    {
                        minDistance = distanceToFloorAndCurrentFloor;
                        nearestElevator = elevator;
                    } 
                    if (distanceToFloorAndCurrentFloor >= 1 && distanceFromFloorAndCurrentFloor >= 1 && distanceToFloorAndCurrentFloor < minDistance && direction == Direction.Down)
                    {
                        minDistance = distanceToFloorAndCurrentFloor;
                        nearestElevator = elevator;
                    } 
                }
            }

            // If an idle elevator is found, assign the request
            if (nearestElevator != null && !nearestElevator.IsMoving)
            {
                if(nearestElevator.CurrentFloor != fromFloor) toFloor = fromFloor;
                Console.WriteLine($"Elevator {nearestElevator.Id} is going to floor {toFloor}.");
                nearestElevator.AddRequest(toFloor);
                nearestElevator.IsMoving = true;
            }
            else
            {
                Console.WriteLine("No available elevators at the moment.");
            }
        }

        public async Task StartElevators()
        {
            // Start all elevators asynchronously
            var tasks = elevators.Select(elevator => elevator.Move());
            await Task.WhenAll(tasks);
        }
    }

}