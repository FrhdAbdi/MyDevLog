using MyDevLog.Data;

namespace MyDevLog.Services;


public interface IEmailService
{
    Task<bool> SendContactEmailAsync(ContactForm model);
}