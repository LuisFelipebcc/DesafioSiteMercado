using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SiteMercado.Desafio.Business;
using SiteMercado.Desafio.Entities.Filters;
using SiteMercado.Desafio.Entities.Models;
using SiteMercado.Desafio.Utils.Models;

namespace SiteMercado.Desafio.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProdutoController : APIController
    {
        public ProdutoController(IConfiguration configuration) : base(configuration) { }


        [HttpGet("GetById/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ActionResult<ResultModel<Produto>>), 200)]
        public ActionResult<ResultModel<Produto>> GetById(int id)
        {
            using (var business = new ProdutoBusiness())
            {
                var data = business.Get(id);

                if (data.Items[0] == null)
                    return NotFound();

                return data;
            }
        }

        /// <summary>
        /// Endpoint para obter lista de Produtos
        /// </summary>
        /// <param name="filter">Filtro para buscar determinadas lista de Produtos</param>
        /// <returns>Lista de Produtos</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("getList")]
        [ProducesResponseType(typeof(ActionResult<ResultModel<Produto>>), 200)]
        public ActionResult<ResultModel<Produto>> GetList([FromQuery] ProdutoFilter filter)
        {
            using (var business = new ProdutoBusiness())
            {
                filter.ProcessQueryString(Request.Query);
                var data = business.GetList(filter);
                return data;
            }
        }

        /// <summary>
        /// EndPoint para inserir um novo Produto
        /// </summary>
        /// <param name="produto">Objeto para inserir o Produto</param>
        /// <returns>Confirmação/Resultado da inserção do Produto</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Insert")]
        [ProducesResponseType(typeof(ActionResult<ResultModel<Produto>>), 200)]
        public ActionResult<ResultModel<Produto>> Insert([FromBody] Produto produto)
        {
            using (var business = new ProdutoBusiness())
            {
                var data = business.Insert(produto);
                return Ok(data);
            }
        }

        /// <summary>
        /// EndPoint para alterer um Produto
        /// </summary>
        /// <param name="produto">Objeto para alterer o Produto</param>
        /// <returns>Confirmação/Resultado da altereção do Produto</returns>
        [HttpPut]
        [AllowAnonymous]
        [Route("Update")]
        [ProducesResponseType(typeof(ActionResult<ResultModel<Produto>>), 200)]
        public ActionResult<ResultModel<Produto>> Update([FromBody] Produto produto)
        {
            using (var business = new ProdutoBusiness())
            {
                var data = business.Update(produto);
                return data;
            }
        }

        /// <summary>
        /// EndPoint para excluir um produto, caso o produto esteja vincolado a algo, não será feito a exclusão, mas sim será desativado
        /// </summary>
        /// <param name="id">Id de Produto</param>
        /// <returns>Confirmação/Resultado da Exclusão do Produto</returns>
        [HttpDelete("Delete/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ActionResult<ResultModel<Produto>>), 200)]
        public ActionResult<ResultModel<Produto>> Delete(int id)
        {
            using (var business = new ProdutoBusiness())
            {
                var data = business.Delete(new Produto() { Id = id });
                return data;
            }
        }

    }
}
