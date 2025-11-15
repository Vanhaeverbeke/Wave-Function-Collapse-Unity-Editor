using Editor.CommonFunctions.Layout;
using FinalStateMachine.FSM;
using FinalStateMachine.States;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEditor;

namespace WFCEditor
{
    public partial class WFCEditor
    {
        public class WFCEditorStateBaseClass : StateBaseClass
        {
            private new WFCEditorFSM _fsm { get { return base._fsm as WFCEditorFSM; } }

            private WFCEditor _context { get { return _fsm.Context; } }

            public WFCEditorStateBaseClass(FSMBaseClass fsm) : base(fsm)
            {
            }

            public override void Initialize()
            {

            }

            public override void OnEnter()
            {
                _context.PropertyChanged += PropertyHasChanged;
            }

            public override void OnExit()
            {
                _context.PropertyChanged -= PropertyHasChanged;
            }

            public override void Update()
            {
                DisplayTitle();
                DisplayTabs();
            }

            public override void PropertyHasChanged(object sender, PropertyChangedEventArgs eventArgs)
            {
                if(eventArgs.PropertyName.Equals(nameof(_context.CurrentTab)))
                {
                    ChangeState();
                }
            }

            private void ChangeState()
            {
                if (_context._INTEGERBYTABNAMES[_context._TABNAMES[0]] == _context.CurrentTab)
                {
                    _fsm.TransisionTo(_fsm.PresetEditorState);
                }
                else if (_context._INTEGERBYTABNAMES[_context._TABNAMES[1]] == _context.CurrentTab)
                {
                    _fsm.TransisionTo(_fsm.PresetNodesState);
                }
                else if (_context._INTEGERBYTABNAMES[_context._TABNAMES[2]] == _context.CurrentTab)
                {
                    _fsm.TransisionTo(_fsm.NodesEditorState);
                }
                else if (_context._INTEGERBYTABNAMES[_context._TABNAMES[3]] == _context.CurrentTab)
                {
                    _fsm.TransisionTo(_fsm.NodesBuilderState);
                }
            }

            private void DisplayTabs()
            {
                const int SPACE = 10;

                _context.CurrentTab = EditorGUILayoutCommonFunctions.ToolBarTabs(SPACE, _context.CurrentTab, _context._TABNAMES);
            }

            private void DisplayTitle()
            {
                const int SPACE = 10;
                const string LABEL = "WFC Editor";

                EditorGUILayoutCommonFunctions.DrawCentralLabel(LABEL, SPACE, EditorStyles.boldLabel);
            }

        }
    }

}

