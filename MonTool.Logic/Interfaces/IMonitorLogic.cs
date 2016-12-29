using MonTool.Logic.DomainModels;
using MonTool.Logic.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonTool.Logic.Interfaces
{
   public interface IMonitorLogic
    {

        IEnumerable<Monitor> GetAll();

    }
}
