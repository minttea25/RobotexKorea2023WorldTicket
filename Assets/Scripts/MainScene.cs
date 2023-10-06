using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Core;
using WorldTicket.Data;
using WorldTicket.Utils;

namespace WorldTicket
{
    public class MainScene : BaseScene
    {
        [SerializeField]
        MainUI ui;

        public CardPanel RobotSumo1kgPanel;

        public CardPanel RobotSumo3kgPanel;

        List<int> robotSumo1kgList;
        List<int> robotSumo3kgList;

        protected override void Init()
        {
            base.Init();
            ManagerCore.UI.SetSceneUI(ui);
        }

        private void Start()
        {
            if (ui == null) ui = FindObjectOfType<MainUI>();

        }

        public void Load()
        {
            Draw();

            RobotSumo1kgPanel.SetTickets(robotSumo1kgList, ManagerCore.Data.TicketConfig.GetTicket(Sports.LEGO_ROBOT_SUMO_1KG), Sports.LEGO_ROBOT_SUMO_1KG);
            RobotSumo3kgPanel.SetTickets(robotSumo3kgList, ManagerCore.Data.TicketConfig.GetTicket(Sports.LEGO_ROBOT_SUMO_3KG), Sports.LEGO_ROBOT_SUMO_3KG);

        }


        void Draw()
        {
            var selected1 = Kuji.GetRandoms(
                ManagerCore.Data.SportsTeams[Sports.LEGO_ROBOT_SUMO_1KG],
                ManagerCore.Data.TicketConfig.Sum_LEGO_ROBOT_SUMO_1KG);

            robotSumo1kgList = new();

            foreach (var t in selected1)
            {
                //Debug.Log($"{t}");
                robotSumo1kgList.Add(t.Index);
            }

            //for(int i=0; i< ManagerCore.Data.TicketConfig.GetTicket(Sports.LEGO_ROBOT_SUMO_1KG); ++i) robotSumo1kgList.Add(selected1[i].Index);

            var selected3 = Kuji.GetRandoms(
                ManagerCore.Data.SportsTeams[Sports.LEGO_ROBOT_SUMO_3KG],
                ManagerCore.Data.TicketConfig.Sum_LEGO_ROBOT_SUMO_3KG);

            robotSumo3kgList = new();

            foreach (var t in selected3)
            {
                //Debug.Log($"{t}");
                robotSumo3kgList.Add(t.Index);
            }

            //for (int i = 0; i < ManagerCore.Data.TicketConfig.GetTicket(Sports.LEGO_ROBOT_SUMO_3KG); ++i) robotSumo3kgList.Add(selected3[i].Index);


            Dictionary<Sports, List<TeamData>> list = new();
            list.Add(Sports.LEGO_ROBOT_SUMO_1KG, selected1);
            list.Add(Sports.LEGO_ROBOT_SUMO_3KG, selected3);

            ExcelWriter.WriteData(list);
        }
    }
}
