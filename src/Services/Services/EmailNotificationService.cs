using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Entity.Attributes;
using Services.Dtos.Email;
using Services.Interfaces;

namespace Services.Services;

[Injectable]
public class EmailNotificationService : IEmailNotificationService
{

    private SmtpClient _smtpClient;
    
    public EmailNotificationService(SmtpClient smtpClient)
    {
        _smtpClient = smtpClient;
    }

    public async Task SendEmail(SendEmailDto emailDto)
    {
        MailMessage mailMessage = new MailMessage()
        {
            From = new MailAddress("golibjonabdurasulov1@gmail.com"),
            Sender = new MailAddress("golibjonabdurasulov1@gmail.com"),
            To = { emailDto.Email },
            Subject = emailDto.Subject,
            Body = emailDto.Message,
            Priority = MailPriority.Low,
            IsBodyHtml = true,
        };
        try
        { 
            await this._smtpClient.SendMailAsync(mailMessage);
        }
        catch (Exception e)
        {
            throw new ("Email service error", e);
        }
    }
   
    public async Task SendBatchEmail(SendBatchEmailDto batchEmailDto)
    {
        MailMessage mailMessage = new MailMessage()
        {
            From = new MailAddress("libraryreu@gmail.com"),
            Sender = new MailAddress("libraryreu@gmail.com"),
            Subject = batchEmailDto.Subject,
            Body = batchEmailDto.Message,
            Priority = MailPriority.High,
            IsBodyHtml = true,
        };
        foreach (var s in batchEmailDto.Email)
            mailMessage.To.Add(s);
        await this._smtpClient.SendMailAsync(mailMessage);
    }

   
}