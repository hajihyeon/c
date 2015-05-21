using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Windows;

using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using System.Data;
using System.Windows.Threading;

namespace SchedulerStart
{
    class GoogleCalendarLoad
    {
        static IList<string> scopes = new List<string>();
        static CalendarService service;      
        static public List<Event> EventCollectionAllusers = new List<Event>();

        private IList<Event> events;

        public GoogleCalendarLoad()
        {
            GoogleLoadRefresh();

           // GoogleLoadRefreshStart();
        }


        public void GoogleLoadRefresh()
        {
            EventCollectionAllusers.Clear();
            foreach (DataRow dbdata in DBconnect.loadDB().Rows)
            {
                events = GoogleCalendarLoad.CalendarLoad(dbdata.ItemArray[2].ToString());
                foreach (Event evt in events)
                {
                    EventCollectionAllusers.Add(evt);
                }
            }
        }


        DispatcherTimer Timer = new DispatcherTimer();

        private void GoogleLoadRefreshStart()
        {
            Timer.Interval = new TimeSpan(0, 1, 0);
            Timer.Tick += GoogleLoadInit;
            Timer.Start();
        }

        void GoogleLoadInit(object sender, EventArgs e)
        {
            EventCollectionAllusers.Clear();
            foreach (DataRow dbdata in DBconnect.loadDB().Rows)
            {
                events = GoogleCalendarLoad.CalendarLoad(dbdata.ItemArray[2].ToString());
                foreach (Event evt in events)
                {
                        EventCollectionAllusers.Add(evt);
                }
            }
        }

        public static IList<Event> CalendarLoad(string username="")
        {
            scopes.Add(CalendarService.Scope.Calendar);
            UserCredential credential;
            using (FileStream stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets, scopes, username, CancellationToken.None,
                        new FileDataStore("Calendar.VB.Sample")).Result;
            }

            service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Google Calander Test Project",
            });

            IList<CalendarListEntry> list = service.CalendarList.List().Execute().Items;


            EventsResource.ListRequest request = service.Events.List(list[0].Id);

            request.MaxResults = 1000;
            request.TimeMin = DateTime.Now.AddDays(-1);
            request.TimeMax = DateTime.Now.AddDays(4);
          
            return request.Execute().Items;
            //foreach (Event calendarEvent in request.Execute().Items)
            //{
            //    string startDate = "Unspecified";
            //    if (calendarEvent.Start != null)
            //    {
            //        if (calendarEvent.Start.Date != null)
            //        {
            //            startDate = startDate = calendarEvent.Start.Date.ToString();
            //        }
            //        else
            //        {
            //            startDate = calendarEvent.Start.DateTime.Value.ToString("yyyy-MM-dd");
            //        }
            //    }
            //    //MessageBox.Show((calendarEvent.Summary + ". Start at: " + startDate));
            //}



            ////DisplayList(list);
            //foreach (CalendarListEntry calendar in list)
            //{
            //    DisplayFirstCalendarEvents(calendar);
            //}
        }

        private static void DisplayFirstCalendarEvents(CalendarListEntry list)
        {         
            EventsResource.ListRequest request = service.Events.List(list.Id);

            // Set MaxResults and TimeMin with sample values
            request.MaxResults = 1000;
            request.TimeMin = DateTime.Now.AddMonths(-1);
            request.TimeMax = DateTime.Now.AddMonths(1);

            // Fetch the list of events
            foreach (Event calendarEvent in request.Execute().Items)
            {
                string startDate = "Unspecified";
                if (calendarEvent.Start != null)
                {
                    if (calendarEvent.Start.Date != null)
                    {
                        startDate = startDate = calendarEvent.Start.Date.ToString();
                    }
                    else
                    {
                        startDate = calendarEvent.Start.DateTime.Value.ToString("yyyy-MM-dd");
                    }
                }
                MessageBox.Show((calendarEvent.Summary + ". Start at: " + startDate));
            }
        }

        private static void DisplayList(IList<CalendarListEntry> list)
        {
            //Console.WriteLine("Lists of calendars:");
            //foreach (CalendarListEntry item in list)
            //{
            //    Console.WriteLine(item.Summary + ". Location: " + item.Location + ", TimeZone: " + item.TimeZone);
            //}
        }

    }
}
