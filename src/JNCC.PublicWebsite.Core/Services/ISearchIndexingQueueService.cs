using JNCC.PublicWebsite.Core.Models;

namespace JNCC.PublicWebsite.Core.Services
{
    internal interface ISearchIndexingQueueService
    {
        void QueueUpsert(SearchIndexDocumentModel document);
        void QueueDelete(SearchIndexDocumentModel document);
    }
}
