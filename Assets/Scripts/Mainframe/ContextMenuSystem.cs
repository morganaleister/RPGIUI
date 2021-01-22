using UnityEngine;

namespace Scripts.Mainframe
{
    class ContextMenuSystem : MonoBehaviour
    {
        public static ContextMenuSystem Singleton { get; set; }

        ContextMenuSystem()
        {
            if (!Singleton) Singleton = this;
            else Destroy(gameObject);


        }


    }
    public class ContextMenuItems : MonoBehaviour
    {

    }
}
