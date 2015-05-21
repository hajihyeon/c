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
using System.Windows.Threading;
using Google.Apis.Calendar.v3.Data;
using System.Windows.Media.Animation;

namespace SchedulerStart
{
    /// <summary>
    /// News.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class News : UserControl
    {
        public News()
        {
            InitializeComponent();

            timer2.Interval = new TimeSpan(100);
            timer2.Tick += new EventHandler(timer_Tick);
            timer2.Start();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            List<Newsitem> newlist = new List<Newsitem>();
            newlist = NewsChungbuk.NewsItems;
            const int NewsTextSize = 40;

            int i = 0;
            foreach (Newsitem news in newlist)
            {
                string NewsText = "●  " + news.Title.Split('(')[0];
                if (NewsText.Length > NewsTextSize)
                {
                    NewsText = NewsText.Substring(0, NewsTextSize) + "...";

                }
                else
                {
                    while(true)
                    {
                        if (NewsText.Length <= NewsTextSize)
                        {
                            NewsText += " ";
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                
                if (i == 5)
                    break;

                switch(i+1)
                {
                    case 1:
                        Tb_News1.Text = NewsText;
                        Tb_News1.FontSize = 30;
                        break;
                    case 2:
                        Tb_News2.Text =NewsText;
                        Tb_News2.FontSize = 30;
                        break;
                    case 3:
                        Tb_News3.Text =NewsText;
                        Tb_News3.FontSize = 30;
                        break;
                    case 4:
                        Tb_News4.Text = NewsText;
                        Tb_News4.FontSize = 30;
                        break;
                    case 5:
                        Tb_News5.Text = NewsText;
                        Tb_News5.FontSize = 30;
                        break;
                }

                //Viewbox viewbox = new Viewbox();
                //Label block = new Label();
                //block.Margin = new Thickness(50, 10, 50, 0);
                //block.Content = "●  "+news.Title.Split('(')[0];
                //viewbox.HorizontalAlignment=HorizontalAlignment.Left;         
                //viewbox.Child = block;
                //NewsList.Children.Add(viewbox);
                //Grid.SetRow(viewbox, i + 1);

                //Storyboard storyboard = new Storyboard();
                //DoubleAnimationUsingKeyFrames animation = new DoubleAnimationUsingKeyFrames();
                //animation.SetValue(Storyboard.TargetNameProperty, block.Name);
                //animation.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
                //animation.BeginTime = new TimeSpan(0, 0, i);

                //EasingDoubleKeyFrame keyframe = new EasingDoubleKeyFrame();
                //keyframe.KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 1));
                //keyframe.Value = 2;
                //animation.KeyFrames.Add(keyframe);
                //storyboard.Children.Add(animation);

                i++;

            }

        }

        DispatcherTimer timer2 = new DispatcherTimer();
        Run UserNameRn = new Run();
        Run ConRn = new Run();
        int count = 0;

        public void time()
        {
            news.SetValue(Canvas.LeftProperty, (double)news.GetValue(Canvas.LeftProperty) - 1.0);
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            string Stmainnews = " ";
            string Stetc = " ";
            string Stbirth = "";
            string Stnews = "";

            Run a = new Run();
            Run b = new Run();

            // 문자열 이동
            if ((double)news.GetValue(Canvas.LeftProperty) < 0 - news.ActualWidth) //문자열 모두 지나가면 다시 리셋
            {
                MainTb.Text = "";
                EtcTb.Text = "";
                BirthTb.Text = "";
                NewsTb.Text = "";

                news.SetValue(Canvas.LeftProperty, this.Width);
                count = 0; // 업데이트를 위해 count를 0으로 초기화
            }
            else if ((double)news.GetValue(Canvas.LeftProperty) <= this.Width)
            {
                news.SetValue(Canvas.LeftProperty, (double)news.GetValue(Canvas.LeftProperty) - 0.1);
            }

            if (count == 0) //문자열 모두 지나가면 업데이트
            {
                string userna = "";

                //foreach (Event evt in ScheduleofDay.ScheduleOfDayEvents)
                //{
                //    if (evt.End.DateTime.Value.CompareTo(DateTime.Now) >= 0 && evt.Start.DateTime.Value.Day.CompareTo(DateTime.Now.Day) == 0)
                //    {
                //        //시간 계산해서 20분 전부터 빨간색으로 표시.

                //        //news.Inlines.Add(rn); //Run동적생성 후 news에 추가
                //        if ((int.Parse(evt.Start.DateTime.Value.TimeOfDay.Hours.ToString()) - int.Parse(System.DateTime.Now.ToString("HH"))) * 60 + (int.Parse(evt.Start.DateTime.Value.TimeOfDay.Minutes.ToString()) - int.Parse(System.DateTime.Now.ToString("mm"))) < 20)
                //        {
                //            Stmainnews = "";
                //            Stmainnews += evt.Start.DateTime.Value.TimeOfDay.ToString().Substring(0, 5) + " ~ " + evt.End.DateTime.Value.TimeOfDay.ToString().Substring(0, 5) + "  " + evt.Summary;
                //            userna = " (" + DBconnect.loadUserName(evt.Creator.Email) + ") ";
                //            GetContent2(MainTb, Stmainnews, userna); // 20분전에 띄울 일정

                //        }
                //        else
                //        {
                //            Stetc = "";
                //            Stetc += evt.Start.DateTime.Value.TimeOfDay.ToString().Substring(0, 5) + " ~ " + evt.End.DateTime.Value.TimeOfDay.ToString().Substring(0, 5) + "  " + evt.Summary;
                //            userna = " (" + DBconnect.loadUserName(evt.Creator.Email) + ") ";
                //            GetContent(EtcTb, Stetc, userna); //20분전에 띄울 일정 이후의 일정
                //        }
                //    }
                //    count++;
                //}

                //if (Stmainnews == " " && Stetc == " ")
                //    Stetc = ""; // 오늘의 일정이 없을 시에는 [일정없음]을 출력하기 위함.

                //if (Stbirth == "")
                //{
                //    BirthStatic.Text = "";
                //}

                //else
                //{
                //    GetContent(BirthTb, Stbirth, userna); //오늘의 생일자
                //}

                //if (Stnews == "")
                //{
                //    NewsStatic.Text = "";
                //}
                //else
                //{
                //    GetContent(NewsTb, Stnews, userna); //개신뉴스
                //}

            }
        }

        /// <summary>
        /// 받은 정보를 뉴스라인에 넣는 함수 , 구성(etc1,mainnews,etc2,Tobirth,Tonews)
        /// </summary>
        /// <param name="Rn">내용 넣을 곳</param>
        /// <param name="Con">넣을 내용 , null값으면 [일정없음] 출력</param>
        private void GetContent(TextBlock Tb, string Con, string UserName = "user")
        {
            UserNameRn = new Run();
            ConRn = new Run();
            if (Con == " ") Con = "[일정없음]";
            else if (Con == "")
            {

            }
            ConRn.Text = Con;
            UserNameRn.Text = UserName;
            UserNameRn.Foreground = Brushes.Blue;
            //ConRn.Foreground = Brushes.Black;
            Tb.Inlines.Add(UserNameRn);
            Tb.Inlines.Add(ConRn);
        }

        private void GetContent2(TextBlock Tb, string Con, string UserName = "user")
        {
            UserNameRn = new Run();
            ConRn = new Run();
            if (Con == " ") Con = "[일정없음]";
            ConRn.Text = Con;

            UserNameRn.Text = UserName;
            UserNameRn.Foreground = Brushes.Blue;
            ConRn.Foreground = Brushes.Red;
            Tb.Inlines.Add(UserNameRn);
            Tb.Inlines.Add(ConRn);
        }

        /// <summary>
        /// Username 과 일정 내용을 받아서 합치는 함수
        /// </summary>
        /// <param name="Username">유저이름</param>
        /// <param name="Summary">일정내용(시작시간 + 내용)</param>
        private void CombineText(string St, string Username, string Summary)
        {
            St = St + Username + Summary;
        }


        // ScheduleofDay.ScheduleOfDayEvents


    }
}
