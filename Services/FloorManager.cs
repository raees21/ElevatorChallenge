using ElevatorChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator_Challenge.Services
{
    public class FloorManager
    {
        private List<FloorModel> floors;
        public int TotalFloors { get; set; }
        public int NumPropleWaiting { get; set; }

        public FloorManager(int totalFloors)
        {
            floors = new List<FloorModel>();
            for (int i = 1; i <= totalFloors; i++)
            {
                floors.Add(new FloorModel(i));
            }
            TotalFloors = totalFloors;
        }

        public FloorModel GetFloor(int floorNumber)
        {
            return floors.FirstOrDefault(floor => floor.FloorNumber == floorNumber);
        }

        public void SetNumPeopleWaiting(int floorNumber, int numPeople)
        {
            var floor = GetFloor(floorNumber);
            floor?.SetNumPeopleWaiting(numPeople);
        }

        public void DisplayFloorStatus()
        {
            foreach (var floor in floors)
            {
                Console.WriteLine($"Floor {floor.FloorNumber}: {floor.NumPeopleWaiting} people waiting");
            }
        }
    }
}