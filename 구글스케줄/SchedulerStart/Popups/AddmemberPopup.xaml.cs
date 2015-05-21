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
using System.IO;
using System.Windows.Threading;
using System.Threading;
using System.Timers;
using Google.Apis.Calendar.v3.Data;
namespace SchedulerStart
{
    class WPFBrushList : List<WPFBrush>//리스트 생성 클래스
    {
        public WPFBrushList()
        {
            Add(new WPFBrush("#FF2D97D3"));
            Add(new WPFBrush("#FF70C59C"));
            Add(new WPFBrush("#FFAAA7D4"));
            Add(new WPFBrush("#FFFDBA52"));
            Add(new WPFBrush("#FFECA0BD"));
            Add(new WPFBrush("#FFF76D3C"));
            Add(new WPFBrush("#FFF87073"));
            Add(new WPFBrush("#FFF9FF91"));
            Add(new WPFBrush("#FF997D65"));
            Add(new WPFBrush("#FFDF93FF"));
            Add(new WPFBrush("#FFB6B6B6"));
            Add(new WPFBrush("#FFB8D37F"));
            //Add(new WPFBrush("#"));
            //Add(new WPFBrush("#"));
            //Add(new WPFBrush("#"));
            //Add(new WPFBrush("#"));
            //Add(new WPFBrush("#"));
        }

    }
    class WPFBrush
    {
        public WPFBrush(string name)
        {
            this.Name = name;
        }
        public string Name { get; set; }
    }



    /// <summary>
    /// UserControl1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AddmemberPopup : UserControl
    {
        private WPFBrushList _brushes = new WPFBrushList();//전역 리스트
        int index = 0;
        List<Thread> listthread = new List<Thread>();
        List<System.Timers.Timer> listtimer = new List<System.Timers.Timer>();
        Thread th;
        System.Timers.Timer timer;


        string image = "";

        MainWindow _mainwindow;

        public AddmemberPopup(MainWindow mainwindow)
        {
            InitializeComponent();

            lsbBrushes.DataContext = _brushes;
            _mainwindow = mainwindow;

            foreach(int usedindex in DBconnect.UsedColorindex())
            {   
            }
        }

        private void bt_SearchImage_Click(object sender, RoutedEventArgs e)
        {
            Stream checkStream = null;

            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Multiselect = false;

            openFileDialog.Filter = "All Image Files | *.*";

            if ((bool)openFileDialog.ShowDialog())
            {
                try
                {
                    if ((checkStream = openFileDialog.OpenFile()) != null)
                    {
                        BitmapImage src = new BitmapImage(new Uri(openFileDialog.FileName));
                        im_profile.Source = src;

                        Byte[] binaryData = getJPGFromImageControl(src);

                        image = Convert.ToBase64String(binaryData);
                        //TextRange textRange = new TextRange(tb_picture.Document.ContentStart, tb_picture.Document.ContentEnd);
                        // textRange.Text = openFileDialog.FileName;

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("problem occured, try again later");
            }
        }

        public BitmapImage ToImage(byte[] array)    // DB자료 그림으로 바꾸기
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }

        public byte[] getJPGFromImageControl(BitmapImage imageC)
        {
            MemoryStream memStream = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(imageC));
            encoder.Save(memStream);
            return memStream.GetBuffer();
        }

        public void HandleTimerElapsed(object sender, ElapsedEventArgs e)
        {
            // do whatever it is that you need to do on a timer

            listtimer[0].Stop();
            listthread[0].Abort();
            listthread.RemoveAt(0);
            listtimer[0].Close();
            listtimer.RemoveAt(0);
        }

        private void bt_enter_Click(object sender, RoutedEventArgs e)
        {
            TextRange tbuser = new TextRange(tb_username.Document.ContentStart, tb_username.Document.ContentEnd);

            if (tbuser.Text.Trim() == "")
            {
                MessageBox.Show("닉네임을 입력해주세요.");
                return;
            }

            if(index==0)
            {
                MessageBox.Show("색을 선택해주세요.");
                return;
            }

            th = new Thread(new ThreadStart(registrationuser));
            listthread.Add(th);
            th.Start();
            timer = new System.Timers.Timer(180000);
            listtimer.Add(timer);
            timer.Start();
            timer.Elapsed += HandleTimerElapsed;

            _mainwindow.stackday.Children.Clear();
            var ScheduleofDay = new ScheduleOfDay();
            _mainwindow. stackday.Children.Add(ScheduleofDay);

            _mainwindow.ScheduleOfWeek_parentGrid.Children.Clear();
            ScheduleOfWeek sch = new ScheduleOfWeek();
            _mainwindow.ScheduleOfWeek_parentGrid.Children.Add(sch);
        }


        private void registrationuser()
        {
            //https://www.google.com/settings/security/lesssecureapps?pli=1 보안수준 낮추는 기능추가
            DBconnect db = new DBconnect();

            TextRange textusername = new TextRange(tb_username.Document.ContentStart, tb_username.Document.ContentEnd);
            //TextRange textpicture = new TextRange(tb_picture.Document.ContentStart, tb_picture.Document.ContentEnd);
            string user_id = makeusername(textusername.Text);

            if (db.SearchDB(user_id, (index+1).ToString(), textusername.Text))
            {
                db.inputDB(user_id, (index+1).ToString(), textusername.Text, image);//,textpicture.Text
            }

        }


        private void CustomTextBlock_Click(object sender, RoutedEventArgs e)
        {
            CustomTextBlock custom = sender as CustomTextBlock;
            index = lsbBrushes.Items.IndexOf(custom.DataContext);
        }

        private void bt_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            _mainwindow.MainGrid.Opacity = 0;
        }

        List<Event> wholeevent = new List<Event>();

        public string makeusername(string username)
        {
            string user_id = "";
            try
            {

                AllCalendarLoad gCalendar = new AllCalendarLoad();
                IList<Event> events = gCalendar.CalendarLoad(username, out user_id);

                List<Event> dayevents = new List<Event>();

                //int index = 0; // 출력한다 부분
                try
                {
                    //하루치 이벤트를 읽어온다.
                    foreach (Event evt

                        in events)
                        if (evt.Start.DateTime.Value.Day == DateTime.Now.Day)
                            dayevents.Add(evt);
                }
                catch (InvalidOperationException)
                {
                    //보안수준을 낮추세요.
                    MessageBoxResult result = MessageBox.Show("보안수준을 낮추세요");

                }
                //wholeevent.AddRange(dayevents);
                ////정렬한다.
                //IEnumerable<Event> sortedevent =
                //   from dayevent in wholeevent
                //   orderby dayevent.Start.DateTime.Value.Hour ascending, dayevent.Start.DateTime.Value.Minute ascending
                //   select dayevent;


                ////출력한다.
                //foreach (Event evt in sortedevent)
                //{
                //    index++;
                //    if (index <= 15)
                //    {
                //        var Textblocktime = new CustomTextBlock()
                //        {
                //            Style = (Style)FindResource("WeekTextBlockuser1"),
                //        };
                //        var Textblockplan = new CustomTextBlock()
                //        {
                //            Style = (Style)FindResource("WeekTextBlockuser1"),
                //        };

                //        Textblocktime.Text += evt.Start.DateTime.Value.TimeOfDay.ToString().Substring(0, 5) + " ~ " + evt.End.DateTime.Value.TimeOfDay.ToString().Substring(0, 5) + Environment.NewLine;
                //        Textblockplan.Text += evt.Summary;

                //    }


                //}

            }
            catch (AggregateException)
            {

                MessageBoxResult result = MessageBox.Show("왜안되");

            }


            return user_id;
        }
    }
}
