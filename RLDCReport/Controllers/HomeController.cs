using Microsoft.Reporting.WebForms;
using RLDCReport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RLDCReport.Controllers
{
    public class HomeController : Controller
    {
        LATIHAN_DBEntities2 db = new LATIHAN_DBEntities2();
        public ActionResult ProductList()
        {
            return View(db.Products.ToList());
        }
        public ActionResult ExportReport( string ReportType)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reports/ReportProduct.rdlc");
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet1";
            reportDataSource.Value = db.Products.ToList();
            localReport.DataSources.Add(reportDataSource);
            string reportType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtention;
            if (reportType=="Excel")
            {
                fileNameExtention = "xls";
            }
            else if (reportType == "Word")
            {
                fileNameExtention = "docx";
            }
            if (reportType == "PDF")
            {
                fileNameExtention = "pdf";
            }
            else 
            {
                fileNameExtention = "jpg";
            }
            string[] streams;
            Warning[] warnings;
            byte[] renderByte;
            renderByte = localReport.Render(reportType, "", out mimeType, out encoding, out fileNameExtention, out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment;filename=productreport." + fileNameExtention);
            return File(renderByte, fileNameExtention);

        }



    }
}