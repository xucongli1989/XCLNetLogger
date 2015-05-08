using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LogTestWeb
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            XCLNetLogger.Config.LogConfig.SetConfig(Server.MapPath("~/Log.config"));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            XCLNetLogger.Model.LogModel logModel = new XCLNetLogger.Model.LogModel();
            logModel.Contents = this.TextBox1.Text;

            XCLNetLogger.Log.WriteLog(logModel);

            try
            {
                int a = 100; int b = 0;
                int c = a / b;
            }
            catch (Exception ex)
            {
                XCLNetLogger.Log.WriteLog(ex);
            }

        }

        protected void btnClickMore(object sender, EventArgs e)
        {
            for (int i = 0; i < 1000; i++)
            {
                XCLNetLogger.Model.LogModel logModel = new XCLNetLogger.Model.LogModel();
                logModel.Contents = this.TextBox1.Text;

                XCLNetLogger.Log.WriteLog(logModel);

                try
                {
                    int a = 100; int b = 0;
                    int c = a / b;
                }
                catch (Exception ex)
                {
                    XCLNetLogger.Log.WriteLog(ex);
                }
            }
        }
    }
}