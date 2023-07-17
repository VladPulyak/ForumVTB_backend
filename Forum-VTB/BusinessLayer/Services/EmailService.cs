using BusinessLayer.Interfaces;
using System.Text;
using System.Net.Mail;
using System.Net;
using BusinessLayer.Dtos.Email;

namespace BusinessLayer.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendMessage(EmailSenderDto requestDto)
        {
            string senderEmail = "forumvtbds@gmail.com";
            string senderPassword = "mzioajhajzrrhikz";
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(senderEmail);
            mailMessage.To.Add(new MailAddress(requestDto.ReceiverEmail));
            mailMessage.Subject = requestDto.Subject;
            mailMessage.Body = requestDto.Body;
            mailMessage.IsBodyHtml = true;
            mailMessage.BodyEncoding = Encoding.UTF8;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}