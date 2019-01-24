using JNCC.PublicWebsite.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Forms.Core;
using Umbraco.Forms.Core.Enums;
using Umbraco.Forms.Data.Storage;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class UmbracoFormsBlogCommentsService
    {
        public IEnumerable<BlogCommentViewModel> GetComments(int pageId, Guid formId)
        {
            var approvedRecords = GetApprovedRecords(pageId, formId);
            var viewModels = new List<BlogCommentViewModel>();

            foreach (var record in approvedRecords)
            {
                var viewModel = new BlogCommentViewModel()
                {
                    Id = string.Format("comment-{0}", record.UniqueId),
                    Created = record.Created,
                    Name = record.GetValue<string>("name"),
                    Comment = record.GetValue<string>("comment")
                };

                viewModels.Add(viewModel);
            }

            return viewModels.OrderBy(x => x.Created);
        }

        private IEnumerable<Record> GetApprovedRecords(int pageId, Guid formId)
        {
            using (var formStorage = new FormStorage())
            {
                using (var recordStorage = new RecordStorage())
                {
                    var form = formStorage.GetForm(formId);

                    return recordStorage.GetAllRecords(form)
                                        .Where(x => x.UmbracoPageId == pageId && x.State == FormState.Approved)
                                        .ToList();
                }
            }
        }
    }
}
