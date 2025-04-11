using Konscious.Security.Cryptography;
using System;
using System.Text;

namespace ExpenseTracker.Helper


//https://github.com/kmaragon/Konscious.Security.Cryptography

{
    public class Argon2
    {
        // Hashing the password
        public static string HashPassword(string password, byte[] salt)
        {
            // Convert the password to bytes
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            // Create a new Argon2 instance for hashing
            using (var argon2 = new Argon2i(passwordBytes))
            {
                //https://cheatsheetseries.owasp.org/cheatsheets/Password_Storage_Cheat_Sheet.html#argon2id  -- OWASP recommended params
                // Set the parameters for the hashing process 
                argon2.Salt = salt; // Random salt unique to each user
                argon2.DegreeOfParallelism = 1; // Number of threads
                argon2.MemorySize = 9216; // Memory cost (in KB)
                argon2.Iterations = 4; // Number of iterations

                // Generate the hash
                byte[] hashBytes = argon2.GetBytes(32); // Length of the generated hash

                return Convert.ToBase64String(hashBytes); // Return the hash as a Base64 string
            }
        }

        // Verifying a password
        public static bool VerifyPassword(string password, string storedHash, byte[] salt)
        {
            byte[] storedHashBytes = Convert.FromBase64String(storedHash);

            // Hash the password with the same settings and compare
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            using (var argon2 = new Argon2i(passwordBytes))
            {
                argon2.Salt = salt; // Stored salt from DB
                argon2.DegreeOfParallelism = 1; // Number of threads
                argon2.MemorySize = 9216; // Memory cost (in KB)
                argon2.Iterations = 4; // Number of iterations

                byte[] hashBytes = argon2.GetBytes(32);

                return storedHashBytes.Length == hashBytes.Length && storedHashBytes.SequenceEqual(hashBytes);
            }
        }
    }
}
