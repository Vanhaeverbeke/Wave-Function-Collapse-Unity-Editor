using FinalStateMachine.FSM;
using FinalStateMachine.States;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WfcEditor
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
                _context.DisplayTitle();
                _context.DisplayTabs();
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
        }
    }

}

