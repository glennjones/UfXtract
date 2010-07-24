//Copyright (c) 2007 - 2010 Glenn Jones

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace UfXtract.Utilities
{

    /// <summary>
    /// Utility for the canonicalisation of telephone numbers
    /// </summary>
    public class PhoneNumber
    {

        string raw = "";
        string canonicalisation = "";


        /// <summary>
        /// Utility for the canonicalisation of telephone numbers
        /// </summary>
        public PhoneNumber()
        {
        }


        /// <summary>
        /// Utility for the canonicalisation of telephone numbers
        /// </summary>
        public PhoneNumber(string telephoneNumber)
        {
            this.Parse(telephoneNumber);
        }


        private void Reset()
        {
            this.raw = "";
            this.canonicalisation = "";
        }

        /// <summary>
        /// Parses a string representation of a telephone number
        /// </summary>
        /// <param name="telephoneNumber">The telephone number</param>
        public void Parse( string telephoneNumber )
        {
            Reset();
            this.raw = telephoneNumber;
            telephoneNumber = telephoneNumber.Replace(" ", "");
            Regex regexp = new Regex(@"[0-9\+]");
            MatchCollection matches = regexp.Matches(telephoneNumber);

            if (matches.Count > 0 )
            {
                 foreach(Match match in matches) 
                 {
                    if (match.Groups[0].Success)
                        this.canonicalisation += match.Groups[0].ToString();
                 }
            }
        }

        /// <summary>
        /// Returns a canonicalised version of the telephone number 
        /// </summary>
        /// <returns></returns>
        public string ToString()
        {
            return this.canonicalisation;
        }

        /// <summary>
        /// Get the raw text version of the telephone number
        /// </summary>
        public string Raw
        {
            get { return raw; }
        }

        /// <summary>
        /// Gets the canonicalised version of the telephone number 
        /// </summary>
        public string Canonicalised
        {
            get { return canonicalisation; }
        }

    }
}
