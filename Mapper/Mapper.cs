using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trender
{
    public static class Mapper
    {
        public static object GetImplementation(string t)
        {
            if (t == "iTrenderTask")
            {
                return new TrenderDowJonesTask((iTrenderMtApiService)Mapper.GetImplementation("iTrenderMtApiService"), (iTrenderDowJonesService)Mapper.GetImplementation("iTrenderDowJonesService"));
            }

                if (t == "iTrenderMtApiService")
                {
                return new MtAPIFacade();
            }

                if (t == "iTrenderDowJonesService")
                {
                return new DowJonesFacade();
            }
            if (t == "iTrenderTaskHandler")
            {
                return new TrenderTaskHandler();
            }

            return null; 
        }
    }
}
