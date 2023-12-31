﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Encodings;
using System.Threading.Tasks;
using MediaApi.Structure;
using Newtonsoft.Json;
using RequestSupport;

namespace MediaApi.IMDB
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

    public class CalendarD
    {
        public Data data { get; set; }
        public Extensions extensions { get; set; }
    }

    public class Extensions
    {
        public string disclaimer { get; set; }
        public ExperimentalFields experimentalFields { get; set; }
    }

    public class ExperimentalFields
    {
        public List<object> janet { get; set; }
        public List<object> markdown { get; set; }
    }

    //+

    public class Data
    {
        public Title title { get; set; }
    }

    public class Title
    {
        public ReleaseDates releaseDates { get; set; }
        public Akas akas { get; set; }
    }

    #region release dates

    public class ReleaseDates
    {
        public int total { get; set; }
        public List<Edge> edges { get; set; }
        public PageInfo pageInfo { get; set; }
        //public string __typename { get; set; }
    }

    public class PageInfo
    {
        public string endCursor { get; set; }
        public bool hasNextPage { get; set; }
        public string __typename { get; set; }
    }

    public class Edge
    {
        public int position { get; set; }
        public Node node { get; set; }
        public string __typename { get; set; }
    }

    public class Node
    {
        public CountryC country { get; set; }
        public List<Attribute> attributes { get; set; }
        public DisplayableProperty displayableProperty { get; set; }
        //public string __typename { get; set; }
    }

    public class CountryC
    {
        public string id { get; set; }
        public string text { get; set; }
        public string __typename { get; set; }
    }

    public class Attribute
    {
        public string text { get; set; }
        public string id { get; set; }
        public string __typename { get; set; }
    }

    public class DisplayableProperty
    {
        public Value value { get; set; }
        public string __typename { get; set; }
    }

    public class Value
    {
        public string plainText { get; set; }
        public string __typename { get; set; }
    }
    #endregion

    #region Akas title name in country

    public class Akas
    {
        public int total { get; set; }
        public List<Edge> edges { get; set; }
        public PageInfo pageInfo { get; set; }
    }

    #endregion
    
    //+
    /// <summary>
    /// Work with Releases Dates in countries
    /// </summary>
    internal class ReleasesDates
    {
        static CookieContainer cookieContainer = new CookieContainer();
        public static async Task<CalendarD> GetReleasesToData(string titleTt)
        {
            int count = 0;
            CalendarD calendar = new CalendarD();
            string endCourse = "";
            bool oneMore;
            do
            {
                oneMore = false;
                
                var respond = await MakeRequestForData(count, titleTt, "dates", endCourse);
                if (respond.Contains("No page results after cursor", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (count != 0) return calendar;
                    else return new CalendarD();
                }

                if (count == 0)
                {
                    calendar = JsonConvert.DeserializeObject<CalendarD>(respond);
                }
                else
                {
                    var additional = JsonConvert.DeserializeObject<CalendarD>(respond);
                    calendar.data.title.releaseDates.edges.AddRange(additional.data.title.releaseDates.edges);
                    calendar.data.title.releaseDates.pageInfo.hasNextPage = additional.data.title.releaseDates.pageInfo.hasNextPage;
                }
                if (calendar.data.title.releaseDates.pageInfo.hasNextPage)
                {
                    oneMore = true;
                    count++;
                    endCourse = calendar.data.title.releaseDates.pageInfo.endCursor;
                    if (count >= 3) oneMore = false;
                }

            } while (oneMore);

            calendar.data.title.akas = new Akas();
            calendar.data.title.akas = await GetRusTitle(titleTt);

            return calendar;
        }

        private static async Task<Akas> GetRusTitle(string titleTl)
        {
            int count = 0;
            Akas inCounrtyTitle = new Akas();
            string endCourse = "";
            bool oneMore;
            do
            {
                oneMore = false;
                
                var respond = await MakeRequestForData(count, titleTl, "titles", endCourse);
                if (respond.Contains("No page results after cursor", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (count != 0) return inCounrtyTitle;
                    else return new Akas();
                }

                if (count == 0)
                {
                    inCounrtyTitle = JsonConvert.DeserializeObject<CalendarD>(respond).data.title.akas;
                }
                else
                {
                    var additional = JsonConvert.DeserializeObject<CalendarD>(respond);
                    inCounrtyTitle.edges.AddRange(additional.data.title.akas.edges);
                    inCounrtyTitle.pageInfo.hasNextPage = additional.data.title.akas.pageInfo.hasNextPage;
                }
                if (inCounrtyTitle.pageInfo.hasNextPage)
                {
                    oneMore = true;
                    count++;
                    endCourse = inCounrtyTitle.pageInfo.endCursor;
                    if (count >= 3) oneMore = false;
                }

            } while (oneMore);

            //int emptyNode = inCounrtyTitle.edges.Find(f => f.node.country is null).position;
            while (inCounrtyTitle.edges.Count >= 1 && inCounrtyTitle.edges[0].node.country is null)
            {
                inCounrtyTitle.edges.Remove(inCounrtyTitle.edges[0]);
            }                  

            return inCounrtyTitle;
        }

        private static async Task<string> MakeRequestForData(int type, string tittleTt, string key, string endCoursor)
        {
            string typeRequest = "";
            if (type == 0)
            {
                typeRequest = "NA=";
            }
            else if (type == 1)
            {
                //typeRequest = "NTQ=";
                typeRequest = "NDk=";
                typeRequest = endCoursor;
            }
            else if (type == 2)
            {
                typeRequest = "MTA0";
                typeRequest = endCoursor;
            }
            else return string.Empty;

            var code = tittleTt;
            //var proxy = new WebProxy("127.0.0.1:8888");

            GetRequest getRequest;
            if (key == "dates")
                getRequest = new GetRequest(KeysAccess.GetReleaseKey("baseUrl") + KeysAccess.GetReleaseKey("releasesDates") + "={\"after\":\"" + typeRequest +"\",\"const\":\"" + code + "\",\"first\":50,\"isAutoTranslationEnabled\":false,\"locale\":\"en-GB\",\"originalTitleText\":false}&extensions={\"persistedQuery\":{\"sha256Hash\":\"" + KeysAccess.GetReleaseKey("releaseDatesHash") + "\",\"version\":1}}");
            else if (key == "titles")
                getRequest = new GetRequest(KeysAccess.GetReleaseKey("baseUrl") + KeysAccess.GetReleaseKey("inCountryTitle") + "={\"after\":\"" + typeRequest + "\",\"const\":\"" + code + "\",\"first\":50,\"isAutoTranslationEnabled\":false,\"locale\":\"en-US\",\"originalTitleText\":false}&extensions={\"persistedQuery\":{\"sha256Hash\":\"" + KeysAccess.GetReleaseKey("inCountryTitleHash") + "\",\"version\":1}}");
            else 
                return string.Empty;

            getRequest.Accept = "application/graphql+json, application/json";
            getRequest.Useragent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/114.0.0.0 Safari/537.36";
            getRequest.Referer = KeysAccess.GetReleaseKey("imdb") +"/";
            getRequest.Headers.Add("origin", KeysAccess.GetReleaseKey("imdb"));
            getRequest.Headers.Add("content-type", "application/json");

            getRequest.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            getRequest.Headers.Add("Accept-Language", "en-GB,en-US;q=0.9,en;q=0.8,ru;q=0.7");

            getRequest.Host = KeysAccess.GetReleaseKey("baseUrl").Substring(8);
            //getRequest.Proxy = proxy;
            getRequest.Run(cookieContainer);
                        
            return (getRequest.Response);
        }
    }
}
