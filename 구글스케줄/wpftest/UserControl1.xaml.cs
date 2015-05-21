using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using HtmlAgilityPack;
using System.Web;
using System.Net;
namespace wpftest
{
    /// <summary>
    /// UserControl1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public class Newsitem
        {
            public string Title;
            public string Content;
        }

        public List<Newsitem> GetNewsList()
        {
            int count=0;
            WebClient webclient= new WebClient();
            webclient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.1916.153 Safari/537.36");
            webclient.Encoding = Encoding.UTF8;
            HtmlAgilityPack.HtmlDocument htmldoc = new HtmlAgilityPack.HtmlDocument();
            string SearchSource = webclient.DownloadString("http://txtnews.scrapmaster.co.kr/board/chungbuk/chungbuk_list.html?sm_id=chungbuk");
            htmldoc.LoadHtml(SearchSource);

            List<Newsitem> Newslist = new List<Newsitem>();

            foreach (HtmlNode NewsList in htmldoc.DocumentNode.Descendants("tbody"))
            {
                foreach (HtmlNode News in NewsList.Descendants("tr"))
                {
                    if (News.Attributes["class"] == null)
                    {
                        foreach (HtmlNode ContentData in News.Descendants("p"))
                        {
                            Newslist[count/2].Content+= "content: " + ContentData.InnerText.ToString().Replace("\r\n","").Replace("\t","") + Environment.NewLine;
                            count++;
                        }
                    }

                    else if(News.Attributes["class"].Value.ToString()=="base_font")
                    {
                        foreach(HtmlNode TitleData in News.Descendants("a"))
                        {
                            Newsitem news = new Newsitem();
                            news.Title= "tltle: " + TitleData.InnerText.ToString() + Environment.NewLine;
                            Newslist.Add(news);
                            count++;
                        }
                    }              
                }                    
            }
            return Newslist;
        }
        
        public UserControl1()
        {
            InitializeComponent();
            foreach(Newsitem news in GetNewsList())
            {
                News.Text += news.Title + news.Content + Environment.NewLine;
            }
        }
        private void Btn_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
