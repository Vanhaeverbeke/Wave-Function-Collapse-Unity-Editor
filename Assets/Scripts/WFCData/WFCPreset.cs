using UnityEngine;

namespace WFCEditor.Data
{
    [CreateAssetMenu ( fileName = "Data", menuName = "WFCPreset")]
    public class WFCPreset : ScriptableObject
    {
        [HideInInspector]
        public int GridWidth;
        [HideInInspector]
        public int GridHeight;

        [HideInInspector]
        public Vector3 GenerationPosition;
        [HideInInspector]
        public Vector3 TileDimensions;

    }
}

