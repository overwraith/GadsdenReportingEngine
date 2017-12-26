/*Author: Cameron Block*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Configuration;

using Ionic.BZip2;
using Ionic.Zip;

using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Tar;
using ICSharpCode.SharpZipLib.GZip;

namespace GadsdenReporting.Utilities {
    /// <summary>
    /// Archives various file formats. 
    /// </summary>
    public static class ArchiveTool {

        /// <summary>
        /// Zips using a temporary file in the user's %TMP% folder. 
        /// </summary>
        /// <param name="streams"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public static Stream IOZip(String fileName, Stream[] streams, String[] names) {
            //using file based zip for larger files (normally use in memory zipping via memory stream)
            String tmpFileName = String.Format("{0}\\{1}", ConfigurationManager.AppSettings["ArchiveFolder"], fileName);
            Stream archiveStream = new FileStream(tmpFileName, FileMode.Open, FileAccess.ReadWrite);
            using (ZipFile zip = new ZipFile()) {
                var nameEnum = names.GetEnumerator();
                foreach (var stream in streams) {
                    nameEnum.MoveNext();
                    zip.AddEntry(nameEnum.Current.ToString(), stream);
                }
                zip.Save(archiveStream);
            }
            archiveStream.Position = 0;
            archiveStream.Flush();
            return archiveStream;
        }//end method

        /// <summary>
        /// Zips using a temporary file in the user's %TMP% folder. 
        /// </summary>
        /// <param name="streams"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public static Stream IOBzip2(String fileName, Stream[] streams, String[] names) {
            String tmpFileName = String.Format("{0}\\{1}", ConfigurationManager.AppSettings["ArchiveFolder"], fileName);
            Stream archiveStream = new FileStream(tmpFileName, FileMode.Open, FileAccess.ReadWrite);
            BZip2OutputStream zip = new BZip2OutputStream(archiveStream);
            var nameEnum = names.GetEnumerator();
            foreach (var stream in streams) {
                nameEnum.MoveNext();
                byte[] content = new byte[stream.Length];
                stream.Read(content, 0, (Int32)stream.Length);

                zip.Write(content, 0, content.Length);
            }
            archiveStream.Position = 0;
            archiveStream.Flush();
            return archiveStream;
        }//end method

        /// <summary>
        /// Gzip archives are not designed for multiple files. 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static Stream IOGZip(String fileName, Stream stream) {
            String tmpFileName = String.Format("{0}\\{1}", ConfigurationManager.AppSettings["ArchiveFolder"], fileName);
            Stream archiveStream = new FileStream(tmpFileName, FileMode.Open, FileAccess.ReadWrite);
            GZipStream zip = new GZipStream(archiveStream, CompressionMode.Compress);
            byte[] content = new byte[stream.Length];
            stream.Read(content, 0, content.Length);
            zip.Close();
            archiveStream.Position = 0;
            archiveStream.Flush();
            return archiveStream;
        }//end method

        /// <summary>
        /// Zips using a temporary file in the user's %TMP% folder. 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="streams"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public static Stream IOTarGz(String fileName, Stream[] streams, String[] names) {
            String tmpFileName = String.Format("{0}\\{1}", ConfigurationManager.AppSettings["ArchiveFolder"], fileName);
            Stream archiveStream = new FileStream(tmpFileName, FileMode.Open, FileAccess.ReadWrite);
            GZipOutputStream targetStream = new GZipOutputStream(archiveStream);
            TarArchive tarArchive = TarArchive.CreateOutputTarArchive(targetStream, TarBuffer.DefaultBlockFactor);
            var nameEnum = names.GetEnumerator();
            foreach(var stream in streams) {
                nameEnum.MoveNext();
                TarEntry entry = TarEntry.CreateEntryFromFile(nameEnum.Current.ToString());
                tarArchive.WriteEntry(entry, true);
            }
            tarArchive.Close();
            targetStream.Close();
            archiveStream.Position = 0;
            archiveStream.Flush();
            return archiveStream;
        }//end method

    }//end class

}//end namespace
