using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core
{
    public class UIManagerCore : IManager
    {
        /// <summary>
        /// NOTE: The order of normal popup start at 10; You can open the normal popups up to 19.
        /// </summary>
        const int TopPopupSortingLayer = 30;

        public BaseUIPopup TopPopupUI
        {
            private set
            {
                _onTopPopup = value;
            }
            get
            {
                if (_onTopPopup != null) return _onTopPopup;
                else if (_popupStack.Count != 0) return _popupStack.Peek();
                else return null;
            }
        }

        // for main UI
        BaseUIScene _sceneUI { get; set; } = null;
        public T SceneUI<T>() where T : BaseUIScene => _sceneUI as T;

        // for popup UI
        int _order = 10;
        readonly Stack<BaseUIPopup> _popupStack = new();
        BaseUIPopup _onTopPopup = null;

        public GameObject RootObject
        {
            get
            {
                GameObject rootObject = GameObject.Find($"{Const.RootUIName}");
                if (rootObject == null)
                {
                    rootObject = new GameObject { name = $"{Const.RootUIName}" };
                }
                return rootObject;
            }
        }

        /// <summary>
        /// It should be called BaseUI Object containing Canvas
        /// </summary>
        /// <param name="canvasUI">Root object with Canvas component</param>
        /// <param name="sort">Does it need to be sorting-layer-displayed? (default = false)</param>
        public void SetCanvas(GameObject canvasUI, bool sort = false)
        {
            Canvas canvas = canvasUI.GetOrAddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.overrideSorting = true;

            if (sort == true)
            {
                canvas.sortingOrder = _order;
                _order++;
            }
            else
            {
                canvas.sortingOrder = 0;
            }
        }

        public void SetSceneUI<T>(T sceneUI) where T : BaseUIScene
        {
            _sceneUI = sceneUI;
        }

        public T ShowPopupUI<T>(string key, bool alwaysShowOnTop = false) where T : BaseUIPopup
        {
            var ui = ManagerCore.Resource.Instantiate(key);
            if (ui == null) return null;
            T popupUI = ui.GetOrAddComponent<T>();
            if (alwaysShowOnTop == true)
            {
                _onTopPopup = popupUI;
                popupUI.gameObject.GetOrAddComponent<Canvas>().sortingOrder = TopPopupSortingLayer;
            }
            else
            {
                _popupStack.Push(popupUI);
            }

            //popupUI.transform.SetParent(RootObject.transform);
            popupUI.Show();

            return popupUI;
        }

        public T ShowSceneUI<T>(string key) where T : BaseUIScene
        {
            var ui = ManagerCore.Resource.Instantiate(key);
            if (ui == null) return null;
            T sceneUI = ui.GetOrAddComponent<T>();
            _sceneUI = sceneUI;

            return sceneUI;
        }

        public T ShowUI<T>(string key, int sortingOrder) where T : BaseUI
        {
            var go = ManagerCore.Resource.Instantiate(key);
            if (go == null) return null;
            T ui = go.GetOrAddComponent<T>();
            ui.GetComponent<Canvas>().sortingOrder = sortingOrder;

            return ui;
        }

        public T AddItemUI<T>(string key, Transform parent) where T : BaseUIItem
        {
            var ui = ManagerCore.Resource.Instantiate(key, parent);

            if (ui == null) return null;
            T itemUI = ui.GetOrAddComponent<T>();

            return itemUI;
        }

        public void CloseTopPopupUI()
        {
            if (_onTopPopup != null)
            {
                ManagerCore.Resource.Destroy(TopPopupUI.gameObject);
                TopPopupUI = null;
                return;
            }
            else
            {
                ClosePopupUI();
            }
        }

        public void ClosePopupUI(BaseUIPopup popup)
        {

            if (_popupStack.Count == 0) return;

            // assertion crash
            if (_popupStack.Peek() != popup)
            {
                Debug.LogError($"Close popup UI failed", _popupStack.Peek());
                return;
            }

            ClosePopupUI();
        }

        public void ClosePopupUI()
        {
            if (_popupStack.Count == 0) return;

            BaseUIPopup popup = _popupStack.Pop();
            ManagerCore.Resource.Destroy(popup.gameObject);
            popup = null;

            _order--;
        }

        public void CloseAllPopupUI()
        {
            while(_popupStack.Count > 0)
            {
                ClosePopupUI();
            }
        }

        void IManager.InitManager()
        {
        }

        void IManager.ClearManager()
        {
            ClosePopupUI();

            if (_sceneUI != null)
            {
                ManagerCore.Resource.Destroy(_sceneUI.gameObject);
            }
            
            _sceneUI = null;
        }
    }
}
