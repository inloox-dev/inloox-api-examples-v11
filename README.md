# inloox-api-examples-v11

## Start
Some very simple samples to use the InLoox OData API for InLoox 11 (Cloud & OnPrem). For start see:
https://github.com/inloox-dev/inloox-api-examples-v11/blob/main/InLooxApiSamplesv11/Program.cs

## OData specification
InLoox Odata specification is available via SWAGGER
https://app.inloox.com/api/v1/swagger/index.html

## Token generation
To call the ODATA api a PersonalAccessToken is required.
API Token can be generated on your profile page: https://login.inloox.com/Manage/PersonalAccessToken (OnPrem: https://YOUR-ON-PREM-URL/login/Manage/PersonalAccessToken)
The API Token can be provided in the HTTP Header. Key: 'x-api-key' Value: YOUR_PAT_TOKEN

## Models
The referenced NugetPackage (https://www.nuget.org/packages/InLoox.PM.Domain.Model.Public/) contains models for all relevant entites.

## Custom fields
Retrieval of custom fields values is also possible for Project, TaskItem, Budget, LineItem and TimeEntry currently. Therefore use the ODataEndpoints:
DynamicProject, DynamicTaskItem, DynamicBudget, DynamicLineItem, DynamicTimeEntry
To retrieve the custom field values you need to extend the Custom\*.cs files in the Models subfolder.

## Paging and filtering
By default the InLoox ODATA API returns 100 elements per API call. To implement paging see example 3 in program.cs.
Filtering is also explained in this example.
