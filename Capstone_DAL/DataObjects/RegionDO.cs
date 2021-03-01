

namespace Capstone_DAL.DataObjects
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Used for mapping Region data across Region objects
    /// </summary>
    public class RegionDO
    {
        private string regionName;
        private int regionDanger;
        private int hasShop;
        private int regionID;
        private string regionDesc;

        public string RegionDesc {
            get { return regionDesc; }
            set { regionDesc = value; }
        }

        public int RegionID {
            get { return regionID; }
            set { regionID = value; }
        }

        public string RegionName {
            get { return regionName; }
            set { regionName = value; }
        }

        public int RegionDanger {
            get { return regionDanger; }
            set { regionDanger = value; }
        }

        public int HasShop {
            get { return hasShop; }
            set { hasShop = value; }
        }
    }
}
