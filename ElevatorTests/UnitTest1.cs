using NUnit.Framework;
using FluentAssertions;
using elevator.Services;
using System.Collections.Generic;
using System.IO;
using System;

namespace ElevatorTests
{
    public class Tests
    {
        [Test]
        public void AddRequest_ShouldAddFloorToQueue()
        {
            // Arrange
            var elevator = new Elevator(1);
            // Act
            elevator.AddRequest(3);
            // Assert
            elevator.CurrentFloor.Should().Be(1);
        }

        [Test]
        public void Elevator_ShouldMoveToRequestedFloor()
        {
            // Arrange
            var elevator = new Elevator(1);
            elevator.AddRequest(3);

            // Act
            var currentFloor = elevator.CurrentFloor;

            // Assert
            Assert.AreEqual(1, currentFloor);

        }

        [Test]
        public void Elevator_AssignsAndMoves_MultipleRequests()
        {
            // Arrange
            var controller = new ElevatorController(new List<Elevator>
            {
                new(1),
                new(2),
                new(3)
            });

            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            controller.RequestElevator(3, 1, Direction.Down);
            controller.RequestElevator(4, 6, Direction.Up);
            controller.RequestElevator(2, 10, Direction.Up);

            // Assert
            var consoleOutput = output.ToString();
            Assert.IsTrue(consoleOutput.Contains("Going up selected by a passenger from floor 4."));
            Assert.IsTrue(consoleOutput.Contains("Going up selected by a passenger from floor 2."));
            Assert.IsTrue(consoleOutput.Contains("Passenger requested to go from floor 2 to floor 10 going up."));
            Assert.IsTrue(consoleOutput.Contains("Passenger requested to go from floor 3 to floor 1 going down."));
            Assert.IsTrue(consoleOutput.Contains("Passenger requested to go from floor 4 to floor 6 going up."));
        }
        
        [Test]
        public void Elevator_ShouldThrow_Message_NoElevatorIsAvailable()
        {
            // Arrange
            var controller = new ElevatorController(new List<Elevator>
            {
                new(1),
                new(2),
                new(3),
                new(4)
            });

            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            controller.RequestElevator(10, 1, Direction.Up);
            controller.RequestElevator(10, 4, Direction.Up);
            controller.RequestElevator(10, 3, Direction.Up);
            controller.RequestElevator(10, 2, Direction.Up);
            controller.RequestElevator(1, 10, Direction.Up);// this should throw a message no available elevators at the moment

            // Assert
            var consoleOutput = output.ToString();
            Assert.IsTrue(consoleOutput.Contains("No available elevators at the moment."));

        }
    }
}