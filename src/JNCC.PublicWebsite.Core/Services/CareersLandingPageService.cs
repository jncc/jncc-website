using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class CareersLandingPageService
    {
        private const int NumberOfLatestJobs = 5;
        private readonly NavigationItemService _navigationItemService;

        public CareersLandingPageService(NavigationItemService navigationItemService)
        {
            _navigationItemService = navigationItemService;
        }

        public CareersLandingPageViewModel GetViewModel(CareersLandingPage model)
        {
            return new CareersLandingPageViewModel()
            {
                Preamble = model.Preamble,
                MainContent = model.MainContent,
                Careers = GetCareers(model)
            };
        }

        public CareersSidebarViewModel GetSidebarViewModel(CareersLandingPage model)
        {
            return new CareersSidebarViewModel
            {
                PrimaryCallToActionButton = _navigationItemService.GetViewModel(model.SidebarPrimaryCallToActionButton),
                LatestJobs = GetLatestJobs(model),
                SeeAlsoLinks = _navigationItemService.GetViewModels(model.SidebarSeeAlsoLinks)
            };
        }

        private IEnumerable<JobItemViewModel> GetLatestJobs(CareersLandingPage model)
        {
            var latestJobs = model.Children<IndividualJobPage>().OrderByDescending(x => x.UpdateDate).Take(NumberOfLatestJobs);
            var viewModels = new List<JobItemViewModel>();

            if (latestJobs.Any() == false)
            {
                return viewModels;
            }

            foreach (var job in latestJobs)
            {
                var viewModel = new JobItemViewModel()
                {
                    JobTitle = job.GetHeadline(),
                    Url = job.Url,
                    Grade = job.Grade,
                    Location = job.Location
                };

                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        private IEnumerable<CareersListItemViewModel> GetCareers(CareersLandingPage model)
        {
            var jobs = model.Children<IndividualJobPage>();
            var viewModels = new List<CareersListItemViewModel>();

            if (jobs.Any() == false)
            {
                return viewModels;
            }

            foreach (var job in jobs.OrderBy(x => x.SortOrder))
            {
                var viewModel = new CareersListItemViewModel()
                {
                    Grade = job.Grade,
                    JobTitle = job.GetHeadline(),
                    Location = job.Location,
                    Team = job.Team,
                    Type = job.TypeOfAppointment,
                    Url = job.Url
                };

                viewModels.Add(viewModel);
            }

            return viewModels;
        }
    }
}
