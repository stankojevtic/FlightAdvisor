using System;
using System.Collections.Generic;
using System.Text;

namespace FlightAdvisor.Core.Models
{
    public class ImportInfoModel
    {
        public int SuccessfullyImportedRows { get; set; }
        public int SkippedRows{ get; set; }
    }
}
