/*Author: Cameron Block*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GadsdenReporting.Models {

    public enum DeliveryFormat {

    }

    public enum OutputFormat {

    }

    public enum ArchiveFormat {

    }

    public enum SplitFormat {

    }

    public class DeliveryInfo {
        public int DeliveryInfoId {
            get; set;
        }

        public DeliveryFormat DeliveryFmt {
            get; set;
        }

        public OutputFormat OutputFmt {
            get; set;
        }

        public ArchiveFormat ArchiveFmt {
            get; set;
        }

        public String CrystalReportPath {
            get; set;
        }

        public SplitFormat SplitFmt {
            get; set;
        }

        public String DeliveryPath {
            get; set;
        }

        public String DeliveryFileName {
            get; set;
        }

        public int FtpCredentialId {
            get; set;
        }

        public FtpCredential FtpCredential {
            get; set;
        }

        public int ReportId {
            get; set;
        }

        public Report Report {
            get; set;
        }
    }//end class

}//end namespace
