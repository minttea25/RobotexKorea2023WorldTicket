using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldTicket.Data
{
    public class ExcelData
    {
        public static Dictionary<string, Sports> GetSportNameDictionary(ReadConfig config)
        {
            Dictionary<string, Sports> dict = new();

            dict.Add(config.Sport_LEGO_ROBOT_SUMO_1KG, Sports.LEGO_ROBOT_SUMO_1KG);
            dict.Add(config.Sport_LEGO_ROBOT_SUMO_3KG, Sports.LEGO_ROBOT_SUMO_3KG);
            dict.Add(config.Sport_LEGO_LINE_FOLLOWING_E, Sports.LEGO_LINE_FOLLOWING_E);
            dict.Add(config.Sport_LEGO_LINE_FOLLOWING_MH, Sports.LEGO_LINE_FOLLOWING_MH);
            dict.Add(config.Sport_LEGO_FOLKRACE_E, Sports.LEGO_FOLKRACE_E);
            dict.Add(config.Sport_LEGO_FOLKRACE_MH, Sports.LEGO_FOLKRACE_MH);
            dict.Add(config.Sport_LEGO_SHOTPUT_E, Sports.LEGO_SHOTPUT_E);
            dict.Add(config.Sport_LEGO_SHOTPUT_MH, Sports.LEGO_SHOTPUT_MH);
            dict.Add(config.Sport_LEGO_FOOTBALL, Sports.LEGO_FOOTBALL);
            dict.Add(config.Sport_OPEN_FOLKRACE_MH, Sports.OPEN_FOLKRACE_MH);

            return dict;
        }
    }

    public interface IData
    {

    }

    public enum Sports
    {
        LEGO_ROBOT_SUMO_1KG,
        LEGO_ROBOT_SUMO_3KG,
        LEGO_FOOTBALL,
        LEGO_SHOTPUT_E,
        LEGO_SHOTPUT_MH,
        LEGO_LINE_FOLLOWING_E,
        LEGO_LINE_FOLLOWING_MH,
        LEGO_FOLKRACE_E,
        LEGO_FOLKRACE_MH,
        OPEN_FOLKRACE_MH,
    }

    public class TeamData : IData
    {
        public int Index;
        public string Organization;
        public Sports Sport;
        public int TeamNo;
        public string TeamName;
        public override string ToString()
        {
            return $"[{Index}: {Organization}, {Sport}, {TeamNo}, {TeamName}]";
        }

    }
}
