using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UObject = UnityEngine.Object;

namespace Core
{
    public class ResourceManagerCore : IManager
    {
        public T Load<T>(string path) where T : UObject
        {
            return Resources.Load<T>(path);
        }

        public GameObject Instantiate(string path, Transform parent = null)
        {
            GameObject original = Load<GameObject>($"Prefabs/{path}");
            if (!original)
            {
                Debug.Log($"Failed to load prefab : {path}");
                return null;
            }

            GameObject go = UObject.Instantiate(original, parent);
            go.name = original.name;

            return go;
        }

        public void Destroy(GameObject go, float time = -1)
        {
            if (go == null) return;

            if (time == -1) UObject.Destroy(go);
            else UObject.Destroy(go, time);

        }

        public void DestroyAllItems(Transform parent)
        {
            foreach(Transform item in parent)
            {
                Destroy(item.gameObject);
            }
        }

        void IManager.InitManager()
        {
        }

        void IManager.ClearManager()
        {
        }
    }
}


