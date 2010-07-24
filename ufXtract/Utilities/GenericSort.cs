//Copyright (c) 2007 - 2010 Glenn Jones

using System;
using System.Collections;
using System.Reflection;

namespace UfXtract
{
	/// <summary>
	/// Generic Sort
	/// </summary>
	public class GenericSort : IComparer
	{
		String sortMethodName;
		String sortOrder;


        /// <summary>
        /// Generic Sort
        /// </summary>
        /// <param name="sortMethodName">Method name to invoke</param>
        /// <param name="sortOrder">ASC or DESC</param>
		public GenericSort(String sortMethodName, String sortOrder) 
		{
			this.sortMethodName = sortMethodName;
			this.sortOrder = sortOrder;
		}


        /// <summary>
        /// Compares to onjects
        /// </summary>
        /// <param name="x">First object</param>
        /// <param name="y">Second object</param>
        /// <returns></returns>
		public int Compare(object x, object y)
		{
			IComparable ic1 = (IComparable)x.GetType().GetMethod(sortMethodName).Invoke(x,null);
			IComparable ic2 = (IComparable)y.GetType().GetMethod(sortMethodName).Invoke(y,null);
			if( sortOrder != null && sortOrder.ToUpper().Equals("ASC") )
				return ic1.CompareTo(ic2);
			else
				return ic2.CompareTo(ic1);
		}
	}

}
