using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using MaximalTourism;
using System.Linq;

namespace MaximalTourism.Tests
{
    [TestFixture]
    public class MaximalTourism2Test
    {
        public const int largeTestCount = 100000;

        [Test]
        [TestCase(1, "1 1", 1)]
        [TestCase(8, "1 2|7 4|7 3|5 8|1 3", 5)]
        [TestCase(9, "1 1|2 3|4 5|6 3|5 6|4 3|8 7", 5)]
        public void CalculatesValidExamples(int maxCities, string inputStr, int expectedMaxCities)
        {
            var input = inputStr.Split("|").Select(s => s.Split(" ").Select(ss => int.Parse(ss)).ToArray()).ToArray();
            var sut = new MaximalTourism2();

            var result = sut.CalculateMaxConnections(maxCities, input);

            Assert.AreEqual(expectedMaxCities, result);
        }

        [Test]
        public void CalculatesWithAllDisjoint()
        {
            var input = new List<int[]>();

            for (int i = 1; i <= largeTestCount; ++i)
            {
                input.Add(new int[] { i, i });
            }

            input.Add(new int[] { largeTestCount + 1, largeTestCount + 2 });

            var sut = new MaximalTourism2();

            var result = sut.CalculateMaxConnections(largeTestCount + 2, input.ToArray());

            Assert.AreEqual(2, result);
        }

        [Test]
        public void CalculatesAllConnectedUpwards()
        {
            var input = new List<int[]>();

            for (int i = 1; i < largeTestCount; ++i)
            {
                input.Add(new int[] { i, i + 1 });
            }

            input.Add(new int[] { largeTestCount + 1, largeTestCount + 2 });

            var sut = new MaximalTourism2();

            var result = sut.CalculateMaxConnections(largeTestCount + 2, input.ToArray());

            Assert.AreEqual(largeTestCount, result);
        }

        [Test]
        public void CalculatesAllConnectedDownwards()
        {
            var input = new List<int[]>();

            for (int i = largeTestCount - 1; i > 0; --i)
            {
                input.Add(new int[] { i, i + 1 });
            }

            input.Add(new int[] { largeTestCount + 1, largeTestCount + 2 });

            var sut = new MaximalTourism2();

            var result = sut.CalculateMaxConnections(largeTestCount + 2, input.ToArray());

            Assert.AreEqual(largeTestCount, result);
        }

        [Test]
        public void CalculatesRandom()
        {
            var input = new List<int[]>();
            input.Add(new int[] { 1, 2 });
            input.Add(new int[] { 1, 3 });


            var rand = new Random();

            for (int i = 1; i <= largeTestCount / 10; ++i)
            {
                var currentCity = rand.Next(largeTestCount);
                var citiesToConnect = rand.Next(largeTestCount / 10);
                for (int j = 0; j < citiesToConnect; ++j)
                {
                    input.Add(new int[] { currentCity, rand.Next(largeTestCount) });
                }
            }

            var sut = new MaximalTourism2();

            var result = sut.CalculateMaxConnections(largeTestCount, input.ToArray());

            Assert.IsTrue(result > 2);
        }
    }
}
