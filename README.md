# 简介 #
XCLNetLogger是一个轻量级的.NET环境下的日志记录组件，只需要简单的配置即可记录日志信息。
# 使用环境 #
- SQL SERVER
- Asp.Net(默认4.5)
# 基本配置 #
<pre><code>建立Log.config配置文件，其内容如下：
&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt;
&lt;XCLNetLoggerConfig&gt;
&lt;DBConfig&gt;
&lt;SQLType&gt;MSSQL&lt;/SQLType&gt;
&lt;ConnectionString&gt;server=XCL-PC\MSSQL2008;database=xxxx;uid=xxxx;pwd=xxxx&lt;/ConnectionString&gt;
&lt;CommandText&gt;EXEC [SysLog_ADD] @LogLevel,@LogType,@RefferUrl,@Url,@Code,@Title,@Contents,@ClientIP,@Remark,@CreateTime&lt;/CommandText&gt;
&lt;CommandTimeOut&gt;3&lt;/CommandTimeOut&gt;
&lt;NeedThrowException&gt;true&lt;/NeedThrowException&gt;
&lt;IsAsync&gt;false&lt;/IsAsync&gt;
&lt;/DBConfig&gt;
&lt;ParameterList&gt;
&lt;Parameter&gt;
&lt;Name&gt;@LogLevel&lt;/Name&gt;
&lt;DBType&gt;VarChar&lt;/DBType&gt;
&lt;Size&gt;50&lt;/Size&gt;
&lt;/Parameter&gt;
&lt;Parameter&gt;
&lt;Name&gt;@LogType&lt;/Name&gt;
&lt;DBType&gt;VarChar&lt;/DBType&gt;
&lt;Size&gt;50&lt;/Size&gt;
&lt;/Parameter&gt;
&lt;Parameter&gt;
&lt;Name&gt;@RefferUrl&lt;/Name&gt;
&lt;DBType&gt;VarChar&lt;/DBType&gt;
&lt;Size&gt;1000&lt;/Size&gt;
&lt;/Parameter&gt;
&lt;Parameter&gt;
&lt;Name&gt;@Url&lt;/Name&gt;
&lt;DBType&gt;VarChar&lt;/DBType&gt;
&lt;Size&gt;1000&lt;/Size&gt;
&lt;/Parameter&gt;
&lt;Parameter&gt;
&lt;Name&gt;@Code&lt;/Name&gt;
&lt;DBType&gt;VarChar&lt;/DBType&gt;
&lt;Size&gt;50&lt;/Size&gt;
&lt;/Parameter&gt;
&lt;Parameter&gt;
&lt;Name&gt;@Title&lt;/Name&gt;
&lt;DBType&gt;VarChar&lt;/DBType&gt;
&lt;Size&gt;500&lt;/Size&gt;
&lt;/Parameter&gt;
&lt;Parameter&gt;
&lt;Name&gt;@Contents&lt;/Name&gt;
&lt;DBType&gt;VarChar&lt;/DBType&gt;
&lt;Size&gt;4000&lt;/Size&gt;
&lt;/Parameter&gt;
&lt;Parameter&gt;
&lt;Name&gt;@ClientIP&lt;/Name&gt;
&lt;DBType&gt;VarChar&lt;/DBType&gt;
&lt;Size&gt;50&lt;/Size&gt;
&lt;/Parameter&gt;
&lt;Parameter&gt;
&lt;Name&gt;@Remark&lt;/Name&gt;
&lt;DBType&gt;VarChar&lt;/DBType&gt;
&lt;Size&gt;2000&lt;/Size&gt;
&lt;/Parameter&gt;
&lt;Parameter&gt;
&lt;Name&gt;@CreateTime&lt;/Name&gt;
&lt;DBType&gt;DateTime&lt;/DBType&gt;
&lt;/Parameter&gt;
&lt;/ParameterList&gt;
&lt;/XCLNetLoggerConfig&gt;

该配置信息对应的实体如下：

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCLNetLogger.Model
{
/// &lt;summary&gt;
/// XCLNetLogger配置信息
/// &lt;/summary&gt;
[Serializable]
public class XCLNetLoggerConfig
{
    /// &lt;summary&gt;
    /// DB配置信息
    /// &lt;/summary&gt;
    public XCLNetLogger.Model.DBConfig DBConfig { get; set; }

    /// &lt;summary&gt;
    /// 参数信息
    /// &lt;/summary&gt;
    public List&lt;XCLNetLogger.Model.Parameter&gt; ParameterList { get; set; }
}

/// &lt;summary&gt;
/// DB配置
/// &lt;/summary&gt;
[Serializable]
public class DBConfig
{
    private int _commandTimeOut = 30;
    private bool _isAsync = true;

    /// &lt;summary&gt;
    /// 数据库类型
    /// &lt;/summary&gt;
    public XCLNetLogger.Config.LogConfig.SQLType SQLType { get; set; }

    /// &lt;summary&gt;
    /// 数据库连接串
    /// &lt;/summary&gt;
    public string ConnectionString { get; set; }

    /// &lt;summary&gt;
    /// SQL
    /// &lt;/summary&gt;
    public string CommandText { get; set; }

    /// &lt;summary&gt;
    /// sql执行超时时间 （默认30s）
    /// &lt;/summary&gt;
    public int CommandTimeOut
    {
        get { return this._commandTimeOut; }
        set { this._commandTimeOut = value; }
    }

    /// &lt;summary&gt;
    /// 是否需要抛出异常（默认false）
    /// &lt;/summary&gt;
    public bool NeedThrowException { get; set; }

    /// &lt;summary&gt;
    /// 是否异步执行写入日志（默认true）
    /// &lt;/summary&gt;
    public bool IsAsync
    {
        get { return this._isAsync; }
        set { this._isAsync = value; }
    }
}

/// &lt;summary&gt;
/// 参数配置
/// &lt;/summary&gt;
[Serializable]
public class Parameter
{
    /// &lt;summary&gt;
    /// 参数名
    /// &lt;/summary&gt;
    public string Name { get; set; }

    /// &lt;summary&gt;
    /// 参数类型
    /// &lt;/summary&gt;
    public System.Data.SqlDbType DBType { get; set; }

    /// &lt;summary&gt;
    /// 参数长度
    /// &lt;/summary&gt;
    public int? Size { get; set; }
}
}
</code></pre>

# 开始使用： #
<pre><code>1、在项目中引用XCLNetLogger.dll
2、将上面的配置文件放到项目中
    在Global中的Application_Start添加如下代码以初始化配置信息
    XCLNetLogger.Config.LogConfig.SetConfig(Server.MapPath(&quot;~/Config/Log.config&quot;));
3、最后在程序代码中，可以直接使用了：
    XCLNetLogger.Log.WriteLog(logModel);//logModel可以在代码中转到定义自己看哦
4、报错了吧？别忘记创建日志表了，最好使用存储过程记日志

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
</code></pre>


----------

# MIT授权协议 #
1. 被授权人权利：被授权人有权利使用、复制、修改、合并、出版发行、散布、再授权及贩售软件及软件的副本。被授权人可根据程式的需要修改许可协议为适当的内容。
2. 被授权人义务：在软件和软件的所有副本中都必须包含版权声明和许可声明。
# 贡献者 #
- xucongli1989
# 反馈 #
  如果您发现软件使用过程中，有严重的bug，或者您拥有好的意见或建议，请邮件给我们，谢谢！e-mail:80213876@qq.com