//Copyright (c) 2007 - 2010 Glenn Jones

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace UfXtract.Utilities
{

    /// <summary>
    /// ISO 8601 duration structure and conversion class
    /// </summary>
    public class ISODuration
    {
        private double years = 0;
        private double months = 0;
        private double weeks = 0;
        private double days = 0;
        private double hours = 0;
        private double minutes = 0;
        private double seconds = 0;


        /// <summary>
        /// ISO 8601 duration structure and conversion class
        /// </summary>
        public ISODuration(){}


        /// <summary>
        /// ISO 8601 duration structure and conversion class
        /// </summary>
        public ISODuration(double years, double months, double weeks, double days, double hours, double minutes, double seconds)
        {
            this.years = years;
            this.months = months;
            this.weeks = weeks;
            this.days = days;
            this.hours = hours;
            this.minutes = minutes;
            this.seconds = seconds;
        }


        /// <summary>
        /// Resets all the properties
        /// </summary>
        public void Reset()
        {
            this.years = 0;
            this.months = 0;
            this.weeks = 0;
            this.days = 0;
            this.hours = 0;
            this.minutes = 0;
            this.seconds = 0;
        }


        /// <summary>
        /// Parses a string representation of a ISO 8601 duration
        /// </summary>
        /// <param name="datetime">ISO 8601 duration</param>
        public void Parse(string duration)
        {
            this.Reset();

            //P9MP1Y2M
            //P1Y2M10D
            //P1Y2M10DT20H
            //P1Y2M10DT20H30M
            //P1Y2M10DT20H30M30S
            //P1Y2M10DT20H30M30.5S
            //P1Y2M10DT20.5H
            //P110D
            //PT30M
            //P0001-02-10
            //P0001-02-10T14:30:30

            if( IsRepresentation1(duration) )
            {
                // Representation format 1 - P1Y2M10DT20H30M30S
                string[] parts;
                string datePart = duration.Replace("P", "");
                string timePart = "";

                if (duration.Contains("T"))
                {
                    parts = duration.Split('T');
                    datePart = parts[0].Replace("P", "");
                    timePart = parts[1];
                }

                // Date segment
                if (datePart.Contains("Y"))
                {
                    this.years = Convert.ToDouble(datePart.Split('Y')[0]);
                    datePart = datePart.Split('Y')[1];
                }
                if (datePart.Contains("M"))
                {
                    this.months = Convert.ToDouble(datePart.Split('M')[0]);
                    datePart = datePart.Split('M')[1];
                }
                if (datePart.Contains("W"))
                {
                    this.weeks = Convert.ToDouble(datePart.Split('W')[0]);
                    datePart = datePart.Split('W')[1];
                }
                if (datePart.Contains("D"))
                {
                    this.days= Convert.ToDouble(datePart.Split('D')[0]);
                    datePart = datePart.Split('D')[1];
                }

                // Time segment
                if (timePart != "")
                {
                    if (timePart.Contains("H"))
                    {
                        this.hours = Convert.ToDouble(timePart.Split('H')[0]);
                        timePart = timePart.Split('H')[1];
                    }
                    if (timePart.Contains("M"))
                    {
                        this.minutes = Convert.ToDouble(timePart.Split('M')[0]);
                        timePart = timePart.Split('M')[1];
                    }
                    if (timePart.Contains("S"))
                    {
                        this.seconds = Convert.ToDouble(timePart.Split('S')[0]);
                        timePart = timePart.Split('S')[1];
                    }
                }


            }
            else
            {
                // Representation format 2 - P0001-02-10T14:30:30              
                string[] parts;
                string pattern = @"P(\d\d\d\d)?-?(\d\d)?-?(\d\d)?T?(\d\d)?:?(\d\d)?:?(\d\d)?";
                Regex regexp = new Regex(pattern);
                Match match = regexp.Match(duration);
                if (match.Success)
                {
                    if (match.Groups[1].Success)
                        this.years = Convert.ToDouble(match.Groups[1].ToString());
                    if (match.Groups[2].Success)
                        this.months = Convert.ToDouble(match.Groups[2].ToString());
                    if (match.Groups[3].Success)
                        this.days = Convert.ToDouble(match.Groups[3].ToString());
                    if (match.Groups[4].Success)
                        this.hours = Convert.ToDouble(match.Groups[4].ToString());
                    if (match.Groups[5].Success)
                        this.minutes = Convert.ToDouble(match.Groups[5].ToString());
                    if (match.Groups[6].Success)
                        this.seconds = Convert.ToDouble(match.Groups[6].ToString());
                }
            }



           
        }

 
        private bool IsRepresentation1(string duration)
        {
            bool isRep = false;
            // Finds chars for this format P1Y2M10DT20H30M30S
            string[] letters = "Y M W D H S".Split(' ');
            foreach(String letter in letters)
            {
                if (duration.Contains(letter))
                    isRep = true;
            }
            return isRep;
        }


        /// <summary>
        /// Adds the cuurent duration to a date
        /// </summary>
        /// <param name="startDate">The start date</param>
        /// <returns>The date with the added duration</returns>
        public DateTime AddToDate( DateTime startDate )
        {
            startDate = startDate.AddYears(Convert.ToInt32(this.years));
            startDate = startDate.AddMonths(Convert.ToInt32(this.months));
            startDate = startDate.AddDays(this.weeks * 7);
            startDate = startDate.AddDays(this.days);
            startDate = startDate.AddHours(this.hours);
            startDate = startDate.AddMinutes(this.Minutes);
            startDate = startDate.AddSeconds(this.Seconds);
            return startDate;
        }

        /// <summary>
        /// Returns a ISO 8601 string representation of a duration
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string output = "";
            if (this.years > 0)
                output += this.years.ToString() + "Y";
            if (this.months > 0)
                output += this.months.ToString() + "M";
            if (this.weeks > 0)
                output += this.weeks.ToString() + "W";
            if (this.days > 0)
                output += this.days.ToString() + "D";

            if (this.hours > 0 || this.minutes > 0 || this.seconds > 0)
                output += "T";

            if (this.hours > 0)
                output += this.hours.ToString() + "H";
            if (this.minutes > 0)
                output += this.minutes.ToString() + "M";
            if (this.seconds > 0)
                output += this.seconds.ToString() + "S";

            if (output != "")
                output = "P" + output;

            return output;
        }


        /// <summary>
        /// Gets and sets the number of years
        /// </summary>
        public double Years
        {
            get { return years; }
            set { years = value; }
        }
        
        /// <summary>
        /// Gets and sets the number of months
        /// </summary>
        public double Months
        {
            get { return months; }
            set { months = value; }
        }

        /// <summary>
        /// Gets and sets the number of weeks
        /// </summary>
        public double Weeks
        {
            get { return weeks; }
            set { weeks = value; }
        }

        /// <summary>
        /// Gets and sets the number of days
        /// </summary>
        public double Days
        {
            get { return days; }
            set { days = value; }
        }

        /// <summary>
        /// Gets and sets the number of hours
        /// </summary>
        public double Hours
        {
            get { return hours; }
            set { hours = value; }
        }

        /// <summary>
        /// Gets and sets the number of minutes
        /// </summary>
        public double Minutes
        {
            get { return minutes; }
            set { minutes = value; }
        }

        /// <summary>
        /// Gets and sets the number of seconds
        /// </summary>
        public double Seconds
        {
            get { return seconds; }
            set { seconds = value; }
        }



    }




    
}
