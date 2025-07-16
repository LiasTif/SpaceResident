public class GameStateMachine
{
    public IGameState State { private get; set; }

    private void Request() => State.Update();
}
