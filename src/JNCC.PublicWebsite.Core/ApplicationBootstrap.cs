using JNCC.PublicWebsite.Core.Models;
using Umbraco.Core;
using Umbraco.Core.Persistence;

namespace JNCC.PublicWebsite.Core
{
    internal sealed class ApplicationBootstrap : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            CreateResetPasswordRequestDatabaseTable(applicationContext);
        }

        private void CreateResetPasswordRequestDatabaseTable(ApplicationContext applicationContext)
        {
            var ctx = applicationContext.DatabaseContext;
            var db = new DatabaseSchemaHelper(ctx.Database, applicationContext.ProfilingLogger.Logger, ctx.SqlSyntax);
            db.CreateTable<ResetPasswordRequestModel>(false);
        }
    }
}
