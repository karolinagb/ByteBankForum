using ByteBank.Forum.ViewModels;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Threading.Tasks;

namespace ByteBank.Forum.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Configuração para enviar o email
        public async Task EnviarEmail(string[] destinatario, string assunto, string dados)
        {
            MensagemViewModel mensagem = new MensagemViewModel(destinatario, assunto, dados);

            //Converter mensagem em um e-mail
            var mensagemDeEmail = CriaCorpoEmail(mensagem);
            await Enviar(mensagemDeEmail);
        }

        //Metodo de envio de email
        private async Task Enviar(MimeMessage mensagemDeEmail)
        {
            //SMTP - Simple Mail Transport Protocol = Protocolo simples de transporte de email
            using (var client = new SmtpClient())  //Client Smtp que vai enviar o email
            {
                try
                {
                    //Conectar ao servidor de email
                    client.Timeout = 20000; //milisegundos
                    client.Connect(_configuration.GetValue<string>("EmailSettings:SmtpServer"), //host, porta, enableSsl = true - transporte do email
                    //da nossa aplicação e o google vai ser criptografado
                           _configuration.GetValue<int>("EmailSettings:Port"), true);
                    //Remover esse tipo de conexao mais complexa por hora
                    client.AuthenticationMechanisms.Remove("XOUATH2");
                    //Autenticar com nosso remetente e senha
                    client.Authenticate(_configuration.GetValue<string>("EmailSettings:From"),
                        _configuration.GetValue<string>("EmailSettings:Password"));
                    await client.SendAsync(mensagemDeEmail);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    //Independente de ter conseguido fazer o envio ou nao do email, precisamos desconectar do servidor
                    client.Disconnect(true);
                    //Liberar os recursos do cliente
                    client.Dispose();
                }
            }
        }

        private MimeMessage CriaCorpoEmail(MensagemViewModel mensagem)
        {
            var mensagemEmail = new MimeMessage();
            mensagemEmail.From.Add(new MailboxAddress("", _configuration.GetValue<string>("EmailSettings:From")));
            //To = coleção de destinatário
            mensagemEmail.To.AddRange(mensagem.Destinatario);
            mensagemEmail.Subject = mensagem.Assunto;
            mensagemEmail.Body = new TextPart(MimeKit.Text.TextFormat.Html) //Texto do tipo mine que o e-mail aceita
            {
                Text = $"<a href='{mensagem.Conteudo}'>Clique aqui para ativar sua conta</a>"
            };

            return mensagemEmail;
        }
    }
}
