using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ISCity.Models
{
    public class LogInModel
    {
        [Required(ErrorMessage = "Введите Логин")]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите Пароль")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}