using System;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class ArticleService
    {
        public ArticlePageViewModel GetViewModel(ArticlePage model)
        {
            return new ArticlePageViewModel
            {
                LandingPageUrl = model.Parent.Url,
                MainContent = model.MainContent,
                Teams = model.ArticleTeams,
                ArticleType = model.ArticleType,
                PublishDate = model.PublishDate
            };
        }
    }
}
