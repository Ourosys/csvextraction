using Ease.Data;
using Ease.Data.Sql;
using Ease.Model.Extract;
using Ease.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;

namespace Ease.Extract.Web.Controllers
{
    public class EaseExtractDataController : Controller
    {


        internal EaseDataAccess DataAccess = new EaseDataAccess(new SqlDataAccessCommon(), new SqlDataAccessAudit(), new SqlDataAccessAccount(), new SqlDataAccessLaborStandards(), new SqlDataAccessExtract());
        public FileContentResult GetMetricsData()
        {
            string visibleColumnNames = "ID, OrganizationID, SiteID, OrgName, SiteName, DateFrom, DateTo, PctAuditsCompleted, NumActiveUsers, PctMitigationsClosed, EnvironmentID";
            string language = "EN-US";

            //sort by ID in the sitesintrial table

            IEnumerable<UsageData> ExtractedData = DataAccess.Extract.DataFromEaseExtract();
            CSVReponseHelper csvResponseHelper = new CSVReponseHelper(DataAccess.Common);
            byte[] dataToExport = csvResponseHelper.WriteCsvToMemory<UsageData>(ExtractedData, language, visibleColumnNames);
            return File(dataToExport, "text/csv", "SalesMetrics.csv" + " " + DateTime.Today);
        }

        [Route("Dashboard"), HttpGet]
        public ActionResult Dashboard()
        {
            IEnumerable<UsageData> model = DataAccess.Extract.ExtractFromSitesInTrial();
            if (model != null)
            {
                return View("Dashboard", model);
            }
            else
            {
                return Redirect("User/Index");
            }
        }
    }

}