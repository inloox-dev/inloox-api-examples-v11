using InLoox.PM.Domain.Model.Aggregates.Api;
using Simple.OData.Client;

var EndPoint = new Uri("https://app.inloox.com");
var EndPointOdata = new Uri(EndPoint, "/api/odata/");

//if you´re using InLoox OnPrem please use the following schema for the Endpoint
//var EndPoint = new Uri("https://YOUR-ON-PREM-URL/");
//var EndPointOdata = new Uri(EndPoint, "/api/v1/odata/");

// this is just a simple sample. Token shouldn´t be saved in source code. E.g. use appsettings or (better) Azure Key Vault
var token = "INSERT YOUR TOKEN"; // API Token can be generated on your profile page: https://login.inloox.com/Manage/PersonalAccessToken

// InLoox Odata specification is available via SWAGGER
// https://app.inloox.com/api/v1/swagger/index.html

var settings = new ODataClientSettings(EndPointOdata);
settings.BeforeRequest += delegate (HttpRequestMessage message)
{
    message.Headers.Add("x-api-key", token);
};
var client = new ODataClient(settings);

// example 1: Show name of your account
var accountInfo = await GetAccountInfo();

// example 2: Get projects
var projects = await GetProjects();

// example 3: Get time entries for one month
await GetAllTimeEntriesForMonth(DateTime.Now, a => Console.WriteLine(a));

// example 4: Create TimeEntry
await CreateTimeEntry(projects.First().ProjectId, "Sample Time", DateTime.Now);

// example 5: Update a project name
var project = projects.First();
await UpdateProjectName(project.ProjectId, project.Name + " updated");


// example 1
async Task<ApiAccountInfo> GetAccountInfo()
{
    if (client == null)
        throw new InvalidOperationException("Initialize client first");

    return await client.For<ApiAccountInfo>("AccountInfo").FindEntryAsync();
}


// example 2
async Task<IEnumerable<ApiProject>> GetProjects()
{
    // this will only return the first 100 projects
    // for paging and filtering see sample 3
    if (client == null)
        throw new InvalidOperationException("Initialize client first");

    return await client.For<ApiProject>("Project").FindEntriesAsync();
}


// example 3
async Task<List<ApiDynamicTimeEntry>> GetAllTimeEntriesForMonth(DateTime month, Action<string> loadedFunc)
{
    if (client == null)
        throw new InvalidOperationException("Initialize client first");

    var filterStart = new DateTime(month.Year, month.Month, 1);
    var filterEnd = new DateTime(month.Year, month.Month, 1).AddMonths(1);

    var annotations = new ODataFeedAnnotations();
    var timeentries = (await client.For<ApiDynamicTimeEntry>("DynamicTimeEntry")
        .Filter(k =>
            k.TimeEntry_StartDateTime > filterStart &&
            k.TimeEntry_EndDateTime < filterEnd
        )
        .FindEntriesAsync(annotations)).ToList();

    while (annotations.NextPageLink != null)
    {
        timeentries.AddRange(await client
            .For<ApiDynamicTimeEntry>("DynamicTimeEntry")
            .FindEntriesAsync(annotations.NextPageLink, annotations));

        loadedFunc($"Loaded {timeentries.Count()} entries");
    }

    return timeentries;
}


// example 4
async Task CreateTimeEntry(Guid projectId, string name, DateTime start)
{
    if (client == null)
        throw new InvalidOperationException("Initialize client first");

    var values = new Dictionary<string, object>
    {
        { "ProjectId", projectId },
        { "DisplayName", name },
        { "StartDateTime", start },
        { "EndDateTime", start.AddHours(2) }
    };

    var res = await client.InsertEntryAsync("TimeEntry", values);
}


// example 5
async Task UpdateProjectName(Guid projectId, string newName)
{
    if (client == null)
        throw new InvalidOperationException("Initialize client first");

    var project = new ApiProject()
    {
        Name = newName
    };

    await client.For<ApiProject>().Key(projectId).Set(new { project.Name }).UpdateEntryAsync();
}
