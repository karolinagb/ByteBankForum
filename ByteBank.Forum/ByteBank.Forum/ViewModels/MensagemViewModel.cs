using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace ByteBank.Forum.ViewModels
{
    public class MensagemViewModel
    {
        public List<MailboxAddress> Destinatario { get; set; } //MailboxAddress identifica um endereço de e-mail
        public string Assunto { get; set; }
        public string Conteudo { get; set; }

        public MensagemViewModel(IEnumerable<string> destinatario, string assunto,
            string dados)
        {
            Destinatario = new List<MailboxAddress>();
            Destinatario.AddRange(destinatario.Select(d => new MailboxAddress("", d))); //novo destinario sendo adicionado a lista de destinarios como um MailboxAddress
            Assunto = assunto;
            Conteudo = dados;
        }
    }
}
