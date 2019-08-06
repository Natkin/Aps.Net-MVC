using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IDoctor.Controllers;

namespace IDoctor.Reports
{
    public partial class Ticket : System.Web.UI.Page
    {
        HomeController hc = new HomeController();
        LabdesktopDBEntities db = new LabdesktopDBEntities();
        private string field;
        private string name;
        private string middle;
        private string hour;
        private string minute;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                field = Request.QueryString["id"];
                name = Request.QueryString["nam"];
                middle = Request.QueryString["mid"];
                hour = Request.QueryString["h"];
                minute = Request.QueryString["m"];

                DataTable dat = new DataTable();
                dat.Columns.Add(new DataColumn("Name", typeof(string)));
                dat.Columns.Add(new DataColumn("Surname", typeof(string)));
                dat.Columns.Add(new DataColumn("MiddleName", typeof(string)));
                dat.Columns.Add(new DataColumn("Date", typeof(string)));
                dat.Columns.Add(new DataColumn("Hours", typeof(string)));
                dat.Columns.Add(new DataColumn("Minutes", typeof(string)));

                DataRow dr = dat.NewRow();
                    dr["Name"] = name;
                    dr["SurName"] = field;
                    dr["MiddleName"] = middle;
                    dr["Date"] = DateTime.Now.ToString("dd.MM.yyy");
                    dr["Hours"] = hour;
                    dr["Minutes"] = minute;

                dat.Rows.Add(dr);
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("Report1.rdlc");
                ReportDataSource datasource = new ReportDataSource("DataSet1", dat);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
            }
        }
    }
}