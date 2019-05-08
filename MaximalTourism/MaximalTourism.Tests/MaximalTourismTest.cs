using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using MaximalTourism;

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

            for (int i = largeTestCount; i >0 ; --i)
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
    }
}
