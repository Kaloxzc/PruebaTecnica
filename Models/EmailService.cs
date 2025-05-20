using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Prueba_Tecnica.Controllers;
using Prueba_Tecnica.Models;
using System.Net;
using System.Net.Mail;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    private readonly ILogger<EmailService> _logger;

    public EmailService(
        IOptions<EmailSettings> emailSettings,
        ILogger<EmailService> logger)
    {
        _emailSettings = emailSettings.Value;
        _logger = logger;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            _logger.LogError("Email destination address is null or empty");
            throw new ArgumentException("Email address cannot be empty");
        }

        try
        {
            _logger.LogInformation($"Attempting to send email to: {email}");

            using var mailMessage = new MailMessage
            {
                From = new MailAddress(
                    _emailSettings.SenderEmail,
                    _emailSettings.SenderName),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            mailMessage.To.Add(email);

            using var smtpClient = new SmtpClient(_emailSettings.SmtpServer)
            {
                Port = _emailSettings.SmtpPort,
                Credentials = new NetworkCredential(
                    _emailSettings.SmtpUsername,
                    _emailSettings.SmtpPassword),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Timeout = 10000 // 10 segundos
            };

            await smtpClient.SendMailAsync(mailMessage);
            _logger.LogInformation($"Email sent successfully to {email}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error sending email to {email}");
            throw; // Re-lanza la excepción para manejo superior
        }
    }

}