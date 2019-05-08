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

            Dictionary<int, int> groupCount = new Dictionary<int, int>();
            Dictionary<int, int> groupParrent = new Dictionary<int, int>();

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
                    groupCount[newGroupId] = cityA == cityB ? 1 : 2;
                    groupParrent[newGroupId] = newGroupId;
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

                //re-assign all to parent group A
                groupCount[parentGroupA] += groupCount[parentGroupB];
                groupCount[parentGroupB] = 0;

                groupParrent[cityAssigment[cityB]] = parentGroupA;
                cityAssigment[cityB] = parentGroupA;
                groupParrent[parentGroupB] = parentGroupA;
            }

            return groupCount.Values.Max();
        }
    }
}
