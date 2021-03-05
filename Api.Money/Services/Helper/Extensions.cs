using Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Helper
{
    public static class Extensions
    {
        public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users)
        {
            return users.Select(x => x.WithoutPassword());
        }

        public static User WithoutPassword(this User user)
        {
            user.PasswordHash = null;
            return user;
        }

        /// <summary>
        /// Для перевода хэша в строку
        /// </summary>
        /// <param name="arrInput"></param>
        /// <returns></returns>
        public static string ByteArrayToString(this byte[] arrInput)
        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }
    }
}
