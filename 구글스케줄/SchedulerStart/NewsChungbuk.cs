using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;
using System.Windows.Threading;
namespace SchedulerStart
{
    public class Newsitem
    {
        public string Title;
        public string Content;
    }      
    class NewsChungbuk
    {
        public static List<Newsitem> NewsItems= new List<Newsitem>();

        DispatcherTimer Timer_NewsRefresh = new DispatcherTimer();

        public NewsChungbuk()
        {
            int count = 0;
            WebClient webclient = new WebClient();
            webclient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.1916.153 Safari/537.36");
            webclient.Encoding = Encoding.UTF8;
            HtmlAgilityPack.HtmlDocument htmldoc = new HtmlAgilityPack.HtmlDocument();
            string SearchSource = webclient.DownloadString("http://txtnews.scrapmaster.co.kr/board/chungbuk/chungbuk_list.html?sm_id=chungbuk");
            htmldoc.LoadHtml(SearchSource);

            NewsItems.Clear();

            foreach (HtmlNode NewsList in htmldoc.DocumentNode.Descendants("tbody"))
            {
                foreach (HtmlNode News in NewsList.Descendants("tr"))
                {
                    if (News.Attributes["class"] == null)
                    {
                        foreach (HtmlNode ContentData in News.Descendants("p"))
                        {
                            NewsItems[count / 2].Content += ContentData.InnerText.ToString().Replace("\r\n", "").Replace("\t", "");
                            count++;
                        }
                    }

                    else if (News.Attributes["class"].Value.ToString() == "base_font")
                    {
                        foreach (HtmlNode TitleData in News.Descendants("a"))
                        {
                            Newsitem news = new Newsitem();
                            news.Title = TitleData.InnerText.ToString();
                            NewsItems.Add(news);
                            count++;
                        }
                    }
                }
            }
            NewsRefreshStart();
        }

        private void NewsRefreshStart()
        {
            Timer_NewsRefresh.Interval = new TimeSpan(1, 0, 0);
            Timer_NewsRefresh.Tick += NewsRefresh;
            Timer_NewsRefresh.Start();
        }

        void NewsRefresh(object sender, EventArgs e)
        {
            int count = 0;
            WebClient webclient = new WebClient();
            webclient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.1916.153 Safari/537.36");
            webclient.Encoding = Encoding.UTF8;
            HtmlAgilityPack.HtmlDocument htmldoc = new HtmlAgilityPack.HtmlDocument();
            string SearchSource = webclient.DownloadString("http://txtnews.scrapmaster.co.kr/board/chungbuk/chungbuk_list.html?sm_id=chungbuk");
            htmldoc.LoadHtml(SearchSource);

            NewsItems.Clear();

            foreach (HtmlNode NewsList in htmldoc.DocumentNode.Descendants("tbody"))
            {
                foreach (HtmlNode News in NewsList.Descendants("tr"))
                {
                    if (News.Attributes["class"] == null)
                    {
                        foreach (HtmlNode ContentData in News.Descendants("p"))
                        {
                            NewsItems[count / 2].Content += ContentData.InnerText.ToString().Replace("\r\n", "").Replace("\t", "");
                            count++;
                        }
                    }

                    else if (News.Attributes["class"].Value.ToString() == "base_font")
                    {
                        foreach (HtmlNode TitleData in News.Descendants("a"))
                        {
                            Newsitem news = new Newsitem();
                            news.Title = TitleData.InnerText.ToString();
                            NewsItems.Add(news);
                            count++;
                        }
                    }
                }
            }
        }
    }
}
