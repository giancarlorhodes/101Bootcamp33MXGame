
namespace Capstone_Xavier.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class MonsterModel: BaseModel
    {
        public int monsterID { get; set; }
        public string monsterName { get; set; }

        [Range(0, 300, ErrorMessage = "Please enter valid integer Number")]
        public int health { get; set; }

        [Range(0, 40, ErrorMessage = "Please enter valid integer Number")]
        public int armor { get; set; }

        [Range(0, 100, ErrorMessage = "Please enter valid integer Number")]
        public int damage { get; set; }

        [Range(0, 10, ErrorMessage = "Please enter valid integer Number")]
        public int danger { get; set; }

        public int behaviour { get; set; }
        public List<int> monsterBehaviours = new List<int> {0, 1, 2 };
    }
}