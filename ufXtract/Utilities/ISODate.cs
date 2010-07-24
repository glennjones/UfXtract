using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace ufXtract.Utilities
{


    public class ISODateTime
    {
        private int _year;
        private int _month;
        private int _date;
        private int _hour;
        private int _minute;
        private int _second;
        private int _decimalSecond;
        private bool _utc;
        private string _timeZoneSuffix;
        private int _timeZoneHour;
        private int _timeZoneMinute;

        protected string datePattern = @"(\d\d\d\d)?-?(\d\d)?-?(\d\d)?";
        protected string timePattern = @"(\d\d)?:?(\d\d)?:?(\d\d)?.?([0-9]+)?";
        protected string timeZonePattern = @"[-+]{1})?(\d\d)?:?(\d\d)?";
        protected string dateSeparator = "-";
        protected string timeSeparator = ":";
        private ProfileType _profile = ProfileType.None;

        public ISODateTime()
        {
        }

        public void Reset()
        { 
            _year = new int();
            _month = new int();
            _date = new int();
            _hour = new int();
            _minute = new int();
            _second = new int();
            _decimalSecond = new int();
            _utc = new bool();
            _timeZoneSuffix = null;
            _timeZoneHour = new int();
            _timeZoneMinute = new int();   
        }


        public DateTime Parse(string datetime)
        {
            // Clears
            this.Reset();

            // Only need for W3C profile
            if( Profile == ProfileType.W3CNote )
                datetime = datetime.ToUpper();

            string[] parts;
            string datePart = "";
            string timePart = "";
            string timeZonePart = "";

            if (datetime.IndexOf('T') > -1)
            {
                parts = datetime.Split('T');
                datePart = parts[0];
                timePart = parts[1];

                //Zulu UTC and time zone break
                if (timePart.IndexOf('Z') > -1 || timePart.IndexOf('+') > -1 || timePart.IndexOf('-') > -1)
                {
                    // Zulu time
                    if (timePart.IndexOf('Z') > -1)
                    {
                        timePart = timePart.Replace("Z", "");
                        this.UTC = true;
                        this.TimeZoneSuffix = "Z";
                    }

                    // Timezone
                    if (timePart.IndexOf('+') > -1 || timePart.IndexOf('-') > -1)
                    {
                        int position = 0;
                        if (timePart.IndexOf('+') > -1)
                            position = timePart.IndexOf('+');
                        else
                            position = timePart.IndexOf('-');

                        timeZonePart = timePart.Substring(position, timePart.Length);
                        timePart = timePart.Substring(0, position);
                    }
                }
            }
            else
            {
                datePart = datetime;
            }

            if (datePart != "")
            {
                ParseDate(datePart);
                if (timePart != "")
                {
                    ParseTime(timePart);
                    if (timeZonePart != "")
                    {
                        ParseTimeZone(timeZonePart);
                    }
                }
            }



            return new DateTime();
        }


        private void ParseDate(string dateString)
        {
            string[] parts;
            Regex regexp = new Regex( datePattern );
            Match match = regexp.Match(dateString);
            if (match.Success)
            {
                if (match.Groups[1].Success)
                    this.Year = Convert.ToInt32(match.Groups[1].ToString().Replace(dateSeparator,""));
                if (match.Groups[2].Success)
                    this.Month = Convert.ToInt32(match.Groups[2].ToString().Replace(dateSeparator, ""));
                if (match.Groups[3].Success)
                    this.Date = Convert.ToInt32(match.Groups[3].ToString().Replace(dateSeparator, ""));

            }
        }

        private void ParseTime(string timeString)
        {
            string[] parts;
            Regex regexp = new Regex( timePattern );
            Match match = regexp.Match(timeString);
            if (match.Success)
            {
                if (match.Groups[1].Success)
                    this.Hour = Convert.ToInt32(match.Groups[1].ToString().Replace(timeSeparator, ""));
                if (match.Groups[2].Success)
                    this.Minute = Convert.ToInt32(match.Groups[2].ToString().Replace(timeSeparator, ""));
                if (match.Groups[3].Success)
                    this.Second = Convert.ToInt32(match.Groups[3].ToString().Replace(timeSeparator, ""));
                if (match.Groups[4].Success)
                    this.DecimalSecond = Convert.ToInt32(match.Groups[4].ToString());
            }
        }

        private void ParseTimeZone(string timeString)
        {
            string[] parts;
            Regex regexp = new Regex( timeZonePattern );
            Match match = regexp.Match(timeString);
            if (match.Success)
            {
                if (match.Groups[1].Success)
                    this.TimeZoneSuffix = match.Groups[1].ToString();
                if (match.Groups[2].Success)
                    this.TimeZoneHour = Convert.ToInt32(match.Groups[2].ToString().Replace(timeSeparator, ""));
                if (match.Groups[3].Success)
                    this.TimeZoneMinute = Convert.ToInt32(match.Groups[3].ToString().Replace(timeSeparator, ""));
            }
        }



        public DateTime ToDateTime()
        {   
            DateTime datetime = new DateTime();
            if ( Year != 0 && Month != 0 && Date != 0 )
                datetime = new DateTime(Year, Month, Date, Hour, Minute, Second, DecimalSecond, new GregorianCalendar());
            
            return datetime;
        }


        public DateTime ToUTCDateTime()
        {
            //if( this.
            DateTime datetime = new DateTime(Year, Month, Date, Hour, Minute, Second, DecimalSecond, new GregorianCalendar(), DateTimeKind.Utc);
            return datetime;
        }

        //public string ToString(DateTime datetime)
        //{
        //    // Add code
        //    return ToString();
        //}

        public string ToString()
        {
            string output = "";
	        if( Year > 0 ){			
		        output = Year.ToString();
		        if( Month > 0 && Month < 13  ){ 
			        output += dateSeparator + TwoDigitString(Month);	
			        if(Date > 0 && Date < 32  ){ 
				        output += dateSeparator + TwoDigitString(Date);

				        // Time and can only be created with a full date
				        if( Hour > 0 ){
					        if( Hour > -1 && Hour < 24 ) {
						        output += 'T' + TwoDigitString(Hour);
						        if( Minute > -1 && Minute < 60 ) {
							        output += timeSeparator + TwoDigitString(Minute);
							        if( Second > -1 && Second < 61 ) {
								        output += timeSeparator + TwoDigitString(Second);
								        if( DecimalSecond > 0 )
									        output += '.' + DecimalSecond.ToString(); 
							        }
						        }

                                // Time zone offset can only be created with a hour
                                if(TimeZoneSuffix != "") 
                                    output += TimeZoneSuffix; 
                                
                                if( TimeZoneHour > 0 )
                                {
                                    if( TimeZoneHour > -1 && TimeZoneHour < 24 ) {
                                        output += TwoDigitString(TimeZoneHour);
                                        if (TimeZoneMinute > -1 && TimeZoneMinute < 60)
                                            output += timeSeparator + TimeZoneMinute;
                                    }
                                }
					        }	
				        }	
			        }
		        }	
	        }
	        return output;
        }


        private string TwoDigitString(int number)
        {
            string output = number.ToString();
            if (output.Length == 1)
                return "0" + output;
            else
                return output;
        }

      
        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }

        public int Month
        {
            get { return _month; }
            set { _month = value; }
        }
        
        public int Date
        {
            get { return _date; }
            set { _date = value; }
        }
        
        public int Hour
        {
            get { return _hour; }
            set { _hour = value; }
        }
        
        public int Minute
        {
            get { return _minute; }
            set { _minute = value; }
        }

        public int Second
        {
            get { return _second; }
            set { _second = value; }
        }

        public int DecimalSecond
        {
            get { return _decimalSecond; }
            set { _decimalSecond = value;}
        }

        public bool UTC
        {
            get { return _utc; }
            set { _utc = value; }
        }
    
        public string TimeZoneSuffix
        {
            get { return _timeZoneSuffix; }
            set { _timeZoneSuffix = value; }
        }

        public int TimeZoneHour
        {
            get { return _timeZoneHour; }
            set { _timeZoneHour = value; }
        }

        public int TimeZoneMinute
        {
            get { return _timeZoneMinute; }
            set { _timeZoneMinute = value; }
        }

        public ProfileType Profile
        {
            get { return _profile; }
            set { _profile = value; }
        }

        public enum ProfileType
        {
            None,
            Rfc3389,
            W3CNote
        }

    
    }




    public class Rfc3389DateTime : ISODateTime
    {
        //Should work against all these formats
        //2007
        //2007-05
        //2007-05-01T11:30
        //2007-05-01T11:30Z
        //2007-05-01T11:30:00Z
        //2007-05-01T11:30+08:00
        //2007-05-01T11:30:00+08:00
        //2007-05-01T11:30:00.0135
        //200801
        //20080121
        //20070501T1130
        //20070501T113015
        //20070501T113015Z
        //20070501t113025z
        //2007-05-01T113025
        //20070501T11:30:25


        public Rfc3389DateTime() 
        {
            SetProfile();
        }

        public Rfc3389DateTime( string datetime )
        {
            SetProfile();
            Parse(datetime);
        }

        private void SetProfile()
        {
            datePattern = @"(\d\d\d\d)?-?(\d\d)?-?(\d\d)?";
            timePattern = @"(\d\d)?:?(\d\d)?:?(\d\d)?.?([0-9]+)?";
            timeZonePattern = @"[-+]{1})?(\d\d)?:?(\d\d)?";
            dateSeparator = "-";
            timeSeparator = ":";
            Profile = ProfileType.Rfc3389;
        }
  
    }




    public class W3CNoteDateTime : ISODateTime
    {
        //Should work against all these formats
        //2007
        //2007-05
        //2007-05-01T11:30
        //2007-05-01T11:30Z
        //2007-05-01T11:30:00Z
        //2007-05-01T11:30+08:00
        //2007-05-01T11:30:00+08:00
        //2007-05-01T11:30:00.0135

        public W3CNoteDateTime()
        {
            SetProfile();
        }

        public W3CNoteDateTime( string datetime )
        {
            SetProfile();
            Parse(datetime);
        }

        private void SetProfile()
        {
            datePattern = @"(\d\d\d\d-)?(\d\d)?(-\d\d)?";
            timePattern = @"(\d\d:)?(\d\d)?(:\d\d)?.?([0-9]+)?";
            timeZonePattern = @"[-+]{1})?(\d\d)?(:\d\d)?";
            dateSeparator = "-";
            timeSeparator = ":";
            Profile = ProfileType.W3CNote;
        }

  
    } 
}
