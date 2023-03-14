using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ISun.CommandLine.Tests
{
    [TestClass()]
    public class CitiesArgumentExtractorTests
    {
        [TestMethod()]
        [DataRow("--cities Vilnius", 1)]
        [DataRow("--cities Vilnius, Kaunas, Klaipėda", 3)]
        [DataRow("--cities", 0)]
        [DataRow("--period=2 --cities Vilnius --test", 1)]
        [DataRow("--period=2 --cities Vilnius,Kaunas --test", 2)]
        [DataRow("--period=2 --cities Vilnius, Kaunas --test", 2)]
        [DataRow("--period=2 --cities --test", 0)]
        [DataRow("--period=2 --test", 0)]
        public void ExtractCitiesListFromArgumentsCountTests(string commandLine, int citiesCount)
        {
            var args = commandLine.Split(' ');
            var c = CitiesArgumentExtractor.ExtractCitiesListFromArguments(args);
            Assert.AreEqual(citiesCount, c.Count());
        }
    }
}