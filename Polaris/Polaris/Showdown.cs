using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polaris
{
    public class Showdown
    {
        private readonly IApplicationLifetime _appLifetime;

        public Showdown(IApplicationLifetime appLifetime)
        {
            _appLifetime = appLifetime;
        }

        public void Shutdown()
        {
            _appLifetime.StopApplication();
        }
    }
}
