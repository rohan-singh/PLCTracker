using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace ProjectDummy
{
    namespace BusinessLayer
    {
        namespace Entity
        {
           public class plcs
            {
               [XmlElement("plcDevice")]
               public List<plcDevice> plcList = new List<plcDevice>();

               
            }
           public class plcDevice
           {
              public string ipAddress { get; set; }
               public string ownerName { get; set; }
               public string status { get; set; }
               public string userName { get; set; }
           }
        }
    }
   
}
