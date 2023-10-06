using UnityEngine;
using UnityEngine.EventSystems;

namespace Core
{
    public abstract class BaseScene : MonoBehaviour
    {
        public SceneType Type { get; protected set; } = SceneType.INVALID;
        protected bool _init = false;



        private void Awake()
        {
            Init();
            _init = true;
        }

        /// <summary>
        /// It is called at Awake.
        /// </summary>
        protected virtual void Init()
        {
            ManagerCore.Init();

            UnityEngine.Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
            if (obj == null)
            {
                ManagerCore.Resource.Instantiate(Const.AddressableEventSystemKey);
                obj.name = Const.EventSystemsName;
                // Debug.LogWarning($"Can not find object [type=EventSystem]");
            }
        }

        public virtual void Clear() { }
    }
}
