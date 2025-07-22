public class InitializeState : IGameState
{
    public void Enter()
    {
        SceneTransition.Transit("Initialization");
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }
}