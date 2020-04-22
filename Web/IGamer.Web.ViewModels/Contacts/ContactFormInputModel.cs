namespace IGamer.Web.ViewModels.Contacts
{
    using System.ComponentModel.DataAnnotations;

    public class ContactFormInputModel
    {
        [Required]
        public string Message { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }
    }
}
