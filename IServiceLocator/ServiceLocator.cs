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
        // map that contains pairs of interfaces and
        // references to concrete implementations
        private IDictionary<object, object> services;

        public ServiceLocator()
        {
            services = new Dictionary<object, object>();

            // fill the map
            this.services.Add(typeof(iTrenderMtApiService), new MtAPIFacade());
            this.services.Add(typeof(iTrenderDowJonesService), new DowJonesFacade());
            this.services.Add(typeof(iTrenderTaskHandler), new TrenderTaskHandler());
            this.services.Add(typeof(iTrenderTask), new TrenderDowJonesTask(new ServiceLocator().GetService<iTrenderMtApiService>(), new ServiceLocator().GetService<iTrenderDowJonesService>()));
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
