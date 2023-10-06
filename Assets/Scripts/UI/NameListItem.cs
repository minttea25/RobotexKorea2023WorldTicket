using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Core;
using DG.Tweening;
using TMPro;

namespace WorldTicket
{
    [System.Serializable]
    class NameListItemUIContext : UIContext
    {
        public Property<string> NameText = new();
    }

    public class NameListItem : BaseUIItem
    {
        [SerializeField]
        NameListItemUIContext Context = new();

        const float fadeInDuration = 1.5f;

        public void SetText(string text)
        {
            Context.NameText.Value = text;
            Context.NameText.BindObject.GetComponent<TextMeshProUGUI>().DOFade(1.0f, fadeInDuration);
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
