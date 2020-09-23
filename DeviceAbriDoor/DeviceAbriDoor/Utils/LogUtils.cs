using System;

namespace DeviceAbriDoor.Utils
{
    public static class LogUtils
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void WirteLogInfo(string message)
        {
            logger.Info(message);
        }
        public static void WirteLogInfo(string message, Exception ex)
        {
            logger.Info(message, ex);
        }

        public static void WirteLogDebug(string message)
        {
            logger.Debug(message);
        }
        public static void WirteLogDebug(string message, Exception ex)
        {
            logger.Debug(message, ex);
        }

        public static void WirteLogError(string message)
        {
            logger.Error(message);
        }
        public static void WirteLogError(string message, Exception ex)
        {
            logger.Error(message, ex);
        }
    }
}
