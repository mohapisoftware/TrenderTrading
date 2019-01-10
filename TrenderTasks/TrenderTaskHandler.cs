using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trender
{
    public class TrenderTaskHandler : TrenderTaskHandlerBase
    {
        public TrenderTaskHandler()
        {
            _tasks = new List<iTrenderTask>();

        }
        public override Task RunTasks()
        {
            foreach (var task in _tasks)
            {
                task.ExcecuteTask(0);
            }
            return Task.FromResult(true);
        }

        public override Task RegisterTask(iTrenderTask task)
        {
            _tasks.Add(task);

            return Task.FromResult(true);
        }
    }
}
