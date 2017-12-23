using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ISCity.Models
{
    public static class ISCityFramework
    {
        public static void SendMail(Users user, DBISCityEntities dbEnt)
        {
            string _pass = dbEnt.Accounts.Where(a => a.user_id == user.id).Select(a => a.password).FirstOrDefault();
            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("otdel_rsis@mail.ru", "ИС \"Грозный\"");
            // кому отправляем
            MailAddress to = new MailAddress(user.Email);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Подтверждение Email";
            // текст письма
            m.Body = "<h2>Здравствуйте, " + user.SecondName + "!</h2><h4>Ваши учетные данные от системы ИС Грозный</h4><p>Логин:" + user.Email + "<br>Пароль:" + _pass + "<br/><br/>Пока вы не выполните вход, Ваш Email не будет подтвержден.</p>";
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 25);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("otdel_rsis@mail.ru", "razrab0tka");
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(m);
        }

        public static string GetPass(Random rnd)
        {
            int[] arr = new int[16]; // сделаем длину пароля в 16 символов
            string Password = "";

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rnd.Next(33, 125);
                Password += (char)arr[i];
            }
            return Password;
        }
    }
}