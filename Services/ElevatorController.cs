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

    public void RequestElevator(int floor)
    {
        if (floor < MinFloor || floor > MaxFloor)
        {
            Console.WriteLine($"There is no floor {floor}. Select floors from 1 to 10.");
            return; 
        }

        Console.WriteLine($"Floor {floor} selected.");

        //
        AssignRequestToNearestElevator(floor);
    }

    private void AssignRequestToNearestElevator(int requestedFloor)
    {
        Elevator nearestElevator = null;
        int minDistance = int.MaxValue;

        // Loop through all elevators to find the nearest one that is idle
        foreach (var elevator in elevators)
        {
            // Check if the elevator is idle and ready to move
            if (!elevator.IsMoving)
            {
                int distance = Math.Abs(elevator.CurrentFloor - requestedFloor);

                // Find the nearest elevator
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestElevator = elevator;
                }
            }
        }

        // If an idle elevator is found, assign the request
        if (nearestElevator != null && !nearestElevator.IsMoving)
        {
            Console.WriteLine($"Elevator {nearestElevator.Id} assigned to floor {requestedFloor}.");
            nearestElevator.AddRequest(requestedFloor);
            nearestElevator.IsMoving = true; // Set the elevator to moving state
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