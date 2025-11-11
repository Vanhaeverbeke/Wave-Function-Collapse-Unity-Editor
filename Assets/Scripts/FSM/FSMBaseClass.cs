using FinalStateMachine.States;
using UnityEngine;

namespace FinalStateMachine.FSM
{
    public abstract class FSMBaseClass
    {
        private IState _currentState;
        public IState Currentstate
        {
            get { return _currentState; }
            private set { _currentState = value; }
        }

        public void TransisionTo(IState newState)
        {
            if(Currentstate == newState || newState == null)
            {
                return;
            }

            if(Currentstate != null)
            {
                Currentstate.OnExit();
            }

            Currentstate = newState;
            Currentstate.OnEnter();
        }
    }
}

