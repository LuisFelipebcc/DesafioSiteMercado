using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NLog;
using System;

namespace SiteMercado.Desafio.Utils.ContractBase
{
    public abstract class AbstractControllerBase<T> : ControllerBase where T : DbContext
    {
        public T context;
        protected IConfiguration Configuration { get; set; }

        protected Logger logger { get; private set; }

        protected AbstractControllerBase()
        {
            context = Activator.CreateInstance<T>();
            logger = LogManager.GetLogger(GetType().ToString());
        }

        protected AbstractControllerBase(IConfiguration configuration)
        {
            context = Activator.CreateInstance<T>();
            Configuration = configuration;
            logger = LogManager.GetLogger(GetType().ToString());
        }
    }

    public abstract class AbstractControllerBase : ControllerBase
    {
        public IConfiguration Configuration { get; set; }

        public Logger logger { get; private set; }

        public AbstractControllerBase()
        {
            ////context = Activator.CreateInstance<T>();
            ////logger = LogManager.GetLogger(GetType().ToString());
        }

        public AbstractControllerBase(IConfiguration configuration)
        {
            ////context = Activator.CreateInstance<T>();
            Configuration = configuration;
        }

    }
}
