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
using MahApps.Metro.Controls;
using System.Windows.Threading;

using Google.Apis.Calendar.v3.Data;
using System.IO;
namespace SchedulerStart
{

    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        NewsChungbuk NewsChungbukInit = new NewsChungbuk();
        GoogleCalendarLoad GoogleLoadInit = new GoogleCalendarLoad();
        public MainWindow()
        {
            InitializeComponent();

            HeaderLabel_Today.Content = HelpFrameOfSchedule.LabelDayStringForm(DateTime.Now);
            HeaderLabel_Todayplus1.Content = HelpFrameOfSchedule.LabelDayStringForm(DateTime.Now.AddDays(1));
            HeaderLabel_Todayplus2.Content = HelpFrameOfSchedule.LabelDayStringForm(DateTime.Now.AddDays(2));

            TabChangeStart();
        }


        DispatcherTimer timer_tabchange = new DispatcherTimer();

        private void TabChangeStart()
        {
            timer_tabchange.Interval = new TimeSpan(0, 0, 10);
            timer_tabchange.Tick += TabChange;
            timer_tabchange.Start();
        }

        static int count = 0;

        void TabChange(object sender, EventArgs e)
        {
            if (count == 6)
                count = 0;

            if (count == 0)
            {
                TabControl_Scheduler_item1.IsSelected = true;
            }

            else if (count==3)
            {
                TabControl_Scheduler_item2.IsSelected = true;

                NewsMainRefresh();

            }
            else if (count==4)
            {
                GoogleLoadInit.GoogleLoadRefresh();
            }
            
            else if (count==5)
            {
                TodayScheduleRefresh();
                WeekScheduleRefresh();
                NewsLineRefresh();
            }
            count++;
        }


        private void Button_TabpanelRight_Clicked(object sender, RoutedEventArgs e)
        {
            if (TabControl_Scheduler_item1.IsSelected)
            {
                TabControl_Scheduler_item2.IsSelected = true;
            }
            else if (TabControl_Scheduler_item2.IsSelected)
            {
                TabControl_Scheduler_item3.IsSelected = true;
            }
            else if (TabControl_Scheduler_item3.IsSelected)
            {
                TabControl_Scheduler_item1.IsSelected = true;
            }
        }

        private void Btn_Addmember_MouseEnter(object sender, MouseEventArgs e)
        {
            Btn_Addmember.Opacity = 1;
        }

        private void Btn_Addmember_MouseLeave(object sender, MouseEventArgs e)
        {
            Btn_Addmember.Opacity = 0;
        }

        private void Btn_Addmember_Click(object sender, RoutedEventArgs e)
        {
            PART_Popup.Visibility = Visibility.Visible;
            PART_Popup.Content = new AddmemberPopup(this);
            MainGrid.Opacity = 0.7;
        }

     
        //DispatcherTimer timer = new DispatcherTimer();

        //private void ThreadingStart()
        //{
        //    timer.Interval = new TimeSpan(0, 0, 1);
        //    timer.Tick += WeekRefresh;
        //    timer.Tick += doingRefresh;
        //    timer.Tick += NewsLineRefrsh;
        //    timer.Start();
        //}

        //void timer_Tick(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //void doingRefresh(object sender, EventArgs e)
        //{
        //        stackday.Children.Clear();
        //        var ScheduleofDay = new ScheduleOfDay();
        //        stackday.Children.Add(ScheduleofDay);
        //}

        //void WeekRefresh(object sender, EventArgs e)
        //{
        //        HeaderLabel_Today.Content = HelpFrameOfSchedule.LabelDayStringForm(DateTime.Now);
        //        HeaderLabel_Todayplus1.Content = HelpFrameOfSchedule.LabelDayStringForm(DateTime.Now.AddDays(1));
        //        HeaderLabel_Todayplus2.Content = HelpFrameOfSchedule.LabelDayStringForm(DateTime.Now.AddDays(2));

        //        ScheduleOfWeek sch = new ScheduleOfWeek();
        //        ScheduleOfWeek_parentGrid.Children.Clear();
        //        ScheduleOfWeek_parentGrid.Children.Add(sch);
        //}
        
        //void NewsLineRefrsh(object sender, EventArgs e)
        //{
        //    News_Label.Text = "[오늘의 뉴스] ";
        //    foreach (Newsitem news in NewsChungbuk.NewsItems)
        //    {
        //        News_Label.Text += " / " + news.Title.Split('(')[0];
        //    }
        //}

        void TodayScheduleRefresh()
        {
            stackday.Children.Clear();
            var ScheduleofDay = new ScheduleOfDay();
            stackday.Children.Add(ScheduleofDay);
        }

        void WeekScheduleRefresh()
        {
            HeaderLabel_Today.Content = HelpFrameOfSchedule.LabelDayStringForm(DateTime.Now);
            HeaderLabel_Todayplus1.Content = HelpFrameOfSchedule.LabelDayStringForm(DateTime.Now.AddDays(1));
            HeaderLabel_Todayplus2.Content = HelpFrameOfSchedule.LabelDayStringForm(DateTime.Now.AddDays(2));

            ScheduleOfWeek sch = new ScheduleOfWeek();
            ScheduleOfWeek_parentGrid.Children.Clear();
            ScheduleOfWeek_parentGrid.Children.Add(sch);
        }

        void NewsLineRefresh()
        {
            News_Label.Text = "[오늘의 뉴스] ";
            foreach (Newsitem news in NewsChungbuk.NewsItems)
            {
                News_Label.Text += " / " + news.Title.Split('(')[0];
            }
        }

        void NewsMainRefresh()
        {
            News NewsObject = new News();
            News_parentGrid.Children.Clear();
            News_parentGrid.Children.Add(NewsObject);
        }

    }
}
