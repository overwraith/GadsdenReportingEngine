/*Author: Cameron Block*/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GadsdenReporting {

    /// <summary>
    /// A single quarter elemnt. Quarter of a year, broken up as you see fit. 
    /// </summary>
    public class Quarter : ConfigurationElement {
        [ConfigurationProperty("month", IsRequired = true)]
        public String Month {
            get {
                return (String)this["month"];
            }
            set {
                this["month"] = value;
            }
        }//end method

    }//end class

    /// <summary>
    /// A collection of quarter designations. Designate 4 quarters via the web config or app config. 
    /// </summary>
    public class QuarterCollection : ConfigurationElementCollection {
        protected override ConfigurationElement CreateNewElement() {
            if (this.Count > 4 || this.Count < 4)
                throw new ArgumentException("Cannot have more or less than 4 months designated as quarters. ");
            return new Quarter();
        }//end method

        protected override object GetElementKey(ConfigurationElement element) {
            return ((Quarter)element).Month.GetHashCode();
        }//end method

        public new Quarter this[String key] {
            get {
                return (Quarter)BaseGet(key);
            }
        }//end method

    }//end method

    /// <summary>
    /// The quarter section from the web config. 
    /// </summary>
    public class QuarterSection : ConfigurationSection {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(QuarterCollection))]
        public QuarterCollection Quarters {
            get {
                return (QuarterCollection)base[""];
            }
        }
    }//end method

}//end namespace
