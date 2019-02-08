namespace JNCC.PublicWebsite.Core.Services
{
    internal interface IContentFullUrlResolver
    {
        string ResolveContentFullUrlById(int id);
    }
}