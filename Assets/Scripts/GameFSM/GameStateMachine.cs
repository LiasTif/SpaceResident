using VContainer;
using VContainer.Unity;

public class GameStateMachine : IStartable
{
    public IGameState State { private get; set; }

    [Inject] private InitializeState _initializeState;

    public void Start()
    {
        ChangeState(_initializeState);
    }

    private void ChangeState(IGameState state)
    {
        if (State == state) return;

        State?.Exit();
        State = state;
        State.Enter();
    }

    private void Request() => State?.Update();
}
