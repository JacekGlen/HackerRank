using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MaximalTourism
{
    public class MaximalTourism
    {
        IEnumerable<CityConnection> cityConnections;

        public MaximalTourism(string[] args)
        {
            cityConnections = args.Select(a => new CityConnection(a));
        }

        public MaximalTourism(IEnumerable<Tuple<int, int>> args)
        {
            cityConnections = args.Select(a => new CityConnection(a)).ToList();
        }

        public int FindMaximumConnectedCities()
        {
            var grouped = cityConnections
                .GroupBy(cc => cc.First)
                .OrderBy(gcc => gcc.Key)
                .Select(gcc => new CityGroup()
                {
                    Key = gcc.Key,
                    Connected = gcc.Select(cc => cc.Second).ToArray(),
                    Processed = false,
                })
                .ToDictionary(gcc => gcc.Key, gcc => gcc);


            var workingSet = cityConnections
                .GroupBy(cc => cc.First)
                .ToDictionary(gcc => gcc.Key, gcc => new CityToProcess()
                {
                    ConnectedCities = gcc.Select(s => s.Second).ToList()
                });

            foreach (var secondGroup in cityConnections.GroupBy(cc => cc.Second))
            {
                if (workingSet.ContainsKey(secondGroup.Key))
                {
                    workingSet[secondGroup.Key].ConnectedCities.AddRange(secondGroup.Select(sg => sg.First));
                }
                else
                {
                    workingSet[secondGroup.Key] = new CityToProcess()
                    {
                        ConnectedCities = secondGroup.Select(sg => sg.First).ToList()
                    };
                }
            }



            var connectedGroups = new List<int[]>();

            foreach (var g in grouped)
            {
                if (g.Value.Processed)
                {
                    continue;
                }

                var processingStack = new Stack<int>();
                var connectedCities = new List<int>();

                processingStack.Push(g.Key);
                connectedCities.Add(g.Key);

                while (processingStack.Any())
                {
                    var currentCity = processingStack.Pop();

                    if (grouped.ContainsKey(currentCity))
                    {
                        var currentGroup = grouped[currentCity];
                        if (!currentGroup.Processed)
                        {
                            currentGroup.Processed = true;
                            connectedCities.AddRange(currentGroup.Connected);
                            foreach (var c in currentGroup.Connected)
                            {
                                processingStack.Push(c);
                            }
                        }
                    }
                }

                connectedGroups.Add(connectedCities.Distinct().ToArray());
            }

            return connectedGroups.Select(cg => cg.Length).Max();
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
        }



        private class CityGroup
        {
            public int Key { get; set; }
            public int[] Connected { get; set; }
            public bool Processed { get; set; }
        }

        private class CityToProcess
        {
            public List<int> ConnectedCities;
            public int GroupId;
        }

        public struct CityToProcessStruct
        {
            public int[] ConnectedCities;
            public int GroupId;
        }
    }
}
