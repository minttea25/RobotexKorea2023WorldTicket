using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldTicket.Data
{
    public class ReadConfig
    {
        public string SheetName;

        public int NumberOfTeams;

        public int OrganizationColumnIndex = 1;
        public int SportsColumnIndex = 2;
        public int TeamNumberColumnIndex = 3;
        public int TeamNameColumnIndex = 4;

        public string Sport_LEGO_ROBOT_SUMO_1KG;
        public string Sport_LEGO_ROBOT_SUMO_3KG;
        public string Sport_LEGO_LINE_FOLLOWING_E;
        public string Sport_LEGO_LINE_FOLLOWING_MH;
        public string Sport_LEGO_FOLKRACE_E;
        public string Sport_LEGO_FOLKRACE_MH;
        public string Sport_LEGO_SHOTPUT_E;
        public string Sport_LEGO_SHOTPUT_MH;
        public string Sport_LEGO_FOOTBALL;
        public string Sport_OPEN_FOLKRACE_MH;

        public override string ToString()
        {
            return $"{SheetName}, {NumberOfTeams}";
        }
    }
}
