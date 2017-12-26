/*Author: Cameron Block*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Data;
using System.Configuration;
using System.Threading;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.OracleClient;

namespace GadsdenReporting.Models {

    /// <summary>
    /// Report is all fields associated with executing a sql report against the database. 
    /// </summary>
    public class Report {

        /// <summary>
        /// Unique report Id. 
        /// </summary>
        public int ReportId {
            get; set;
        }

        /// <summary>
        /// The name of the report, displayed by the GUI. 
        /// </summary>
        public String ReportName {
            get; set;
        }

        /// <summary>
        /// Turn the report on or off. Don't want to delete hard won code. 
        /// </summary>
        public bool IsActive{
            get; set;
        }

        /// <summary>
        /// The description of the report displayed by the GUI. (make a tool tip)
        /// </summary>
        public String Description {
            get; set;
        }

        /// <summary>
        /// The date this report was created on. 
        /// </summary>
        public DateTime CreatedDate {
            get; set;
        }

        /// <summary>
        /// The sql to be executed by the report, decorated by toggle comments. Toggle into code. 
        /// </summary>
        public String PythonCode {
            get; set;
        }//end property

        /// <summary>
        /// The file name of the last ran report's archive. For resending reports that failed to be delivered. 
        /// </summary>
        public String LastRanArchive {
            get; set;
        }

        /// <summary>
        /// Users that are subscribed to this report. 
        /// </summary>
        public IList<User> Users {
            get; set;
        }

        /// <summary>
        /// Groups that are subscribed to this report. 
        /// </summary>
        public IList<Group> Groups {
            get; set;
        }

        public Report() { }

        /// <summary>
        /// Executes the report, toggles limitations into actual code, and applies TemplateItems to the SQL. 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public DataTable ExecuteReport() {
            throw new NotImplementedException("Please implement this method dummy. ");
        }//end method

    }//end class

}//end namespace