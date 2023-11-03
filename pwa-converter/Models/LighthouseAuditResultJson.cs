using Newtonsoft.Json;

namespace pwa_converter.Models
{
        public class LighthouseAuditResultJson
        {
            public string lighthouseVersion { get; set; }
            public string requestedUrl { get; set; }
            public string mainDocumentUrl { get; set; }
            public string finalDisplayedUrl { get; set; }
            public string finalUrl { get; set; }
            public DateTime fetchTime { get; set; }
            public object[] runWarnings { get; set; }
            public Audits audits { get; set; }
            public Categories categories { get; set; }
        }

        public class Audits
        {
            public Viewport viewport { get; set; }
            [JsonProperty("first-contentful-paint")]
            public FirstContentfulPaint firstcontentfulpaint { get; set; }
            [JsonProperty("largest-contentful-paint")]
            public LargestContentfulPaint largestcontentfulpaint { get; set; }
            [JsonProperty("server-response-time")]
            public ServerResponseTime serverresponsetime { get; set; }
            [JsonProperty("critical-request-chains")]
            public CriticalRequestChains criticalrequestchains { get; set; }
            public Redirects redirects { get; set; }
            [JsonProperty("installable-manifest")]
            public InstallableManifest installablemanifest { get; set; }
            [JsonProperty("splash-screen")]
            public SplashScreen splashscreen { get; set; }
            [JsonProperty("themed-omnibox")]
            public ThemedOmnibox themedomnibox { get; set; }
            [JsonProperty("maskable-icon")]
            public MaskableIcon maskableicon { get; set; }
            [JsonProperty("content-width")]
            public ContentWidth contentwidth { get; set; }
            [JsonProperty("mainthread-work-breakdown")]
            public MainthreadWorkBreakdown mainthreadworkbreakdown { get; set; }
            [JsonProperty("bootup-time")]
            public BootupTime bootuptime { get; set; }
            [JsonProperty("uses-rel-preload")]
            public UsesRelPreload usesrelpreload { get; set; }
            [JsonProperty("uses-rel-preconnect")]
            public UsesRelPreconnect usesrelpreconnect { get; set; }
            [JsonProperty("font-display")]
            public FontDisplay fontdisplay { get; set; }
            public Diagnostics diagnostics { get; set; }
            public NetworkRequests networkrequests { get; set; }
            public Metrics metrics { get; set; }
            [JsonProperty("third-party-summary")]
            public ThirdPartySummary thirdpartysummary { get; set; }
            [JsonProperty("third-party-facades")]
            public ThirdPartyFacades thirdpartyfacades { get; set; }
            [JsonProperty("largest-contentful-paint-element")]
            public LargestContentfulPaintElement largestcontentfulpaintelement { get; set; }
            [JsonProperty("lcp-lazy-loaded")]
            public LcpLazyLoaded lcplazyloaded { get; set; }
            [JsonProperty("layout-shift-elements")]
            public LayoutShiftElements layoutshiftelements { get; set; }
            [JsonProperty("long-tasks")]
            public LongTasks longtasks { get; set; }
            [JsonProperty("total-byte-weight")]
            public TotalByteWeight totalbyteweight { get; set; }
            [JsonProperty("non-composited-animations")]
            public NonCompositedAnimations noncompositedanimations { get; set; }
            [JsonProperty("unsized-images")]
            public UnsizedImages unsizedimages { get; set; }
            [JsonProperty("prioritize-lcp-image")]
            public PrioritizeLcpImage prioritizelcpimage { get; set; }
            [JsonProperty("pwa-cross-browser")]
            public PwaCrossBrowser pwacrossbrowser { get; set; }
            [JsonProperty("pwa-page-transitions")]
            public PwaPageTransitions pwapagetransitions { get; set; }
            [JsonProperty("pwa-each-page-has-url")]
            public PwaEachPageHasUrl pwaeachpagehasurl { get; set; }
            [JsonProperty("render-blocking-resources")]
            public RenderBlockingResources renderblockingresources { get; set; }
            [JsonProperty("unminified-css")]
            public UnminifiedCss unminifiedcss { get; set; }
            [JsonProperty("unminified-javascript")]
            public UnminifiedJavascript unminifiedjavascript { get; set; }
            [JsonProperty("unused-css-rules")]
            public UnusedCssRules unusedcssrules { get; set; }
            [JsonProperty("unused-javascript")]
            public UnusedJavascript unusedjavascript { get; set; }
            [JsonProperty("uses-text-compression")]
            public UsesTextCompression usestextcompression { get; set; }
            [JsonProperty("efficient-animated-content")]
            public EfficientAnimatedContent efficientanimatedcontent { get; set; }
            [JsonProperty("duplicated-javascript")]
            public DuplicatedJavascript duplicatedjavascript { get; set; }
            [JsonProperty("legacy-javascript")]
            public LegacyJavascript legacyjavascript { get; set; }
            [JsonProperty("dom-size")]
            public DomSize domsize { get; set; }
        }

        public class Viewport
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public object[] warnings { get; set; }
        }

        public class FirstContentfulPaint
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public float numericValue { get; set; }
            public string numericUnit { get; set; }
            public string displayValue { get; set; }
        }

        public class LargestContentfulPaint
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public float numericValue { get; set; }
            public string numericUnit { get; set; }
            public string displayValue { get; set; }
        }

        public class ServerResponseTime
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public float numericValue { get; set; }
            public string numericUnit { get; set; }
            public string displayValue { get; set; }
            public Details3 details { get; set; }
        }

        public class Details3
        {
            public string type { get; set; }
            public Heading[] headings { get; set; }
            public Item2[] items { get; set; }
            public double overallSavingsMs { get; set; }
        }

        public class Heading
        {
            public string key { get; set; }
            public string valueType { get; set; }
            public string label { get; set; }
        }

        public class Item2
        {
            public string url { get; set; }
            public float responseTime { get; set; }
        }
  
        public class CriticalRequestChains
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public string displayValue { get; set; }
            public Details5 details { get; set; }
        }

        public class Details5
        {
            public string type { get; set; }
            public Chains chains { get; set; }
            public Longestchain longestChain { get; set; }
        }

        public class Chains
        {
            public C933FD840A11BA92696DC5AAB89D6EBE C933FD840A11BA92696DC5AAB89D6EBE { get; set; }
        }

        public class C933FD840A11BA92696DC5AAB89D6EBE
        {
            public Request request { get; set; }
            public Children children { get; set; }
        }

        public class Request
        {
            public string url { get; set; }
            public float startTime { get; set; }
            public float endTime { get; set; }
            public float responseReceivedTime { get; set; }
            public int transferSize { get; set; }
        }

        public class Children
        {
            public _229082 _229082 { get; set; }
        }

        public class _229082
        {
            public Request1 request { get; set; }
        }

        public class Request1
        {
            public string url { get; set; }
            public float startTime { get; set; }
            public float endTime { get; set; }
            public float responseReceivedTime { get; set; }
            public int transferSize { get; set; }
        }

        public class Longestchain
        {
            public float duration { get; set; }
            public int length { get; set; }
            public int transferSize { get; set; }
        }

        public class Redirects
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public double numericValue { get; set; }
            public string numericUnit { get; set; }
            public string displayValue { get; set; }
            public Details6 details { get; set; }
        }

        public class Details6
        {
            public string type { get; set; }
            public object[] headings { get; set; }
            public object[] items { get; set; }
            public double overallSavingsMs { get; set; }
        }

        public class InstallableManifest
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public object[] warnings { get; set; }
            public Details7 details { get; set; }
        }

        public class Details7
        {
            public string type { get; set; }
            public object[] headings { get; set; }
            public object[] items { get; set; }
            public Debugdata debugData { get; set; }
        }

        public class Debugdata
        {
            public string type { get; set; }
            public string manifestUrl { get; set; }
        }

        public class SplashScreen
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public Details8 details { get; set; }
        }

        public class Details8
        {
            public string type { get; set; }
            public Item3[] items { get; set; }
        }

        public class Item3
        {
            public object[] failures { get; set; }
            public bool isParseFailure { get; set; }
            public bool hasStartUrl { get; set; }
            public bool hasIconsAtLeast144px { get; set; }
            public bool hasIconsAtLeast512px { get; set; }
            public bool fetchesIcon { get; set; }
            public bool hasPWADisplayValue { get; set; }
            public bool hasBackgroundColor { get; set; }
            public bool hasThemeColor { get; set; }
            public bool hasShortName { get; set; }
            public bool shortNameLength { get; set; }
            public bool hasName { get; set; }
            public bool hasMaskableIcon { get; set; }
        }

        public class ThemedOmnibox
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public string explanation { get; set; }
            public Details9 details { get; set; }
        }

        public class Details9
        {
            public string type { get; set; }
            public Item4[] items { get; set; }
        }

        public class Item4
        {
            public string[] failures { get; set; }
            public object themeColor { get; set; }
            public bool isParseFailure { get; set; }
            public bool hasStartUrl { get; set; }
            public bool hasIconsAtLeast144px { get; set; }
            public bool hasIconsAtLeast512px { get; set; }
            public bool fetchesIcon { get; set; }
            public bool hasPWADisplayValue { get; set; }
            public bool hasBackgroundColor { get; set; }
            public bool hasThemeColor { get; set; }
            public bool hasShortName { get; set; }
            public bool shortNameLength { get; set; }
            public bool hasName { get; set; }
            public bool hasMaskableIcon { get; set; }
        }

        public class MaskableIcon
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
        }

        public class ContentWidth
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
        }

        public class MainthreadWorkBreakdown
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public float numericValue { get; set; }
            public string numericUnit { get; set; }
            public string displayValue { get; set; }
            public Details10 details { get; set; }
        }

        public class Details10
        {
            public string type { get; set; }
            public Heading1[] headings { get; set; }
            public Item5[] items { get; set; }
            public string[] sortedBy { get; set; }
        }

        public class Heading1
        {
            public string key { get; set; }
            public string valueType { get; set; }
            public string label { get; set; }
            public int granularity { get; set; }
        }

        public class Item5
        {
            public string group { get; set; }
            public string groupLabel { get; set; }
            public float duration { get; set; }
        }

        public class BootupTime
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public float numericValue { get; set; }
            public string numericUnit { get; set; }
            public string displayValue { get; set; }
            public Details11 details { get; set; }
        }

        public class Details11
        {
            public string type { get; set; }
            public Heading2[] headings { get; set; }
            public Item6[] items { get; set; }
            public Summary summary { get; set; }
            public string[] sortedBy { get; set; }
        }

        public class Summary
        {
            public float wastedMs { get; set; }
        }

        public class Heading2
        {
            public string key { get; set; }
            public string valueType { get; set; }
            public string label { get; set; }
            public int granularity { get; set; }
        }

        public class Item6
        {
            public string url { get; set; }
            public float total { get; set; }
            public float scripting { get; set; }
            public float scriptParseCompile { get; set; }
        }

        public class UsesRelPreload
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public Details12 details { get; set; }
        }

        public class Details12
        {
            public string type { get; set; }
            public object[] headings { get; set; }
            public object[] items { get; set; }
            public double overallSavingsMs { get; set; }
        }

        public class UsesRelPreconnect
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public double numericValue { get; set; }
            public string numericUnit { get; set; }
            public string displayValue { get; set; }
            public object[] warnings { get; set; }
            public Details13 details { get; set; }
        }

        public class Details13
        {
            public string type { get; set; }
            public object[] headings { get; set; }
            public object[] items { get; set; }
            public double overallSavingsMs { get; set; }
            public string[] sortedBy { get; set; }
        }

        public class FontDisplay
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public object[] warnings { get; set; }
            public Details14 details { get; set; }
        }

        public class Details14
        {
            public string type { get; set; }
            public object[] headings { get; set; }
            public object[] items { get; set; }
        }

        public class Diagnostics
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public Details15 details { get; set; }
        }

        public class Details15
        {
            public string type { get; set; }
            public Item7[] items { get; set; }
        }

        public class Item7
        {
            public int numRequests { get; set; }
            public int numScripts { get; set; }
            public int numStylesheets { get; set; }
            public int numFonts { get; set; }
            public int numTasks { get; set; }
            public int numTasksOver10ms { get; set; }
            public int numTasksOver25ms { get; set; }
            public int numTasksOver50ms { get; set; }
            public int numTasksOver100ms { get; set; }
            public int numTasksOver500ms { get; set; }
            public float rtt { get; set; }
            public float throughput { get; set; }
            public float maxRtt { get; set; }
            public float maxServerLatency { get; set; }
            public int totalByteWeight { get; set; }
            public float totalTaskTime { get; set; }
            public int mainDocumentTransferSize { get; set; }
        }

        public class NetworkRequests
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public Details16 details { get; set; }
        }

        public class Details16
        {
            public string type { get; set; }
            public Heading3[] headings { get; set; }
            public Item8[] items { get; set; }
            public Debugdata1 debugData { get; set; }
        }

        public class Debugdata1
        {
            public string type { get; set; }
            public float networkStartTimeTs { get; set; }
        }

        public class Heading3
        {
            public string key { get; set; }
            public string valueType { get; set; }
            public string label { get; set; }
            public int granularity { get; set; }
            public string displayUnit { get; set; }
        }

        public class Item8
        {
            public string url { get; set; }
            public string sessionTargetType { get; set; }
            public string protocol { get; set; }
            public float rendererStartTime { get; set; }
            public float networkRequestTime { get; set; }
            public float networkEndTime { get; set; }
            public bool finished { get; set; }
            public int transferSize { get; set; }
            public int resourceSize { get; set; }
            public int statusCode { get; set; }
            public string mimeType { get; set; }
            public string resourceType { get; set; }
            public string priority { get; set; }
            public bool experimentalFromMainFrame { get; set; }
            public string entity { get; set; }
        }

        public class Metrics
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public double numericValue { get; set; }
            public string numericUnit { get; set; }
            public Details20 details { get; set; }
        }

        public class Details20
        {
            public string type { get; set; }
            public Item12[] items { get; set; }
        }

        public class Item12
        {
            public int firstContentfulPaint { get; set; }
            public int firstMeaningfulPaint { get; set; }
            public int largestContentfulPaint { get; set; }
            public int interactive { get; set; }
            public int speedIndex { get; set; }
            public int totalBlockingTime { get; set; }
            public int maxPotentialFID { get; set; }
            public double cumulativeLayoutShift { get; set; }
            public double cumulativeLayoutShiftMainFrame { get; set; }
            public int lcpLoadStart { get; set; }
            public int lcpLoadEnd { get; set; }
            public int timeToFirstByte { get; set; }
            public int observedTimeOrigin { get; set; }
            public long observedTimeOriginTs { get; set; }
            public int observedNavigationStart { get; set; }
            public long observedNavigationStartTs { get; set; }
            public int observedFirstPaint { get; set; }
            public long observedFirstPaintTs { get; set; }
            public int observedFirstContentfulPaint { get; set; }
            public long observedFirstContentfulPaintTs { get; set; }
            public int observedFirstContentfulPaintAllFrames { get; set; }
            public long observedFirstContentfulPaintAllFramesTs { get; set; }
            public int observedFirstMeaningfulPaint { get; set; }
            public long observedFirstMeaningfulPaintTs { get; set; }
            public int observedLargestContentfulPaint { get; set; }
            public long observedLargestContentfulPaintTs { get; set; }
            public int observedLargestContentfulPaintAllFrames { get; set; }
            public long observedLargestContentfulPaintAllFramesTs { get; set; }
            public int observedTraceEnd { get; set; }
            public long observedTraceEndTs { get; set; }
            public int observedLoad { get; set; }
            public long observedLoadTs { get; set; }
            public int observedDomContentLoaded { get; set; }
            public long observedDomContentLoadedTs { get; set; }
            public double observedCumulativeLayoutShift { get; set; }
            public double observedCumulativeLayoutShiftMainFrame { get; set; }
            public int observedFirstVisualChange { get; set; }
            public long observedFirstVisualChangeTs { get; set; }
            public int observedLastVisualChange { get; set; }
            public long observedLastVisualChangeTs { get; set; }
            public int observedSpeedIndex { get; set; }
            public long observedSpeedIndexTs { get; set; }
            public bool lcpInvalidated { get; set; }
        }

        public class ThirdPartySummary
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
        }

        public class ThirdPartyFacades
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
        }

        public class LargestContentfulPaintElement
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public string displayValue { get; set; }
            public Details21 details { get; set; }
        }

        public class Details21
        {
            public string type { get; set; }
            public Item13[] items { get; set; }
        }

        public class Item13
        {
            public string type { get; set; }
            public Heading7[] headings { get; set; }
            public Item14[] items { get; set; }
        }

        public class Heading7
        {
            public string key { get; set; }
            public string valueType { get; set; }
            public string label { get; set; }
        }

        public class Item14
        {
            public Node node { get; set; }
            public string phase { get; set; }
            public float timing { get; set; }
            public string percent { get; set; }
        }

        public class Node
        {
            public string type { get; set; }
            public string lhId { get; set; }
            public string path { get; set; }
            public string selector { get; set; }
            public Boundingrect boundingRect { get; set; }
            public string snippet { get; set; }
            public string nodeLabel { get; set; }
        }

        public class Boundingrect
        {
            public int top { get; set; }
            public int bottom { get; set; }
            public int left { get; set; }
            public int right { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class LcpLazyLoaded
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public Details22 details { get; set; }
        }

        public class Details22
        {
            public string type { get; set; }
            public Heading8[] headings { get; set; }
            public Item15[] items { get; set; }
        }

        public class Heading8
        {
            public string key { get; set; }
            public string valueType { get; set; }
            public string label { get; set; }
        }

        public class Item15
        {
            public Node1 node { get; set; }
        }

        public class Node1
        {
            public string type { get; set; }
            public string lhId { get; set; }
            public string path { get; set; }
            public string selector { get; set; }
            public Boundingrect1 boundingRect { get; set; }
            public string snippet { get; set; }
            public string nodeLabel { get; set; }
        }

        public class Boundingrect1
        {
            public int top { get; set; }
            public int bottom { get; set; }
            public int left { get; set; }
            public int right { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class LayoutShiftElements
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public Details23 details { get; set; }
        }

        public class Details23
        {
            public string type { get; set; }
            public object[] headings { get; set; }
            public object[] items { get; set; }
        }

        public class LongTasks
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public string displayValue { get; set; }
            public Details24 details { get; set; }
        }

        public class Details24
        {
            public string type { get; set; }
            public Heading9[] headings { get; set; }
            public Item16[] items { get; set; }
            public string[] sortedBy { get; set; }
            public string[] skipSumming { get; set; }
            public Debugdata2 debugData { get; set; }
        }

        public class Debugdata2
        {
            public string type { get; set; }
            public string[] urls { get; set; }
            public Task[] tasks { get; set; }
        }

        public class Task
        {
            public int urlIndex { get; set; }
            public float startTime { get; set; }
            public float duration { get; set; }
            public int other { get; set; }
            public int paintCompositeRender { get; set; }
        }

        public class Heading9
        {
            public string key { get; set; }
            public string valueType { get; set; }
            public string label { get; set; }
            public int granularity { get; set; }
        }

        public class Item16
        {
            public string url { get; set; }
            public float duration { get; set; }
            public float startTime { get; set; }
        }

        public class NonCompositedAnimations
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public Details25 details { get; set; }
        }

        public class Details25
        {
            public string type { get; set; }
            public object[] headings { get; set; }
            public object[] items { get; set; }
        }

        public class UnsizedImages
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public Details26 details { get; set; }
        }

        public class Details26
        {
            public string type { get; set; }
            public object[] headings { get; set; }
            public object[] items { get; set; }
        }

        public class PrioritizeLcpImage
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public double numericValue { get; set; }
            public string numericUnit { get; set; }
            public string displayValue { get; set; }
            public Details27 details { get; set; }
        }

        public class Details27
        {
            public string type { get; set; }
            public object[] headings { get; set; }
            public object[] items { get; set; }
            public double overallSavingsMs { get; set; }
            public string[] sortedBy { get; set; }
            public Debugdata3 debugData { get; set; }
        }

        public class Debugdata3
        {
            public string type { get; set; }
            public Initiatorpath[] initiatorPath { get; set; }
            public int pathLength { get; set; }
        }

        public class Initiatorpath
        {
            public string url { get; set; }
            public string initiatorType { get; set; }
        }

        public class Details28
        {
            public string type { get; set; }
            public Node2[] nodes { get; set; }
        }

        public class Node2
        {
            public string name { get; set; }
            public int resourceBytes { get; set; }
            public int unusedBytes { get; set; }
        }

        public class PwaCrossBrowser
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
        }

        public class PwaPageTransitions
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
        }

        public class PwaEachPageHasUrl
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
        }

        public class RenderBlockingResources
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public double numericValue { get; set; }
            public string numericUnit { get; set; }
            public Details32 details { get; set; }
        }

        public class Details32
        {
            public string type { get; set; }
            public object[] headings { get; set; }
            public object[] items { get; set; }
            public double overallSavingsMs { get; set; }
        }

        public class UnminifiedCss
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public double numericValue { get; set; }
            public string numericUnit { get; set; }
            public string displayValue { get; set; }
            public Details33 details { get; set; }
        }

        public class Details33
        {
            public string type { get; set; }
            public object[] headings { get; set; }
            public object[] items { get; set; }
            public double overallSavingsMs { get; set; }
            public int overallSavingsBytes { get; set; }
            public string[] sortedBy { get; set; }
            public Debugdata6 debugData { get; set; }
        }

        public class Debugdata6
        {
            public string type { get; set; }
            public Metricsavings1 metricSavings { get; set; }
        }

        public class Metricsavings1
        {
            public int FCP { get; set; }
            public int LCP { get; set; }
        }

        public class UnminifiedJavascript
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public double numericValue { get; set; }
            public string numericUnit { get; set; }
            public string displayValue { get; set; }
            public object[] warnings { get; set; }
            public Details34 details { get; set; }
        }

        public class Details34
        {
            public string type { get; set; }
            public object[] headings { get; set; }
            public object[] items { get; set; }
            public double overallSavingsMs { get; set; }
            public int overallSavingsBytes { get; set; }
            public string[] sortedBy { get; set; }
            public Debugdata7 debugData { get; set; }
        }

        public class Debugdata7
        {
            public string type { get; set; }
            public Metricsavings2 metricSavings { get; set; }
        }

        public class Metricsavings2
        {
            public int FCP { get; set; }
            public int LCP { get; set; }
        }

        public class UnusedCssRules
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public double numericValue { get; set; }
            public string numericUnit { get; set; }
            public string displayValue { get; set; }
            public Details35 details { get; set; }
        }

        public class Details35
        {
            public string type { get; set; }
            public object[] headings { get; set; }
            public object[] items { get; set; }
            public double overallSavingsMs { get; set; }
            public int overallSavingsBytes { get; set; }
            public string[] sortedBy { get; set; }
            public Debugdata8 debugData { get; set; }
        }

        public class Debugdata8
        {
            public string type { get; set; }
            public Metricsavings3 metricSavings { get; set; }
        }

        public class Metricsavings3
        {
            public int FCP { get; set; }
            public int LCP { get; set; }
        }

        public class UnusedJavascript
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public double numericValue { get; set; }
            public string numericUnit { get; set; }
            public string displayValue { get; set; }
            public Details36 details { get; set; }
        }

        public class Details36
        {
            public string type { get; set; }
            public object[] headings { get; set; }
            public object[] items { get; set; }
            public double overallSavingsMs { get; set; }
            public int overallSavingsBytes { get; set; }
            public string[] sortedBy { get; set; }
            public Debugdata9 debugData { get; set; }
        }

        public class Debugdata9
        {
            public string type { get; set; }
            public Metricsavings4 metricSavings { get; set; }
        }

        public class Metricsavings4
        {
            public int FCP { get; set; }
            public int LCP { get; set; }
        }

        public class UsesTextCompression
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public double numericValue { get; set; }
            public string numericUnit { get; set; }
            public string displayValue { get; set; }
            public Details39 details { get; set; }
        }

        public class Details39
        {
            public string type { get; set; }
            public object[] headings { get; set; }
            public object[] items { get; set; }
            public double overallSavingsMs { get; set; }
            public int overallSavingsBytes { get; set; }
            public string[] sortedBy { get; set; }
            public Debugdata12 debugData { get; set; }
        }

        public class Debugdata12
        {
            public string type { get; set; }
            public Metricsavings7 metricSavings { get; set; }
        }

        public class Metricsavings7
        {
            public int FCP { get; set; }
            public int LCP { get; set; }
        }

        public class Details40
        {
            public string type { get; set; }
            public Heading13[] headings { get; set; }
            public Item20[] items { get; set; }
            public double overallSavingsMs { get; set; }
            public int overallSavingsBytes { get; set; }
            public string[] sortedBy { get; set; }
            public Debugdata13 debugData { get; set; }
        }

        public class Debugdata13
        {
            public string type { get; set; }
            public Metricsavings8 metricSavings { get; set; }
        }

        public class Metricsavings8
        {
            public int FCP { get; set; }
            public int LCP { get; set; }
        }

        public class Heading13
        {
            public string key { get; set; }
            public string valueType { get; set; }
            public string label { get; set; }
        }

        public class Item20
        {
            public Node4 node { get; set; }
            public string url { get; set; }
            public int totalBytes { get; set; }
            public int wastedBytes { get; set; }
            public float wastedPercent { get; set; }
        }

        public class Node4
        {
            public string type { get; set; }
            public string lhId { get; set; }
            public string path { get; set; }
            public string selector { get; set; }
            public Boundingrect3 boundingRect { get; set; }
            public string snippet { get; set; }
            public string nodeLabel { get; set; }
        }

        public class Boundingrect3
        {
            public int top { get; set; }
            public int bottom { get; set; }
            public int left { get; set; }
            public int right { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class EfficientAnimatedContent
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public double numericValue { get; set; }
            public string numericUnit { get; set; }
            public string displayValue { get; set; }
            public Details41 details { get; set; }
        }

        public class Details41
        {
            public string type { get; set; }
            public object[] headings { get; set; }
            public object[] items { get; set; }
            public double overallSavingsMs { get; set; }
            public int overallSavingsBytes { get; set; }
            public string[] sortedBy { get; set; }
            public Debugdata14 debugData { get; set; }
        }

        public class Debugdata14
        {
            public string type { get; set; }
            public Metricsavings9 metricSavings { get; set; }
        }

        public class Metricsavings9
        {
            public int FCP { get; set; }
            public int LCP { get; set; }
        }

        public class DuplicatedJavascript
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public double numericValue { get; set; }
            public string numericUnit { get; set; }
            public string displayValue { get; set; }
            public Details42 details { get; set; }
        }

        public class Details42
        {
            public string type { get; set; }
            public object[] headings { get; set; }
            public object[] items { get; set; }
            public double overallSavingsMs { get; set; }
            public int overallSavingsBytes { get; set; }
            public string[] sortedBy { get; set; }
            public Debugdata15 debugData { get; set; }
        }

        public class Debugdata15
        {
            public string type { get; set; }
            public Metricsavings10 metricSavings { get; set; }
        }

        public class Metricsavings10
        {
            public int FCP { get; set; }
            public int LCP { get; set; }
        }

        public class LegacyJavascript
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public double numericValue { get; set; }
            public string numericUnit { get; set; }
            public string displayValue { get; set; }
            public Details43 details { get; set; }
        }

        public class Details43
        {
            public string type { get; set; }
            public object[] headings { get; set; }
            public object[] items { get; set; }
            public double overallSavingsMs { get; set; }
            public int overallSavingsBytes { get; set; }
            public string[] sortedBy { get; set; }
            public Debugdata16 debugData { get; set; }
        }

        public class Debugdata16
        {
            public string type { get; set; }
            public Metricsavings11 metricSavings { get; set; }
        }

        public class Metricsavings11
        {
            public int FCP { get; set; }
            public int LCP { get; set; }
        }

        public class DomSize
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double? score { get; set; }
            public string scoreDisplayMode { get; set; }
            public double numericValue { get; set; }
            public string numericUnit { get; set; }
            public string displayValue { get; set; }
            public Details44 details { get; set; }
        }

        public class Details44
        {
            public string type { get; set; }
            public Heading14[] headings { get; set; }
            public Item21[] items { get; set; }
        }

        public class Heading14
        {
            public string key { get; set; }
            public string valueType { get; set; }
            public string label { get; set; }
        }

        public class Item21
        {
            public string statistic { get; set; }
            public Value value { get; set; }
            public Node5 node { get; set; }
        }

        public class Value
        {
            public string type { get; set; }
            public int granularity { get; set; }
            public int value { get; set; }
        }

        public class Node5
        {
            public string type { get; set; }
            public string lhId { get; set; }
            public string path { get; set; }
            public string selector { get; set; }
            public Boundingrect4 boundingRect { get; set; }
            public string snippet { get; set; }
            public string nodeLabel { get; set; }
        }

        public class Boundingrect4
        {
            public int top { get; set; }
            public int bottom { get; set; }
            public int left { get; set; }
            public int right { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Categories
        {
            public Performance performance { get; set; }
            public Pwa pwa { get; set; }
        }

        public class Performance
        {
            public string title { get; set; }
            public string[] supportedModes { get; set; }
            public Auditref[] auditRefs { get; set; }
            public string id { get; set; }
            public double? score { get; set; }
        }

        public class Auditref
        {
            public string id { get; set; }
            public int weight { get; set; }
            public string group { get; set; }
            public string acronym { get; set; }
            public string[] relevantAudits { get; set; }
        }

        public class Pwa
        {
            public string title { get; set; }
            public string description { get; set; }
            public string manualDescription { get; set; }
            public string[] supportedModes { get; set; }
            public Auditref1[] auditRefs { get; set; }
            public string id { get; set; }
            public double? score { get; set; }
        }

        public class Auditref1
        {
            public string id { get; set; }
            public int weight { get; set; }
            public string group { get; set; }
        }

    public class TotalByteWeight
    {
        public string id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public double? score { get; set; }
        public string scoreDisplayMode { get; set; }
        public double numericValue { get; set; }
        public string numericUnit { get; set; }
        public string displayValue { get; set; }
        public Details29 details { get; set; }
    }

    public class Details29
    {
        public string type { get; set; }
        public Heading11[] headings { get; set; }
        public Item18[] items { get; set; }
        public string[] sortedBy { get; set; }
    }

    public class Heading11
    {
        public string key { get; set; }
        public string valueType { get; set; }
        public string label { get; set; }
    }

    public class Item18
    {
        public string url { get; set; }
        public int totalBytes { get; set; }
    }
}