using MessagePack;
using System.ComponentModel.DataAnnotations;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace App_banAo.Models
{
    public class ChangePasswordViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Display(Name ="Mat khau hien tai")]
        public string PasswordOld { get; set; }
        [Display(Name = "Mat khau moi")]
        [Required(ErrorMessage ="Vui long nhap mat khau")]
        [MinLength(6,ErrorMessage ="ban can dat mat khau toi da 6 ki tu")]
        public string PasswordNew { get; set; }
        [Display(Name = " Nhap lai Mat khau moi")]
        [MinLength(6, ErrorMessage = "ban can dat mat khau toi da 6 ki tu")]
      
        public string ConfirmPassword { get; set; }


        
    }
}
