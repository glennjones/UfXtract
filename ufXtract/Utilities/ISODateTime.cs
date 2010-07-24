//Copyright (c) 2007 - 2010 Glenn Jones

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace UfXtract.Utilities
{

    /// <summary>
    /// ISO 8601 datetime structure and conversion class
    /// </summary>
    public class ISODateTime
    {
        private int year = -1;
        private int month= -1;
        private int date = -1;
        private int hour = -1;
        private int minute = -1;
        private int second = -1;
        private decimal decimalSecond = -1;
        private bool utc = false;
        private string timeZoneSuffix = "";
        private int timeZoneHour = -1;
        private int timeZoneMinute = -1;

        protected string datePattern = @"(\d\d\d\d)?-?(\d\d)?-?(\d\d)?";
        protected string timePattern = @"(\d\d)?:?(\d\d)?:?(\d\d)?.?([0-9]+)?";
        protected string timeZonePattern = @"([-+]{1})?(\d\d)?:?(\d\d)?";
        protected string dateSeparator = "-";
        protected string timeSeparator = ":";
        private ProfileType profile = ProfileType.None;

        /// <summary>
        /// ISO 8601 datetime structure and conversion class
        /// </summary>
        public ISODateTime(){}


        public void Reset()
        {
            year = -1;
            month= -1;
            date = -1;
            hour = -1;
            minute = -1;
            second  = -1;
            decimalSecond = -1;
            utc = false;
            timeZoneSuffix = "";
            timeZoneHour = -1;
            timeZoneMinute = -1;   
        }


        public void Parse(string datetime)
        {
            // Clears
            this.Reset();

            // Only need for RFC 3389 profile
            if( Profile == ProfileType.Rfc3389 )
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
                        {
                            parts = timePart.Split('+');
                            this.TimeZoneSuffix = "+";
                        }

                        if (timePart.IndexOf('-') > -1)
                        {
                            parts = timePart.Split('-');
                            this.TimeZoneSuffix = "-";
                        }

                        timePart = parts[0];
                        if(parts.Length > 0)
                            timeZonePart = this.TimeZoneSuffix + parts[1];

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
        }


        // This function parses fragmented structures from the microformat value-class-pattern 
        public string ParseUFFragmented(string datetime)
        {
            datetime = datetime.ToUpper().Trim().Replace("  ", " ");
            datetime = datetime.Replace(" PM", "PM");
            datetime = datetime.Replace(" P.M.", "P.M.");
            datetime = datetime.Replace(" AM", "AM");
            datetime = datetime.Replace(" A.M.", "A.M.");
            bool uct = false;

            if (datetime.IndexOf('Z') > -1)
                uct = true;

            string output = "";
            string[] parts = new string[1];
            parts[0] = datetime;

            if (datetime.IndexOf(' ') > -1)
                parts = datetime.Split(' ');


            for (int i = 0; i < parts.Length; i++)
            {
                parts[i] = parts[i].Trim();

                //Is it a date structure
                if ((parts[i].Length == 4 
                    && parts[i].Contains("PM") == false 
                    && parts[i].Contains("AM") == false 
                    && parts[i].StartsWith("+") == false 
                    && parts[i].StartsWith("-") == false) 
                    || (parts[i].Contains("-") && i == 0))
                {
                    if (parts[i].Contains("-"))
                    {
                        string[] dataParts = parts[i].Split('-');
                        // 2009-234  YYYY-DDD format
                        if (dataParts.Length == 2)
                        {
                            DateTime dateTime = new DateTime(Convert.ToInt32(dataParts[0]), 1, 1);
                            dateTime = dateTime.AddDays(Convert.ToInt32(dataParts[1])-1);
                            parts[i] = dateTime.Year.ToString() + "-" + TwoDigitString(dateTime.Month) + "-" + TwoDigitString(dateTime.Day);
                        }
                    }
                }


                //Is it a time structure
                if ((parts[i].Length < 3 || parts[i].Contains(":") || parts[i].Contains("PM") || parts[i].Contains("P.M.") 
                    || parts[i].Contains("AM")
                    || parts[i].Contains("A.M.") )
                    && parts[i].StartsWith("+") == false
                    && parts[i].StartsWith("-") == false)
                {

                    string timez = string.Empty;
                    // If time segement contains timezone data
                    if (parts[i].Contains("-") || parts[i].Contains("+"))
                    {
                        string[] timezParts;

                        if (parts[i].Contains("-"))
                        {
                            timezParts = parts[i].Split('-');
                            parts[i] = timezParts[0].Trim();
                            timez = FixTimeZoneString("-" + FixTimeString(timezParts[1].Trim()));
                        }

                        if (parts[i].Contains("+"))
                        {
                            timezParts = parts[i].Split('+');
                            parts[i] = timezParts[0].Trim();
                            timez = FixTimeZoneString("+" + FixTimeString(timezParts[1].Trim()));
                        }

                     
                    }


                    if (parts[i].Contains("PM") || parts[i].Contains("P.M."))
                    {
                        parts[i] = parts[i].Replace("PM", "").Trim();
                        parts[i] = parts[i].Replace("P.M.", "").Trim();
                        parts[i] = parts[i].Replace("Z", "").Trim();

                        parts[i] = FixTimeString(parts[i]);

                        // Parse time from string
                        ParseTime(parts[i]);
                        if (Hour < 12)
                            Hour = Hour + 12;

                        // Rebuild new time structure
                        parts[i] = TwoDigitString(Hour);
                        if (Minute != -1)
                            parts[i] += ":" + TwoDigitString(Minute);
                        if (Second != -1)
                            parts[i] += ":" + TwoDigitString(Second);
                        Reset();

                    }

                    if (parts[i].Contains("AM") || parts[i].Contains("A.M."))
                    {
                        parts[i] = parts[i].Replace("AM", "").Trim();
                        parts[i] = parts[i].Replace("A.M.", "").Trim();
                        parts[i] = parts[i].Replace("Z", "").Trim();

                        parts[i] = FixTimeString(parts[i]);

                        // Parse time from string
                        ParseTime(parts[i]);
                        if (Hour == 12)
                            Hour = 0;

                        // Rebuild new time structure
                        parts[i] = TwoDigitString(Hour);
                        if (Minute != -1)
                            parts[i] += ":" + TwoDigitString(Minute);
                        if (Second != -1)
                            parts[i] += ":" + TwoDigitString(Second);
                        Reset();

                    }

                    if (uct)
                        parts[i] += "Z";

                    parts[i] += timez;

          
                    parts[i] = "T" + parts[i];
                }
                

                //Is it a timezone structure
                if (parts[i].StartsWith("+") || parts[i].StartsWith("-"))
                {
                    parts[i] = FixTimeZoneString(parts[i]);
                }

                output += parts[i];

            }

            return output;

        }




        private string FixTimeString(string time)
        {
            // If we have just an hour and it less than two digits
            if (time.Length == 1)
                time = TwoDigitString(Convert.ToInt32(time));

            // If hour starts is still not two digits  
            if (time.IndexOf(":") == 1)
                time = "0" + time;

            return time;

        }


        private string FixTimeZoneString(string time)
        {

            if (time.Contains("-"))
            {
                time = FixTimeString(time.Replace("-", ""));

                if (time.Length == 4 && time.Contains(":"))
                    time = time.Substring(0, 2) + ":" + time.Substring(3, 4);

                if (time.Length == 6 && time.Contains(":"))
                    time = time.Substring(0, 2) + ":" + time.Substring(3, 4) + ":" + time.Substring(5, 6);

                time = "-" + time;

            }

            if (time.Contains("+"))
            {
                time = FixTimeString(time.Replace("+", ""));

                if(time.Length == 4 && time.Contains(":"))
                    time = time.Substring(0, 2) + ":" + time.Substring(3, 4);

                if (time.Length == 6 && time.Contains(":"))
                    time = time.Substring(0, 2) + ":" + time.Substring(3, 4) + ":" + time.Substring(5, 6);

                time = "+" + time;
            }
            return time;

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
                    this.DecimalSecond = Convert.ToDecimal(FormatNumber("." + match.Groups[4].ToString()));
            }
        }

        /// <summary>
        /// Remove trailing zeros and decimal points from string representation of a number.
        /// </summary>
        /// <param name="number">String number</param>
        /// <returns>String number</returns>
        public string FormatNumber(string number)
        {
            number = number.TrimEnd("0".ToCharArray());
            number = number.TrimEnd(".".ToCharArray());
            return number;
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
            if (Year != -1 && Month != -1 && Date != -1)
            {
                datetime = new DateTime(Year, Month, Date, new GregorianCalendar());
                if (Hour != -1)
                {
                    if (Minute == -1 || Second == -1)
                    {
                        if (Minute == -1 && Second == -1)
                            datetime = new DateTime(Year, Month, Date, Hour, 0, 0, new GregorianCalendar());
                        if (Minute != -1 && Second == -1)
                            datetime = new DateTime(Year, Month, Date, Hour, Minute, 0, new GregorianCalendar());
                    }
                }
                if (Hour != -1 && Minute != -1 && Second != -1)
                {
                    datetime = new DateTime(Year, Month, Date, Hour, Minute, Second, new GregorianCalendar());
                    if (DecimalSecond != -1)
                        datetime = new DateTime(Year, Month, Date, Hour, Minute, Second, DecimalSecondToInt(), new GregorianCalendar());
                }
            }
            return datetime;
        }

        // For dates that may only have year or year/month
        public DateTime ToDateTime(bool beginningOf)
        {
            // If we have all three date parts
            DateTime datetime = ToDateTime();


            if (Year != -1 && Month != -1 && Date == -1)
                datetime = new DateTime(Year, Month, 1, new GregorianCalendar());

            if (Year != -1 && Month == -1 && Date == -1)
                datetime = new DateTime(Year, 1, 1, new GregorianCalendar());


            if (Hour != -1)
            {
                if (Minute == -1 || Second == -1)
                {
                    if (Minute == -1 && Second == -1)
                        datetime = new DateTime(Year, Month, Date, Hour, 0, 0, new GregorianCalendar());
                    if (Minute != -1 && Second == -1)
                        datetime = new DateTime(Year, Month, Date, Hour, Minute, 0, new GregorianCalendar());
                }
            }
            if (Hour != -1 && Minute != -1 && Second != -1)
            {
                datetime = new DateTime(Year, Month, Date, Hour, Minute, Second, new GregorianCalendar());
                if (DecimalSecond != -1)
                    datetime = new DateTime(Year, Month, Date, Hour, Minute, Second, DecimalSecondToInt(), new GregorianCalendar());
            }

            // Get the end of the period
            if (beginningOf == false)
            {
                if (Year != -1 && Month != -1 && Date == -1)
                    datetime = datetime.AddMonths(1).AddDays(-1);

                if (Year != -1 && Month == -1 && Date == -1)
                    datetime =  datetime.AddYears(1).AddDays(-1);

            }
    
            return datetime;
        }




        public override string ToString()
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
                                            output += '.' + DecimalSecondToString(); 
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


        public string TwoDigitString(int number)
        {
            string output = number.ToString();
            if (output.Length == 1)
                return "0" + output;
            else
                return output;
        }

        private int DecimalSecondToInt()
        {
            string output = DecimalSecond.ToString();
            output = output.Replace(".","");
            int milsec = Convert.ToInt32(output);
            if (milsec > 999)
                milsec = 999;
            return milsec;
        }

        private string DecimalSecondToString()
        {
            string output = DecimalSecond.ToString();
            return output.Replace("0.", "");
        }

        /// <summary>
        /// Year element of DateTime
        /// </summary>
        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        /// <summary>
        /// Month element of DateTime
        /// </summary>
        public int Month
        {
            get { return month; }
            set { month= value; }
        }

        /// <summary>
        /// Date element of DateTime
        /// </summary>
        public int Date
        {
            get { return date; }
            set { date = value; }
        }

        /// <summary>
        /// Hour element of DateTime
        /// </summary>
        public int Hour
        {
            get { return hour; }
            set { hour = value; }
        }

        /// <summary>
        /// Minute element of DateTime
        /// </summary>
        public int Minute
        {
            get { return minute; }
            set { minute = value; }
        }

        /// <summary>
        /// Second element of DateTime
        /// </summary>
        public int Second
        {
            get { return second; }
            set { second = value; }
        }

        /// <summary>
        /// Decimal second element of DateTime
        /// </summary>
        public decimal DecimalSecond
        {
            get { return decimalSecond; }
            set { decimalSecond = value;}
        }

        /// <summary>
        /// UTC version of DateTime
        /// </summary>
        public bool UTC
        {
            get { return utc; }
            set { utc = value; }
        }

        /// <summary>
        /// Time zone suffix element of DateTime
        /// </summary>
        public string TimeZoneSuffix
        {
            get { return timeZoneSuffix; }
            set { timeZoneSuffix = value; }
        }

        /// <summary>
        /// Time zone hour element of DateTime
        /// </summary>
        public int TimeZoneHour
        {
            get { return timeZoneHour; }
            set { timeZoneHour = value; }
        }

        /// <summary>
        /// Time zone minute element of DateTime
        /// </summary>
        public int TimeZoneMinute
        {
            get { return timeZoneMinute; }
            set { timeZoneMinute = value; }
        }

        /// <summary>
        /// Current profile DateTime
        /// </summary>
        public ProfileType Profile
        {
            get { return profile; }
            set { profile = value; }
        }

        /// <summary>
        /// DateTime profile types
        /// </summary>
        public enum ProfileType
        {
            None,
            Rfc3389,
            W3CNote
        }

    
    }


    /// <summary>
    /// Rfc3389 datetime structure and conversion class
    /// </summary>
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

        /// <summary>
        /// Rfc3389 datetime structure and conversion class
        /// </summary>
        public Rfc3389DateTime() 
        {
            SetProfile();
        }

        /// <summary>
        /// Rfc3389 datetime structure and conversion class
        /// </summary>
        /// <param name="datetime">DataTime to be parsed</param>
        public Rfc3389DateTime( string datetime )
        {
            SetProfile();
            Parse(datetime);
        }

        /// <summary>
        /// Set current profile to Rfc3389
        /// </summary>
        private void SetProfile()
        {
            datePattern = @"(\d\d\d\d)?-?(\d\d)?-?(\d\d)?";
            timePattern = @"(\d\d)?:?(\d\d)?:?(\d\d)?.?([0-9]+)?";
            timeZonePattern = @"([-+]{1})?(\d\d)?:?(\d\d)?";
            dateSeparator = "-";
            timeSeparator = ":";
            Profile = ProfileType.Rfc3389;
        }
  
    }



    /// <summary>
    /// W3CNoteDateTime structure and conversion class
    /// </summary>
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

        /// <summary>
        /// W3CNoteDateTime structure and conversion class
        /// </summary>
        public W3CNoteDateTime()
        {
            SetProfile();
        }

        /// <summary>
        /// W3CNoteDateTime structure and conversion class
        /// </summary>
        /// <param name="datetime">DataTime to be parsed</param>
        public W3CNoteDateTime( string datetime )
        {
            SetProfile();
            Parse(datetime);
        }

        /// <summary>
        /// Set current profile to W3CNoteDateTime
        /// </summary>
        private void SetProfile()
        {
            datePattern = @"(\d\d\d\d)?(-\d\d)?(-\d\d)?";
            timePattern = @"(\d\d)?(:\d\d)?(:\d\d)?.?([0-9]+)?";
            timeZonePattern = @"([-+]{1})?(\d\d)?(:\d\d)?";
            dateSeparator = "-";
            timeSeparator = ":";
            Profile = ProfileType.W3CNote;
        }

  
    } 
}
