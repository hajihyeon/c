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
using System.IO;
using System.Data;
namespace SchedulerStart
{

    /// <summary>
    /// ScheduleOfDay.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ScheduleOfDay : UserControl
    {
        public List<Event> ScheduleOfDayEvents = new List<Event>();

        public ScheduleOfDay()
        {
            InitializeComponent();
            HelpFrameOfSchedule HelpFrameOfschedule = new HelpFrameOfSchedule();
            List<Event> Croppedevents = new List<Event>();

            foreach (Event evt in GoogleCalendarLoad.EventCollectionAllusers)
            {
                if (evt.End.DateTime.Value.CompareTo(DateTime.Now) >= 0 && evt.Start.DateTime.Value.Day.CompareTo(DateTime.Now.Day) == 0)
                {
                    Croppedevents.Add(evt);
                }
            }

            if(Croppedevents.Count!=0)
            {
                IEnumerable<Event> Sortedevents =
                    from evt in Croppedevents
                    orderby evt.Start.DateTime.Value.Hour ascending, evt.Start.DateTime.Value.Minute ascending
                    select evt;

                foreach (Event evt in Sortedevents)
                {
                    ScheduleOfDayEvents.Add(evt);

                    var Textblock = new CustomTextBlock();
                    int styleindex = DBconnect.loadStyle(evt.Creator.Email.ToString());
                    if (styleindex != 0)
                    {
                        Textblock.Style = (Style)FindResource("DailyTextBlockuser" + styleindex.ToString());
                    }

                    Textblock.Text += "(" + evt.Start.DateTime.Value.TimeOfDay.ToString().Substring(0, 5) + "~" + evt.End.DateTime.Value.TimeOfDay.ToString().Substring(0, 5) + ") " +evt.Location+Environment.NewLine+evt.Summary;
                    DailySchedule.Children.Add(Textblock);
                }
            } 
        }
    }
}
