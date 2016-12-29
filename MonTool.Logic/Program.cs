using MonTool.Logic.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonitorTest
{
    class Program
    {
        static void Main(string[] args)
        {
            CapabilitiesLogic cl = new CapabilitiesLogic();
            MonitorLogic ml = new MonitorLogic(cl);


            var test = ml.GetAll();

            Console.WriteLine("TEST");
        }
    }
}
