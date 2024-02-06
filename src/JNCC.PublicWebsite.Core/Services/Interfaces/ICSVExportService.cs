using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JNCC.PublicWebsite.Core.Services.Interfaces
{
	public interface ICSVExportService
	{
		void GenericExport(object[] dList, string FileName, bool SpaceCapitals = false);
		string GenerateCSVData(object[] dList, bool SpaceCapitals = false);
	}
}
