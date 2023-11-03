namespace pwa_converter.Models;
using pwa_converter.Enums;

public interface ILighthouseAuditResultContainer
{
    public IDictionary<Category, IDictionary<Type, IDictionary<System.Type, Audit>>> LighthouseAuditResults { get; set; }
}
