﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MtApi;

namespace Trender
{
    public class DowJonesFacade : iTrenderDowJonesService
    {
        public Task<TrenderTradeOperation> GetTradeOperation()
        {
            //dont trade candles older that 5 minutes
            throw new NotImplementedException();
        }
    }

    
}
