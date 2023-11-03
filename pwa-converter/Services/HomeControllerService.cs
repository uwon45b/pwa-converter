namespace pwa_converter.Services;

using Newtonsoft.Json;
using pwa_converter.Enums;
using pwa_converter.Models;
using System.Diagnostics;
using System.Text.RegularExpressions;


public class HomeControllerService : IHomeControllerService
{
    private readonly ILighthouseAuditResultContainer _container;
    public HomeControllerService(ILighthouseAuditResultContainer container)
    {
        _container = container;
    }

    public bool ValidateUrl(string url)
    {
        Uri uriResult;
        var result = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        return result;
    }

    public async Task<string> RunLighthouseAudit(string url, string outputPath)
    {
        var fileName = $"result-{Guid.NewGuid()}.json";
        var ps = new ProcessStartInfo();
        ps.FileName = "cmd.exe";
        ps.WindowStyle = ProcessWindowStyle.Hidden;
        ps.Arguments = $"/k lighthouse {url} --output json --output-path {outputPath}\\{fileName} --only-categories=pwa, performance";
        Process.Start(ps);  

        await System.Threading.Tasks.Task.Delay(10000);
        var timeoutCount = 0;
        while (!File.Exists(Path.Combine(outputPath, fileName)) && timeoutCount < 5)
        {
            await System.Threading.Tasks.Task.Delay(2500);
            timeoutCount++;
        }

        return timeoutCount < 5 ? fileName : throw new FileNotFoundException("Lighthouse Audit Error");
    }

    public bool ValidateLighthouseAuditResultFile(string path)
    {
        var lighthouseAuditResult = File.ReadAllText(path);
        return !Regex.IsMatch(lighthouseAuditResult, "\"runtimeError\"", RegexOptions.Singleline);
    }

    public LighthouseAuditResultJson DeserialiseLighthouseAuditResult(string filePath)
    {
        var jsonString = System.IO.File.ReadAllText(filePath);
        var lighthouseAuditResult = JsonConvert.DeserializeObject<LighthouseAuditResultJson>(jsonString);

        return lighthouseAuditResult;
    }

    public void DeleteAFile(string filePath)
    {
        File.Delete(filePath);
    }

    public IDictionary<Category, IList<AuditResult>> GetAuditResultsViewData(LighthouseAuditResultJson lighthouseAuditResultJson)
    {
        var properties = lighthouseAuditResultJson.audits.GetType().GetProperties();

        var auditResultsViewData = new Dictionary<Category, IList<AuditResult>>
        {
            [Category.Performance] = new List<AuditResult>(),
            [Category.Pwa] = new List<AuditResult>()
        };

        foreach (var category in _container.LighthouseAuditResults)
        {
            foreach (var type in category.Value)
            {
                foreach (var audit in type.Value)
                {
                    var matchingProperty = properties.FirstOrDefault(prop => prop.PropertyType == audit.Key);
                    var auditObject = matchingProperty.GetValue(lighthouseAuditResultJson.audits);
                    var scoreProperty = auditObject.GetType().GetProperties()[3];
                    var actualScore = (double?)scoreProperty.GetValue(auditObject);

                    if (!(actualScore == null))
                    {
                        var auditResult = new AuditResult(audit.Value, actualScore);

                        auditResultsViewData[category.Key].Add(auditResult);
                    }
                }
            }
        }

        auditResultsViewData[Category.Performance] = auditResultsViewData[Category.Performance].OrderBy(auditResult => auditResult.Score).ToList();
        auditResultsViewData[Category.Pwa] = auditResultsViewData[Category.Pwa].OrderBy(auditResult => auditResult.Score).ToList();

        return auditResultsViewData;
    }

    public int CountNumOfAuditsPassed(IList<AuditResult> auditResults)
    {
        var numOfPasses = 0;
        foreach (var auditResult in auditResults)
        {
            if (auditResult.Score == 1)
            {
                numOfPasses++;
            }
        }

        return numOfPasses;
    }
}