using System.Collections.Generic;
using Thev2Andy.Utils;

namespace Thev2Andy
{
	
	public sealed class Triangle: IDisposable
	{
		private List<Site> _sites;
		public List<Site> sites {
			get { return this._sites; }
		}
		
		public Triangle (Site a, Site b, Site c)
		{
			_sites = new List<Site> () { a, b, c };
		}
		
		public void Dispose ()
		{
			_sites.Clear ();
			_sites = null;
		}

	}
}