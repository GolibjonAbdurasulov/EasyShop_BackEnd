namespace Services.Interfaces;

public interface IOtpService
{
    public void SaveOtp(string email, string code);
    public bool ValidateOtp(string email, string code);
}