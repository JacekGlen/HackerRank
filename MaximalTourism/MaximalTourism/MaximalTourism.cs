using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MaximalTourism
{
    public class MaximalTourism
    {
        IEnumerable<CityConnection> cityConnections;
        int groupId;

        public MaximalTourism(string[] args)
        {
            cityConnections = args.Select(a => new CityConnection(a));
        }

        public MaximalTourism(IEnumerable<Tuple<int, int>> args)
        {
            cityConnections = args.Select(a => new CityConnection(a)).ToList();
        }

        public MaximalTourism(int [][] args)
        {
            cityConnections = args.Select(a => new CityConnection(a));
        }

        public int FindMaximumConnectedCities()
        {
            var workingSet = cityConnections
                .GroupBy(cc => cc.First)
                .ToDictionary(gcc => gcc.Key, gcc => new WorkingSetItem()
                {
                    ConnectedCities = gcc.Select(s => s.Second).ToList()
                });

            foreach (var reversedGroup in cityConnections.GroupBy(cc => cc.Second))
            {
                if (workingSet.ContainsKey(reversedGroup.Key))
                {
                    workingSet[reversedGroup.Key].ConnectedCities.AddRange(reversedGroup.Select(sg => sg.First));
                }
                else
                {
                    workingSet[reversedGroup.Key] = new WorkingSetItem()
                    {
                        ConnectedCities = reversedGroup.Select(sg => sg.First).ToList()
                    };
                }
            }

            ResetGroupId();

            foreach (var item in workingSet)
            {
                if (HasBeenProcessed(item.Value))
                {
                    continue;
                }

                var groupId = FetchNewGroupId();

                var processingStack = new Stack<int>();
                processingStack.Push(item.Key);

                while (processingStack.Any())
                {
                    var cityIndex = processingStack.Pop();
                    var relatedItem = workingSet[cityIndex];

                    if (HasBeenProcessed(relatedItem))
                    {
                        continue;
                    }

                    relatedItem.GroupId = groupId;

                    foreach (var c1 in relatedItem.ConnectedCities)
                    {
                        processingStack.Push(c1);
                    }
                }
            }

            var maxConnections = workingSet
                .GroupBy(si => si.Value.GroupId, si => si.Key)
                .Max(gsi => gsi.Count());

            return maxConnections;
        }

        private void ResetGroupId()
        {
            groupId = 0;
        }

        private int FetchNewGroupId()
        {
            return ++groupId;
        }

        private bool HasBeenProcessed(WorkingSetItem city)
        {
            return city.GroupId != default;
        }

        private class CityConnection
        {
            public int First { get; set; }
            public int Second { get; set; }

            public CityConnection(string line)
            {
                var tokens = line.Split(' ');
                First = Int32.Parse(tokens[0]);
                Second = Int32.Parse(tokens[1]);

                if (First > Second)
                {
                    var tmp = First;
                    First = Second;
                    Second = tmp;
                }
            }

            public CityConnection(Tuple<int, int> arg)
            {
                if (arg.Item1 < arg.Item2)
                {
                    First = arg.Item1;
                    Second = arg.Item2;
                }
                else
                {
                    First = arg.Item2;
                    Second = arg.Item1;
                }
            }

            public CityConnection(int[] arg)
            {
                First = arg[0];
                Second = arg[1];
            }
        }


        private class WorkingSetItem
        {
            public List<int> ConnectedCities;
            public int GroupId;
        }

    }
}
