namespace ATM.Controllers
{
    public class RexaHash
    {
        public string MD5(string input)
        {
            System.Text.StringBuilder hash = new System.Text.StringBuilder();
            byte[] bytes = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(new System.Text.UTF8Encoding().GetBytes(input));
            for (int i = 0; i < bytes.Length; i++)
                hash.Append(bytes[i].ToString("x2"));
            return hash.ToString();
        }
    }

}