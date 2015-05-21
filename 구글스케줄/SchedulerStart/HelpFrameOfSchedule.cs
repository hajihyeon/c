using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3.Data;

namespace SchedulerStart
{
    class HelpFrameOfSchedule
    {
        static DayOfWeek Today = DateTime.Now.DayOfWeek;
        enum dayToKorean { 일, 월, 화, 수, 목, 금, 토 }

        /// <summary>
        /// 주간 일정표 헤더 표시방식
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        static public string LabelDayStringForm(DateTime? time)
        {
            return (time.Value.Month.ToString() + "." + time.Value.Day.ToString() + " (" + ((dayToKorean)(int)time.Value.DayOfWeek + ")"));

        }

        /// <summary>
        ///  time 시간에 해당하는 row 위치 반환함수
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public int PositionOfTime(DateTime? time)
        {
            if (time.Value.Minute == 0)
            {
                return time.Value.Hour * 2;
            }
            else if (time.Value.Minute == 30)
            {
                return time.Value.Hour * 2 + 1;
            }
            else
                return 0;
        }

        /// <summary>
        ///  오늘 날짜와 이벤트 날짜를 비교하여 오늘을 기준으로 좌우로 표시할 이벤트Column 위치 반환
        /// </summary>
        /// <param name="today"></param>
        /// <param name="evtday"></param>
        /// <returns></returns>
        public int PositionOfDay(DayOfWeek today, DayOfWeek evtday)
        {
            int n =(int)evtday- (int)today;

            if (n == 0)
            {
                return 1;
            }

            switch ((int)today)
            {
                case 0: case 1: case 2: case 3:
                    return n+1;

                case 4: case 5: case 6:
                    if (n > 0)
                        return n + 1;
                    else
                        return n + 8;
            }

            return 0;
        }

        /// <summary>
        /// 리스트로 받은 이벤트들을 각 위치에 할당 시켜주는함수.
        /// </summary>
        /// <param name="evt"></param>
        /// <param name="GridPosition_day"></param>
        /// <param name="GridPosition_starttime"></param>
        /// <param name="GridPosition_span"></param>
        public void SeparationOfEvent(Event evt, out int GridPosition_day, out int GridPosition_starttime, out int GridPosition_span)
        {
            DayOfWeek today = DateTime.Now.DayOfWeek;

            if (evt.Start.DateTime != null)
            {
            }
            DayOfWeek evtday = evt.Start.DateTime.Value.DayOfWeek;

            GridPosition_day = PositionOfDay(today, evtday);
            GridPosition_starttime = PositionOfTime(evt.Start.DateTime);
            if (evt.End.DateTime.Value.Hour != 0) // 24시까지 스케줄일경우 00:00 으로 반환하는 것때문에 따로 처리
            {
                GridPosition_span = PositionOfTime(evt.End.DateTime) - GridPosition_starttime;
            }
            else
            {
                GridPosition_span =48- GridPosition_starttime;
            }
        }

    }
}
