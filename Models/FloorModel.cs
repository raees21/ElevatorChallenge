using Elevator_Challenge.Models;
using Elevator_Challenge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorChallenge.Models
{
    public class FloorModel
    {
        public int FloorNumber { get; }
        public int NumPeopleWaiting { get; private set; }
        public FloorModel(int floorNumber)
        {
            FloorNumber = floorNumber;
            NumPeopleWaiting = 0;
        }

        public void SetNumPeopleWaiting(int numPeople)
        {
            NumPeopleWaiting = numPeople;
        }

        public void ResetNumPeopleWaiting()
        {
            NumPeopleWaiting = 0;
        }

        public List<PersonModel> GetPeople(int numPeople, int totalFloors)
        {
            List<PersonModel> people = new List<PersonModel>();

            for (int i = 0; i < numPeople; i++)
            {
                var person = new PersonModel(FloorNumber, GetRandomDestinationFloor(totalFloors));
                people.Add(person);
            }

            return people;
        }

        private int GetRandomDestinationFloor(int totalFloors)
        {
            // Generate a random destination floor different from the current floor
            Random random = new Random();
            int destinationFloor;
            do
            {
                destinationFloor = random.Next(1, totalFloors + 1);
            } while (destinationFloor == FloorNumber);

            return destinationFloor;
        }
    }
}