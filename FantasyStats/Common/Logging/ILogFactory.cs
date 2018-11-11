namespace Common.Logging
{
    public interface ILogFactory
    {
        ICustomLogger Create();
    }
}
