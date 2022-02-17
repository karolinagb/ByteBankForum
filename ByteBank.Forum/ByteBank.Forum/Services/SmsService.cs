using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Verify.V2.Service;
using Twilio.TwiML.Messaging;

namespace ByteBank.Forum.Services
{
    public class SmsService
    {
        private readonly IConfiguration _configuration;
        private string sidConta;
        private string tokenConta;
        private string sidServico;

        public SmsService(IConfiguration configuration)
        {
            _configuration = configuration;
            sidConta = _configuration.GetValue<string>("Twilio:SIDConta");
            tokenConta = _configuration.GetValue<string>("Twilio:TokenConta");
            sidServico = _configuration.GetValue<string>("Twilio:SIDServico");
        }


        public async Task<VerificationResource> Enviar(Message mensagemSMS)
        {

            TwilioClient.Init(sidConta, tokenConta);

             CreateVerificationOptions createVerificationOptions = new CreateVerificationOptions(sidServico,
                mensagemSMS.To, "sms");

            return await VerificationResource.CreateAsync(createVerificationOptions);
        }

        public async Task<VerificationCheckResource> VerificarCodigo(string token, string to)
        {
            return await VerificationCheckResource.CreateAsync(sidServico, token, to);
        }

        //public async Task<VerificationResource> ObterVerificacao()
        //{
        //    TwilioClient.Init(sidConta, tokenConta);

        //    var result = await VerificationResource.FetchAsync(sidServico, sidConta);

        //    return result;
        //}
    }
}
