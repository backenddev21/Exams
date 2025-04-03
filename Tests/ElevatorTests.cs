// using NUnit.Framework;
// using Moq;
// using System.Collections.Generic;

// [TestFixture]
// public class ElevatorSystemTests
// {
//     private ElevatorSystem _elevatorSystem;

//     [SetUp]
//     public void Setup()
//     {
//         _elevatorSystem = new ElevatorSystem(3); // Assuming 3 elevators
//     }

//     [Test]
//     public void AssignElevator_ShouldAssignNearestElevator()
//     {
//         // Arrange
//         _elevatorSystem.RequestElevator(5);

//         // Act
//         var assignedElevator = _elevatorSystem.GetAssignedElevator(5);

//         // Assert
//         Assert.IsNotNull(assignedElevator);
//         Assert.AreEqual(5, assignedElevator.DestinationFloor);
//     }

//     [Test]
//     public void Elevator_ShouldMoveToRequestedFloor()
//     {
//         // Arrange
//         var elevator = new Elevator(1);
//         elevator.MoveToFloor(3);

//         // Act
//         var currentFloor = elevator.CurrentFloor;

//         // Assert
//         Assert.AreEqual(3, currentFloor);
//     }
// }
