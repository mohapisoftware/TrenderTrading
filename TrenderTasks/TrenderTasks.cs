using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trender
{
    public interface iTrenderTaskHandler
    {
        Task RunTasks();

        Task RegisterTask(iTrenderTask task);
    }
    public abstract class TrenderTaskHandlerBase: iTrenderTaskHandler
    {
        public List<iTrenderTask> _tasks;

        public virtual Task RegisterTask(iTrenderTask task)
        {
            throw new NotImplementedException();
        }

        public virtual Task RunTasks()
        {
            return Task.FromResult(false);
        }

    }
    public interface iTrenderTask
    {
        Task<Boolean> ExcecuteTask(int TaskID);
        Task<Boolean> RemoveTask(int TaskID);

        Boolean IsTaskRunning();
    }

}
