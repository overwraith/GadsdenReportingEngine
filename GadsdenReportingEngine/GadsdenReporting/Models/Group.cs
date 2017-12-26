using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Data;
using System.Configuration;
using System.Diagnostics;

using GadsdenReporting.Models;

namespace GadsdenReporting.Models {
    public class Group {

        /// <summary>
        /// Unique identifier for group used by program and database layer. 
        /// </summary>
        public int GroupId {
            get; set;
        }

        /// <summary>
        /// Short group name for describing the group summarily. 
        /// </summary>
        public String GroupName {
            get; set;
        }

        /// <summary>
        /// Group description field describes the group. 
        /// </summary>
        public String Description {
            get; set;
        }

        /// <summary>
        /// All users associated with a given group. 
        /// </summary>
        public IList<User> Users {
            get; set;
        }

        /// <summary>
        /// All reports associated with a given group. 
        /// </summary>
        public IList<Report> Reports {
            get; set;
        }

        public Group() {

        }

        public bool HasAccess(Report report) {
            foreach (var grp in report.Groups)
                foreach (var usr in grp.Users)
                    if (grp.Users.Contains(usr))
                        return true;

            return false;
        }//end method

    }//end class
}//end namespace