using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using Core;
using WorldTicket.Data;

namespace WorldTicket
{
    [System.Serializable]
    class CardPanelContext : UIContext
    {
        public Property<string> TitleText = new();
        public Property<int, Spinner> Spinner = new();

        public Property<UnityAction> PushButton = new();
        public Property<string> LeftTicketText = new();

        public Property NameList = new();
        public Property<UnityAction> ReserveListButton = new();
    }

    public class CardPanel : BaseUIItem
    {
        [SerializeField]
        CardPanelContext Context = new();

        ReserveListPopup reservePopup;

        int reserveTickets;
        int leftTickets = 0;
        List<int> list;

        bool drawSwitch = false;

        Sports sports;

        public override void Init()
        {
            base.Init();

            RemoveDynamicEventHandler();

            ManagerCore.Resource.DestroyAllItems(Context.NameList.BindObject.transform);

            Context.PushButton.Value += OnPushButtonClicked;
            Context.ReserveListButton.Value += OnReserveListButtonClicked;
        }

        public void SetTickets(List<int> selectedList, int tickets, Sports sports)
        {
            if (leftTickets != 0) return;

            this.sports = sports;
            
            leftTickets = tickets;
            list = selectedList;
            reserveTickets = list.Count - tickets;

            if (sports == Sports.LEGO_ROBOT_SUMO_1KG)
            {
                Context.TitleText.Value = Const.TITLE_RobotSumo1kgTicket;
            }
            else if (sports == Sports.LEGO_ROBOT_SUMO_3KG)
            {
                Context.TitleText.Value = Const.TITLE_RobotSumo3kgTicket;
            }

            UpdateLeftTicketUI();
        }

        void OnPushButtonClicked()
        {
            if (drawSwitch == false) DrawTeam();
            else
            {
                Context.Spinner.Component.StopSpin();
                ActivePushButton(false);
            }
            drawSwitch = !drawSwitch;
        }

        public void DrawTeam()
        {
            if (leftTickets == 0) return;

            Context.Spinner.Component.StartSpin(ManagerCore.Data.Teams[list[leftTickets - 1]].TeamNo, list[leftTickets - 1]);
            leftTickets--;
            UpdateLeftTicketUI();
        }

        void UpdateLeftTicketUI()
        {
            Context.LeftTicketText.Value = leftTickets.ToString();
        }

        public void AddTeam(TeamData team)
        {
            var parent = Context.NameList.BindObject.transform;
            var item = ManagerCore.UI.AddItemUI<NameListItem>(AddrKeys.NameListItem, parent);
            item.SetText(string.Format(Const.TeamNumberNameFormat, team.TeamNo, team.TeamName));
        }

        public void ActivePushButton(bool active)
        {
            Context.PushButton.BindObject.GetComponent<Button>().interactable = active;
        }

        void OnReserveListButtonClicked()
        {
            if (list == null || list.Count == 0) return;

            if (reservePopup == null)
            {
                List<int> t = new();
                for (int i = list.Count - reserveTickets; i < list.Count; ++i)
                {
                    t.Add(list[i]);
                }
                reservePopup = ManagerCore.UI.ShowPopupUI<ReserveListPopup>(AddrKeys.ReserveListPopup);
                reservePopup.SetBase(sports == Sports.LEGO_ROBOT_SUMO_1KG ? Const.RobotSumo1kgReserveTitle : Const.RobotSumo3kgReserveTitle, t);
            }
            else
            {
                reservePopup.OpenPopup();
            }
        }

        #region BaseUI Editor 
#if UNITY_EDITOR
        protected override List<string> GetContextFieldsNames()
        {
            List<string> list = new();
            foreach (var name in Context.GetType().GetFields())
            {
                list.Add(name.Name);
            }
            return list;
        }

        protected override object GetContextFieldValue(string fieldName)
        {
            return Context.GetType().GetField(fieldName).GetValue(Context);
        }
#endif
        #endregion
    }
}
