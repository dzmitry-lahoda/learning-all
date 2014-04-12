namespace AIbyExample.Common {
    public interface IState<T> where T:Agent{
        void Enter(T agent);
        void Execute(T agent);
        void Exit(T agent);
    }
}