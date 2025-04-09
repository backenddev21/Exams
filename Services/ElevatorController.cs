using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit;

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

            //Assign the request to the nearest available elevator
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
                if (elevator.IsMoving == false)
                {
                    int distanceToFloorAndCurrentFloor = Math.Abs(elevator.CurrentFloor - toFloor);
                    int distanceFromFloorAndCurrentFloor = Math.Abs(elevator.CurrentFloor - fromFloor);
                    
                    //Scenario 1                    
                    if (distanceToFloorAndCurrentFloor == 0 &&  //means elevator is on the same floor as the request to floor
                        distanceFromFloorAndCurrentFloor >= 1 && //means elevator is not on the same floor as the request from floor
                        direction == (fromFloor > toFloor ? Direction.Down : Direction.Down) && //means elevator request is going down
                        distanceFromFloorAndCurrentFloor < minDistance )
                    {
                        minDistance = distanceFromFloorAndCurrentFloor;
                        nearestElevator = elevator;
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
                    //test case from floor 7 to floor 2 and elevator is going down
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
                Console.WriteLine($"Passenger requested to go from floor {fromFloor} to floor {toFloor} going {direction.ToString().ToLower()}.");

                //check if the elevator request is going up or down
                if(fromFloor < toFloor && direction == Direction.Up)
                {
                    nearestElevator.Direction = Direction.Up;
                    nearestElevator.AddRequest(fromFloor);
                    nearestElevator.AddRequest(toFloor);
                    nearestElevator.IsMoving = true;
                }
                else if(fromFloor < toFloor && direction == Direction.Down)
                {
                    nearestElevator.Direction = Direction.Up;
                    nearestElevator.AddRequest(toFloor);
                    nearestElevator.AddRequest(fromFloor);
                    nearestElevator.IsMoving = true;
                }
                else if(fromFloor > toFloor && direction == Direction.Down)
                {
                    nearestElevator.Direction = Direction.Down;
                    nearestElevator.AddRequest(fromFloor);
                    nearestElevator.AddRequest(toFloor);
                    nearestElevator.IsMoving = true;
                    Task.Delay(2500).Wait(); // Simulate delay for elevator to reach the floor
                }
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