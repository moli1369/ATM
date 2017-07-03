namespace ATM.Controllers
{
    public class RexaHash
    {
        public string MD5(string input)
        {

            // METHOD 1
            // using linqTechnique

            //string hashPassword;
            //using (MD5 hash = MD5.Create())
            //{
            //    hashPassword = String.Join
            //    (
            //        "",
            //        from ba in hash.ComputeHash
            //        (
            //            Encoding.UTF8.GetBytes(login.Pass)
            //        )
            //        select ba.ToString("x2")
            //    );
            //}




            /*
            // METHOD 2
            // using classic

            
            // at first we need to convert it to char []
            char[] chars = input.ToCharArray();
            // and then conver them to UTF-8 bytes
            byte[] encoded = new System.Text.UTF8Encoding().GetBytes(chars);
            // define a cryptography provider
            System.Security.Cryptography.MD5CryptoServiceProvider md5Provider =
                new System.Security.Cryptography.MD5CryptoServiceProvider();
            // hash each byte
            byte[] hashBytes = md5Provider.ComputeHash(encoded);
                        // the output
                        System.Text.StringBuilder theHash = new System.Text.StringBuilder();
            // foreach hashbyte
            for (int i = 0; i < hashBytes.Length; i++)
            {
                //lowerCase; X2 if uppercase desired
                theHash.Append(hashBytes[i].ToString("x2"));
            }       
            return theHash.ToString();
            */



            // METHOD 2
            // using classic
            // ultra way

            System.Text.StringBuilder hash = new System.Text.StringBuilder();
            byte[] bytes = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(new System.Text.UTF8Encoding().GetBytes(input));
            for (int i = 0; i < bytes.Length; i++)
                hash.Append(bytes[i].ToString("x2"));
            return hash.ToString();



        }
    }

}