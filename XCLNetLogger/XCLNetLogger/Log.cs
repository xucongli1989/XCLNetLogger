using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
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
            try
            {
                Action<HttpContext> action = new Action<HttpContext>((context) =>
                {
                    #region 设置部分属性的默认值

                    if (null != context)
                    {
                        var request = context.Request;
                        if (null == model.RefferUrl)
                        {
                            if (null != request.UrlReferrer)
                            {
                                model.RefferUrl = request.UrlReferrer.AbsoluteUri;
                            }
                        }

                        if (null == model.Url)
                        {
                            model.Url = request.Url.AbsoluteUri;
                        }

                        if (null == model.ClientIP)
                        {
                            model.ClientIP = XCLNetLogger.Lib.Common.GetIPAddress(context);
                        }

                    }

                    #endregion 设置部分属性的默认值

                    #region 写入数据库

                    using (SqlConnection connection = new SqlConnection(XCLNetLogger.Config.LogConfig.GetXLoggerConfig.DBConfig.ConnectionString))
                    {
                        SqlCommand cmd = connection.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = XCLNetLogger.Config.LogConfig.GetXLoggerConfig.DBConfig.CommandText;
                        cmd.CommandTimeout = XCLNetLogger.Config.LogConfig.GetXLoggerConfig.DBConfig.CommandTimeOut;
                        XCLNetLogger.Config.LogConfig.GetXLoggerConfig.ParameterList.ForEach(k =>
                        {
                            if (null == k.Size)
                            {
                                cmd.Parameters.Add(new SqlParameter(k.Name, k.DBType));
                            }
                            else
                            {
                                cmd.Parameters.Add(new SqlParameter(k.Name, k.DBType, (int)k.Size));
                            }
                        });

                        if (null != cmd.Parameters && cmd.Parameters.Count > 0)
                        {
                            PropertyInfo[] pros = model.GetType().GetProperties();
                            for (int m = 0; m < cmd.Parameters.Count; m++)
                            {
                                for (int n = 0; n < pros.Length; n++)
                                {
                                    if (string.Equals(pros[n].Name, cmd.Parameters[m].ParameterName.TrimStart('@'), StringComparison.OrdinalIgnoreCase))
                                    {
                                        cmd.Parameters[m].Value = pros[n].GetValue(model) ?? DBNull.Value;
                                        break;
                                    }
                                }
                            }
                        }
                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }

                    #endregion 写入数据库
                });
                if (XCLNetLogger.Config.LogConfig.GetXLoggerConfig.DBConfig.IsAsync)
                {
                    IAsyncResult asyncResult = action.BeginInvoke(HttpContext.Current, null, null);
                }
                else
                {
                    action.Invoke(HttpContext.Current);
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