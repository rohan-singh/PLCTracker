using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Xml.Serialization;
using System.IO;

namespace DataAccessLayer
{
    public class serialization
    {
        public static List<properties> read()
        {
            //creating the objects
            properties details1 = new properties();
            details1.ipaddress = "103.34.56.213";
            details1.owner = "Kejin";
            details1.status = "Active";
            details1.user = "Nil";

            properties details2 = new properties();
            details2.ipaddress = "172.168.0.1";
            details2.owner = "Rohan";
            details2.status = "Active";
            details2.user = "Nil";


            //adding the objects to the list
            List<properties> plcdetails = new List<properties>();
            plcdetails.Add(details1);
            plcdetails.Add(details2);
            
            //serializing
            Serialize(plcdetails);

            List<properties> readdetails = new List<properties>();
            XmlSerializer xml = new XmlSerializer(typeof(List<properties>));
            FileStream f = File.Open("E:\\plc.xml", FileMode.Open, FileAccess.Read);
            readdetails = (List<properties>)xml.Deserialize(f);


            //var detail = readdetails.Where(d => d.HouseNo == 4);
            //readdetails = detail.ToList();
            //foreach (var d in readdetails)
            //{
            //    Console.WriteLine(d.StreetName);
            //}
            return readdetails;


        }
        static public void Serialize(List<properties> details)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<properties>));
            using (TextWriter writer = new StreamWriter(@"E:\plc.xml"))
            {
                serializer.Serialize(writer, details);
            }
        }
    }
}
