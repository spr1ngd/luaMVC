namespace LuaMVC
{
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public class Util
    {
        /// <summary>
        /// Create md5 value for string.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string md5( string source )
        {
            byte[] data = Encoding.UTF8.GetBytes(source);
            MD5 md5 = MD5.Create();
            byte[] md5Value = md5.ComputeHash(data, 0, data.Length);
            md5.Clear();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < md5Value.Length; i++)
                stringBuilder.Append(md5Value[i].ToString("x"));
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Create md5 value for a file.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string md5file( string filePath )
        {
            MD5 md5 = MD5.Create();
            byte[] md5Value;
			using (System.IO.FileStream fs = new System.IO.FileStream(filePath, FileMode.Open))
            {
                md5Value = md5.ComputeHash(fs);
                md5.Clear();
            }
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < md5Value.Length; i++)
                stringBuilder.Append(md5Value[i].ToString("x"));
            return stringBuilder.ToString();
        }
    }
}