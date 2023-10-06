using UnityEngine;

namespace WorldTicket.Utils
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get { Init(); return _instance; } }

        private static T _instance;
        private static void Init()
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject go = new() { name = typeof(T).Name };
                    _instance = go.AddComponent<T>();

                    // Even if a new scene loaded, gameobject in 'DontDestroyOnLoad' is not destroyed
                    // Always accessable through Instance
                    DontDestroyOnLoad(go);
                }
            }
        }
    }
}