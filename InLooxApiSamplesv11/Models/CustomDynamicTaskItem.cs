using InLoox.PM.Domain.Model.Aggregates.Api;

namespace InLooxApiSamplesv11.Models
{
    // this class can be extended as required with custom fields
    // for all available fields see: https://app.inloox.com/api/v1/swagger/index.html

    public class CustomDynamicTaskItem : ApiDynamicTaskItem
    {
        // CustomFields can be queried:
        // public string? TaskItem_cf_2283e9e4_CFDTest { get; set; }  

        // to see all available fields (including custom fields) open the following URL when you´re logged in in your browser
        // Cloud URL:  https://app.inloox.com/api/odata/dynamictaskitem
        // OnPrem URL: https://YOUR-ON-PREM-URL.com/api/v1/odata/dynamictaskitem
    }
}
