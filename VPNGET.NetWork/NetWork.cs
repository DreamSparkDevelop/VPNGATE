using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VPNGET.NetWork
{
   public partial class NetWork
    {
        public async Task<Object> Post(string Uri = "http://www.vpngate.net/")
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage hrm= await httpClient.PostAsync(Uri, null);
            if (hrm.IsSuccessStatusCode)
            {
                return hrm.Content;
            }
            return null;
        }

        public async Task<String> Get(string Uri = "http://www.vpngate.net/")
        {
            HttpClient httpClient = new HttpClient();
            //HttpResponseMessage hrm = await httpClient.GetAsync(Uri);

            return await httpClient.GetStringAsync(Uri);
        }
    }
}
