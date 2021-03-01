

namespace Capstone_Xavier.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    /// <summary>
    /// Class Model used to transferring values into methods as needed.
    /// </summary>
    public class ClassModel
    {
        public string className { get; set; }
        public int classID { get; set; }
        public int classArmor { get; set; }
        public int baseHP { get; set; }
        public int classStamina { get; set; }
        public int classMagica { get; set; }
        public string classWeapons { get; set; }
        public int classDamage { get; set; }
    }
}