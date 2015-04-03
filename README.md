# 简介 #
XCLNetLogger是一个轻量级的.NET环境下的日志记录组件，只需要简单的配置即可记录日志信息。
# 使用环境 #
- SQL SERVER
- Asp.Net(默认4.5)
# 开始使用： #
1. 在项目中引用XCLNetLogger.dll
2. 将配置文件放到项目中
3. 在Global中的Application_Start添加如下代码以初始化配置信息 XCLNetLogger.Config.LogConfig.SetConfig(Server.MapPath("~/Config/Log.config"));
4. 最后在程序代码中，可以直接使用了：XCLNetLogger.Log.WriteLog(logModel);//logModel可以在代码中转到定义自己看哦
5. 报错了吧？别忘记创建日志表了，建议使用存储过程记日志
# [MIT](https://raw.githubusercontent.com/xucongli1989/XCLNetLogger/master/LICENSE)授权协议 #
1. 被授权人权利：被授权人有权利使用、复制、修改、合并、出版发行、散布、再授权及贩售软件及软件的副本。被授权人可根据程式的需要修改许可协议为适当的内容。
2. 被授权人义务：在软件和软件的所有副本中都必须包含版权声明和许可声明。
# 贡献者 #
- xucongli1989
# 反馈 #
  如果您发现软件使用过程中，有严重的bug，或者您拥有好的意见或建议，请邮件给我们，谢谢！e-mail:80213876@qq.com



