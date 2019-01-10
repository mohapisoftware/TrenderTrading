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
            ServiceLocator serviceLocator = new ServiceLocator();

            iTrenderTaskHandler taskHandler = serviceLocator.GetService<iTrenderTaskHandler>();

            iTrenderTask task = serviceLocator.GetService<iTrenderTask>();

            taskHandler.RegisterTask(task);

            taskHandler.RunTasks();
        }
    }
}
