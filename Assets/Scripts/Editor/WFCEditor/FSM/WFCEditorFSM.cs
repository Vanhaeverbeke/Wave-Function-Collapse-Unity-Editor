using FinalStateMachine.FSM;

namespace WFCEditor
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

                PresetEditorState = new PresetEditorState(this);
                PresetNodesState = new PresetNodesState(this);
                NodesEditorState = new NodesEditorState(this);
                NodesBuilderState = new NodesBuilderState(this);

                TransisionTo(PresetEditorState);
            }

            public override void Initialize()
            {
                base.Initialize();

                PresetEditorState.Initialize();
                PresetNodesState.Initialize();
                NodesEditorState.Initialize();
                NodesBuilderState.Initialize();
            }
        }
    }
}

