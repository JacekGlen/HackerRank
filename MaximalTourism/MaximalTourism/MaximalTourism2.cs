using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MaximalTourism
{
    public class MaximalTourism2
    {
        public int CalculateMaxConnections(int maxCities, int[][] args)
        {
            int[] cityAssigment = new int[maxCities + 1];

            var groupCount = new List<int>() { 0 };
            var groupParrent = new List<int>() { 0 };

            var newGroupId = 0;

            for (int ci = 0; ci < args.Length; ++ci)
            {
                var cityA = args[ci][0];
                var cityB = args[ci][1];

                //no groups assigned
                if (cityAssigment[cityA] == 0 && cityAssigment[cityB] == 0)
                {
                    newGroupId++;
                    cityAssigment[cityA] = newGroupId;
                    cityAssigment[cityB] = newGroupId;
                    groupCount.Add(cityA == cityB ? 1 : 2);
                    groupParrent.Add(newGroupId);
                    continue;
                }

                //A not assigned, B assigned
                if (cityAssigment[cityA] == 0 && cityAssigment[cityB] != 0)
                {
                    var parentGroup = groupParrent[cityAssigment[cityB]];
                    cityAssigment[cityA] = cityAssigment[cityB] = parentGroup;
                    groupCount[parentGroup]++;
                    continue;
                }

                //A assigned, B not assigned
                if (cityAssigment[cityA] != 0 && cityAssigment[cityB] == 0)
                {
                    var parentGroup = groupParrent[cityAssigment[cityA]];
                    cityAssigment[cityA] = cityAssigment[cityB] = parentGroup;
                    groupCount[parentGroup]++;
                    continue;
                }

                //same assigment
                if (cityAssigment[cityA] == cityAssigment[cityB])
                {
                    continue;
                }

                var parentGroupA = groupParrent[cityAssigment[cityA]];
                var parentGroupB = groupParrent[cityAssigment[cityB]];

                //same parent assigment
                if (parentGroupA == parentGroupB)
                {
                    continue;
                }

                var groupToAssign = Math.Min(parentGroupA, parentGroupB);
                var groupToDitch = Math.Max(parentGroupA, parentGroupB);



                //re-assign all to parent group A
                groupCount[groupToAssign] += groupCount[groupToDitch];
                groupCount[groupToDitch] = 0;

                groupParrent[cityAssigment[cityB]] = groupToAssign;
                cityAssigment[cityB] = groupToAssign;
                groupParrent[groupToDitch] = groupToAssign;
            }

            //var citiesToCheck = new int[] { 62561, 49173, 16371, 17061, 46641, 32369, 48501, 20081, 11949, 23681, 54101, 8364, 13537, 9411, 13357, 261, 58221, 35753, 52943, 53529, 46246, 47525, 24166, 19337, 57521, 7031, 15191, 6887, 13439, 15816 };

            //var groupsToCheck = new List<int>();
            //foreach(var city in citiesToCheck)
            //{
            //    groupsToCheck.Add(cityAssigment[city]);
            //}

            //compress groups

            for (int i = 0; i < groupCount.Count; ++i)
            {
                if (groupCount[i] > 0)
                {
                    var currentGroup = i;
                    var currentParent = groupParrent[i];
                    var total = groupCount[i];

                    while (currentGroup != currentParent)
                    {
                        groupCount[currentGroup] = 0;
                        currentGroup = currentParent;
                        currentParent = groupParrent[currentGroup];

                        total += groupCount[currentGroup];
                    }

                    groupCount[currentGroup] = total;
                }
            }

            var groupCheck = new List<int>();
            for (int i = 0; i < groupCount.Count; ++i)
            {
                if (groupCount[i] > 0 &&
                    i != groupParrent[i])
                {
                    groupCheck.Add(i);
                }
            }


            return groupCount.Max();
        }
    }
}
