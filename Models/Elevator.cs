using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Elevator
{
    public int Id { get; private set; }
    public int CurrentFloor { get; set; }
    public bool IsMoving { get; set; }
    public Direction Direction { get; set; } // direction of elevator movement
    private Queue<int> Requests;

    public Elevator(int id, bool isMoving = false, Direction going = Direction.Up)
    {
        Id = id;
        CurrentFloor = 1;
        IsMoving = isMoving;
        Direction = going;
        Requests = new Queue<int>(); // Initialize the queue for requests
    }

    public void AddRequest(int toFloor)
    {
        Requests.Enqueue(toFloor);
    }

    // method to simulate elevator
    public async Task Move()
    {
        while (Requests.Count > 0)
        {
            IsMoving = true;
            int destinationFloor = Requests.Dequeue();
            Console.WriteLine($"Elevator {Id} is moving from floor {CurrentFloor} to {destinationFloor} floor.");
            Task.Delay(500).Wait(); 
            
            while (CurrentFloor != destinationFloor)
            {
                if (CurrentFloor < destinationFloor)
                    CurrentFloor++;
                else
                    CurrentFloor--;

                Console.WriteLine($"Elevator {Id} is at floor {CurrentFloor}");
                await Task.Delay(500); // Simulating elevator movement delay
            }

            Console.WriteLine($"Elevator {Id} arrived at floor {CurrentFloor}. Passenger can enter or exit");
            // await Task.Delay(1000); // Simulate passenger interaction time
            CurrentFloor = destinationFloor; // Set the current floor to the destination floor after arrival
            IsMoving = false;
        }
    }
}
