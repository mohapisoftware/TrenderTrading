using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trender;
namespace TrenderConsole
{
    class Program
    {
        static  void Main(string[] args)
        {

            iTrenderTaskHandler taskHandler = ServiceLocator.Instance.GetService<iTrenderTaskHandler>();

            iTrenderTask task = ServiceLocator.Instance.GetService<iTrenderTask>();

            taskHandler.RegisterTask(task);

            taskHandler.RunTasks();
        }
    }
}
