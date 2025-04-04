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

    // method to simulate elevator
    public async Task Move()
    {
        while (requests.Count > 0)
        {
            IsMoving = true;
            int requestedFloor = requests.Dequeue();
            Console.WriteLine($"Elevator {Id} is moving from floor {CurrentFloor} to {requestedFloor} floor.");
            Task.Delay(10000).Wait(); 
            
            while (CurrentFloor != requestedFloor)
            {
                if (CurrentFloor < requestedFloor)
                    CurrentFloor++;
                else
                    CurrentFloor--;

                Console.WriteLine($"Elevator {Id} is at floor {CurrentFloor}");
                await Task.Delay(10000); // Simulating elevator movement delay
            }

            Console.WriteLine($"Elevator {Id} arrived at floor {CurrentFloor}.");
            IsMoving = false;
            await Task.Delay(1000); // Simulate passenger interaction time
        }
    }
}
