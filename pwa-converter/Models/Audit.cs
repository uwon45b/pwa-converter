namespace pwa_converter.Models;

public class Audit
{
    public string Title { get; set; }

    public string Description { get; set; }

    public IList<string> Fixes { get; set; }

    public Audit(string title, string description, IList<string> fixes)
    {
        Title = title;
        Description = description;
        Fixes = fixes;
    }
}