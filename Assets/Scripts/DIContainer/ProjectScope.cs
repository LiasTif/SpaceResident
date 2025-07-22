using VContainer;
using VContainer.Unity;

public class ProjectScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<GameStateMachine>(Lifetime.Scoped);
        builder.Register<InitializeState>(Lifetime.Scoped);
    }
}