using System.ComponentModel.DataAnnotations;

namespace Hinnova.Authorization.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
