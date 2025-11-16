using UnityEngine;

namespace WFCEditor.Data
{
    public class WFCNode : ScriptableObject
    {
        [HideInInspector]
        public GameObject Prefab;

        [HideInInspector]
        public string RightState;
        [HideInInspector]
        public string LeftState;
        [HideInInspector]
        public string TopState;
        [HideInInspector]
        public string BottomState;
    }
}

