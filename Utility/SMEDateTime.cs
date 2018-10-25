using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace SMEPayroll
{
    public class SMEDateTime
    {

        #region Public constants
        public const char TIME_SEPERATOR = ':';
        #endregion

        #region Declarations
        public int Hour;
        public int Minute;
        public int Second;
        #endregion

        #region Constructors
        public SMEDateTime()
        {
            Hour = DateTime.Now.Hour;
            Minute = DateTime.Now.Minute;
            Second = DateTime.Now.Second;
        }
        public SMEDateTime(string value)
        {
            string[] vals = value.Split(TIME_SEPERATOR);
            Hour = int.Parse(vals[0]);
            Minute = int.Parse(vals[1]);
            if (vals.Length > 2)
                Second = int.Parse(vals[2]);
            new SMEDateTime(this.ToSeconds());
        }

        public SMEDateTime(int hour, int minute, int second)
        {
            Hour = hour;
            Minute = minute;
            Second = second;
            new SMEDateTime(this.ToSeconds());
        }

        public SMEDateTime(int seconds)
        {
            Minute = seconds / 60;
            Second = seconds % 60;
            Hour = Minute / 60;
            Minute = Minute % 60;
        }
        #endregion

        #region Public methods
        public SMEDateTime Add(SMEDateTime time)
        {
            this.Hour += time.Hour;
            this.Minute += time.Minute;
            this.Second += time.Second;
            return new SMEDateTime(GetStringTime(this.ToSeconds()));
        }

        public SMEDateTime Add(string value)
        {
            return Add(new SMEDateTime(value));
        }
        #endregion

        #region Public static methods
        public static SMEDateTime Now()
        {
            DateTime dt = DateTime.Now;
            return GetTimeFromSeconds(ToSeconds(dt));
        }

        public static SMEDateTime TimeDiff(SMEDateTime time1, SMEDateTime time2)
        {
            try
            {
                int _secs1 = time1.ToSeconds();
                int _secs2 = time2.ToSeconds();
                int _secs = _secs1 - _secs2;
                return GetTimeFromSeconds(_secs);
            }
            catch
            {
                return new SMEDateTime(0, 0, 0);
            }
        }

        public static SMEDateTime TimeDiff(string time1, string time2)
        {
            try
            {
                SMEDateTime t1 = new SMEDateTime(time1);
                SMEDateTime t2 = new SMEDateTime(time2);
                return TimeDiff(t1, t2);
            }
            catch
            {
                return new SMEDateTime(0, 0, 0);
            }
        }
        public static SMEDateTime TimeDiff(DateTime dateTime1, DateTime dateTime2)
        {
            try
            {
                TimeSpan span = dateTime1 - dateTime2;
                return new SMEDateTime(span.Seconds);
            }
            catch
            {
                return new SMEDateTime(0, 0, 0);
            }
        }
        public static SMEDateTime TimeDiff(int seconds1, int seconds2)
        {
            try
            {
                SMEDateTime t1 = new SMEDateTime(seconds1);
                SMEDateTime t2 = new SMEDateTime(seconds2);
                return TimeDiff(t1, t2);
            }
            catch
            {
                return new SMEDateTime(0, 0, 0);
            }
        }
        #endregion

        #region Convert methods
        public int ToSeconds()
        {
            return this.Hour * 3600 + this.Minute * 60 + this.Second;
        }
        public static int ToSeconds(DateTime dateTime)
        {
            return dateTime.Hour * 3600 + dateTime.Minute * 60 + dateTime.Second;
        }
        public override string ToString()
        {
            return String.Format("{0:00}:{1:00}:{2:00}", Hour, Minute, Second);
        }
        public static SMEDateTime GetTimeFromSeconds(int seconds)
        {
            int _mins = seconds / 60;
            seconds = seconds % 60;
            int _hours = _mins / 60;
            _mins = _mins % 60;
            return new SMEDateTime(_hours, _mins, seconds);
        }
        private string GetStringTime(int seconds)
        {
            int _mins = seconds / 60;
            seconds = seconds % 60;
            int _hours = _mins / 60;
            _mins = _mins % 60;
            this.Hour = _hours;
            this.Minute = _mins;
            this.Second = seconds;
            return String.Format("{0:00}:{1:00}:{2:00}", _hours, _mins, seconds); ;
        }

        public static SMEDateTime Parse(string value)
        {
            try
            {
                return new SMEDateTime(value);
            }
            catch
            {
                throw new ApplicationException("Error parsing time!");
            }
        }
        #endregion

        #region Subtract time objects
        public static SMEDateTime operator +(SMEDateTime t1, SMEDateTime t2)
        {
            SMEDateTime t3 = new SMEDateTime(t1.Hour, t1.Minute, t1.Second);
            t3.Add(t2);
            return t3;
        }
        public static SMEDateTime operator -(SMEDateTime t1, SMEDateTime t2)
        {
            return TimeDiff(t1, t2);
        }
        #endregion
    }
}

