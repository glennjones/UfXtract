using System;
using System.Xml;
using System.Collections;
using System.Data.SqlClient;

namespace ufXtract
{


	/// <summary>
    /// Url Cache Collection
	/// </summary>
	public class UrlCacheCollection : CollectionBase  //: IList
	{

        //Copyright (c) 2007 Glenn Jones


		public UrlCacheCollection() : base()
		{
		}

		public void Sort(string propertyName, string direction )
		{
			InnerList.Sort(new GenericSort(propertyName, direction));
		}

		protected void Load()
		{
			using (Database db = new Database(  ))
			{
				StoredProcedure storedProcedure = new StoredProcedure( "GetUrlCache" );
				System.Data.SqlClient.SqlDataReader rs = db.Get( storedProcedure );
				LoadRS ( rs );
				rs.Close();
			}
		}


		private void LoadRS ( System.Data.SqlClient.SqlDataReader rs )
		{
			while ( rs.Read() )
			{
				UrlCache urlCache = new UrlCache();
				urlCache.LoadRS( rs );
				this.Add( urlCache );
			}
		}



		#region "Strongly typed collection form IList"

		public void Insert(int index, UrlCache newUrlCache)
		{
			InnerList.Insert(index, newUrlCache);
		}

		public void Remove(UrlCache aUrlCache)
		{
			InnerList.Remove(aUrlCache);
		}

		public bool Contains(UrlCache aUrlCache)
		{
			return InnerList.Contains(aUrlCache);
		}

		public int IndexOf(UrlCache aUrlCache)
		{
			return InnerList.IndexOf(aUrlCache);
		}

		public int Add(UrlCache newUrlCache)
		{
			return InnerList.Add(newUrlCache);
		}




		public UrlCache this[int index]
		{
			get           
			{
				if (this.InnerList.Count > index)
				{
					return (UrlCache)this.InnerList[index];
				}
				else
				{
					throw new ArgumentException("Out of range");
				}
			}
			set           
			{
				if (this.InnerList.Count > index)
				{
					this.InnerList[index] = value;
				}
				else 
				{
					throw new ArgumentException("Out of range");
				}
			} 
		}
			

		
		#endregion

	}


	/// <summary>
	/// Summary description for Schedule.
	/// </summary>
	public class UrlCache
	{

		private int m_liUrlCacheID = -1;
		private string m_sUrl = string.Empty;
		private string m_sContents = string.Empty;
		private string m_sDomain = string.Empty;
		private string m_sRequestVerbString = string.Empty;
		private string m_sParameterString = string.Empty;
		private DateTime m_dtLastUpdated;
		private DateTime m_dtCreated;


		public UrlCache()
		{
		}

		protected void Load( int UrlCacheID )
		{
			using (Database db = new Database(  ))
			{
				StoredProcedure storedProcedure = new StoredProcedure( "GetUrlCacheByID" );	
				storedProcedure.AddParam( "liUrlCacheID", UrlCacheID );
				System.Data.SqlClient.SqlDataReader rs = db.Get( storedProcedure );
				while ( rs.Read() )
					LoadRS ( rs );
			
				rs.Close();
			}
		}

		protected void Load( string Url )
		{
			using (Database db = new Database(  ))
			{
				StoredProcedure storedProcedure = new StoredProcedure( "GetUrlCacheByUrl" );	
				storedProcedure.AddParam( "sUrl", Url );
				System.Data.SqlClient.SqlDataReader rs = db.Get( storedProcedure );
				while ( rs.Read() )
					LoadRS ( rs );
			
				rs.Close();
			}
		}

		protected void Load( string Url, string Parameters, string RequestVerb )
		{
			using (Database db = new Database(  ))
			{
				StoredProcedure storedProcedure = new StoredProcedure( "GetUrlCacheByUrlParam" );	
				storedProcedure.AddParam( "sUrl", Url );
				storedProcedure.AddParam( "sParameters", Parameters );
				storedProcedure.AddParam( "sRequestVerb ", RequestVerb  );
				System.Data.SqlClient.SqlDataReader rs = db.Get( storedProcedure );
				while ( rs.Read() )
					LoadRS ( rs );
			
				rs.Close();
			}
		}
	


		
		// Single function to load data from data reader
		internal void LoadRS ( System.Data.SqlClient.SqlDataReader rs )
		{

			this.UrlCacheID = Convert.ToInt32( rs["liUrlCacheID"] );
			this.Url = rs["sUrl"].ToString();
			this.Contents = rs["sContents"].ToString();
			this.Domain = rs["sDomain"].ToString();
			this.ParameterString = rs["sParameters"].ToString();
			this.RequestVerbString = rs["sRequestVerb"].ToString();
			
			if( rs["dtLastUpdated"] != System.DBNull.Value )
				this.LastUpdated = Convert.ToDateTime( rs["dtLastUpdated"] );

			if( rs["dtCreated"] != System.DBNull.Value )
				this.Created = Convert.ToDateTime( rs["dtCreated"] );
			
		}


		protected void Create()
		{
			using ( Database db = new Database( ))
			{
				StoredProcedure storedProcedure = new StoredProcedure( "AddUrlCache" );
				LoadSP( storedProcedure );
				storedProcedure.AddParam( "dtCreated", DateTime.Now );
				storedProcedure.AddParam( "dtLastUpdated", DateTime.Now );
				db.Get( storedProcedure ).Close();
			}
		}


		protected void Update()
		{
			using ( Database db = new Database( ))
			{
				StoredProcedure storedProcedure = new StoredProcedure( "UpdateUrlCache" );
				LoadSP( storedProcedure );
				storedProcedure.AddParam( "dtLastUpdated", DateTime.Now );
				db.Get( storedProcedure ).Close();
			}
		}


		private StoredProcedure LoadSP( StoredProcedure storedProcedure )
		{
			storedProcedure.AddParam( "liUrlCacheID", this.UrlCacheID );
			storedProcedure.AddParam( "sUrl", this.Url );
			storedProcedure.AddParam( "sContents", this.Contents );
			storedProcedure.AddParam( "sDomain", this.Domain );
			storedProcedure.AddParam( "sParameters", this.ParameterString );
			storedProcedure.AddParam( "sRequestVerb", this.RequestVerbString );
			return storedProcedure;
		}


		protected void Delete()
		{
			using ( Database db = new Database( ))
			{
				StoredProcedure storedProcedure = new StoredProcedure( "DeleteUrlCache" );
				storedProcedure.AddParam( "liUrlCacheID", this.UrlCacheID );
				db.Get( storedProcedure ).Close();
			}
		}




		public int UrlCacheID
		{
			get{ return m_liUrlCacheID; }
			set{ m_liUrlCacheID = value; }
		}

		public string Url
		{
			get{ return m_sUrl; }
			set{ m_sUrl = value; }
		}

		public string Contents
		{
			get{ return m_sContents; }
			set{ m_sContents = value; }
		}

		public string Domain
		{
			get{ return m_sDomain; }
			set{ m_sDomain = value; }
		}

		public string ParameterString
		{
			get{ return m_sParameterString; }
			set{ m_sParameterString = value; }
		}

		public string RequestVerbString
		{
			get{ return m_sRequestVerbString; }
			set{ m_sRequestVerbString = value; }
		}

		public DateTime LastUpdated
		{
			get{ return m_dtLastUpdated; }
			set{ m_dtLastUpdated = value; }
		}

		public DateTime Created
		{
			get{ return m_dtCreated; }
			set{ m_dtCreated = value; }
		}

		




	}
}
