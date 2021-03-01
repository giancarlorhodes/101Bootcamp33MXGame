

namespace Capstone_Xavier.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// Used only for holding regionns data
    /// </summary>
    public class RegionModel
    {
        public string regionName { get; set; }
        public int danger { get; set; }
        public int hasShop { get; set; }
        public int regionID { get; set; }
        public string regionDesc { get; set; }

    }
}