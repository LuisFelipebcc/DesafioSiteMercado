using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace SiteMercado.Desafio.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public abstract class APIController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected IConfiguration Configuration { get; set; }

        protected IWebHostEnvironment _env { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        public APIController(IConfiguration configuration, IWebHostEnvironment env)
        {
            this._env = env;
            this.Configuration = configuration;
        }

        protected APIController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}