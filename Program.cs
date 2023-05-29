using Elevator_Challenge.Controllers;
using Elevator_Challenge.Controllers.ElevatorChallenge;
using Elevator_Challenge.Helpers;

namespace ElevatorChallenge
{
    public class Program
    {
        public static async Task Main()
        {
            ElevatorController elevatorController = new ElevatorController(10, 3);

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("1. Call Elevator");
                Console.WriteLine("2. Set Number of People Waiting on Floor");
                Console.WriteLine("3. Update Elevators");
                Console.WriteLine("4. Display Elevator Status");
                Console.WriteLine("5. Display Floor Status");
                Console.WriteLine("6. Exit");
                Console.WriteLine("Enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await CallElevator(elevatorController);
                        break;
                    case "2":
                        SetNumPeopleWaiting(elevatorController);
                        break;
                    case "3":
                        elevatorController.UpdateElevators();
                        break;
                    case "4":
                        elevatorController.DisplayElevatorStatus();
                        break;
                    case "5":
                        elevatorController.DisplayFloorStatus();
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }

        private static async Task CallElevator(ElevatorController elevatorController)
        {
            int floorNumber = ConsoleHelper.ReadIntegerInput("Enter the floor number: ");
            await elevatorController.CallElevator(floorNumber);
        }

        private static void SetNumPeopleWaiting(ElevatorController elevatorController)
        {
            int floorNumber = ConsoleHelper.ReadIntegerInput("Enter the floor number: ");
            int numPeople = ConsoleHelper.ReadIntegerInput("Enter the number of people waiting: ");
            elevatorController.SetNumPeopleWaiting(floorNumber, numPeople);
        }
    }
}