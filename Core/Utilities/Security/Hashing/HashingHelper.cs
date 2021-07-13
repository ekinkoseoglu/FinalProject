using System.Text;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) // Bu method girdiğimiz bir password'un Hash değerini ve Salt değerini oluşturmamıza yarıyor.
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512()) // Burada oluşturulan class için "hmac" diyeceğim. O "hmac aslında bizim kriptografide kullandığımız class'a (HMACSHA512) karşılık geliyor."
            {
                passwordSalt = hmac.Key; // İlgili kullandığımız algoritmanın o an oluşturduğu key değeridir. Her kullanıcı için başka bir Key oluşturur.
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); // Hash'imiz de string tipindeki password'u byte değerinde göndermemizi istiyor.
            }
        }

        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt) // Datadaki şifreylegirilen şifrenin Hashlerinin karşılaştırarak doğrulama yapıyor
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)) // Aşağıdaki hashleme tamamen yukarıdaki Salt algoritması kullanılarak yapıylıyor.
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));  // Bu password kullanıcının sonrasında girdiği parola, bunun eskisiyle eşleştirmek için tekrardan hash'ini yaratacağız (yukarıdaki Saltlama algoritmasını kullanarak)

                for (int i = 0; i < computedHash.Length; i++) // İki hash'i karşılaştırıyoruz
                {
                    if (computedHash[i] != passwordHash[i]) // Kullanıcının girdiği "comtupeHash" ile databasemdeki "passwordHash"' i karşılaştırıyorum.
                    {
                        return false;       // eşleşmezlerse uyumsuz parola
                    }
                }
            }

            return true; // Eşleşirlerse parola doğru
        }
    }
}
