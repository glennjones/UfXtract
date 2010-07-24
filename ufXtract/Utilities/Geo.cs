//Copyright (c) 2007 - 2010 Glenn Jones

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace UfXtract.Utilities
{

    /// <summary>
    /// Geo structure and conversion class
    /// </summary>
    public class Geo
    {
        private decimal latitude = 0;
        private decimal longitude = 0;

        /// <summary>
        /// Geo structure and conversion
        /// </summary>
        public Geo(){}


        /// <summary>
        /// Geo structure and conversion
        /// </summary>
        /// <param name="latitude">latitude</param>
        /// <param name="longitude">longitude</param>
        public Geo(decimal latitude, decimal longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }


        /// <summary>
        /// Geo structure and conversion
        /// </summary>
        /// <param name="geo">Geo as ; delimited string ie 37.77;-122.41</param>
        public Geo(string geo)
        {
            Parse(geo);
        }

        /// <summary>
        /// Resets all the properties
        /// </summary>
        public void Reset()
        {
            this.latitude = 0;
            this.longitude = 0;
        }

        /// <summary>
        /// Parses a string representation of a geo
        /// </summary>
        /// <param name="geo">Geo as ; delimited string ie 37.77;-122.41</param>
        public void Parse(string geo)
        {
            this.Reset();
            //+23.70000;+90.30000
            //-23.70000;-90.30000
            //23.70000;90.30000
            //23.7;90.3

            if (geo.Contains(";"))
            {
                string[] parts = geo.Split(';');
                this.latitude = Convert.ToDecimal(FormatNumber(parts[0]));
                this.longitude = Convert.ToDecimal(FormatNumber(parts[1]));

                if (this.latitude > 90 || this.latitude < -90)
                    throw (new Exception("Latitude out of range"));

                if (this.longitude > 180 || this.longitude < -180)
                    throw (new Exception("Longitude out of range"));
            }

        }


        /// <summary>
        /// Remove trailing zeros and decimal points from string representation of a number.
        /// </summary>
        /// <param name="number">Number</param>
        /// <returns>Number</returns>
        public string FormatNumber(string number)
        {
            if (number != "0")
            {
                number = number.TrimEnd("0".ToCharArray());
                number = number.TrimEnd(".".ToCharArray());
                number = number.Replace("+", "");
            }
            return number;
        }

        /// <summary>
        /// Parses a string representation of a latitude and longitude
        /// </summary>
        /// <param name="latitude">Latitude string</param>
        /// <param name="longitude">Longitude string</param>
        public void Parse(string latitude, string longitude)
        {
            Parse(latitude + ";" + longitude);
        }


        /// <summary>
        /// Returns a canonicalised geo string representation
        /// </summary>
        /// <returns>String of latitude and longitude separated by ;</returns>
        public override string ToString()
        {
            return GetCanonicalisedLatitude() + ";" + GetCanonicalisedLongitude();
        }

        /// <summary>
        /// Get a canonicalised string representation of the latitude
        /// </summary>
        /// <returns>String of latitude</returns>
        public string GetCanonicalisedLatitude()
        {
            return this.latitude.ToString();
        }

        /// <summary>
        /// Get a canonicalised string representation of the longitude
        /// </summary>
        /// <returns>String of longitude</returns>
        public string GetCanonicalisedLongitude()
        {
            return this.longitude.ToString();
        }


        /// <summary>
        /// Gets and sets the latitude
        /// </summary>
        public decimal Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }
        
        /// <summary>
        /// Gets and sets the longitude
        /// </summary>
        public decimal Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }

        
       
    }
  
}
