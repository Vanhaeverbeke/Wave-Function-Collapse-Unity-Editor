using FinalStateMachine.FSM;
using UnityEngine;

namespace FinalStateMachine.States
{
    public abstract class StateBaseClass : IState
    {
        public StateBaseClass(FSMBaseClass fsm)
        {
            FSM = fsm;
        }

        public FSMBaseClass FSM {  get; private set; }

        public abstract void OnEnter();

        public abstract void OnExit();

        public abstract void Update(float deltaTime);

    }
}

