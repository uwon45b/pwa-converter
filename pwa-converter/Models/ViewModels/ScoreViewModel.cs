namespace pwa_converter.Models.ViewModels;
using pwa_converter.Enums;

public class ScoreViewModel
{
    public int TotalNumOfPerformance { get; set; }

    public int TotalNumOfPwa { get; set; }

    public int NumOfPassesPerformance { get; set; }

    public int NumOfPassesPwa { get; set; }

    public IDictionary<Category, IList<AuditResult>> AuditResults { get; set; }

    public ScoreViewModel(int totalNumOfPerformance, int totalNumOfPwa, int numOfPassesPerformance, int numberOfPassesPwa, IDictionary<Category, IList<AuditResult>> auditResults)
    {
        TotalNumOfPerformance = totalNumOfPerformance;
        TotalNumOfPwa = totalNumOfPwa;
        NumOfPassesPerformance = numOfPassesPerformance;
        NumOfPassesPwa = numberOfPassesPwa;
        AuditResults = auditResults;
    }
}
