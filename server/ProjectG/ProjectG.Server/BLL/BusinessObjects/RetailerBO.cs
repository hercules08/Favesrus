using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectG.Server.BLL.BusinessObjects
{
    public class RetailerBO
    {
        public int RetailerId { get; set; }
        public string RetailerName { get; set; }
        public string RetailerLogo { get; set; }
        public string RetailerLogoDataString { get; set; }
    }
}