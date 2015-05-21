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
using Google.Apis.Calendar.v3.Data;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using System.IO;
using System.Data;

namespace SchedulerStart
{
    /// <summary>
    /// ScheduleOfWeek.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ScheduleOfWeek : UserControl
    {
        DispatcherTimer timer = new DispatcherTimer();
        double postion;
        double Minute;
        int[,] scheduleoverloaded= new int[3,48];
        
        private void ScrollCheckStart()
        {
            timer.Interval = new TimeSpan(10000);
            timer.Tick += Change_Position;
            timer.Start();
        }

        void Change_Position(object sender, EventArgs e)
        {
            DateTime time_now=DateTime.Now;
            Minute = (Schedule_Week_Scroll.ScrollableHeight / (18*60));
            if (time_now.Hour >= 4 || time_now.Hour <= 21)
            {
                postion = Minute * (time_now.Hour - 3) * 60 + Minute * time_now.Minute;
                Schedule_Week_Scroll.ScrollToVerticalOffset(postion);
            }
        }

        public ScheduleOfWeek()
        {
            InitializeComponent();
            WeekHeaderSetting();
            WeekBorderLine();
            ScrollCheckStart();
            HelpFrameOfSchedule HelpFrameOfschedule = new HelpFrameOfSchedule();
            List<Event> Croppedevents = new List<Event>();


            foreach (Event evt in GoogleCalendarLoad.EventCollectionAllusers)
            {
                if ((evt.Start.DateTime.Value.Year.CompareTo(DateTime.Now.Year) == 0 && evt.Start.DateTime.Value.Month.CompareTo(DateTime.Now.Month) == 0 && evt.Start.DateTime.Value.Day.CompareTo(DateTime.Now.Day) == 0) ||
                   (evt.Start.DateTime.Value.Year.CompareTo(DateTime.Now.AddDays(1).Year) == 0 && evt.Start.DateTime.Value.Month.CompareTo(DateTime.Now.AddDays(1).Month) == 0 && evt.Start.DateTime.Value.Day.CompareTo(DateTime.Now.AddDays(1).Day) == 0) ||
                   (evt.Start.DateTime.Value.Year.CompareTo(DateTime.Now.AddDays(2).Year) == 0 && evt.Start.DateTime.Value.Month.CompareTo(DateTime.Now.AddDays(2).Month) == 0 && evt.Start.DateTime.Value.Day.CompareTo(DateTime.Now.AddDays(2).Day) == 0))
                {
                    Croppedevents.Add(evt);
                }
            }

            foreach (Event evt in Croppedevents)
            {
                string DisplayTime = evt.Start.DateTime.Value.TimeOfDay.ToString().Substring(0, 5) + " ~ " + evt.End.DateTime.Value.TimeOfDay.ToString().Substring(0, 5);
                string DisplaySummary = evt.Summary;

                WeekScheduleBlock Block = new WeekScheduleBlock(DBconnect.loadStyle(evt.Creator.Email), DisplayTime, DisplaySummary);
                Schedule_Week.Children.Add(Block);


                int GridPosition_day, GridPosition_starttime, GridPosition_span;
                HelpFrameOfschedule.SeparationOfEvent(evt, out GridPosition_day, out GridPosition_starttime, out GridPosition_span);

                if (scheduleoverloaded[GridPosition_day - 1, GridPosition_starttime - 1] > scheduleoverloaded[GridPosition_day - 1, (GridPosition_starttime + GridPosition_span - 1) - 1])
                    Block.Margin = new Thickness(scheduleoverloaded[GridPosition_day - 1, GridPosition_starttime - 1] * 20, 0, 5, 0);
                else
                    Block.Margin = new Thickness(scheduleoverloaded[GridPosition_day - 1, (GridPosition_starttime + GridPosition_span - 1) - 1] * 20, 0, 5, 0);

                if (GridPosition_span == 0)
                {
                    scheduleoverloaded[GridPosition_day - 1, GridPosition_starttime - 1]++;
                }
                else
                {
                    for (int j = 0; j < GridPosition_span; j++)
                    {
                        scheduleoverloaded[GridPosition_day - 1, GridPosition_starttime - 1 + j]++;
                    }
                }

                Grid.SetColumn((UIElement)Block, GridPosition_day);
                Grid.SetRow((UIElement)Block, GridPosition_starttime);
                Grid.SetRowSpan((UIElement)Block, GridPosition_span);
            }
        }

        /// <summary>
        /// 주간 일정표 시간표시 헤더 와 요일 표시 헤더 동기화 
        /// </summary>
        public void WeekHeaderSetting()
        {
            for (int j = 0; j < 24; j++)
            {
                var HeaderLabel = new Label()
                {
                    Style = (Style)FindResource("WeekHeaderTimeStyle"),
                };

                if (j < 12)
                {
                    HeaderLabel.Content = "오전 " + j.ToString() + "시";
                }
                else if (j == 12)
                    HeaderLabel.Content = "정오";

                else
                {
                    HeaderLabel.Content = "오후 " + (j - 12).ToString() + "시";
                }

                Schedule_Week.Children.Add(HeaderLabel);
                Grid.SetRow(HeaderLabel, j * 2);
            }
        }


        /// <summary>
        ///  주간 일정표 Borderline 출력 양식
        /// </summary>
        public void WeekBorderLine()
        {

            for (int i = 0; i < 4; i++)
            {
                var BorderHeight = new Border();
                BorderHeight.BorderThickness = new Thickness(1, 0, 0, 0);
                BorderHeight.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#33808080"));
                Schedule_Week.Children.Add(BorderHeight);
                Grid.SetColumn(BorderHeight, i);
                Grid.SetRowSpan(BorderHeight, 48);
            }
            for (int j = 0; j < 48; j++)
            {
                var BorderWidth = new Border();

                if (j % 2 != 0)
                {
                    BorderWidth.BorderThickness = new Thickness(0, 0, 0, 1);
                    BorderWidth.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#33808080"));
                    Schedule_Week.Children.Add(BorderWidth);

                    Grid.SetColumnSpan(BorderWidth, 4);
                    Grid.SetRow(BorderWidth, j);
                }
                else
                {
                    BorderWidth.BorderThickness = new Thickness(0, 0, 0, 1);
                    BorderWidth.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#33808080"));
                    Schedule_Week.Children.Add(BorderWidth);

                    Grid.SetColumn(BorderWidth, 1);
                    Grid.SetColumnSpan(BorderWidth, 3);
                    Grid.SetRow(BorderWidth, j);

                }
            }
        }
    }
}
