using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading;
using System.Web;

namespace XCLNetLogger
{
    /// <summary>
    /// 日志类
    /// </summary>
    public class Log
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Log()
        {

        }

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="logLevel">日志级别</param>
        /// <param name="title">标题</param>
        /// <param name="contents">内容</param>
        public static void WriteLog(XCLNetLogger.Config.LogConfig.LogLevel logLevel, string title, string contents=null)
        {
            XCLNetLogger.Model.LogModel log = new XCLNetLogger.Model.LogModel();
            log.LogLevel = logLevel;
            log.Title = title;
            log.Contents = contents;
            Log.WriteLog(log);
        }

        /// <summary>
        /// 写入异常信息
        /// </summary>
        /// <param name="ex">异常</param>
        public static void WriteLog(Exception ex, string remark = null)
        {
            XCLNetLogger.Model.LogModel log = new XCLNetLogger.Model.LogModel();
            log.LogLevel = Config.LogConfig.LogLevel.ERROR;
            log.Title = ex.Message;
            log.Contents = ex.StackTrace;
            log.Remark = remark;
            Log.WriteLog(log);
        }

        /// <summary>
        /// 写日志
        /// </summary>
        public static void WriteLog(XCLNetLogger.Model.LogModel model)
        {
            var currentContext = HttpContext.Current;
            Action<HttpContext, XCLNetLogger.Model.LogModel> insertLogAction = new Action<HttpContext, Model.LogModel>((actionContext,actionLogModel) =>
            {
                XCLNetLogger.Lib.Common.InsertLogToDB(actionContext, actionLogModel);
            });

            try
            {
                if (XCLNetLogger.Config.LogConfig.GetXLoggerConfig.DBConfig.IsAsync)
                {
                    insertLogAction.BeginInvoke(currentContext, model, null, null);
                }
                else
                {
                    insertLogAction.Invoke(currentContext, model);
                }
            }
            catch (Exception ex)
            {
                if (XCLNetLogger.Config.LogConfig.GetXLoggerConfig.DBConfig.NeedThrowException)
                {
                    throw ex;
                }
            }
        }
    }
}