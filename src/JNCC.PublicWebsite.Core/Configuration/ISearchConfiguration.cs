namespace JNCC.PublicWebsite.Core.Configuration
{
    internal interface ISearchConfiguration
    {
        string AWSESAccessKey { get; }
        string AWSESSecretKey { get; }
        string AWSESRegion { get; }
        string AWSService { get; }
        string AWSESEndpoint { get; }
        string AWSESIndex { get; }
        string AWSSQSEndpoint { get; }
        string AWSSQSPayloadBucket { get; }
        string AWSESWriteAccessKey { get; }
        string AWSESWriteSecretKey{ get; }
        bool EnableIndexing { get; }
    }
}
