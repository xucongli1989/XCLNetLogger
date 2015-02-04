using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
