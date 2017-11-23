using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPNGET.Model
{
   public partial class VPN_Model
    {
        /// <summary>
        /// 国家简称
        /// </summary>
        public String CountryShort { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        public String IP { get; set; }
        /// <summary>
        /// 延迟
        /// </summary>
        public String Ping { get; set; }
    }
}
