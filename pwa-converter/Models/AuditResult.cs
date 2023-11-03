namespace pwa_converter.Models;

public class AuditResult
{
    public Audit Audit { get; set; }
    
    public double? Score { get; set; }

    public AuditResult(Audit audit, double? score)
    {
        Audit = audit;
        Score = score;
    }
}
