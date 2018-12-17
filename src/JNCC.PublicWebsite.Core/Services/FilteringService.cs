using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;

namespace JNCC.PublicWebsite.Core.Services
{
    internal abstract class FilteringService<TModel, TViewModel> where TModel : FilteringModel
                                                                 where TViewModel : FilteringViewModel
    {
        public abstract TViewModel GetFilteringViewModel(TModel filteringModel);
    }
}