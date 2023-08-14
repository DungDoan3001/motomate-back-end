using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using Application.Web.Service.Interfaces;
using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Service.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Application.Web.Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;
        private readonly bool _useSSL;
        private readonly string _textParse;

        private readonly string _fromName;
        private readonly string _fromEmail;
        public EmailService(IConfiguration config)
        {
            var gmailConfig = config.GetSection("Gmail");
            _smtpServer = gmailConfig["SmtpServer"];
            _smtpUsername = gmailConfig["SmtpUsername"];
            _smtpPassword = gmailConfig["SmtpPassword"];
            _smtpPort = int.Parse(gmailConfig["smtpPort"]);
            _useSSL = bool.Parse(gmailConfig["UseSSL"]);
            _fromName = gmailConfig["FromName"];
            _fromEmail = gmailConfig["FromEmail"];
            _textParse = gmailConfig["TextParse"];
        }

        public async Task<bool> SendEmailAsync(SendEmailOptions emailOptions)
        {
            try
            {
                MimeMessage message = CreateEmail(emailOptions.ToName, emailOptions.ToEmail, emailOptions.Subject, emailOptions.Body);
                return await SendEmail(message);
            }
            catch (Exception ex)
            {
                throw new StatusCodeException(message: "Error Hit", statusCode: StatusCodes.Status500InternalServerError, ex); 
               
            }
        }

        public async Task<bool> SendBulkBccEmailAsync(SendBulkEmailOptions bulkEmailOptions)
        {
            try
            {
                MimeMessage message = CreateBulkBccEmail(bulkEmailOptions.Emails, bulkEmailOptions.Subject, bulkEmailOptions.Body);
                return await SendEmail(message);
            }
            catch (Exception ex)
            {
                throw new StatusCodeException(message: "Error Hit", statusCode: StatusCodes.Status500InternalServerError, ex);
            }
        }

        private async Task<bool> SendEmail(MimeMessage message)
        {
            try
            {
                var smtpClient = new SmtpClient();
                await smtpClient.ConnectAsync(_smtpServer, _smtpPort, _useSSL);
                await smtpClient.AuthenticateAsync(_smtpUsername, _smtpPassword);
                await smtpClient.SendAsync(message);
                await smtpClient.DisconnectAsync(true);
                return true;
            }
            catch (Exception ex)
            {
                throw new StatusCodeException(message: "Error Hit", statusCode: StatusCodes.Status500InternalServerError, ex);
            }
        }

        private MimeMessage CreateEmail(string toName, string toEmail, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_fromName, _fromEmail));
            message.To.Add(new MailboxAddress(toName, toEmail));
            message.Subject = subject;
            message.Body = new TextPart(_textParse) { Text = body };
            return message;
        }

        private MimeMessage CreateBulkBccEmail(List<BulkEmailSendingRequestModel> emailSenders, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_fromName, _fromEmail));
            emailSenders.ForEach(sender =>
            {
                message.Bcc.Add(new MailboxAddress(sender.Name, sender.Email));
            });
            message.Subject = subject;
            message.Body = new TextPart(_textParse) { Text = body };
            return message;
        }
    }
}
