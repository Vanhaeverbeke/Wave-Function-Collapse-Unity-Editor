using System.Collections.Generic;
using UnityEngine;

namespace WFCEditor.Data
{
    public class WFCSideStates : ScriptableObject
    {
        [HideInInspector]
        public List<string> SideStates = new List<string>();
    }
}

