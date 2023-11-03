namespace pwa_converter.Models;

public class FaviconGeneratorApiJson
{
    public Favicon_Generation_Result favicon_generation_result { get; set; }
}

public class Favicon_Generation_Result
{
    public Result result { get; set; }
    public Favicon favicon { get; set; }
    public Files_Location files_location { get; set; }
    public string preview_picture_url { get; set; }
    public string custom_parameter { get; set; }
    public string version { get; set; }
    public string non_interactive_request { get; set; }
}

public class Result
{
    public string status { get; set; }
}

public class Favicon
{
    public string package_url { get; set; }
    public string[] files_urls { get; set; }
    public string compression { get; set; }
    public string html_code { get; set; }
    public string[] overlapping_markups { get; set; }
}

public class Files_Location
{
    public string type { get; set; }
    public string path { get; set; }
}