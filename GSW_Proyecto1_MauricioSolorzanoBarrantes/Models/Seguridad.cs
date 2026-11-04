using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace GSW_Proyecto1_MauricioSolorzanoBarrantes.Models
{
    public class Seguridad
    {
        public static string GenerarHash(string contrasena)
        {
            byte[] salt = new byte[16];

            using (var generador = RandomNumberGenerator.Create())
            {
                generador.GetBytes(salt);
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(
                contrasena, salt, 100000))
            {
                byte[] hash = pbkdf2.GetBytes(32);

                return Convert.ToBase64String(salt) + ":" +
                       Convert.ToBase64String(hash);
            }
        }

        public static bool VerificarContrasena(
            string contrasena, string hashGuardado)
        {
            string[] partes = hashGuardado.Split(':');

            if (partes.Length != 2)
                return false;

            byte[] salt = Convert.FromBase64String(partes[0]);
            byte[] hashOriginal = Convert.FromBase64String(partes[1]);

            using (var pbkdf2 = new Rfc2898DeriveBytes(
                contrasena, salt, 100000))
            {
                byte[] hashNuevo = pbkdf2.GetBytes(32);

                return CompararHashes(hashOriginal, hashNuevo);
            }
        }

        private static bool CompararHashes(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;

            int diferencia = 0;

            for (int i = 0; i < a.Length; i++)
            {
                diferencia |= a[i] ^ b[i];
            }

            return diferencia == 0;
        }
    }
}