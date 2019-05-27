using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace MaximalTourism
{
    public class MaximalTourism2
    {
        public int CalculateMaxConnections(int maxCities, int[][] args)
        {
            CityGroup[] cityAssigment = new CityGroup[maxCities + 1];
            var allGroups = new List<CityGroup>();

            int groupId = 1;

            for (int ci = 0; ci < args.Length; ++ci)
            {
                var cityA = args[ci][0];
                var cityB = args[ci][1];

                //no groups assigned
                if (cityAssigment[cityA] == null && cityAssigment[cityB] == null)
                {
                    var cityGroup = new CityGroup(groupId++);

                    cityGroup.AddCity(cityA);
                    if (cityA != cityB)
                    {
                        cityGroup.AddCity(cityB);
                    }

                    cityAssigment[cityA] = cityAssigment[cityB] = cityGroup;
                    allGroups.Add(cityGroup);

                    continue;
                }

                //A not assigned, B assigned
                if (cityAssigment[cityA] == null && cityAssigment[cityB] != null)
                {
                    cityAssigment[cityB].AddCity(cityA);
                    cityAssigment[cityA] = cityAssigment[cityB];

                    continue;
                }

                //A assigned, B not assigned
                if (cityAssigment[cityA] != null && cityAssigment[cityB] == null)
                {
                    cityAssigment[cityA].AddCity(cityB);
                    cityAssigment[cityB] = cityAssigment[cityA];

                    continue;
                }

                //same assigment
                if (cityAssigment[cityA] == cityAssigment[cityB])
                {
                    continue;
                }
                                
                //re-assign all to parent group A
                cityAssigment[cityA].ConvertToSubGroup(cityAssigment[cityB]);
            }

            return allGroups.Max(g => g.Count);
        }

        [DebuggerDisplay("Id: {GroupId} ({Count}), Parent Id: {TopLevel.GroupId} ({TopLevel.Count}) ")]
        private class CityGroup
        {
            public int GroupId { get; }
            public int Count { get; set; }
            public CityGroup TopLevel { get; set; }

            public CityGroup(int groupId)
            {
                GroupId = groupId;
                TopLevel = this;
                Count = 0;
            }

            public void AddCity(int city)
            {
                TopLevel.Count++;
            }

            public void ConvertToSubGroup(CityGroup subGroup)
            {

                if ( TopLevel == subGroup || TopLevel == subGroup.TopLevel )
                {
                    return;
                }

                //set subgroup parent
                TopLevel.Count += subGroup.TopLevel.Count;
                subGroup.TopLevel.Count = 0;
                subGroup.TopLevel.TopLevel = TopLevel;

                //set subgroup
                TopLevel.Count += subGroup.Count;
                subGroup.Count = 0;
                subGroup.TopLevel = TopLevel;
            }
        }
    }
}
