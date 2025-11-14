
namespace FinalStateMachine.States
{
    public interface IState
    {
        public abstract void OnEnter();

        public abstract void OnExit();

        public abstract void Update();

    }
}

