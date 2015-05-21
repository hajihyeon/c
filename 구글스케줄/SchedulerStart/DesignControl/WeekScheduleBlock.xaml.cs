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

namespace SchedulerStart
{
    /// <summary>
    /// WeekScheduleBlock.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class WeekScheduleBlock : UserControl
    {
        public WeekScheduleBlock(int Styleindex, string Time, string Summary)
        {
            InitializeComponent();

            BorderLine.Style = (Style)FindResource("WeekBorderuser");
            Tb_Time.Style = (Style)FindResource("WeekTimeTextBlockuser" + Styleindex.ToString());
            Tb_Summary.Style = (Style)FindResource("WeekContentTextBlockuser" + Styleindex.ToString());
            Tb_Time.Text = "("+Time+")";
            Tb_Summary.Text = Summary;
        }
    }
}
