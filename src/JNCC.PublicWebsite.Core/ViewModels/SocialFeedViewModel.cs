namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class SocialFeedViewModel
    {
        public string TwitterFeedUrl { get; set; }
        public int NumberOfTweets { get; set; }

        public bool HasTwitterFeedUrl
        {
            get
            {
                return string.IsNullOrWhiteSpace(TwitterFeedUrl) == false;
            }
        }
    }
}