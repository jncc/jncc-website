using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.SQS;
using Amazon.SQS.Model;
using Amazon.Runtime;
using Amazon.SQS.ExtendedClient;
using Newtonsoft.Json;
using System.Configuration;

namespace JNCC.PublicWebsite.Core.Services
{
    public class SearchService
    {
        public  EsPut()
        {
            return Task.Run(() => ESPutAsync()).Result;
        }

        public async Task<> ESPutAsync()
        {
            var credentials = new BasicAWSCredentials(ConfigurationManager.AppSettings["JNCC.PublicWebsite.Core.AWS-ES-AccessKey"], ConfigurationManager.AppSettings["JNCC.PublicWebsite.Core.AWS-SecretKey"]);
            var region = RegionEndpoint.GetBySystemName(ConfigurationManager.AppSettings["JNCC.PublicWebsite.Core.AWS-Region"]);
            var s3 = new AmazonS3Client(credentials, region);
            var sqs = new AmazonSQSClient(credentials, region);
            var sqsExtendedClient = new AmazonSQSExtendedClient(sqs,
                new ExtendedClientConfiguration().WithLargePayloadSupportEnabled(s3, ConfigurationManager.AppSettings["JNCC.PublicWebsite.Core.AWS-SQS-PayloadBucket"])    
            );

            // index documents

            var simpleMessage = new
            {
                verb = "upsert",
                index = "test",
                document = new
                {
                    id = "123456789", // ID managed by Umbraco
                    site = "website", // as opposed to datahub|sac|mhc
                    title = "An example searchable document",
                    content = "This is a searchable document made purely for example purposes.",
                    url = "http://example.com/pages/123456789", // the URL of the page, for clicking through
                    keywords = new[]
                    {
                        new { vocab = "http://vocab.jncc.gov.uk/jncc-web", value = "Example" }
                    },
                    published_date = "2019-01-14",
                }
            };

            var basicResponse = await sqsExtendedClient.SendMessageAsync(ConfigurationManager.AppSettings["JNCC.PublicWebsite.Core.AWS-SQS-Endpoint"],
                JsonConvert.SerializeObject(simpleMessage, Formatting.None)
            );

            // index PDFs
        }
    }
}
