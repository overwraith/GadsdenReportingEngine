/*Author: Cameron Block*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Configuration;

namespace GadsdenReporting.Models {
    /// <summary>
    /// FtpCredential stores passwords in the database, and uploads files to remote FTP sites. 
    /// </summary>
    public class FtpCredential {
        private const int CHARS_AVAILABLE = 32;

        public FtpCredential() {

        }
        
        /// <summary>
        /// The Ftp Site name. 
        /// </summary>
        public String FtpSite {
            get; set;
        }

        /// <summary>
        /// The user name to use when logging onto ftp site. 
        /// </summary>
        public String UserName {
            get; set;
        }

        /// <summary>
        /// The encrypted password of the ftp site. 
        /// </summary>
        public byte[] PasswordEncrypted {
            get; set;
        }

        /// <summary>
        /// Gets the underlying symmetric algorithim for the object's password capability. 
        /// </summary>
        /// <returns></returns>
        public SymmetricAlgorithm GetSymmetricAlgorithim() {
            return Rijndael.Create();
        }//end method

        /// <summary>
        /// Gets the key from the web config, pads it with null bytes so that it is valid. 
        /// </summary>
        /// <param name="crypt"></param>
        /// <returns></returns>
        public byte[] GetKey(SymmetricAlgorithm crypt) {
            int keySize = crypt.LegalKeySizes[0].MinSize / 8;
            return Encoding.ASCII.GetBytes(System.Configuration.ConfigurationManager.AppSettings["EncryptionKey"].PadRight(keySize, '\0').ToCharArray(), 0, keySize);
        }//end method

        /// <summary>
        /// Gets the initialization vector from the web config, pads it with null bytes so that it is valid. 
        /// </summary>
        /// <param name="crypt"></param>
        /// <returns></returns>
        public byte[] GetIV(SymmetricAlgorithm crypt) {
            int ivSize = crypt.BlockSize / 8;
            return Encoding.ASCII.GetBytes(System.Configuration.ConfigurationManager.AppSettings["EncryptionIV"].PadRight(ivSize, '\0').ToCharArray(), 0, ivSize);
        }//end method

        /// <summary>
        /// Gets the decrypted password. 
        /// </summary>
        /// <returns></returns>
        public String GetPassword() {
            MemoryStream ms = new MemoryStream();
            SymmetricAlgorithm crypt = GetSymmetricAlgorithim();
            crypt.Padding = PaddingMode.PKCS7;
            crypt.Key = GetKey(crypt);
            crypt.IV = GetIV(crypt);
            CryptoStream cs = new CryptoStream(ms,
            crypt.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(PasswordEncrypted, 0, PasswordEncrypted.Length);
            cs.Close();
            byte[] decryptedData = ms.ToArray();
            return Encoding.ASCII.GetString(decryptedData);
        }//end method

        /// <summary>
        /// Sets the byte array containing the encrypted password. 
        /// </summary>
        public void SetPassword(String password) {
            //we only have a specific number of bytes in the database. 
            if (password.Length > CHARS_AVAILABLE)
                throw new ArgumentException("Password text is too big. ");

            byte[] passBytes = Encoding.ASCII.GetBytes(password);
            MemoryStream ms = new MemoryStream();
            SymmetricAlgorithm crypt = GetSymmetricAlgorithim();
            crypt.Padding = PaddingMode.PKCS7;
            crypt.Key = GetKey(crypt);
            crypt.IV = GetIV(crypt);
            CryptoStream cs = new CryptoStream(ms,
            crypt.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(passBytes, 0, passBytes.Length);
            cs.Close();
            byte[] encryptedData = ms.ToArray();
            PasswordEncrypted = encryptedData;
        }//end method

        /// <summary>
        /// Gets the number of bytes for the encoding used by the credential object. 
        /// </summary>
        /// <returns></returns>
        public int GetEncodingNumBytes() {
            return Encoding.ASCII.GetByteCount("1");
        }//end method

        /// <summary>
        /// Upload file to ftp site. 
        /// </summary>
        /// <param name="stream"></param>
        public void FtpUpload(Stream stream) {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FtpSite);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(UserName, GetPassword());
            //copy the contents of the file to the request stream. 
            byte[] fileContents = new byte[stream.Length];
            stream.Read(fileContents, 0, (int)stream.Length);

            request.ContentLength = fileContents.Length;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            response.Close();
        }//end method
    }//end class

}//end namespace
