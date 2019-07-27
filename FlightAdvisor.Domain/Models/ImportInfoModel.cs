namespace FlightAdvisor.Domain.Models
{
    public class ImportInfoModel
    {
        public int SuccessfullyImportedRows { get; set; }
        public int SkippedRows { get; set; }
    }
}
