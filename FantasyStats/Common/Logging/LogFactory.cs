namespace Common.Logging
{
    public class LogFactory : ILogFactory
    {
        public ICustomLogger Create()
        {
            return new Log4NetCustomLogger();
        }
    }
}
