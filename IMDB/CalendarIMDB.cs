using RequestSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MediaApi.IMDB
{
    public class CalendarData
    {
        public Props props { get; set; }
        public string page { get; set; }
        public List<object> scriptLoader { get; set; }
    }

    public class Props
    {
        public PageProps pageProps { get; set; }
    }

    public class PageProps
    {
        public List<Group> groups { get; set; }
        public QueryVariables queryVariables { get; set; }
        public object urqlState { get; set; }
        public object fetchState { get; set; }
    }

    public class Group
    {
        public string group { get; set; }
        public List<Entry> entries { get; set; }
    }

    public class QueryVariables
    {
        public string comingSoonType { get; set; }
        public string regionOverride { get; set; }
        public string countryOverride { get; set; }
        public string releasingOnOrAfter { get; set; }
        public string releasingOnOrBefore { get; set; }
    }

    public class Entry
    {
        public string id { get; set; }
        public string titleText { get; set; }
        public TitleType titleType { get; set; }
        public ImageModel imageModel { get; set; }
        public string releaseDate { get; set; }
        public List<string> genres { get; set; }
        public List<PrincipalCredit> principalCredits { get; set; }
        public ReleaseYear releaseYear { get; set; }
    }

    public class ReleaseYear
    {
        public int year { get; set; }
    }

    public class TitleType
    {
        public string id { get; set; }
        public string text { get; set; }
        public bool canHaveEpisodes { get; set; }
    }

    public class ImageModel
    {
        public string url { get; set; }
        public int maxHeight { get; set; }
        public int maxWidth { get; set; }
        public string caption { get; set; }
    }

    public class PrincipalCredit
    {
        public string id { get; set; }
        public string text { get; set; }
    }

    internal class CalendarIMDB
    {
        public static async Task<CalendarData> GetIMDBCalendarAsync(string country = "RU", string content = "MOVIE")
        {
            country = country.ToUpper();
            content = content.ToUpper();

            CookieContainer cookieContainer = new CookieContainer();

            //var proxy = new WebProxy("127.0.0.1:8888");

            var getRequest = new GetRequest($"https://www.imdb.com/calendar/?ref_=rlm&region={country}&type={content}");
            getRequest.Headers.Add("content-type", "application/json");
            //getRequest.Proxy = proxy;
            getRequest.Run(cookieContainer,0);
            if (getRequest.Response is not null)
                return PullOutData(getRequest.Response);
            return null;
        }

        private static CalendarData PullOutData(string json)
        {
            var what = @"<script id=""__NEXT_DATA__"" type=""application/json"">";
            int from = json.IndexOf(what);
            json = json.Substring(from + what.Length);
            int to = json.IndexOf(@"}</s");
            return JsonSerializer.Deserialize<CalendarData>(json.Substring(0, to + 1));
        }
    }
}
