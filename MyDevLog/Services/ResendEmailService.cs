using MyDevLog.Data;
using Resend;

namespace MyDevLog.Services;


public class ResendEmailService : IEmailService
{
    private readonly IResend _resend;

    public ResendEmailService(IResend resend)
    {
        _resend = resend;
    }

    public async Task<bool> SendContactEmailAsync(ContactForm model)
    {
        var message = new EmailMessage
        {
            From = "onboarding@resend.dev",
            To = "farhad10abdi@gmail.com",
            Subject = $"New Contact Form Message: {model.Subject}",
            HtmlBody = $@"
                    <h3>You have a new message from your portfolio contact form!</h3>
                    <p><strong>Name:</strong> {model.Name}</p>
                    <p><strong>Email:</strong> {model.Email}</p>
                    <hr>
                    <h4>Message:</h4>
                    <p>{model.Message}</p>
                "
        };

        try
        {
            var response = await _resend.EmailSendAsync(message);

            if (response.Success)
            {
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
            return false;
        }
    }
}