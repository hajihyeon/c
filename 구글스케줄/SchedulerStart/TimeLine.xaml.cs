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
using System.Windows.Threading;
namespace SchedulerStart
{
    /// <summary>
    /// TimeLine.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TimeLine : UserControl
    {
        DispatcherTimer timer = new DispatcherTimer();

        public TimeLine()
        {
            InitializeComponent();

            TimeCheckStart();
        }

        private void TimeCheckStart()
        {
            timer.Interval = new TimeSpan(10000);
            timer.Tick += Change_Time;
            timer.Start();
        }

        void Change_Time(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            Tb_Time.Text = "현재시간: "+now.Hour.ToString() + "시 " + now.Minute.ToString() + "분 " + now.Second.ToString()+"초";
            TimeLine_Grid.Margin = new Thickness(0, -20 + now.Hour*200 + now.Minute*(200/60) + (now.Second <30 ? 0:1) , 0, 0);
        }
    }
}
