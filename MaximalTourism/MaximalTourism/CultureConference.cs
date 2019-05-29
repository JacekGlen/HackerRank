using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MaximalTourism
{
    public static class CultureConference
    {
        public static int getMinimumEmployees(int[][] e)
        {
            //0 - do nothing
            //1 - optional
            //2 - mandatory
            var conferenceSchedule = new int[e.Length];

            for (int idx = e.Length - 1; idx >= 0; --idx)
            {
                var parentIdx = ToIndex(e[idx][0]);

                //node healthy - check out with parent and skip
                if (e[idx][1] == 1)
                {
                    if (parentIdx != -1 && conferenceSchedule[idx] == 2 && conferenceSchedule[parentIdx] == 0)
                    {
                        conferenceSchedule[parentIdx] = 1;
                    }

                    continue;
                }

                //if parent CEO
                if (e[idx][0] == 0)
                {
                    if (conferenceSchedule[idx] == 0)
                    {
                        conferenceSchedule[idx] = 2;
                    }

                    continue;
                }

                ////parent healthy (or CEO)
                //if (e[idx][0] == 0 || e[parentIdx][1] == 1)
                //{
                //    //mark current as mandatory, only if not marked as optional earlier
                //    if (conferenceSchedule[idx] == 0)
                //    {
                //        conferenceSchedule[idx] = 2;
                //    }

                //}
                //else
                //parent sick

                if (conferenceSchedule[parentIdx] == 2)
                {
                    continue;
                }

                if (conferenceSchedule[idx] == 1)
                {
                    continue;
                }

                //already marked as mandatory, then parent optional
                if (conferenceSchedule[idx] == 2)
                {
                    if (conferenceSchedule[parentIdx] == 0)
                    {
                        conferenceSchedule[parentIdx] = 1;
                    }
                    continue;
                }


                //not mandatory, then set to optional but parent mandatory
                {
                    conferenceSchedule[idx] = 1;
                    conferenceSchedule[parentIdx] = 2;
                }
            }

            return conferenceSchedule.Count(s => s == 2);
        }

        public static int ToIndex(int itemNumber)
        {
            return itemNumber - 1;
        }

    }
}
