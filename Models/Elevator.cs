using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Elevator
{
    public int Id { get; private set; }
    public int CurrentFloor { get; set; }
    public bool IsMoving { get; set; }
    private Queue<int> requests;

    public Elevator(int id)
    {
        Id = id;
        CurrentFloor = 1;
        IsMoving = false;
        requests = new Queue<int>();
    }

    public void AddRequest(int floor)
    {
        requests.Enqueue(floor);
    }

    // Asynchronous move method simulating the elevator's movement.
    public async Task Move()
    {
        while (requests.Count > 0)
        {
            IsMoving = true;
            int requestedFloor = requests.Dequeue();
            Console.WriteLine($"Elevator {Id} is moving from floor {CurrentFloor} to floor {requestedFloor}.");

            // Simulate movement with a delay
            while (CurrentFloor != requestedFloor)
            {
                if (CurrentFloor < requestedFloor)
                    CurrentFloor++;
                else
                    CurrentFloor--;

                Console.WriteLine($"Elevator {Id} is at floor {CurrentFloor}");
                await Task.Delay(500); // Simulating elevator movement delay
            }

            Console.WriteLine($"Elevator {Id} has arrived at floor {CurrentFloor}. Waiting for passengers...");
            IsMoving = false;
            await Task.Delay(1000); // Simulate passenger interaction time
        }
    }
}
