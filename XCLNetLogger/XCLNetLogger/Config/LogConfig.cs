using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace XCLNetLogger.Config
{
    /// <summary>
    /// log配置
    /// </summary>
    public class LogConfig
    {
        /// <summary>
        /// 当前log的配置信息
        /// </summary>
        public static XCLNetLogger.Model.XCLNetLoggerConfig GetXLoggerConfig { get; set; }

        /// <summary>
        /// 设置配置信息
        /// </summary>
        /// <param name="configPath">配置文件所在路径</param>
        public static void SetConfig(string configPath)
        {
            using(XmlTextReader rd = new XmlTextReader(configPath))
            {
                XmlSerializer xmlSer = new XmlSerializer(typeof(XCLNetLogger.Model.XCLNetLoggerConfig));
                LogConfig.GetXLoggerConfig = xmlSer.Deserialize(rd) as XCLNetLogger.Model.XCLNetLoggerConfig;
            }
        }

        /// <summary>
        /// 日志级别
        /// </summary>
        public enum LogLevel
        {
            /// <summary>
            /// 一般信息
            /// </summary>
            INFO,
            /// <summary>
            /// 警告
            /// </summary>
            WARN,
            /// <summary>
            /// 异常
            /// </summary>
            ERROR,
            /// <summary>
            /// 调试信息
            /// </summary>
            DEBUG
        }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public enum SQLType
        { 
            /// <summary>
            /// MSSQL
            /// </summary>
            MSSQL
        }
    }
}
