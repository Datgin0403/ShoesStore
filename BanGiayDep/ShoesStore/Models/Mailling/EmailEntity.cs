using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ShoesStore.Models.Mailling
{
    public class EmailEntity
    {
        public string FromEmailAddress {  get; set; }
        public string ToEmailAddress { get; set; }
        public string Subject { get; set; }
        public string EmailBodyMessage {  get; set; }

        [ValidateNever]
        public string AttachMentUrl {  get; set; } //Can be options
    }
}
