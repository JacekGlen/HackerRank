using System.IO;
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
        public const int largeTestCount = 150000;

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
            var input = new List<int[]>((largeTestCount / 10) * (largeTestCount / 10) / 2);
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

        [Test]
        [TestCase("MaximalTourismSample_1.txt", 28045)]
        [TestCase("MaximalTourismSample_1a.txt", 3)]
        [TestCase("MaximalTourismSample_1b.txt", 4)]
        [TestCase("MaximalTourismSample_1c.txt", 5)]
        [TestCase("MaximalTourismSample_1d.txt", 10)]
        [TestCase("MaximalTourismSample_1e.txt", 30)]
        [TestCase("MaximalTourismSample_1e_1.txt", 30)]
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

            var sut = new MaximalTourism2();

            var result = sut.CalculateMaxConnections(n, route);

            Assert.AreEqual(expectedREsult, result);
        }
    }
}
