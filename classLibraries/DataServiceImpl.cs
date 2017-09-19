using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using ProjectDummy.BusinessLayer.Entity;
using System.Xml;

namespace ProjectDummy
{
  namespace DataService
  {
      namespace DataServiceImpl
      {
         public class DataServiceXmlImpl:IDataService
          {
              //private XmlSerializer xml;
              //private List<plcs> employees = new List<plcs>();

              public plcs Read()
              {
                  XmlSerializer deserialiser = new XmlSerializer(typeof(plcs));
                  TextReader fs = new StreamReader(@"E:\InformationPlc.xml");
                  object obj = deserialiser.Deserialize(fs);
                  plcs XmlData = (plcs)obj;
                  fs.Close();
                  return XmlData;
              }
          }
      }
  }
}
