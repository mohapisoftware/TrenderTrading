using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trender
{
    public interface IServiceLocator
    {
        T GetService<T>();
    }
    public class ServiceLocator : IServiceLocator
    {

        private static ServiceLocator instance = null;

        private static readonly object LockObject = new object();
        public static ServiceLocator Instance
        {
            get
            {
                lock (LockObject)
                {
                    if (instance==null)
                    {
                        instance = new ServiceLocator();
                    }
                }
                return instance;
            }
        }


        // map that contains pairs of interfaces and
        // references to concrete implementations
        private IDictionary<object, object> services;

        private ServiceLocator()
        {
            services = new Dictionary<object, object>();

            // fill the map
            this.services.Add(typeof(iTrenderMtApiService), new MtAPIFacade("127.0.0.1",1433));
            this.services.Add(typeof(iTrenderDowJonesService), new DowJonesFacade());
            this.services.Add(typeof(iTrenderTaskHandler), new TrenderTaskHandler());
            this.services.Add(typeof(TrenderDowJonesBaseTask), new TrenderDowJonesTask(new MtAPIFacade("127.0.0.1", 1433), new DowJonesFacade()));
        }

        public T GetService<T>()
        {
            try
            {
                return (T)services[typeof(T)];
            }
            catch (KeyNotFoundException)
            {
                throw new ApplicationException("The requested service is not registered");
            }
        }
    }

}
