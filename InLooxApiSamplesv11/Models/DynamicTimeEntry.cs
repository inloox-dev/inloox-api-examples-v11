namespace InLooxApiSamplesv11.Models
{
    // this class can be extended as required
    // even with custom fields
    // for all available fields see: https://app.inloox.com/api/v1/swagger/index.html

    public class DynamicTimeEntry
    {
        public Guid TimeEntry_TimeEntryId { get; set; }
        public Guid TimeEntry_PerformedByContactId { get; set; }
        public string? Project_Number { get; set; }
        public DateTime TimeEntry_StartDateTime { get; set; }
        public DateTime TimeEntry_EndDateTime { get; set; }
        public int TimeEntry_DurationMinutes { get; set; }

        // CustomFields can be queried as well:
        // public string? TimeEntry_cf_2283e9e4_CFDTest { get; set; }  

        // to see all available fields (including custom fields) open the following URL when you´re logged in in your browser
        // https://app.inloox.com/api/odata/dynamictimeentry
        // OnPrem URL: https://YOUR-ON-PREM-URL.com/api/v1/odata/dynamictimeentry
    }
}
