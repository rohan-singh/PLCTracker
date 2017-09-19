using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectDummy.BusinessLayer.Entity;
namespace ProjectDummy
{
    namespace BusinessLayer
    {
        namespace Service
        {
           public interface IPlcService
            {
                plcs GetPlcs();
            }
        }
    }
}
