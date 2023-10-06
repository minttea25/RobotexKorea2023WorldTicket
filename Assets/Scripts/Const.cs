using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldTicket
{
    public class Const
    {
        public const string TeamNumberNameFormat = "[{0}] {1}";

        public readonly static Color ReserveColor1 = new(0.5f, 0f, 0.3f);
        public readonly static Color ReserveColor2 = new(0.5f, 0f, 0.3f);
        public readonly static Color ReserveColor3 = new(0.5f, 0f, 0.3f);
        public readonly static Color ReserveColorDefault = new(0, 0, 0, 0); // transparent

        public const string RobotSumo1kgReserveTitle = "Robot Sumo 1kg [¿¹ºñ Âü°¡ÆÀ]";
        public const string RobotSumo3kgReserveTitle = "Robot Sumo 3kg [¿¹ºñ Âü°¡ÆÀ]";

        public const string TITLE_RobotSumo1kgTicket = "Robot Sumo 1kg World Tickets";
        public const string TITLE_RobotSumo3kgTicket = "Robot Sumo 3kg World Tickets";
    }
}
