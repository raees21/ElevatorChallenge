using Elevator_Challenge.Services;
using Elevator_Challenge.Services.ElevatorChallenge.Services;
using ElevatorChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator_Challenge.Controllers
{
    namespace ElevatorChallenge
    {
        public class ElevatorController
        {
            private List<ElevatorModel> elevators;
            private FloorManager floorManager;

            public ElevatorController(int totalFloors, int numElevators)
            {
                floorManager = new FloorManager(totalFloors);
                elevators = new List<ElevatorModel>();
                for (int i = 1; i <= numElevators; i++)
                {
                    elevators.Add(new ElevatorModel(i, floorManager));
                }

            }

            public async Task CallElevator(int floorNumber)
            {
                var elevator = GetNearestAvailableElevator(floorNumber);
                if (elevator != null)
                {
                    elevator.IsMoving = true;
                    await elevator.MoveToDestinationFloor(floorNumber, floorManager.TotalFloors);
                    elevator.IsMoving = false;
                }
            }

            public void SetNumPeopleWaiting(int floorNumber, int numPeople)
            {
                floorManager.SetNumPeopleWaiting(floorNumber, numPeople);
            }

            public async Task UpdateElevators()
            {
                foreach (var elevator in elevators)
                {
                    if (!elevator.IsMoving && elevator.Passengers.Count == 0)
                    {
                        await elevator.MoveToDestinationFloor(1, floorManager.TotalFloors);
                    }
                }
            }

            public void DisplayElevatorStatus()
            {
                foreach (var elevator in elevators)
                {
                    elevator.DisplayStatus();
                }
            }

            public void DisplayFloorStatus()
            {
                floorManager.DisplayFloorStatus();
            }

            private ElevatorModel GetNearestAvailableElevator(int floorNumber)
            {
                ElevatorModel nearestElevator = null;
                int minDistance = int.MaxValue;

                foreach (var elevator in elevators)
                {
                    if (!elevator.IsMoving)
                    {
                        int distance = Math.Abs(elevator.CurrentFloor - floorNumber);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            nearestElevator = elevator;
                        }
                    }
                }

                return nearestElevator;
            }
        }
    }
}