using System;
using System.Linq;
using System.Web.Http;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using Umbraco.Core;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Persistence;
using Umbraco.Core.Services;
using ArticulateModel = JNCC.PublicWebsite.Core.Models.Articulate;

namespace JNCC.PublicWebsite.Core
{
    internal sealed class ApplicationBootstrap : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            CreateResetPasswordRequestDatabaseTable(applicationContext);
            DeleteBlogPostCommentWhenABlogPostIsSentToRecycleBin();
        }

        private void DeleteBlogPostCommentWhenABlogPostIsSentToRecycleBin()
        {
            ContentService.Trashing += ContentService_Trashing;
        }

        private void ContentService_Trashing(IContentService sender, MoveEventArgs<IContent> e)
        {
            var service = new UmbracoFormsBlogCommentsService();

            foreach (var info in e.MoveInfoCollection)
            {
                var entity = info.Entity;

                if (entity.ContentType.Alias != ArticulateMarkdown.ModelTypeAlias && entity.ContentType.Alias != ArticulateRichText.ModelTypeAlias)
                {
                    break;
                }

                var blogRoot = entity.Ancestors().FirstOrDefault(x => x.ContentType.Alias == ArticulateModel.ModelTypeAlias);

                if (blogRoot == null)
                {
                    break;
                }
                var blogCommentForm = blogRoot.GetValue<Guid>("commentsForm");

                if (blogCommentForm == default(Guid))
                {
                    break;
                }

                service.DeleteComments(entity.Id, blogCommentForm);
            }
        }

        private void CreateResetPasswordRequestDatabaseTable(ApplicationContext applicationContext)
        {
            var ctx = applicationContext.DatabaseContext;
            var db = new DatabaseSchemaHelper(ctx.Database, applicationContext.ProfilingLogger.Logger, ctx.SqlSyntax);
            db.CreateTable<ResetPasswordRequestModel>(false);
        }
    }
}