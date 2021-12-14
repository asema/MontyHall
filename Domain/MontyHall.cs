using System;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public interface IMontyHall
    {
        Task<int> PlayTheGame(int numberOfSimulations, bool isDoorChanged);
    }

    public class MontyHall : IMontyHall
    {
        private static readonly Random Random = new Random();
        private readonly string[] gameDoors;
        private readonly int[] gameDoorNumbers;

        public MontyHall()
        {
            gameDoors = new[] { "Goat", "Car", "Goat" };
            gameDoorNumbers = new[] { 0, 1, 2 };
        }
        public async Task<int> PlayTheGame(int numberOfSimulations, bool isDoorChanged)
        {
            int numberOfWins = 0;

            for (var i = 0; i < numberOfSimulations; i++)
            {
                Random.Rearrange(gameDoors);
                var carDoorNumber = Array.IndexOf(gameDoors, "Car");

                var doorNumberChosen = Random.Next(0, 3);

                if (isDoorChanged)
                {
                    int changedDoor;
                    while (true)
                    {
                        changedDoor = Random.Next(0, 3);
                        if (gameDoors[changedDoor] != "Car" && changedDoor != doorNumberChosen)
                            break;
                    }

                    doorNumberChosen =
                        gameDoorNumbers.SingleOrDefault(a => a != changedDoor && a != doorNumberChosen);
                }

                if (doorNumberChosen == carDoorNumber)
                {
                    numberOfWins++;
                }
            }

            return numberOfWins;
        }
    }

    static class RandomExtensions
    {
        public static void Rearrange<T>(this Random range, T[] array)
        {
            int arrayLength = array.Length;
            while (arrayLength > 1)
            {
                int reducedRandom = range.Next(arrayLength--);
                T temporary = array[arrayLength];
                array[arrayLength] = array[reducedRandom];
                array[reducedRandom] = temporary;
            }
        }
    }
}
