using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using MaximalTourism;
using System.IO;
using System.Linq;

namespace MaximalTourism.Tests
{
    [TestFixture]
    public class MaximalTourismTest
    {
        public const int largeTestCount = 10000;

        [Test]
        [TestCase("1 1", 1)]
        [TestCase("1 2|7 4|7 3|5 8|1 3", 5)]
        public void CalculatesValidExamples(string inputStr, int expectedMaxCities)
        {
            var sut = new MaximalTourism(inputStr.Split("|"));

            var result = sut.FindMaximumConnectedCities();

            Assert.AreEqual(expectedMaxCities, result);
        }

        [Test]
        public void CalculatesWithAllDisjoint()
        {
            List<Tuple<int, int>> cities = new List<Tuple<int, int>>();

            for (int i = 1; i <= largeTestCount; ++i)
            {
                cities.Add(new Tuple<int, int>(i, i));
            }

            cities.Add(new Tuple<int, int>(largeTestCount + 1, largeTestCount + 2));

            var sut = new MaximalTourism(cities);

            var result = sut.FindMaximumConnectedCities();

            Assert.AreEqual(2, result);
        }

        [Test]
        public void CalculatesAllConnectedUpwards()
        {
            List<Tuple<int, int>> cities = new List<Tuple<int, int>>();

            for (int i = 1; i <= largeTestCount; ++i)
            {
                cities.Add(new Tuple<int, int>(i, i + 1));
            }

            cities.Add(new Tuple<int, int>(largeTestCount + 2, largeTestCount + 3));

            var sut = new MaximalTourism(cities);

            var result = sut.FindMaximumConnectedCities();

            Assert.AreEqual(largeTestCount + 1, result);
        }


        [Test]
        public void CalculatesAllConnectedDownwards()
        {
            List<Tuple<int, int>> cities = new List<Tuple<int, int>>();

            for (int i = largeTestCount; i > 0; --i)
            {
                cities.Add(new Tuple<int, int>(i, i + 1));
            }

            cities.Add(new Tuple<int, int>(largeTestCount + 2, largeTestCount + 3));

            var sut = new MaximalTourism(cities);

            var result = sut.FindMaximumConnectedCities();

            Assert.AreEqual(largeTestCount + 1, result);
        }

        [Test]
        public void CalculatesRandom()
        {
            List<Tuple<int, int>> cities = new List<Tuple<int, int>>();
            cities.Add(new Tuple<int, int>(1, 2));
            cities.Add(new Tuple<int, int>(1, 3));

            var rand = new Random();

            for (int i = 1; i <= largeTestCount / 10; ++i)
            {
                var currentCity = rand.Next(largeTestCount);
                var citiesToConnect = rand.Next(largeTestCount / 10);
                for (int j = 0; j < citiesToConnect; ++j)
                {
                    cities.Add(new Tuple<int, int>(currentCity, rand.Next(largeTestCount)));
                }
            }

            var sut = new MaximalTourism(cities);

            var result = sut.FindMaximumConnectedCities();

            Assert.IsTrue(result > 2);
        }

        [Test]
        [TestCase("MaximalTourismSample_1.txt", 28045)]
        [TestCase("MaximalTourismSample_1a.txt", 3)]
        [TestCase("MaximalTourismSample_1b.txt", 4)]
        [TestCase("MaximalTourismSample_1c.txt", 5)]
        [TestCase("MaximalTourismSample_1d.txt", 10)]
        [TestCase("MaximalTourismSample_1e.txt", 30)]
        public void UsesSampleInput(string fileName, int expectedREsult)
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, fileName);

            var allText = File.ReadAllText(filePath).Split(Environment.NewLine);
            string[] tokens_n = allText.First().Split(' ');
            int n = Convert.ToInt32(tokens_n[0]);
            int m = Convert.ToInt32(tokens_n[1]);
            int[][] route = new int[allText.Length - 1][];

            var index = 0;
            foreach (var line in allText.Skip(1))
            {
                string[] route_temp = line.Split(' ');
                route[index] = Array.ConvertAll(route_temp, Int32.Parse);
                index++;
            }

            var sut = new MaximalTourism(route);

            var result = sut.FindMaximumConnectedCities();

            Assert.AreEqual(expectedREsult, result);
        }
    }
}
