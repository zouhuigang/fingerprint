using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace WindowsFormsApp1
{


    /*返回的json数据：

   {
"data": {},
"info": "签名错误",
"status": 501
}


           //反序列化Json数据  
            var rs = JsonConvert.DeserializeObject<JsonAnooc>(resultJson);//result为上面的Json数据 
            req += rs.info;
   */

    public class JsonAnooc
    {
        [JsonProperty("data")]
        public Data data;

        [JsonProperty("info")]
        public string info;

        [JsonProperty("status")]
        public long status;
    }

    public class Data
    {
    }
}
