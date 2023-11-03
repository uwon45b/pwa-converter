namespace pwa_converter.Models;
using pwa_converter.Enums;
using System.Collections.Generic;

public class LighthouseAuditResultContainer : ILighthouseAuditResultContainer
{
    public IDictionary<Category, IDictionary<Type, IDictionary<System.Type, Audit>>> LighthouseAuditResults { get; set; }

    public LighthouseAuditResultContainer()
    {
        LighthouseAuditResults = InitialiseLighthouseAuditResults();
        AddAllCategoryPerformanceTypeOpportunity();
        AddAllCategoryPerformanceTypeDiagnostics();
        AddAllCategoryPwaTypeInstallable();
        AddAllCategoryPwaTypePwaOptimized();
    }

    private IDictionary<Category, IDictionary<Type, IDictionary<System.Type, Audit>>> InitialiseLighthouseAuditResults()
    {
        var dictionary = new Dictionary<Category, IDictionary<Type, IDictionary<System.Type, Audit>>>
        {
            [Category.Performance] = new Dictionary<Type, IDictionary<System.Type, Audit>>(),
            [Category.Pwa] = new Dictionary<Type, IDictionary<System.Type, Audit>>()
        };

        dictionary[Category.Performance].Add(Type.Opportunity, new Dictionary<System.Type, Audit>());
        dictionary[Category.Performance].Add(Type.Diagnostics, new Dictionary<System.Type, Audit>());
        dictionary[Category.Pwa].Add(Type.PwaOptimized, new Dictionary<System.Type, Audit>());
        dictionary[Category.Pwa].Add(Type.Installable, new Dictionary<System.Type, Audit>());
        return dictionary;
    }

    private void AddAllCategoryPerformanceTypeOpportunity()
    {
        var dictionary = LighthouseAuditResults[Category.Performance][Type.Opportunity];
        dictionary.Add(typeof(ServerResponseTime), new Audit("Initial server response time is short",
                                                         "The browser waits more than 600 ms for the server to respond to the main document request. Users dislike when pages take a long time to load.",
                                                         new List<string> { "Optimise the server's application logic to prepare pages faster.",
                                                                            "Optimize how your server queries databases, or migrate to faster database systems.",
                                                                            "Upgrade your server hardware to have more memory or CPU.",
                                                                            "Use a CDN to reduce network latency." }));
        dictionary.Add(typeof(RenderBlockingResources), new Audit("Eliminate render-blocking resources",
                                                              "Render-blocking resources are files that prevent a web page from loading quickly.",
                                                              new List<string>
                                                              {
                                                                  "Consider delivering critical JS/CSS inline and deferring all non-critical JS/styles.",
                                                                  "Mnify your CSS to remove any extra whitespace or characters."
                                                              }));

        dictionary.Add(typeof(Redirects), new Audit("Avoid multiple page redirects",
                                     "Redirects slow down your page load speed. A page fails this audit when it has two or more redirects.",
                                     new List<string>
                                     {
                                         "Point links to the resources' current locations.",
                                         "If redirects are for redirecting to mobile version of the page, consider redesigning your site to use Responsive Design."
                                     }));

        dictionary.Add(typeof(UsesTextCompression), new Audit("Enable text compression",
                                                          "Text-based resources should be served with compression to minimize total network bytes (gzip, deflate or brotli).",
                                                          new List<string>
                                                          {
                                                            "Enable text compression on the server that serves the text-based resources. It should support Brotli (br) and GZIP as a fallback to Brotli."
                                                          }));

        dictionary.Add(typeof(UsesRelPreconnect), new Audit("Preconnect to required origins",
                                     "Consider adding preconnect or dns-prefetch resource hints to inform the browser to improve the site speed.",
                                     new List<string>
                                     {
                                         "Prioritise fetch request with &lt;link rel=preconnect&gt;. <div><pre class=\"shadow text-primary border-[1px] border-primary rounded-md inline-block whitespace-pre-wrap font-roboto px-2\">&lt;!--example--&gt;\n&lt;link rel=\"preconnect\" href=\"https://your-website.com\"&gt;</pre></div>",
                                         "Add &lt;link rel=\"dns-prefetch\"&gt; as a fallback to preconnect. <div><pre class=\"shadow text-primary border-[1px] border-primary rounded-md inline-block whitespace-pre-wrap font-roboto px-2\">&lt;!--example--&gt;\n&lt;link rel=\"dns-prefetch\" href=\"https://your-website.com\"&gt;</pre></div>"
                                     }));

        //dictionary.Add(typeof(UsesRelPreload), new Audit("Preload key requests",
        //                             "Consider preload links to prioritise fetching resources to make your pages load faster.",
        //                             new List<string>
        //                             {
        //                                "Declare preload links in your HTML to instruct the browser to download key resources as soon as possible. For example, <link rel=\"preload\" href=\"styles.css\" as=\"style\" />"
        //                             }));

        dictionary.Add(typeof(UnminifiedJavascript), new Audit("Minify JavaScript",
                                     "Minifying JavaScript files can reduce payload sizes and script parse time.",
                                     new List<string>
                                     {
                                        "Use JavaScript compression tool such as Terser, which is a popular JavaScript compressoin tool."
                                     }));

        dictionary.Add(typeof(UnminifiedCss), new Audit("Minify CSS",
                                     "Minifying CSS files can improve your page load performance. CSS files are often larger than they need to be.",
                                     new List<string>
                                     {
                                        "Use a CSS minifier to minify your CSS code using an online service, or automated tools such as Gulp or Webpack."
                                     }));

        dictionary.Add(typeof(UnusedCssRules), new Audit("Reduce unused CSS",
                                     "Reduce unused rules from stylesheets to improve website performance.",
                                     new List<string>
                                     {
                                        "Inline critical styles inside &lt;style&gt; block at the head of the HTML page.",
                                        "Load non-critical stylesheets asynchronously using the preload link."
                                     }));

        dictionary.Add(typeof(PrioritizeLcpImage), new Audit("Preload Largest Contentful Paint image",
                                     "If the LCP element is dynamically added to the page, you should preload the image in order to improve LCP. LCP represents how quickly the main content of a web page is loaded",
                                     new List<string>
                                     {
                                        "Load the stylesheet that will reference the LCP images. <div><pre class=\"shadow text-primary border-[1px] border-primary rounded-md inline-block whitespace-pre-wrap font-roboto px-2\">&lt;!--example--&gt;\n&lt;link rel=\"stylesheet\" href=\"/path/to/styles.css\"&gt;</pre></div>",
                                        "Preload the LCP image with a high fetchpriority so it starts loading with the stylesheet. <div><pre class=\"shadow text-primary border-[1px] border-primary rounded-md inline-block whitespace-pre-wrap font-roboto px-2\">&lt;!--example--&gt;\n&lt;link rel=\"preload\" fetchpriority=\"high\" as=\"image\" href=\"/path/to/hero-image.webp\" type=\"image/webp\"&gt;</pre></div>"
                                     }));

        dictionary.Add(typeof(UnusedJavascript), new Audit("Reduce unused JavaScript",
                                     "Unused JavaScript can slow down your page load speed so reduce unused JavaScript and defer loading scripts until they are required.",
                                     new List<string>
                                     {
                                        "Use the <a class=\"text-primary hover:text-primary-hover underline\" href=\"https://developer.chrome.com/docs/devtools/css/reference/#coverage\">Coverage tab</a> which can give you a line-by-line breakdown of unused code."
                                     }));

        dictionary.Add(typeof(EfficientAnimatedContent), new Audit("Use video formats for animated content",
                                     "Large GIFs are inefficient for delivering animated content. By converting large GIFs to videos, you can save big on users' bandwidth.",
                                     new List<string>
                                     {
                                        "Consider using MPEG4/WebM videos for animations and GIF. For MPEG/WebM <a class=\"text-primary hover:text-primary-hover underline\" href=\"https://ffmpeg.org/\">FFmpeg</a> is recommended.",
                                        "Consider using PNG/WebP for static images instead of GIF. Static images can be converted to WebP with <a class=\"text-primary hover:text-primary-hover underline\" href=\"/Images/Optimise\">Image Optimisation</a> feature of PWA Converter."
                                     }));

        dictionary.Add(typeof(DuplicatedJavascript), new Audit("Remove duplicate modules in JavaScript bundles",
                                     "Remove large, duplicate JavaScript modules from bundles to improve website speed.",
                                     new List<string>
                                     {
                                        "Use a module bundler like Webpack "
                                     }));

        dictionary.Add(typeof(LegacyJavascript), new Audit("Avoid serving legacy JavaScript to modern browsers",
                                     "JavaScript technologies such as Polyfills and transforms enable legacy browsers to use new JavaScript features. However, many aren't necessary for modern browsers",
                                     new List<string>
                                     {
                                        "For your bundled JavaScript, adopt a modern script deployment strategy using module/nomodule feature detection to reduce the amount of code shipped to modern browsers, while retaining support for legacy browsers."
                                     }));

        LighthouseAuditResults[Category.Performance][Type.Opportunity] = dictionary;
    }

    private void AddAllCategoryPerformanceTypeDiagnostics()
    {
        var dictionary = LighthouseAuditResults[Category.Performance][Type.Diagnostics];

        dictionary.Add(typeof(FontDisplay), new Audit("All text remains visible during webfont loads",
                                    "Fonts are often large files with slow load times. Some browsers hide text until the font loads",
                                    new List<string>
                                    {
                                        "Include font-display: swap in your @font-face style",
                                        "Use <pre class=\"shadow text-primary border-[1px] border-primary rounded-md whitespace-pre-wrap font-roboto px-2 inline-block\">&lt;link rel=\"preload\" as=\"font\"&gt;</pre> to preload your font files"
                                    }));

        dictionary.Add(typeof(LargestContentfulPaintElement), new Audit("Largest Contentful Paint element",
                                     "LCP measures when the largest content element in the web page is rendered to the screen",
                                     new List<string>
                                     {
                                        "Learn more at https://web.dev/optimize-lcp/#how-to-optimize-each-part"
                                     }));

        dictionary.Add(typeof(TotalByteWeight), new Audit("Avoids enormous network payloads",
                                     "Large network payloads are highly correlated with long load times and they also cost users money. So, reducing the total size of your page's network requests is good for your users' experience on your site and their wallets.",
                                     new List<string>
                                     {
                                        "Defer requests until they're needed.",
                                        "Optimize requests to be as small as possible. For images, convert to WebP with <a class=\"text-primary hover:text-primary-hover underline\" href=\"/Images/Optimise\">Image Optimisation</a> feature of PWA Converter.",
                                        "Cache requests. Create a service worker by converting your website into a PWA using <a class=\"text-primary hover:text-primary-hover underline\" href=\"/Pwa/ICon\">PWA Converter</a>"
                                     }));

        dictionary.Add(typeof(LcpLazyLoaded), new Audit("Largest Contentful Paint image was not lazily loaded",
                                     "Lazy loading is a technique to defer downloading a resource until it's needed, which can delay the largest contentful paint (LCP).",
                                     new List<string>
                                     {
                                        "Add loading=\"lazy\" for your images. <div><pre class=\"shadow text-primary border-[1px] border-primary rounded-md inline-block whitespace-pre-wrap font-roboto px-2\">&lt;!--example--&gt; \n&lt;img src=\"image.jpg\" alt=\"...\" loading=\"lazy\" /&gt;</pre></div>"
                                     }));

        dictionary.Add(typeof(ThirdPartySummary), new Audit("Reduce the impact of third-party code",
                                     "Third-party code can significantly impact load performance.",
                                     new List<string>
                                     {
                                       "Limit the number of redundant third-party providers.",
                                       "Load the script using the async or defer attribute to avoid blocking document parsing.",
                                       "Consider self-hosting the script if the third-party server is slow.",
                                       "Consider Resource Hints like &lt;link rel=preconnect&gt; or &lt;link rel=dns-prefetch&gt; to perform a DNS lookup for domains hosting third-party scripts."
                                     }));

        //dictionary.Add(typeof(ThirdPartyFacades), new Audit("Lazy load third-party resources with facades",
        //                             "Some third-party embeds can be lazy loaded.",
        //                             new List<string>
        //                             {
        //                                "Consider replacing them with a facade until they are required. For recommended facades go to https://developer.chrome.com/docs/lighthouse/performance/third-party-facades/#recommended-facades"
        //                             }));

        dictionary.Add(typeof(BootupTime), new Audit("Reduce JavaScript execution time",
                                     "When your JavaScript takes a long time to execute, it slows down your page performance",
                                     new List<string>
                                     {
                                        "Only send necessary code by implementing code splitting. <a class=\"text-primary hover:text-primary-hover underline\" href=\"https://web.dev/reduce-javascript-payloads-with-code-splitting/\">Learn More</a>",
                                        "Minify and compress your code.",
                                        "Remove unused code.",
                                        "Cache your code with the PRPL pattern to reduce network trips. <a class=\"text-primary hover:text-primary-hover underline\" href=\"https://web.dev/apply-instant-loading-with-prpl/\">Learn More</a>"
                                     }));

        dictionary.Add(typeof(DomSize), new Audit("Avoid an excessive DOM size",
                                     "A large DOM will increase memory usage, cause longer style calculations, and may produce slower layout rendering.",
                                     new List<string>
                                     {
                                        "Remove the unnecessary tags and refactor or rewrite some parts of the code.",
                                        "Reduce the complexity of your selectors.",
                                        "Reduce the number of elements on which style calculation must be calculated."
                                     }));

        dictionary.Add(typeof(UnsizedImages), new Audit("Image elements have explicit width and height",
                                     "Layout shift occurs when a visible element on your page changes position or size, affecting the position of content around it",
                                     new List<string>
                                     {
                                        "Set an explicit width and height on image elements to reduce layout shifts and improve CLS.",
                                        "Reserve the required space with CSS aspect-ratio."
                                     }));

        LighthouseAuditResults[Category.Performance][Type.Diagnostics] = dictionary;
    }

    private void AddAllCategoryPwaTypeInstallable()
    {
        var dictionary = LighthouseAuditResults[Category.Pwa][Type.Installable];
        dictionary.Add(typeof(InstallableManifest), new Audit("Web app manifest and service worker meet the installability requirements",
                                                         "Service worker is the technology that enables your app to use many Progressive Web App features, such as offline, add to homescreen, and push notifications. Web app manifest is a file that provides information about a web application",
                                                         new List<string> { 
                                                             "Make sure your manifest includes a short_name or name properties, icons property with a 192 x 192 px and a 512 x 512 px icon, start_url property, and display property",
                                                         }));
        //dictionary.Add("hasName", new Audit("Web app manifest's has a name property",
        //                                         "Name represents the name of the web application",
        //                                         new List<string>
        //                                         {
        //                                                     "Add a name property to your web app manifest"
        //                                         }));
        //dictionary.Add("hasShortName", new Audit("Web app manifest has a short name property",
        //                                         "Short name gets displayed when there is no sufficient space to display the name",
        //                                         new List<string>
        //                                         {
        //                                             "Add a short name property to your web app manifest"
        //                                         }));
        //dictionary.Add("shortNameLength", new Audit("Web app manifest's short name is short",
        //                                                 "Web app manifest's short name should not exceed 12 characters",
        //                                                 new List<string>
        //                                                 {
        //                                                     "Shorten the length of short name"
        //                                                 }));
        //dictionary.Add("hasPWADisplayValue", new Audit("Web app manifest has a display property",
        //                                         "Display specifies the looks of the website when launched as a PWA.",
        //                                         new List<string>
        //                                         {
        //                                             "Add a display property to your web app manifest"
        //                                         }));
        //dictionary.Add("hasStartUrl", new Audit("Web app manifest has a start_url property",
        //                                         "Start URL tells the browser  where your application should start when it is launched.",
        //                                         new List<string>
        //                                         {
        //                                             "Add a start_url property to your web app manifest"
        //                                         }));
        LighthouseAuditResults[Category.Pwa][Type.Installable] = dictionary;
    }

    private void AddAllCategoryPwaTypePwaOptimized()
    {
        var dictionary = LighthouseAuditResults[Category.Pwa][Type.PwaOptimized];
        dictionary.Add(typeof(SplashScreen), new Audit("Configured for a custom splash screen",
                                                         "A custom splash screen makes your Progressive Web App (PWA) feel more like an app built for that device",
                                                         new List<string>
                                                         {
                                                             "Add name property, background_color property, theme_color property, and icons property to your web app manifest"
                                                         }));
        dictionary.Add(typeof(ThemedOmnibox), new Audit("Sets a theme color for the address bar",
                                                         "The browser address bar can be themed to match your site.",
                                                         new List<string>
                                                         {
                                                            "Add a theme-color meta tag to the &lt;head&gt; of every page. <div><pre class=\"shadow text-primary border-[1px] border-primary rounded-md inline-block whitespace-pre-wrap font-roboto px-2\">&lt;!--example--&gt; \n&lt;meta name=\"theme-color\" content=\"#317EFB\"/&gt;</pre></div>",
                                                            "Add a theme_color property to your web app manifest."
                                                         }));
        dictionary.Add(typeof(ContentWidth), new Audit("Content is sized correctly for the viewport",
                                                         "The viewport is the part of the browser window in which your page's content is visible. If the width of your app's content doesn't match the width of the viewport, your app might not be optimised for mobile screens.",
                                                         new List<string>
                                                         {
                                                             "Make your website responsive. <a class=\"text-primary hover:text-primary-hover underline\" href=\"https://web.dev/responsive-web-design-basics/\">Learn More</a>"
                                                         }));
        dictionary.Add(typeof(Viewport), new Audit("Has a <meta name=\"viewport\"> tag with width or initial-scale",
                                                         "A <meta name=\"viewport\"> optimises your app for mobile screen sizes",
                                                         new List<string>
                                                         {
                                                             "Add a viewport &lt;meta&gt; tag with the appropriate key-value pairs to the &lt;head&gt; of your page. <div><pre class=\"shadow text-primary border-[1px] border-primary rounded-md inline-block whitespace-pre-wrap font-roboto px-2\">&lt;!--example--&gt;\n&lt;meta name =\"viewport\" content=\"width=device-width, initial-scale=1\"&gt;"
                                                         }));
        dictionary.Add(typeof(MaskableIcon), new Audit("Manifest has a maskable icon",
                                                         "Maskable icons is a new icon format that ensures that your PWA icon looks great on all Android devices.",
                                                         new List<string>
                                                         {
                                                             "Use <a class=\"text-primary hover:text-primary-hover underline\" href=\"https://maskable.app/editor\">Makable.app</a> to convert an existing icon to a maskable icon.",
                                                             "Add the Purpose property to one of the icons objects in your web app manifest and set the value of purpose to maskable or any maskable."
                                                         }));

        //dictionary.Add("hasBackgroundColor", new Audit("Web app manifest has a background_color property",
        //                                                 "Background color is used on the splash screen or before the styles for the app are loaded.",
        //                                                 new List<string>
        //                                                 {
        //                                                     "Add a background_color property to your web app manifest"
        //                                                 }));
        //dictionary.Add("hasThemeColor", new Audit("Web app manifest has a theme_color property",
        //                                                 "Theme color sets the default theme colour for the application. It gets used in the toolbar for instance.",
        //                                                 new List<string>
        //                                                 {
        //                                                     "Add a theme_color property to your web app manifest"
        //                                                 }));
        LighthouseAuditResults[Category.Pwa][Type.PwaOptimized] = dictionary;
    }
}
