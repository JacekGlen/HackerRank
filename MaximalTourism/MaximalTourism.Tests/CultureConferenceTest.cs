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
    public class CultureConferenceTest
    {

        [Test]
        [TestCase("0 0|1 0|2 0|2 0", 1)]
        [TestCase("0 0|0 0|0 0|0 1|0 0|0 0|0 1|0 0", 6)]
        [TestCase("0 0|1 1|1 0|1 0|1 1", 1)]
        [TestCase("0 1|1 0|2 0|2 0|4 0|4 0|6 0|6 0|8 0|8 0|10 0|10 0|10 1", 5)]
        [TestCase("0 0|1 0|2 0|3 0|4 0", 2)]
        [TestCase("0 0|1 0|1 0|1 0|3 0|3 0|4 0|4 0", 3)]
        [TestCase("0 1|1 0|2 0|2 0|4 1|4 0|6 1|6 1|6 1|9 1|9 1|9 0|12 0|12 0|12 0|12 1", 3)]
        [TestCase("0 1|1 1|0 0|2 1|1 0|4 1|4 1|5 1|1 0|4 1|6 0|7 1|7 1|0 1|0 0|0 1|14 0|0 1|3 1|7 0|14 0|11 0|15 0|16 1|5 1|14 1|24 0|1 1|0 0|9 0|8 1|24 1|9 1|26 0|24 1|26 0|26 0|35 0|32 1|39 0|5 0|25 0|34 0|37 1|18 0|41 0|21 1|35 1|23 1|41 1|29 0|6 0|52 0|9 0|15 1|50 0|9 0|18 0|12 0|5 0|8 0|51 0|51 1|58 1|45 0|54 0|47 0|46 1|7 0|22 1|68 0|23 1|29 0|63 0|46 1|49 0|41 1|40 0|48 1|52 0|22 0|62 0|75 1|66 1|31 1|81 0|12 0|14 1|73 0|1 0|79 0|15 0|38 1|61 0|76 0|6 0|46 0|32 1|62 0", 30)]
        public void CalculatesSimpleTestCases(string inputStr, int expectedResult)
        {
            var inputArg = inputStr.Split("|")
                .Select(str => Array.ConvertAll(str.Split(" "), Int32.Parse))
                .ToArray();

            var result = CultureConference.getMinimumEmployees(inputArg);

            Assert.AreEqual(expectedResult, result);
        }
    }
}
