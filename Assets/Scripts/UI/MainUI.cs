using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using Core;

namespace WorldTicket
{
    [System.Serializable]
    class MainUIContext : UIContext
    {
        public Property MainPanel = new();
        public Property<UnityAction> RobotSumo1kgButton = new();
        public Property<UnityAction> RobotSumo3kgButton = new();
    }

    public class MainUI : BaseUIScene
    {
        [SerializeField]
        MainUIContext Context = new();

        public CardPanel RobotSumo1kgPanel;
        public CardPanel RobotSumo3kgPanel;

        int selected = 0;

        public CardPanel SelectedCardPanel()
        {
            if (selected == 1) return RobotSumo1kgPanel;
            else if (selected == 3) return RobotSumo3kgPanel;
            return null;
        }

        public override void Init()
        {
            base.Init();

            RobotSumo1kgPanel = GameObject.Find("RobotSumo1kgPanel").GetComponent<CardPanel>();
            RobotSumo3kgPanel = GameObject.Find("RobotSumo3kgPanel").GetComponent<CardPanel>();

            RobotSumo1kgPanel.gameObject.SetActive(false);
            RobotSumo3kgPanel.gameObject.SetActive(false);

            Context.RobotSumo1kgButton.Value += OnButton1Clicked;
            Context.RobotSumo3kgButton.Value += OnButton3Clicked;
        }

        public void OnButton1Clicked()
        {
            RobotSumo3kgPanel.gameObject.SetActive(false);
            RobotSumo1kgPanel.gameObject.SetActive(true);

            selected = 1;
        }

        public void OnButton3Clicked()
        {
            RobotSumo1kgPanel.gameObject.SetActive(false);
            RobotSumo3kgPanel.gameObject.SetActive(true);

            selected = 3;
        }

        public void ActiveAllButtons(bool active)
        {
            ActiveButton1(active);
            ActiveButton2(active);
        }

        public void ActiveButton1(bool active)
        {
            Context.RobotSumo1kgButton.BindObject.GetComponent<Button>().interactable = active;
        }

        public void ActiveButton2(bool active)
        {
            Context.RobotSumo3kgButton.BindObject.GetComponent<Button>().interactable = active;
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
