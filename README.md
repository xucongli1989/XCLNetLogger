# 简介 #
XCLNetLogger是一个轻量级的.NET环境下的日志记录组件，只需要简单的配置即可记录日志信息。
# 使用环境 #
- SQL SERVER
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

# 开始使用： #
1. 在项目中引用XCLNetLogger.dll
2. 将上面的配置文件放到项目中
3. 在Global中的Application_Start添加如下代码以初始化配置信息 XCLNetLogger.Config.LogConfig.SetConfig(Server.MapPath("~/Config/Log.config"));
4. 最后在程序代码中，可以直接使用了：XCLNetLogger.Log.WriteLog(logModel);//logModel可以在代码中转到定义自己看哦
5. 报错了吧？别忘记创建日志表了，建议使用存储过程记日志
# MIT授权协议 #
1. 被授权人权利：被授权人有权利使用、复制、修改、合并、出版发行、散布、再授权及贩售软件及软件的副本。被授权人可根据程式的需要修改许可协议为适当的内容。
2. 被授权人义务：在软件和软件的所有副本中都必须包含版权声明和许可声明。
# 贡献者 #
- xucongli1989
# 反馈 #
  如果您发现软件使用过程中，有严重的bug，或者您拥有好的意见或建议，请邮件给我们，谢谢！e-mail:80213876@qq.com



