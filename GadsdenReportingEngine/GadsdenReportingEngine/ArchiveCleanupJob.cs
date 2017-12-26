/*Author: Cameron Block*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Collections.Generic;

using Quartz;

namespace GadsdenReportingEngine {
    /// <summary>
    /// Delete files every (n) days so we don't fill up the disk. 
    /// </summary>
    public class ArchiveCleanupJob : IJob {
        /// <summary>
        /// Preform the file deletion operation. 
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context) {
            String archiveFolder = ConfigurationManager.AppSettings["ArchiveFolder"];
            int archiveDays = int.Parse(ConfigurationManager.AppSettings["ArchiveDays"]);

            new DirectoryInfo(archiveFolder)
                .GetFiles()
                .Where(file => 
                file.CreationTime.AddDays(archiveDays) < DateTime.Now)
                    .ToList()
                    .ForEach(file => File.Delete(file.FullName));
        }//end method
    }//end class

}//end namespace
