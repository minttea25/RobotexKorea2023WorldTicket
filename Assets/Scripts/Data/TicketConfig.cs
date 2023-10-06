using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldTicket.Data
{
    public class TicketConfig
    {
        public int Sum_LEGO_ROBOT_SUMO_1KG => LEGO_ROBOT_SUMO_1KG + LEGO_ROBOT_SUMO_1KG_RESERVE;
        public int Sum_LEGO_ROBOT_SUMO_3KG => LEGO_ROBOT_SUMO_3KG + LEGO_ROBOT_SUMO_3KG_RESERVE;
        public int Sum_LEGO_LINE_FOLLOWING_E => LEGO_LINE_FOLLOWING_E + LEGO_LINE_FOLLOWING_E_RESERVE;
        public int Sum_LEGO_LINE_FOLLOWING_MH => LEGO_LINE_FOLLOWING_MH + LEGO_LINE_FOLLOWING_MH_RESERVE;
        public int Sum_LEGO_FOLKRACE_E => LEGO_FOLKRACE_E + LEGO_FOLKRACE_E_RESERVE;
        public int Sum_LEGO_FOLKRACE_MH => LEGO_FOLKRACE_MH + LEGO_FOLKRACE_MH_RESERVE;
        public int Sum_LEGO_SHOTPUT_E => LEGO_SHOTPUT_E + LEGO_SHOTPUT_E_RESERVE;
        public int Sum_LEGO_SHOTPUT_MH => LEGO_SHOTPUT_MH + LEGO_SHOTPUT_MH_RESERVE;
        public int Sum_LEGO_FOOTBALL => LEGO_FOOTBALL + LEGO_FOOTBALL_RESERVE;
        public int Sum_OPEN_FOLKRACE_MH => OPEN_FOLKRACE_MH + OPEN_FOLKRACE_MH_RESERVE;

        public int GetSum(Sports sport) => sport switch
        {
            Sports.LEGO_ROBOT_SUMO_1KG => Sum_LEGO_ROBOT_SUMO_1KG,
            Sports.LEGO_ROBOT_SUMO_3KG => Sum_LEGO_ROBOT_SUMO_3KG,
            Sports.LEGO_FOOTBALL => Sum_LEGO_FOOTBALL,
            Sports.LEGO_SHOTPUT_E => Sum_LEGO_SHOTPUT_E,
            Sports.LEGO_SHOTPUT_MH => Sum_LEGO_SHOTPUT_MH,
            Sports.LEGO_LINE_FOLLOWING_E => Sum_LEGO_LINE_FOLLOWING_E,
            Sports.LEGO_LINE_FOLLOWING_MH => Sum_LEGO_LINE_FOLLOWING_MH,
            Sports.LEGO_FOLKRACE_E => Sum_LEGO_FOLKRACE_E,
            Sports.LEGO_FOLKRACE_MH => Sum_LEGO_FOLKRACE_MH,
            Sports.OPEN_FOLKRACE_MH => Sum_OPEN_FOLKRACE_MH,
        };

        public int GetTicket(Sports sport) => sport switch
        {
            Sports.LEGO_ROBOT_SUMO_1KG => LEGO_ROBOT_SUMO_1KG,
            Sports.LEGO_ROBOT_SUMO_3KG => LEGO_ROBOT_SUMO_3KG,
            Sports.LEGO_FOOTBALL => LEGO_FOOTBALL,
            Sports.LEGO_SHOTPUT_E => LEGO_SHOTPUT_E,
            Sports.LEGO_SHOTPUT_MH => LEGO_SHOTPUT_MH,
            Sports.LEGO_LINE_FOLLOWING_E => LEGO_LINE_FOLLOWING_E,
            Sports.LEGO_LINE_FOLLOWING_MH => LEGO_LINE_FOLLOWING_MH,
            Sports.LEGO_FOLKRACE_E => LEGO_FOLKRACE_E,
            Sports.LEGO_FOLKRACE_MH => LEGO_FOLKRACE_MH,
            Sports.OPEN_FOLKRACE_MH => OPEN_FOLKRACE_MH,
        };

        public int GetReserve(Sports sport) => sport switch
        {
            Sports.LEGO_ROBOT_SUMO_1KG => LEGO_ROBOT_SUMO_1KG_RESERVE,
            Sports.LEGO_ROBOT_SUMO_3KG => LEGO_ROBOT_SUMO_3KG_RESERVE,
            Sports.LEGO_FOOTBALL => LEGO_FOOTBALL_RESERVE,
            Sports.LEGO_SHOTPUT_E => LEGO_SHOTPUT_E_RESERVE,
            Sports.LEGO_SHOTPUT_MH => LEGO_SHOTPUT_MH_RESERVE,
            Sports.LEGO_LINE_FOLLOWING_E => LEGO_LINE_FOLLOWING_E_RESERVE,
            Sports.LEGO_LINE_FOLLOWING_MH => LEGO_LINE_FOLLOWING_MH_RESERVE,
            Sports.LEGO_FOLKRACE_E => LEGO_FOLKRACE_E_RESERVE,
            Sports.LEGO_FOLKRACE_MH => LEGO_FOLKRACE_MH_RESERVE,
            Sports.OPEN_FOLKRACE_MH => OPEN_FOLKRACE_MH_RESERVE,
        };

        public int LEGO_ROBOT_SUMO_1KG = 4;
        public int LEGO_ROBOT_SUMO_3KG = 5;
        public int LEGO_LINE_FOLLOWING_E;
        public int LEGO_LINE_FOLLOWING_MH;
        public int LEGO_FOLKRACE_E;
        public int LEGO_FOLKRACE_MH;
        public int LEGO_SHOTPUT_E;
        public int LEGO_SHOTPUT_MH;
        public int LEGO_FOOTBALL;
        public int OPEN_FOLKRACE_MH;

        public int LEGO_ROBOT_SUMO_1KG_RESERVE = 8;
        public int LEGO_ROBOT_SUMO_3KG_RESERVE = 10;
        public int LEGO_LINE_FOLLOWING_E_RESERVE;
        public int LEGO_LINE_FOLLOWING_MH_RESERVE;
        public int LEGO_FOLKRACE_E_RESERVE;
        public int LEGO_FOLKRACE_MH_RESERVE;
        public int LEGO_SHOTPUT_E_RESERVE;
        public int LEGO_SHOTPUT_MH_RESERVE;
        public int LEGO_FOOTBALL_RESERVE;
        public int OPEN_FOLKRACE_MH_RESERVE;
    }
}
