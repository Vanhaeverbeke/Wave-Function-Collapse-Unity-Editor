using Editor.CommonFunctions.Layout;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEditor;

namespace WfcEditor
{
    public partial class WFCEditor : EditorWindow, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly string[] _TABNAMES = new string[] { "Preset Editor", "Preset Nodes", "Nodes Editor", "Nodes Builder" };
        private readonly Dictionary<string, int> _INTEGERBYTABNAMES = new Dictionary<string, int>() { {"Preset Editor", 0 }, { "Preset Nodes", 1 }, { "Nodes Editor", 2 }, { "Nodes Builder", 3 } };

        private int _currentTab;
        public int CurrentTab
        {
            get { return _currentTab; }

            set 
            { 
                if(value == _currentTab)
                {
                    return;
                }

                _currentTab = value;
                OnPropertyChanged();
            }
        }

        private WFCEditorFSM _fsm;

        [MenuItem("Editors/WFCEditor")]
        private static void ShowWindow()
        {
            WFCEditor window = (WFCEditor)GetWindow(typeof(WFCEditor));

        }

        private void OnEnable()
        {
            _fsm = new WFCEditorFSM(this);
        }
        private void OnGUI()
        {
            _fsm.Currentstate.Update();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = " ")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region StateMachine Functions
        private void DisplayTabs()
        {
            const int SPACE = 10;

            CurrentTab = EditorGUILayoutCommonFunctions.ToolBarTabs(SPACE, CurrentTab, _TABNAMES);
        }

        private void DisplayTitle()
        {
            const int SPACE = 10;
            const string LABEL = "WFC Editor";

            EditorGUILayoutCommonFunctions.DrawCentralLabel(LABEL, SPACE, EditorStyles.boldLabel);
        }

        #endregion

    }
}

