using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models
{
   public class UserMenu
    {
        public long UserMenuID { get; set; }
        public string UserID { get; set; }
        public int? MenuID { get; set; }
    }
}
