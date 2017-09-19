using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using ProjectDummy.BusinessLayer.Entity;
using ProjectDummy.BusinessLayer.Service;
using ProjectDummy.DataService.DataServiceImpl;
using ProjectDummy.DataService;
namespace ProjectDummy
{
    namespace BusinessLayer
    {
        namespace ServiceImpl
        {
           public class PlcServiceImpl:IPlcService
            {
                 private IDataService dataservice;
                public PlcServiceImpl()
                {
                    dataservice = new DataServiceXmlImpl();
                }


                //public plcs GetPlcs()
                //{//code to retrieve data from xml
                    
                //}

                plcs IPlcService.GetPlcs()
                {
                    return dataservice.Read();
                }
            }
        }
    }
}
