using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Vms
{
    public class UserOPModel
    {
        public LoginVm?LoginVm { get; set; }
        public RegisterVm?registerVm { get; set; }
        public List<Catergory>?catergories { get; set; }
        public List<Post>?posts { get; set; }
    }
}
