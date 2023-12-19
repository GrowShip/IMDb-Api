using IMDbApiLib.Models;
using MediaApi.IMDB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Objects.Languages;

namespace MediaApi.Structure
{
    public class FilmData
    {
        public List<JsonData> Results { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class JsonData
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string TitleRus { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string RuntimeStr { get; set; }
        public string Genres { get; set; }
        public string ContentRating { get; set; }
        public string IMDbRating { get; set; }
        public string Plot { get; set; }
        public string Stars { get; set; }

        public string LocationSearch { get; set; }

        public string Type { get; set; }
        public string Year { get; set; }
        public string ReleaseDate { get; set; }
        public string Awards { get; set; }
        public string Directors { get; set; }
        public string Companies { get; set; }
        public string Countries { get; set; }
        public string Languages { get; set; }

        public string GrossWorld { get; set; }

        public Dictionary<string, CounrtyRelease> CounrtyReleaseAll { get; set; }
    }

    public class CounrtyReleaseAllO
    {
        public CounrtyRelease US { get; set; }
        public CounrtyRelease DE { get; set; }
        public CounrtyRelease IT { get; set; }
        public CounrtyRelease ES { get; set; }
        public CounrtyRelease GB { get; set; }
        public CounrtyRelease FR { get; set; }
        public CounrtyRelease CN { get; set; }
        public CounrtyRelease RU { get; set; }
    }

    public class CounrtyRelease
    {
        public string code { get; set; }
        public string country { get; set; }
        public string releaseDate { get; set; }
    }

    public class Converter
    {
        public static FilmData AdvToData(AdvancedSearchData AdvancedSearchDataJson, string language)
        {
            var result = new FilmData();
            result.Results = new List<JsonData>();

            foreach (var item in AdvancedSearchDataJson.Results)
            {
                var el = new JsonData();
                el.Id = item.Id;
                el.Title = item.Title;
                el.Genres = item.Genres;
                el.Description = item.Description;
                el.Image = item.Image;
                el.ContentRating = item.ContentRating;
                el.IMDbRating = item.IMDbRating;
                el.Stars = item.Stars;
                el.Plot = item.Plot;
                el.LocationSearch = language;
                result.Results.Add(el);
            }
            return result;
        }

        public static FilmData SrchTitToData(SearchData srhTitData)
        {
            var result = new FilmData();
            result.Results = new List<JsonData>();

            foreach (var item in srhTitData.Results)
            {
                
                var el = new JsonData();
                el.Id = item.Id;
                el.Title = item.Title;
                el.Type = item.ResultType;
                el.Description = item.Description;
                el.Image = item.Image;
                result.Results.Add(el);
            }
            return result;
        }

        public static FilmData TitleToData(TitleData item)
        {
            var result = new FilmData();
            result.Results = new List<JsonData>();

            var el = new JsonData();
            el.Id = item.Id;
            el.Title = item.Title;
            el.Type = item.Type;
            el.Year = item.Year;
            el.Image = item.Image;
            el.ReleaseDate = item.ReleaseDate;
            el.RuntimeStr = item.RuntimeMins;
            el.Plot = item.Plot;
            el.Awards = item.Awards;
            el.Directors = item.Directors;
            el.Stars = item.Stars;
            el.Genres = item.Genres;
            el.Companies = item.Companies;
            el.Countries = item.Countries;
            el.Languages = item.Languages;
            el.ContentRating = item.ContentRating;
            el.IMDbRating = item.IMDbRating;
            if (item.BoxOffice is not null) 
                el.GrossWorld = item.BoxOffice.CumulativeWorldwideGross;
            result.Results.Add(el);

            return result;
        }

        public static FilmData NewMovieToData(NewMovieData item)
        {
            var result = new FilmData();
            result.Results = new List<JsonData>();

            foreach (var elem in item.Items)
            {
                var el = new JsonData();
                el.Id = elem.Id;
                el.Title = elem.Title;
                el.Year = elem.Year;
                el.Image = elem.Image;
                el.ReleaseDate = elem.ReleaseState;
                el.RuntimeStr = elem.RuntimeMins;
                el.Plot = elem.Plot;
                el.Directors = elem.Directors;
                el.Stars = elem.Stars;
                el.Genres = elem.Genres;
                el.ContentRating = elem.ContentRating;
                el.IMDbRating = elem.IMDbRating;

                result.Results.Add(el);
            }
            return result;
        }

        public static FilmData CalendarToData(CalendarData data, string country = "")
        {
            var result = new FilmData();
            result.Results = new List<JsonData>();

            for (int i = 0; i < data.props.pageProps.groups.Count; i++)
            {
                for (int j = 0; j < data.props.pageProps.groups[i].entries.Count; j++)
                {
                    var item = data.props.pageProps.groups[i].entries[j];
                    JsonData el = new JsonData();

                    el.Id = item.id;
                    el.Title = item.titleText;
                    el.Image = item.imageModel != null ? item.imageModel.url : null;
                    //el.Image = item.imageModel.url;
                    //el.Description =
                    //el.RuntimeStr =
                    el.Genres = string.Join(", ", item.genres);
                    //el.ContentRating =
                    //el.IMDbRating =
                    //el.Plot =
                    //el.Stars =
                    el.LocationSearch = country;
                    el.Type = item.titleType.text;
                    el.Year = item.releaseYear.year.ToString();
                    el.ReleaseDate = item.releaseDate.Substring(item.releaseDate.IndexOf(",") + 1, 12);
                    //el.Awards =
                    //el.Directors =
                    //el.Companies =
                    //el.Countries =
                    //el.Languages = 
                    result.Results.Add(el);
                }
            }
            return result;
        }

        public static JsonData EnterReleasesDates(CalendarD calendar, JsonData data)
        {
            data.CounrtyReleaseAll = new Dictionary<string, CounrtyRelease>();
                
                //data.All = new CounrtyReleaseAll();
                //data.All.US = new CounrtyRelease();
                //data.All.US.code = "US";
                //data.All.US.country = "United States";
                //data.All.DE = new CounrtyRelease();
                //data.All.DE.code = "DE";
                //data.All.DE.country = "Germany";
                //data.All.IT = new CounrtyRelease();
                //data.All.IT.code = "IT";
                //data.All.IT.country = "Italy";
                //data.All.ES = new CounrtyRelease();
                //data.All.ES.code = "ES";
                //data.All.ES.country = "Spain";
                //data.All.GB = new CounrtyRelease();
                //data.All.GB.code = "GB";
                //data.All.GB.country = "United Kingdom";
                //data.All.FR = new CounrtyRelease();
                //data.All.FR.code = "FR";
                //data.All.FR.country = "France";
                //data.All.CN = new CounrtyRelease();
                //data.All.CN.code = "CN";
                //data.All.CN.country = "China";
                //data.All.RU = new CounrtyRelease();
                //data.All.RU.code = "RU";
                //data.All.RU.country = "Russia";

            foreach (var item in calendar.data.title.releaseDates.edges)
            {
                if (item.node.attributes.Count == 0 || item.node.attributes[0].id.Contains("internet", StringComparison.CurrentCultureIgnoreCase) || item.node.attributes[0].id.Contains("limited", StringComparison.CurrentCultureIgnoreCase))
                {
                    string notes = "";
                    if(item.node.attributes.Count != 0)
                    { 
                        if (item.node.attributes[0].id.Contains("internet", StringComparison.CurrentCultureIgnoreCase)) notes = " (internet)";
                        if (item.node.attributes[0].id.Contains("limited", StringComparison.CurrentCultureIgnoreCase)) notes = " (limited)";
                    }

                    #region
                    //switch (item.node.country.id.ToLower())
                    //{
                    //    case ("us"):

                    //        if (notes.Contains("internet") && !String.IsNullOrEmpty(data.All.US.releaseDate)) break;
                    //        if (notes.Contains("limited") && !String.IsNullOrEmpty(data.All.US.releaseDate) && !data.All.US.releaseDate.Contains("internet")) break;
                    //        data.All.US.releaseDate = item.node.displayableProperty.value.plainText + notes;
                    //        break;
                    //    case ("de"):
                    //        if (notes.Contains("internet") && !String.IsNullOrEmpty(data.All.DE.releaseDate)) break;
                    //        if (notes.Contains("limited") && !String.IsNullOrEmpty(data.All.DE.releaseDate) && !data.All.DE.releaseDate.Contains("internet")) break;
                    //        data.All.DE.releaseDate= item.node.displayableProperty.value.plainText + notes;
                    //        break;
                    //    case ("it"):
                    //        if (notes.Contains("internet") && !String.IsNullOrEmpty(data.All.IT.releaseDate)) break;
                    //        if (notes.Contains("limited") && !String.IsNullOrEmpty(data.All.IT.releaseDate) && !data.All.IT.releaseDate.Contains("internet")) break;
                    //        data.All.IT.releaseDate= item.node.displayableProperty.value.plainText + notes;
                    //        break;
                    //    case ("es"):
                    //        if (notes.Contains("internet") && !String.IsNullOrEmpty(data.All.ES.releaseDate)) break;
                    //        if (notes.Contains("limited") && !String.IsNullOrEmpty(data.All.ES.releaseDate) && !data.All.ES.releaseDate.Contains("internet")) break;
                    //        data.All.ES.releaseDate= item.node.displayableProperty.value.plainText + notes;
                    //        break;
                    //    case ("gb"):
                    //        if (notes.Contains("internet") && !String.IsNullOrEmpty(data.All.GB.releaseDate)) break;
                    //        if (notes.Contains("limited") && !String.IsNullOrEmpty(data.All.GB.releaseDate) && !data.All.GB.releaseDate.Contains("internet")) break;
                    //        data.All.GB.releaseDate= item.node.displayableProperty.value.plainText + notes;
                    //        break;
                    //    case ("fr"):
                    //        if (notes.Contains("internet") && !String.IsNullOrEmpty(data.All.FR.releaseDate)) break;
                    //        if (notes.Contains("limited") && !String.IsNullOrEmpty(data.All.FR.releaseDate) && !data.All.FR.releaseDate.Contains("internet")) break;
                    //        data.All.FR.releaseDate= item.node.displayableProperty.value.plainText + notes;
                    //        break;
                    //    case ("cn"):
                    //        if (notes.Contains("internet") && !String.IsNullOrEmpty(data.All.CN.releaseDate)) break;
                    //        if (notes.Contains("limited") && !String.IsNullOrEmpty(data.All.CN.releaseDate) && !data.All.CN.releaseDate.Contains("internet")) break;
                    //        data.All.CN.releaseDate= item.node.displayableProperty.value.plainText + notes;
                    //        break;
                    //    case ("ru"):
                    //        if (notes.Contains("internet") && !String.IsNullOrEmpty(data.All.RU.releaseDate)) break;
                    //        if (notes.Contains("limited") && !String.IsNullOrEmpty(data.All.RU.releaseDate) && !data.All.RU.releaseDate.Contains("internet")) break;
                    //        try
                    //        {
                    //            data.TitleRus = calendar.data.title.akas.edges.Find(f => f.node.country.id == "RU").node.displayableProperty.value.plainText;
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            data.TitleRus = "";
                    //        }
                    //        data.All.RU.releaseDate= item.node.displayableProperty.value.plainText + notes;
                    //        break;
                    //    default:
                    //        break;
                    //}
                    #endregion
                    bool a;
                    switch (item.node.country.id.ToLower())
                    {
                        case ("us"):
                            a = ChechAvailCountry(ref data, item, "United States", notes);
                            break;
                        case ("de"):
                            a = ChechAvailCountry(ref data, item, "Germany", notes);
                            break;
                        case ("it"):
                            a = ChechAvailCountry(ref data, item, "Italy", notes);
                            break;
                        case ("es"):
                            a = ChechAvailCountry(ref data, item, "Spain", notes);
                            break;
                        case ("gb"):
                            a = ChechAvailCountry(ref data, item, "United Kingdom", notes);
                            break;
                        case ("fr"):
                            a = ChechAvailCountry(ref data, item, "France", notes);
                            break;
                        case ("cn"):
                            a = ChechAvailCountry(ref data, item, "China", notes);
                            break;
                        case ("ru"):
                            a = ChechAvailCountry(ref data, item, "Russia", notes);
                            data.TitleRus = calendar.data.title.akas.edges.Find(f => f.node.country.id == "RU").node.displayableProperty.value.plainText;
                            break;
                        default:
                            break;
                    }
                }
            }
            return data;
        }

        static private bool ChechAvailCountry(ref JsonData data, Edge element, string country, string noteIn)
        {
            if (!data.CounrtyReleaseAll.ContainsKey(element.node.country.id))
            {
                data.CounrtyReleaseAll.Add(element.node.country.id, new CounrtyRelease()
                {
                    code = element.node.country.id,
                    country = country,
                    releaseDate = element.node.displayableProperty.value.plainText + noteIn

                });
                return false;
            }
            else
            {
                if (noteIn.Contains("internet") && !String.IsNullOrEmpty(data.CounrtyReleaseAll[element.node.country.id].releaseDate)) return true;
                if (noteIn.Contains("limited") && !String.IsNullOrEmpty(data.CounrtyReleaseAll[element.node.country.id].releaseDate) && !data.CounrtyReleaseAll[element.node.country.id].releaseDate.Contains("internet")) return true;
                data.CounrtyReleaseAll[element.node.country.id].releaseDate = element.node.displayableProperty.value.plainText + noteIn;
                return true;
            }    

        }
    }
}