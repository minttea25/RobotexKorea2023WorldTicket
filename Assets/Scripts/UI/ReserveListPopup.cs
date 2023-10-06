using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Core;

namespace WorldTicket
{
    [System.Serializable]
    class ReserveListPopupUIContext : UIContext
    {
        public Property BackgroundBlur_Panel = new();
        public Property<string> TitleText = new();

        public Property NameGrids = new();
    }

    public class ReserveListPopup : BaseReusableUIPopup
    {
        [SerializeField]
        ReserveListPopupUIContext Context = new();

        public override void Init()
        {
            base.Init();

            BindEventUnityAction(ClosePopup);
        }

        public void SetBase(string title, List<int> list)
        {
            Context.TitleText.Value = title;

            var parent = Context.NameGrids.BindObject.transform;
            ManagerCore.Resource.DestroyAllItems(parent);

            for (int i = 0; i < list.Count; ++i)
            {
                var item = ManagerCore.UI.AddItemUI<GridNameItem>(AddrKeys.GridNameItem, parent);
                var team = ManagerCore.Data.Teams[list[i]];
                item.InitBase(i + 1, string.Format(Const.TeamNumberNameFormat, team.TeamNo, team.TeamName));
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
