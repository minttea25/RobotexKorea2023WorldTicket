using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Core;

namespace WorldTicket
{
    [System.Serializable]
    class GridNameItemUIContext : UIContext
    {
        public Property<string> NumberText = new();
        public Property<string> NameText = new();
    }

    public class GridNameItem : BaseUIItem
    {
        [SerializeField]
        GridNameItemUIContext Context = new();

        public void InitBase(int number, string text)
        {
            Context.NumberText.Value = number.ToString();
            Context.NameText.Value = text;

            if (number == 1) gameObject.GetComponent<Image>().color = Const.ReserveColor1;
            else if (number == 2) gameObject.GetComponent<Image>().color = Const.ReserveColor2;
            else if (number == 3) gameObject.GetComponent<Image>().color = Const.ReserveColor3;
            else gameObject.GetComponent<Image>().color = Const.ReserveColorDefault;
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
