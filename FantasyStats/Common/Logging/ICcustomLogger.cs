using System;

namespace Common.Logging
{
    public interface ICustomLogger
    {
        void Error(Exception e, string extendedInformation = "");
        void Info(string message);
    }
}
