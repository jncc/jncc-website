using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;
using Umbraco.Core.Models;

namespace JNCC.PublicWebsite.Core.Services
{
    internal interface IManuallyAuthoredRelatedItemsService
    {
        IEnumerable<RelatedItemViewModel> GetViewModels(IEnumerable<IPublishedContent> manuallyAuthoredItems);
    }
}