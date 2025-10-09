using System.ComponentModel.DataAnnotations;

namespace MyDevLog.Data;


public class ContactForm
{
    [Required(ErrorMessage = "Please enter your name.")]
    [StringLength(100, ErrorMessage = "Your name is too long.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Please enter your email address.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Please enter a subject.")]
    [StringLength(200, ErrorMessage = "The subject is too long.")]
    public string? Subject { get; set; }

    [Required(ErrorMessage = "Please enter a message.")]
    [StringLength(5000, ErrorMessage = "Your message is too long (max 5000 characters).")]
    public string? Message { get; set; }
}