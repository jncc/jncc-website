using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.Services
{
    internal interface IDataHubQueryRelatedItemsService
    {
        IEnumerable<RelatedItemViewModel> GetViewModels(string dataHubQuery, int numberOfItemsRequiredFromDataHub);
    }
}