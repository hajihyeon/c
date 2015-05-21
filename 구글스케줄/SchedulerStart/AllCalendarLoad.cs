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

namespace SchedulerStart
{
    class AllCalendarLoad
    {

        IList<string> scopes = new List<string>();
        CalendarService service;


        public IList<Event> CalendarLoad(string username, out string user_id)
        {
            scopes.Add(CalendarService.Scope.Calendar);
            UserCredential credential;
            using (FileStream stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets, scopes, username.Replace("\r", "").Replace("\n", ""), CancellationToken.None,
                        new FileDataStore("Calendar.VB.Sample")).Result;
            }

            service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Google Calander Test Project",
            });

            
            IList<CalendarListEntry> list = service.CalendarList.List().Execute().Items;
            user_id = list[0].Id;

            EventsResource.ListRequest request = service.Events.List(list[0].Id);

            request.MaxResults = 1000;
            request.TimeMin = DateTime.Now.AddMonths(-1);
            request.TimeMax = DateTime.Now.AddMonths(3);

            return request.Execute().Items;

        }

        private void DisplayFirstCalendarEvents(CalendarListEntry list)
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
              //  MessageBox.Show((calendarEvent.Summary + ". Start at: " + startDate));
            }
        }

        private void DisplayList(IList<CalendarListEntry> list)
        {
            //Console.WriteLine("Lists of calendars:");
            //foreach (CalendarListEntry item in list)
            //{
            //    Console.WriteLine(item.Summary + ". Location: " + item.Location + ", TimeZone: " + item.TimeZone);
            //}
        }
    }
}
