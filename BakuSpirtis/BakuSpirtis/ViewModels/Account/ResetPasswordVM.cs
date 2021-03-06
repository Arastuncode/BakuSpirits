
using System.ComponentModel.DataAnnotations;

namespace BakuSpirtis.ViewModels.Account
{
    public class ResetPasswordVM
    {
        public string Id { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password),Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

    }
}
