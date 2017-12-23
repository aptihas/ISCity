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
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }


    public class RegisterModel
    {
        [Required(ErrorMessage = "Введите Фамилию")]
        [Display(Name = "Фамилия")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите Имя")]
        [Display(Name = "Имя")]
        public string SecondName { get; set; }

        [Display(Name = "Отчество")]
        public string ThirdName { get; set; }

        [Required(ErrorMessage = "Введите Улицу")]
        [Display(Name = "Улица")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Введите Номер дома")]
        [Display(Name = "Дом")]
        public string HouseNumber { get; set; }

        [Display(Name = "Корпус")]
        public string KorpusNumber { get; set; }

        [Display(Name = "Квартира")]
        public string RoomNumber { get; set; }

        [Required(ErrorMessage = "Введите Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Телефон")]
        public string Telephone { get; set; }
    }

    public class ManageCompanyModel
    {
        [Required(ErrorMessage = "Введите Фамилию ответственного лица")]
        [Display(Name = "Фамилия")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите Фамилию ответственного лица")]
        [Display(Name = "Имя")]
        public string SecondName { get; set; }

        [Display(Name = "Отчество")]
        public string ThirdName { get; set; }

        [Required(ErrorMessage = "Введите Email ответственного лица")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите наименвание ресурсоснабжающей компании")]
        [Display(Name = "Наименование организации")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите профиль деятельности ресурсоснабжающей компании")]
        [Display(Name = "Профиль организации")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Введите улицу организации.")]
        [Display(Name = "Улица")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Введите номер здания организации.")]
        [Display(Name = "Дом")]
        public string HouseNumber { get; set; }
    }
}