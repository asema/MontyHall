using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MontyHall.Tests
{
    [TestClass]
    public class MontyHallTest
    {
        [TestMethod]
        public void Number_Of_Simulations_Should_Not_Equal_Number_of_Attempts_If_Door_Is_Not_Changed()
        {
            var montyHall = new MonteyHall.Web.Utility.MontyHall();
            var numberOfSimulations = 1000;
            var expectedWins = montyHall.PlayTheGame(numberOfSimulations, false).Result;

            Assert.AreNotEqual(numberOfSimulations, expectedWins);
        }

        [TestMethod]
        public void Number_Of_Wins_Should_Be_More_Than_60_Percent_When_Door_Is_Changed_And_Simulations_Is_More_Than_1000()
        {
            var montyHall = new MonteyHall.Web.Utility.MontyHall();
            var numberOfSimulations = 1000;
            var expectedWins = montyHall.PlayTheGame(numberOfSimulations, true).Result;

            Assert.IsTrue(expectedWins > 600, "Wins is more than 600");
        }

        [TestMethod]
        [DataRow(1000, false, 300)]
        [DataRow(1000, true, 600)]
        public void Number_Of_Wins(int simulations, bool changeDoor, int expectedWins)
        {
            var montyHall = new MonteyHall.Web.Utility.MontyHall();

            var actual = montyHall.PlayTheGame(simulations, changeDoor).Result;

            Assert.IsFalse(actual < expectedWins);
        }
    }
}
