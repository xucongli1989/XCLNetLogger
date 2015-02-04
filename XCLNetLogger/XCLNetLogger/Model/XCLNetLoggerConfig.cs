using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCLNetLogger.Model
{
    /// <summary>
    /// XCLNetLogger配置信息
    /// </summary>
    [Serializable]
    public class XCLNetLoggerConfig
    {
        /// <summary>
        /// DB配置信息
        /// </summary>
        public XCLNetLogger.Model.DBConfig DBConfig { get; set; }

        /// <summary>
        /// 参数信息
        /// </summary>
        public List<XCLNetLogger.Model.Parameter> ParameterList { get; set; }
    }

    /// <summary>
    /// DB配置
    /// </summary>
    [Serializable]
    public class DBConfig
    {
        private int _commandTimeOut = 30;
        private bool _isAsync = true;

        /// <summary>
        /// 数据库类型
        /// </summary>
        public XCLNetLogger.Config.LogConfig.SQLType SQLType { get; set; }

        /// <summary>
        /// 数据库连接串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// SQL
        /// </summary>
        public string CommandText { get; set; }

        /// <summary>
        /// sql执行超时时间 （默认30s）
        /// </summary>
        public int CommandTimeOut
        {
            get { return this._commandTimeOut; }
            set { this._commandTimeOut = value; }
        }

        /// <summary>
        /// 是否需要抛出异常（默认false）
        /// </summary>
        public bool NeedThrowException { get; set; }

        /// <summary>
        /// 是否异步执行写入日志（默认true）
        /// </summary>
        public bool IsAsync
        {
            get { return this._isAsync; }
            set { this._isAsync = value; }
        }
    }

    /// <summary>
    /// 参数配置
    /// </summary>
    [Serializable]
    public class Parameter
    {
        /// <summary>
        /// 参数名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        public System.Data.SqlDbType DBType { get; set; }

        /// <summary>
        /// 参数长度
        /// </summary>
        public int? Size { get; set; }
    }
}
