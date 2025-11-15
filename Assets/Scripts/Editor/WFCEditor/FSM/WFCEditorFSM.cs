using FinalStateMachine.FSM;

namespace WfcEditor
{
    public partial class WFCEditor
    {
        public class WFCEditorFSM : FSMBaseClass
        {
            private WFCEditor _context;

            public WFCEditor Context
            {
                get { return _context; }
                private set
                {
                    if (_context == value || value == null)
                    {
                        return;
                    }

                    _context = value;
                }
            }

            public WFCEditorStateBaseClass PresetEditorState;

            public WFCEditorStateBaseClass PresetNodesState;

            public WFCEditorStateBaseClass NodesEditorState;

            public WFCEditorStateBaseClass NodesBuilderState;

            public WFCEditorFSM(WFCEditor context)
            {
                Context = context;



                TransisionTo(PresetEditorState);
            }
        }
    }
}

