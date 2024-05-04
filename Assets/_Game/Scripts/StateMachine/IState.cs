public interface IState
{
    public void OnEnter(Bot bot);

    public void OnExecute(Bot bot);

    public void OnExit(Bot bot);

}
