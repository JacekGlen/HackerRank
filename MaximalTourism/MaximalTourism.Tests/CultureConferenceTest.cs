﻿using System.IO;
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
        [TestCase("0 0|1 0|1 0|1 0|3 0|3 0|4 0|4 0|6 0", 4)]
        [TestCase("0 1|1 0|2 0|2 0|4 1|4 0|6 1|6 1|6 1|9 1|9 1|9 0|12 0|12 0|12 0|12 1", 3)]
        [TestCase("0 1|1 1|0 0|2 1|1 0|4 1|4 1|5 1|1 0|4 1|6 0|7 1|7 1|0 1|0 0|0 1|14 0|0 1|3 1|7 0|14 0|11 0|15 0|16 1|5 1|14 1|24 0|1 1|0 0|9 0|8 1|24 1|9 1|26 0|24 1|26 0|26 0|35 0|32 1|39 0|5 0|25 0|34 0|37 1|18 0|41 0|21 1|35 1|23 1|41 1|29 0|6 0|52 0|9 0|15 1|50 0|9 0|18 0|12 0|5 0|8 0|51 0|51 1|58 1|45 0|54 0|47 0|46 1|7 0|22 1|68 0|23 1|29 0|63 0|46 1|49 0|41 1|40 0|48 1|52 0|22 0|62 0|75 1|66 1|31 1|81 0|12 0|14 1|73 0|1 0|79 0|15 0|38 1|61 0|76 0|6 0|46 0|32 1|62 0", 30)]
        [TestCase("0 1|1 0|0 1|2 1|4 0|3 1|4 0|5 0|3 1|2 1|8 0|0 1|0 0|8 1|8 0|2 1|2 1|7 0|14 1|9 0|6 1|20 1|10 0|15 1|17 0|17 1|9 1|9 1|19 1|29 1|12 0|0 1|30 1|21 1|1 0|16 1|11 0|17 1|7 0|12 1|18 1|35 0|16 1|11 0|17 1|29 1|41 1|38 0|40 0|32 1|21 1|29 0|40 0|13 0|23 0|26 0|29 0|27 0|19 1|26 0|40 0|32 0|46 1|41 1|16 0|9 0|52 1|37 1|26 1|42 0|23 1|58 0|20 0|31 1|4 0|59 0|51 0|69 0|18 0|48 0|42 0|4 0|17 0|36 1|38 1|5 1|3 0|1 1|51 0|2 0|7 1|66 1|52 1|39 0|83 1|46 0|88 0|67 1|90 1|81 1|99 1|42 0|29 1|14 0|30 0|49 1|24 0|16 1|79 0|58 0|107 1|99 1|30 1|103 1|62 0|90 1|79 0|38 0|42 0|73 1|99 1|22 1|5 1|110 1|26 1|19 1|23 1|118 0|100 0|4 0|5 0|118 0|54 1|8 0|130 1|53 0|74 1|59 1|107 0|125 1|5 0|66 1|140 0|97 0|29 0|92 1|56 0|83 0|12 0|41 0|102 1|99 0|83 1|34 0|20 1|39 1|91 1|49 1|0 0|28 0|149 1|145 1|16 1|151 0|58 0|149 1|154 0|143 0|107 1|9 0|134 0|169 0|7 0|51 1|164 1|61 1|151 1|91 1|160 1|65 0|167 1|165 0|7 1|120 0|120 0|21 0|43 0|114 1|181 1|117 0|41 0|66 1|142 0|171 0|182 0|67 1|124 0|40 1|99 1|44 1|79 0|45 1|141 0|114 1|55 1|157 0|77 0|168 1|124 0|65 1|133 1|41 1|154 0|80 0|180 1|4 0|89 0|142 0|180 0|186 0|121 0|172 1|80 0|59 0|48 0|57 0|143 0|204 1|222 1|33 0|222 0|225 1|227 1|199 0|156 1|11 0|122 1|91 1|199 0|38 1|51 0|240 1|123 1|95 0|46 1|20 0|81 1|106 0|24 0|240 1|57 1|163 0|71 0|155 1|230 0|243 1|138 1|1 1|92 0|259 0|91 1|25 1|177 1|208 1|128 0|123 1|78 1|110 0|111 1|60 0|74 1|59 1|249 0|159 1|171 1|231 1|74 1|74 0|245 1|219 0|96 1|65 1|245 0|118 1|26 1|139 1|68 1|233 1|147 0|256 0|101 1|76 1|102 1|87 0|80 1|104 1|222 1|9 1|132 1|297 1|286 1|220 1|12 0|119 1|19 1|302 1|296 0|1 0|143 0|113 0|28 1|226 0|130 1|22 0|185 0|72 0|78 0|111 1|75 0|291 0|139 1|113 1|299 1|193 0|282 1|26 0|44 1|143 1|282 1|144 0|312 0|18 1|121 1|132 1|43 0|45 0", 81)]
        public void CalculatesSimpleTestCases(string inputStr, int expectedResult)
        {
            var inputArg = inputStr.Split("|")
                .Select(str => Array.ConvertAll(str.Split(" "), Int32.Parse))
                .ToArray();

            var result = CultureConference.getMinimumEmployees(inputArg);

            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("CultureConferenceSample_1.txt", 7039)]
        [TestCase("CultureConferenceSample_2.txt", 5571)]
        public void UsesSampleInput(string fileName, int expectedREsult)
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, fileName);

            var allText = File.ReadAllText(filePath).Split(Environment.NewLine);
            var a = allText.Skip(1)
                .Select(l => Array.ConvertAll(l.Split(" "), Int32.Parse))
                .ToArray();

            var result = CultureConference.getMinimumEmployees(a);

            Assert.AreEqual(expectedREsult, result);
        }
    }
}
