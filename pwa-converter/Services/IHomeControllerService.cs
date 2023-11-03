namespace pwa_converter.Services;
using pwa_converter.Enums;
using pwa_converter.Models;

public interface IHomeControllerService
{
    public bool ValidateUrl(string url);

    public Task<string> RunLighthouseAudit(string url, string outputPath);

    public bool ValidateLighthouseAuditResultFile(string path);

    public LighthouseAuditResultJson DeserialiseLighthouseAuditResult(string filePath);

    public void DeleteAFile(string filePath);

    public IDictionary<Category, IList<AuditResult>> GetAuditResultsViewData(LighthouseAuditResultJson lighthouseAuditResultJson);

    public int CountNumOfAuditsPassed(IList<AuditResult> auditResults);
}
