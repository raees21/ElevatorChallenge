using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator_Challenge.Models
{
    public class PersonModel
    {
        public int CurrentFloor { get; }
        public int DestinationFloor { get; }

        public PersonModel(int currentFloor, int destinationFloor)
        {
            CurrentFloor = currentFloor;
            DestinationFloor = destinationFloor;
        }
    }
}
