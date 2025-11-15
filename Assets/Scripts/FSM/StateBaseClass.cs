using FinalStateMachine.FSM;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FinalStateMachine.States
{
    public abstract class StateBaseClass : IState
    {
        public StateBaseClass(FSMBaseClass fsm)
        {
            _fsm = fsm;
        }

        protected FSMBaseClass _fsm {  get; private set; }

        public abstract void OnEnter();

        public abstract void OnExit();

        public abstract void PropertyHasChanged(object sender, PropertyChangedEventArgs eventArgs);

        public abstract void Update();

        public abstract void Initialize();
    }
}

