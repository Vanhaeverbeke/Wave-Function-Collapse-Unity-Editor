using Editor.CommonFunctions.AssetDataBase;
using Editor.CommonFunctions.Layout;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

namespace WFCEditor
{
    public partial class WFCEditor : EditorWindow, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly string[] _TABNAMES =
            new string[]
            {
                "Preset Editor",
                "Preset Nodes",
                "Nodes Editor",
                "Nodes Builder"
            };
        private readonly Dictionary<string, int> _INTEGERBYTABNAMES =
        new Dictionary<string, int>()
            {
                {"Preset Editor", 0 },
                { "Preset Nodes", 1 },
                { "Nodes Editor", 2 },
                { "Nodes Builder", 3 }
            };

        private int _currentTab;
        public int CurrentTab
        {
            get { return _currentTab; }

            set
            {
                if (value == _currentTab)
                {
                    return;
                }

                _currentTab = value;
                OnPropertyChanged();
            }
        }
        private const string _ASSETPATH = "Assets";
        private const string _WFCFOLDERNAME = "WFC";
        private const string _WFCFOLDERPATH = _ASSETPATH + "/" + _WFCFOLDERNAME ;

        private WFCEditorFSM _fsm;

        [MenuItem("Editors/WFCEditor")]
        private static void ShowWindow()
        {
            const float WINDOWWIDTH = 425.0f;
            const float WINDOWHEIGHT = 800.0f;
            const float OFFSET = 0.1f;

            WFCEditor window = (WFCEditor)GetWindow(typeof(WFCEditor));
            window.minSize = new Vector2(WINDOWWIDTH, WINDOWHEIGHT);
            window.maxSize = new Vector2(WINDOWWIDTH + OFFSET, WINDOWHEIGHT + OFFSET);
        }

        private void OnEnable()
        {
            CreateDataFolder();

            _fsm = new WFCEditorFSM(this);
            _fsm.Initialize();
        }
        private void OnGUI()
        {
            _fsm.Currentstate.Update();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = " ")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CreateDataFolder()
        {
            AssetDataBaseCommonFunctions.CreateFolderOnce(_ASSETPATH , _WFCFOLDERNAME);
        }
    }
}

