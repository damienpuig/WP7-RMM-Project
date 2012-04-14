using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.IO.IsolatedStorage;
using Newtonsoft.Json;

//http://fiercedesign.wordpress.com/2011/11/03/windows-phone-7-encrypt-data/
//http://msdn.microsoft.com/en-us/library/system.security.cryptography.protecteddata(VS.95).aspx
//http://msdn.microsoft.com/en-us/library/hh487164(v=VS.92).aspx
//http://blogs.technet.com/b/speschka/archive/2011/07/02/using-dpapi-with-isolatedstorage-in-windows-phone-7-mango-release.aspx
//http://debugmode.net/2011/10/16/protecting-password-or-any-data-in-windows-phone-7-using-data-protection-api/
namespace RMM.Business.GeneralService
{
    public abstract class DataProtectionDatabase
    {
        public virtual void Protect(string param, string path)
        {
            var baseByteEncrypt = Encoding.UTF8.GetBytes(param);
            var protectedBase = ProtectedData.Protect(baseByteEncrypt, null);

            WriteProtectedStringToFile(protectedBase, path);

        }

        private void WriteProtectedStringToFile(byte[] strinData, string path)
        {
            // Create a file in the application's isolated storage.
            using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
            {
                IsolatedStorageFileStream writestream = new IsolatedStorageFileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write, file);
                // Write stringData to the file.
                Stream writer = new StreamWriter(writestream).BaseStream;
                writer.Write(strinData, 0, strinData.Length);
                writer.Close();
                writestream.Close();
            }
        }

        public virtual string UnProtect(string path)
        {
            using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
            {
                //byte[] ProtectedPinByte = ReadStringFromFile(path);
                //byte[] BaseByte = ProtectedData.Unprotect(ProtectedPinByte, null);
                //return Encoding.UTF8.GetString(BaseByte, 0, BaseByte.Length);
                
                return "blague";
            }
        }

        private byte[] ReadStringFromFile(string path)
        {
            // Access the file in the application's isolated storage.
            using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
            {
                IsolatedStorageFileStream readstream = new IsolatedStorageFileStream(path, System.IO.FileMode.Open, FileAccess.Read, file);

                Stream reader = new StreamReader(readstream).BaseStream;
                byte[] BaseArray = new byte[reader.Length];
                reader.Read(BaseArray, 0, BaseArray.Length);
                reader.Close();
                readstream.Close();
                return BaseArray;
            }
        }
    }

}
