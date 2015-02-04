By:XCL @2015 mail:80213876@qq.com  http://blog.csdn.net/luoyeyu1989
# 使用环境 #
- SQL SERVER
- Windows
- Asp.Net(默认4.5)
# 基本配置 #
建立Log.config配置文件，其内容如下：

    <?xml version="1.0" encoding="utf-8" ?>
    <XCLNetLoggerConfig>
    <DBConfig>
    <SQLType>MSSQL</SQLType>
    <ConnectionString>server=XCL-PC\MSSQL2008;database=xxxx;uid=xxxx;pwd=xxxx</ConnectionString>
    <CommandText>EXEC [SysLog_ADD] @LogLevel,@LogType,@RefferUrl,@Url,@Code,@Title,@Contents,@ClientIP,@Remark,@CreateTime</CommandText>
    <CommandTimeOut>3</CommandTimeOut>
    <NeedThrowException>true</NeedThrowException>
    <IsAsync>false</IsAsync>
    </DBConfig>
    <ParameterList>
    <Parameter>
    <Name>@LogLevel</Name>
    <DBType>VarChar</DBType>
    <Size>50</Size>
    </Parameter>
    <Parameter>
    <Name>@LogType</Name>
    <DBType>VarChar</DBType>
    <Size>50</Size>
    </Parameter>
    <Parameter>
    <Name>@RefferUrl</Name>
    <DBType>VarChar</DBType>
    <Size>1000</Size>
    </Parameter>
    <Parameter>
    <Name>@Url</Name>
    <DBType>VarChar</DBType>
    <Size>1000</Size>
    </Parameter>
    <Parameter>
    <Name>@Code</Name>
    <DBType>VarChar</DBType>
    <Size>50</Size>
    </Parameter>
    <Parameter>
    <Name>@Title</Name>
    <DBType>VarChar</DBType>
    <Size>500</Size>
    </Parameter>
    <Parameter>
    <Name>@Contents</Name>
    <DBType>VarChar</DBType>
    <Size>4000</Size>
    </Parameter>
    <Parameter>
    <Name>@ClientIP</Name>
    <DBType>VarChar</DBType>
    <Size>50</Size>
    </Parameter>
    <Parameter>
    <Name>@Remark</Name>
    <DBType>VarChar</DBType>
    <Size>2000</Size>
    </Parameter>
    <Parameter>
    <Name>@CreateTime</Name>
    <DBType>DateTime</DBType>
    </Parameter>
    </ParameterList>
    </XCLNetLoggerConfig>

该配置信息对应的实体如下：
    
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
# 开始使用： #
1.	在项目中引用XCLNetLogger.dll
2.	将上面的配置文件放到项目中
3.	在Global中的Application_Start添加如下代码以初始化配置信息
XCLNetLogger.Config.LogConfig.SetConfig(Server.MapPath("~/Config/Log.config"));
4.	最后在程序代码中，可以直接使用了：
XCLNetLogger.Log.WriteLog(logModel);//logModel可以在代码中转到定义自己看哦
5.	报错了吧？别忘记创建日志表了，最好使用存储过程记日志
CREATE TABLE [dbo].[SysLog](
	[SysLogID] [bigint] IDENTITY(1,1) NOT NULL,
	[LogLevel] [varchar](50) NOT NULL,
	[LogType] [varchar](50) NULL,
	[RefferUrl] [varchar](1000) NULL,
	[Url] [varchar](1000) NULL,
	[Code] [varchar](50) NULL,
	[Title] [varchar](500) NULL,
	[Contents] [varchar](4000) NULL,
	[ClientIP] [varchar](50) NULL,
	[Remark] [varchar](2000) NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SYSLOG] PRIMARY KEY CLUSTERED 
(
	[SysLogID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE PROCEDURE [dbo].[SysLog_ADD]
@LogLevel varchar(50),
@LogType varchar(50),
@RefferUrl varchar(1000),
@Url varchar(1000),
@Code varchar(50),
@Title varchar(500),
@Contents varchar(4000),
@ClientIP varchar(50),
@Remark varchar(2000),
@CreateTime datetime

 AS 
	INSERT INTO [SysLog](
	[LogLevel],[LogType],[RefferUrl],[Url],[Code],[Title],[Contents],[ClientIP],[Remark],[CreateTime]
	)VALUES(
	@LogLevel,@LogType,@RefferUrl,@Url,@Code,@Title,@Contents,@ClientIP,@Remark,@CreateTime
	)
 
