using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Cache;
using Umbraco.Core.Events;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.Publishing;
using Umbraco.Core.Services;
using Umbraco.Core.Sync;
using Umbraco.Web.Cache;

namespace JNCC.PublicWebsite.Core.Events
{
    public class SearchEvents : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            //CacheRefresherBase<PageCacheRefresher>.CacheUpdated += CacheServicePublished;
            //CacheRefresherBase<UnpublishedPageCacheRefresher>.CacheUpdated += CacheServiceUnPublished;
            //CacheRefresherBase<MediaCacheRefresher>.CacheUpdated += MediaCacheService;

            //ContentService.Deleted += ContentServiceDeleted;
            //ContentService.UnPublished += ContentServiceUnPublished;

            //MediaService.Created += MediaServiceCreated;
            //MediaService.Deleted += MediaServiceDeleted;
        }

        //private void MediaCacheService(MediaCacheRefresher sender, CacheRefresherEventArgs e)
        //{
        //    // Call to service which adds media item to ES index

        //    switch (e.MessageType)
        //    {
        //        case MessageType.RefreshAll:
        //            break;
        //        case MessageType.RefreshById:
        //            var c1 = ApplicationContext.Current.Services.MediaService.GetById((int)e.MessageObject);
        //            if (c1 != null)
        //            {
        //                //IndexForMedia(c1, c1.Trashed == false);
        //            }
        //            break;
        //        case MessageType.RefreshByJson:
        //            break;
        //        case MessageType.RemoveById:
        //            var c2 = ApplicationContext.Current.Services.MediaService.GetById((int)e.MessageObject);
        //            if (c2 != null)
        //            {
        //                //This is triggered when the item has trashed.
        //                // So we need to delete the index from all indexes not supporting unpublished content.

        //                //DeleteFromIndex(c2.Id, true);

        //                //We then need to re-index this item for all indexes supporting unpublished content

        //                //ReIndexForMedia(c2, false);
        //            }
        //            break;
        //        case MessageType.RefreshByInstance:
        //            break;
        //        case MessageType.RemoveByInstance:
        //            break;
        //        case MessageType.RefreshByPayload:
        //            break;
        //        default:
        //            //We don't support these, these message types will not fire for media
        //            break;
        //    }
        //}

        //private void CacheServiceUnPublished(UnpublishedPageCacheRefresher sender, CacheRefresherEventArgs e)
        //{
        //    // Call to service which removes Umbraco content from ES index
        //    switch (e.MessageType)
        //    {
        //        case MessageType.RefreshAll:
        //            break;
        //        case MessageType.RefreshById:
        //            var c1 = ApplicationContext.Current.Services.ContentService.GetById((int)e.MessageObject);
        //            if (c1 != null)
        //            {
        //                //IndexForContent(c1, false);
        //            }
        //            break;
        //        case MessageType.RefreshByJson:
        //            break;
        //        case MessageType.RemoveById:

        //            // This is triggered when the item is permanently deleted

        //            //DeleteFromIndex((int)e.MessageObject, false);

        //            break;
        //        case MessageType.RefreshByInstance:
        //            break;
        //        case MessageType.RemoveByInstance:
        //            // This is triggered when the item is permanently deleted

        //            var c4 = e.MessageObject as IContent;
        //            if (c4 != null)
        //            {
        //                //DeleteFromIndex(c4.Id, false);
        //            }
        //            break;
        //        case MessageType.RefreshByPayload:
        //            break;
        //        default:
        //            //We don't support these, these message types will not fire for unpublished content
        //            break;
        //    }
        //}

        //private void CacheServicePublished(PageCacheRefresher sender, CacheRefresherEventArgs e)
        //{
        //    // Call to service which adds Umbraco content to ES index
        //    LogHelper.Info<string>("Content cache updated - adding to ES index");

        //    switch (e.MessageType)
        //    {
        //        case MessageType.RefreshAll:
        //            break;
        //        case MessageType.RefreshById:
        //            var c1 = ApplicationContext.Current.Services.ContentService.GetById((int)e.MessageObject);
        //            if (c1 != null)
        //            {
                        
        //                //IndexForContent(c1, true);
        //                //System.Threading.Tasks.Task.Run(async () => await Services.ElasticSearchService.PutContent());
        //                //Services.ElasticSearchService.PutContent();
        //            }
        //            break;
        //        case MessageType.RefreshByJson:
        //            break;
        //        case MessageType.RemoveById:

        //            //This is triggered when the item has been unpublished or trashed (which also performs an unpublish).
        //            var c2 = ApplicationContext.Current.Services.ContentService.GetById((int)e.MessageObject);
        //            if (c2 != null)
        //            {
        //                // So we need to delete the index from all indexes not supporting unpublished content.

        //                //DeleteFromIndex(c2.Id, true);

        //                // We then need to re-index this item for all indexes supporting unpublished content

        //                //ReIndexForContent(c2, false);
        //            }
        //            break;
        //        case MessageType.RefreshByInstance:
        //            break;
        //        case MessageType.RemoveByInstance:

        //            //This is triggered when the item has been unpublished or trashed (which also performs an unpublish).

        //            var c4 = e.MessageObject as IContent;
        //            if (c4 != null)
        //            {
        //                // So we need to delete the index from all indexes not supporting unpublished content.

        //                //DeleteFromIndex(c4.Id, true);

        //                // We then need to re-index this item for all indexes supporting unpublished content

        //                //ReIndexForContent(c4, false);
        //            }

        //            break;
        //        case MessageType.RefreshByPayload:
        //            break;
        //        default:
        //            //We don't support these for ES indexing
        //            break;
        //    }
        //}


        /// <summary>
	    /// Re-indexes a content item whether published or not but only indexes them for indexes supporting unpublished content
	    /// </summary>
	    /// <param name="sender"></param>
	    /// <param name="isContentPublished">
	    /// Value indicating whether the item is published or not
	    /// </param>
        //private static void IndexForContent(IContent sender, bool isContentPublished)
        //{
        //    //System.Threading.Tasks.Task.Run(() => Services.ElasticSearchService.
        //}

        /// <summary>
	    /// Re-indexes a content item whether published or not but only indexes them for indexes supporting unpublished content
	    /// </summary>
        /// <param name="sender"></param>
	    /// <param name="isMediaPublished">
	    /// Value indicating whether the item is published or not
	    /// </param>
        //private static void IndexForMedia(IMedia sender, bool isMediaPublished)
        //{

        //}

        /// <summary>
	    /// Remove items from any index that doesn't support unpublished content
	    /// </summary>
        /// <param name="entityId"></param>
	    /// <param name="keepIfUnpublished">
	    /// If true, indicates that we will only delete this item from indexes that don't support unpublished content.
	    /// If false it will delete this from all indexes regardless.
	    /// </param>
        //private static void DeleteFromIndex(int entityId, bool keepIfUnpublished)
        //{

        //}
    }
}
