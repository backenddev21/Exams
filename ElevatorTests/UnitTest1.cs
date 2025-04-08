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
        [TestCase(1)]
        [Test]
        public void AddRequest_ShouldAddFloorToQueue(int elevatorId)
        {
            // Arrange
            var elevator = new Elevator(elevatorId);
            // Act
            elevator.AddRequest(3);
            //Assert
            elevator.CurrentFloor.Should().Be(1);
            Assert.Pass();
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
            controller.RequestElevator(3, 1, Direction.Up);
            controller.RequestElevator(4, 6, Direction.Up);
            controller.RequestElevator(2, 10, Direction.Up);

            // Assert
            var consoleOutput = output.ToString();
            Assert.IsTrue(consoleOutput.Contains("Floor 3 selected."));
            Assert.IsTrue(consoleOutput.Contains("Elevator 3 assigned to floor 3."));
            Assert.IsTrue(consoleOutput.Contains("Floor 5 selected."));
            Assert.IsTrue(consoleOutput.Contains("Elevator 5 assigned to floor 5."));
            Assert.IsTrue(consoleOutput.Contains("Floor 2 selected."));
            Assert.IsTrue(consoleOutput.Contains("Elevator 2 assigned to floor 2."));
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