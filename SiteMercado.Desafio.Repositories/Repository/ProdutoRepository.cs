using Microsoft.EntityFrameworkCore;
using SiteMercado.Desafio.Entities.Models;
using SiteMercado.Desafio.Utils.Models;
using SiteMercado.Desafio.Utils.Services;
using System;
using System.Linq;
using SiteMercado.Desafio.Entities.Filters;

namespace SiteMercado.Desafio.Repositories.Repository
{
    public class ProdutoRepository : BaseRepository<Produto>
    {
        public ProdutoRepository(DbContext context) : base(context) { }

        public ResultModel<Produto> GetList(ProdutoFilter filter, string sortDir = "asc", int sortCol = 0)
        {
            var query = GetList(x => (EF.Functions.Like(x.Nome, $"%{filter.search}%")));


            if (sortDir.ToLower() == "asc")
            {
                switch (sortCol)
                {
                    case 0:
                        query = query.OrderBy(x => x.Nome);
                        break;
                }
            }
            else
            {
                switch (sortCol)
                {
                    case 0:
                        query = query.OrderByDescending(x => x.Nome);
                        break;
                }
            }

            //94057206515
            var data = new ResultModel<Produto>();

            if (query.Any())
            {
                var total = query.LongCount();

                var quantidadePaginas = (double)total / (double)filter.ResultPagesModel.Offset;

                query = query.Skip(filter.ResultPagesModel.Actual.ToInt32()).Take(filter.ResultPagesModel.Offset.ToInt32());

                data.Items = query.ToList();

                data.Pages.TotalItems = total;
                data.Pages.Total = (long)Math.Ceiling(quantidadePaginas);//;
                data.Pages.Actual = filter.ResultPagesModel.Actual;
                data.Pages.Offset = filter.ResultPagesModel.Offset == int.MaxValue ? total : filter.ResultPagesModel.Offset;
            }

            return data;
        }
    }
}
