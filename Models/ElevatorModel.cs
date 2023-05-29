using Elevator_Challenge.Models;
using Elevator_Challenge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorChallenge.Models
{
    public class ElevatorModel
    {
        public int Id { get; }
        public int CurrentFloor { get; private set; }
        public bool IsMoving { get; set; }
        public List<PersonModel> Passengers { get; }

        private FloorManager floorManager;

        public ElevatorModel(int id, FloorManager floorManager)
        {
            Id = id;
            this.floorManager = floorManager;
            CurrentFloor = 1;
            IsMoving = false;
            Passengers = new List<PersonModel>();
        }

        public async Task MoveToDestinationFloor(int floorNumber, int totalFloors)
        {
            var direction = GetDirection(floorNumber);

            while (CurrentFloor != floorNumber)
            {
                await Task.Delay(TimeSpan.FromSeconds(1)); // Simulating the time it takes to move between floors

                if (direction == Direction.Up)
                    CurrentFloor++;
                else if (direction == Direction.Down)
                    CurrentFloor--;

                DisplayStatus();
            }

            await Task.Delay(TimeSpan.FromSeconds(1)); // Simulating the time it takes to open the doors
            OpenDoors();

            // Unload passengers who want to get off at this floor
            UnloadPassengers();

            // Load passengers who are waiting on this floor
            LoadPassengers(floorNumber, totalFloors);

            await Task.Delay(TimeSpan.FromSeconds(1)); // Simulating the time it takes to close the doors
            CloseDoors();
        }

        private Direction GetDirection(int floorNumber)
        {
            return floorNumber > CurrentFloor ? Direction.Up : Direction.Down;
        }

        private void OpenDoors()
        {
            Console.WriteLine($"Elevator {Id} doors opened on floor {CurrentFloor}.");
        }

        private void CloseDoors()
        {
            Console.WriteLine($"Elevator {Id} doors closed on floor {CurrentFloor}.");
        }

        private void UnloadPassengers()
        {
            int numPassengersToUnload = Passengers.Count(p => p.DestinationFloor == CurrentFloor);
            Console.WriteLine($"Elevator {Id} unloaded {numPassengersToUnload} passengers on floor {CurrentFloor}.");
            Passengers.RemoveAll(p => p.DestinationFloor == CurrentFloor);
        }
        private void LoadPassengers(int floorNumber, int totalFloors)
        {
            var floor = floorManager.GetFloor(floorNumber);
            if (floor != null)
            {
                int numPassengersToLoad = floor.NumPeopleWaiting;
                Console.WriteLine($"Elevator {Id} loaded {numPassengersToLoad} passengers from floor {floorNumber}.");
                Passengers.AddRange(floor.GetPeople(numPassengersToLoad, totalFloors));
                floor.ResetNumPeopleWaiting();
            }
        }

        public void DisplayStatus()
        {
            string direction = IsMoving ? (CurrentFloor < floorManager.TotalFloors ? "Up" : "Down") : "Stationary";
            string passengerText = Passengers.Count == 1 ? "passenger" : "passengers";
            Console.WriteLine($"Elevator {Id}: Floor {CurrentFloor}, Direction: {direction}, Passengers: {Passengers.Count} {passengerText}");
        }
    }
}