using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace XCLNetLogger.Lib
{
    /// <summary>
    /// 公共帮助类
    /// </summary>
    internal class Common
    {
        /// <summary>
        /// 获取用户ip地址
        /// </summary>
        public static string GetIPAddress(HttpContext context)
        {
            if (null == context)
            {
                return string.Empty;
            }
            string result = string.Empty;
            try
            {
                string ipAddress = string.Empty;
                if (context.Request.ServerVariables.HasKeys() && context.Request.ServerVariables.AllKeys.ToList().Exists(k => string.Equals(k, "HTTP_X_FORWARDED_FOR", StringComparison.OrdinalIgnoreCase)))
                {
                    ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (!string.IsNullOrEmpty(ipAddress))
                    {
                        string[] addresses = ipAddress.Split(',');
                        if (addresses.Length != 0)
                        {
                            result = addresses[0];
                        }
                    }
                }

                if (string.IsNullOrEmpty(result))
                {
                    result = context.Request.ServerVariables["REMOTE_ADDR"];
                }
            }
            catch(Exception ex)
            {
                
            }
            return string.Equals("::1", result) ? "127.0.0.1" : result;
        }

        /// <summary>
        /// 向数据库中写入日志信息
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="model">日志model</param>
        public static void InsertLogToDB(HttpContext context, XCLNetLogger.Model.LogModel model)
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
        }
    }
}
