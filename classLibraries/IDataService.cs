using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectDummy.BusinessLayer.Entity;
namespace ProjectDummy
{
    namespace DataService
    {
       public interface IDataService
        {
           plcs Read();
           //List<plcs> Read();
        }
    }
    
}
