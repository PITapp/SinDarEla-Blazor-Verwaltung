using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radzen;
using Radzen.Blazor;
using Microsoft.JSInterop;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;


namespace SinDarElaVerwaltung.Pages
{
    public partial class AnmeldungComponent
    {
        public async Task WriteLocalStorage(string key, string value)
        {
            await JSRuntime.InvokeVoidAsync("localStorage.setItem", new object[] { key, value });
        }

        public async Task<string> ReadLocalStorage(string key)
        {
            return await JSRuntime.InvokeAsync<string>("localStorage.getItem", key);
        }

        public async Task RemoveLocalStorage(string key)
        {
            await JSRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }

        public async Task ClearLocalStorage()
        {
            await JSRuntime.InvokeVoidAsync("localStorage.clear");
        }

        private async Task<string> GetHashPassword(string password)
        {
            // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = RandomNumberGenerator.Create())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

            // byte[] salt = new byte[16];
            // var rng = RandomNumberGenerator.Create();
            // rng.GetBytes(salt);
            // Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            Console.WriteLine($"Hashed: {hashed}");

            return hashed;

            /*
             * SAMPLE OUTPUT
             *
             * Enter a password: Xtw9NMgx
             * Salt: CGYzqeN4plZekNC88Umm1Q==
             * Hashed: Gt9Yc4AiIvmsC1QQbe2RZsCIqvoYlst2xbz0Fs8aHnw=
             */
        }

        public string EncryptPassword(string password)
        {
            // Encrypt password
            byte[] salt = new byte[128 / 8]; // Generate a 128-bit salt using a secure PRNG
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string encryptedPassw = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            ));

            return encryptedPassw;
        }

        public string GetDeterministicHashCode(string str)
        {
            unchecked
            {
                int hash1 = (5381 << 16) + 5381;
                int hash2 = hash1;

                for (int i = 0; i < str.Length; i += 2)
                {
                    hash1 = ((hash1 << 5) + hash1) ^ str[i];
                    if (i == str.Length - 1)
                        break;
                    hash2 = ((hash2 << 5) + hash2) ^ str[i + 1];
                }

                return (hash1 + (hash2 * 1566083941)).ToString();
            }
        }
    }
}
