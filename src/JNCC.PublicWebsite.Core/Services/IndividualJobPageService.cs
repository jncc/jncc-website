using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;

namespace JNCC.PublicWebsite.Core.Controllers.Services
{
    internal sealed class IndividualJobPageService
    {
        public IndividualJobPageViewModel GetViewModel(IndividualJobPage model)
        {
            return new IndividualJobPageViewModel()
            {
                KeyData = GetKeyData(model),
                TabbedContent = GetTabbedContent(model),
                PreambleText = model.PreambleText,
            };
        }

        private IEnumerable<AccordionItemViewModel> GetTabbedContent(IndividualJobPage model)
        {
            var tabbedContent = new List<AccordionItemViewModel>
            {
                new AccordionItemViewModel ( "background", "Background", model.BackgroundContent ),
                new AccordionItemViewModel ( "post-duties", "Post Duties", model.PostDutiesContent ),
                new AccordionItemViewModel ( "successprofiles", "Success Profiles", model.SuccessProfilesContent ),
                new AccordionItemViewModel ( "salary-and-benefits", "Salary & Benefits", model.SalaryBenefitsContent )
            };

            return tabbedContent.Where(x => ExistenceUtility.IsNullOrWhiteSpace(x.Content) == false).ToArray();
        }

        private IReadOnlyDictionary<string, string> GetKeyData(IndividualJobPage model)
        {
            var keyData = new Dictionary<string, string>
            {
                { "Ref No", model.ReferenceNumber },
                { "Grade", model.Grade },
                { "Type of appointment", model.TypeOfAppointment },
                { "Location",  model.Location },
                { "Team", model.Team }
            };

            return keyData.Where(x => string.IsNullOrWhiteSpace(x.Value) == false)
                          .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}