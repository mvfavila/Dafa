using Microsoft.AspNet.Identity;
using System.Configuration;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace DAFA.Infra.CrossCutting.Identity.Configuration
{
    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            if (ConfigurationManager.AppSettings["Internet"] == "true")
            {
                // Using TWILIO as the SMS Provider.
                // https://www.twilio.com/docs/quickstart/csharp/sms/sending-via-rest

                const string accountSid = "SID";
                const string authToken = "AuthToken";

                TwilioClient.Init(accountSid, authToken);

                MessageResource.Create(
                    from: new PhoneNumber("+61429064260"), // From number, must be an SMS-enabled Twilio number
                    to: new PhoneNumber(message.Destination), // To number, if using Sandbox see note above
                    body: message.Body); // Message content
            }

            return Task.FromResult(0);
        }
    }
}
