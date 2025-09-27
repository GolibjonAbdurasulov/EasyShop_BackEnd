using System.Threading.Tasks;
using Services.Dtos.Email;

namespace Services.Interfaces;

public interface IEmailNotificationService
{
    public Task SendEmail(SendEmailDto emailDto);
    public Task SendBatchEmail(SendBatchEmailDto batchEmailDto);
}