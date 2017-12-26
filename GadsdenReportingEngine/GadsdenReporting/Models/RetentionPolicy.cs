/*Author: Cameron Block*/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GadsdenReporting.Models {
    /// <summary>
    /// The intervals it is possible to store reports for. 
    /// </summary>
    public enum RetentionInterval {
        MONTH,
        QUARTER,
        YEAR,
        FOREVER
    }//end enum

    /// <summary>
    /// Controls the expiration of certain report objects. 
    /// </summary>
    public class RetentionPolicy {
        /// <summary>
        /// The Unique database identification number for the retention policy. 
        /// </summary>
        public int RetentionId {
            get; set;
        }

        /// <summary>
        /// Unique identification number for the associated report object. 
        /// </summary>
        public int ReportId {
            get; set;
        }

        /// <summary>
        /// The associated report object. 
        /// </summary>
        public Report Report {
            get; set;
        }

        /// <summary>
        /// The deprecation date for the associated report. 
        /// </summary>
        public DateTime DeprecationDate {
            get; set;
        }

        /// <summary>
        /// The Retention interval for the report. 
        /// </summary>
        public RetentionInterval RetentionInterval {
            get; set;
        }

        /// <summary>
        /// Determine whether the associated report is expired. 
        /// </summary>
        /// <returns></returns>
        public bool IsExpired() {
            //if retention policy has a specific date in mind for deprecation
            if (Report.CreatedDate != null) {
                if (Report.CreatedDate > DeprecationDate)
                    return true;
            }
            
            //if retention interval is in months
            if (RetentionInterval.MONTH == RetentionInterval
                && Report.CreatedDate.AddMonths(1) < DateTime.Now)
                return true;

            //if retention interval is in quarters
            if (RetentionInterval == RetentionInterval.QUARTER) {
                QuarterSection section = ConfigurationManager.GetSection("quarters") as QuarterSection;
                foreach (Quarter quarter in section.Quarters) {
                    if (RetentionInterval.QUARTER == this.RetentionInterval
                        && Report.CreatedDate.Month < DateTime.Now.Month
                        && quarter.Month == DateTime.Now.ToString("MMMM"))
                        return true;
                }
            }

            return false;
        }//end method

    }//end class

}//end namespace
