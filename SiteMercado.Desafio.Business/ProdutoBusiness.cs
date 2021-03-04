using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SiteMercado.Desafio.Entities.Models;
using SiteMercado.Desafio.Repositories.Context;
using SiteMercado.Desafio.Repositories.UnitOfWork;
using SiteMercado.Desafio.Utils.Models;
using SiteMercado.Desafio.Utils.Services;
using System;
using System.Linq;
using static SiteMercado.Desafio.Utils.Enums.EnumCommon;

namespace SiteMercado.Desafio.Business
{
    public class ProdutoBusiness : BusinessBase<UnitOfWork>
    {
        public ProdutoBusiness() : base(new DBDesafioSiteMercadoContext())
        {

        }

        public ProdutoBusiness(DbContext context) : base(context)
        {
        }

        public ResultModel<Produto> Get(long id)
        {
            return UnitOfWork.ProdutoRepository.GetById(new Entities.Filters.ProdutoFilter() { Id = id });
        }

        public ResultModel<Produto> GetList(Entities.Filters.ProdutoFilter filter, string sortDir = "asc", int sortCol = 0)
        {
            try
            {
                if (filter.ResultPagesModel != null &&
                    (filter.ResultPagesModel.Offset == 0 &&
                     filter.ResultPagesModel.Actual == 0))
                {
                    filter.ResultPagesModel = new Entities.Filters.PaginationFilter().ResultPagesModel;
                }

                var resultModel = UnitOfWork.ProdutoRepository.GetList(filter, sortDir, sortCol);
                resultModel.Messages = null;
                return resultModel;

            }
            catch (Exception ex)
            {

                var resultModel = new ResultModel<Produto>(false);
                resultModel.Pages = null;
                resultModel.AddMessage(ex.Message, SystemMessageTypeEnum.Error);
                return resultModel;
            }

        }

        public ResultModel<Produto> Insert(Produto produto)
        {
            try
            {
                var resultModel = new ResultModel<Produto>(true);

                var data = UnitOfWork.ProdutoRepository.Add(produto);
                if (data is null)
                {
                    resultModel.IsOk = false;
                    resultModel.AddMessage(string.Format(MensagemErro.MSG007, "Produto"), SystemMessageTypeEnum.Info);
                }
                else
                {
                    resultModel.Items.Add(data);
                    resultModel.AddMessage(string.Format(MensagemSucesso.MSG001), SystemMessageTypeEnum.Success);
                    this.Commit();
                }
                resultModel.Pages = null;

                return resultModel;
            }
            catch (Exception ex)
            {
                var resultModel = new ResultModel<Produto>(false);
                resultModel.Pages = null;
                resultModel.AddMessage(ex.Message, SystemMessageTypeEnum.Error);
                return resultModel;
            }
        }

        public ResultModel<Produto> Update(Produto produto)
        {
            try
            {
                var resultModel = new ResultModel<Produto>(true);

                var data = UnitOfWork.ProdutoRepository.Update(produto);
                if (data is null)
                {
                    resultModel.IsOk = false;
                    resultModel.AddMessage(string.Format(MensagemErro.MSG007, "Produto"), SystemMessageTypeEnum.Info);
                }
                else
                {
                    resultModel.Items.Add(data);
                    resultModel.AddMessage(string.Format(MensagemSucesso.MSG001), SystemMessageTypeEnum.Success);
                    this.Commit();
                }
                resultModel.Pages = null;

                return resultModel;
            }
            catch (Exception ex)
            {
                var resultModel = new ResultModel<Produto>(false);
                resultModel.Pages = null;
                resultModel.AddMessage(ex.Message, SystemMessageTypeEnum.Error);
                return resultModel;
            }
        }

        public ResultModel<Produto> Delete(Produto produto)
        {
            try
            {
                var resultModel = new ResultModel<Produto>(true);

                Produto prod = this.Get(produto.Id).Items[0];

                var data = UnitOfWork.ProdutoRepository.Delete(prod);
                if (data is null)
                {
                    resultModel.IsOk = false;
                    resultModel.AddMessage(string.Format(MensagemErro.MSG019, "Produto"), SystemMessageTypeEnum.Info);
                }
                else
                {
                    resultModel.Items.Add(produto);
                    resultModel.AddMessage(string.Format(MensagemSucesso.MSG004), SystemMessageTypeEnum.Success);
                    this.Commit();
                }
                resultModel.Pages = null;

                return resultModel;
            }
            catch (Exception ex)
            {
                var resultModel = new ResultModel<Produto>(false);
                resultModel.Pages = null;
                resultModel.AddMessage(ex.Message, SystemMessageTypeEnum.Error);
                return resultModel;
            }
        }

        public ResultModel<Produto> Save(Produto produto, string perfis, string perfisInterno, IFormFile foto)
        {
            try
            {
                var data = new Produto();

                var resultModel = new ResultModel<Produto>(true);

                var item = Get(produto.Id).Items.FirstOrDefault();

                var isNew = item is null;
                var error = string.Empty;

                var perfisArray = !string.IsNullOrWhiteSpace(perfis) ? perfis.Split(',') : new string[] { };

                if (isNew)
                {
                    produto.Nome = produto.Nome;
                    produto.ValorVenda = produto.ValorVenda;
                    produto.Imagem = produto.Imagem;
                }
                else
                {
                    item.Nome = produto.Nome;
                    item.ValorVenda = produto.ValorVenda;

                    if (produto.Imagem != null)
                        item.Imagem = produto.Imagem;

                    data = UnitOfWork.ProdutoRepository.Update(produto);
                }

                if (data.Id > 0)
                {
                    resultModel.IsOk = false;
                    resultModel.AddMessage(string.Format(MensagemErro.MSG007, "Produto"), SystemMessageTypeEnum.Info);
                }
                else
                {
                    resultModel.Items.Add(data);
                    resultModel.AddMessage(string.Format(MensagemSucesso.MSG001), SystemMessageTypeEnum.Success);
                    Commit();
                }

                resultModel.Pages = null;

                return resultModel;
            }
            catch (Exception ex)
            {
                var resultModel = new ResultModel<Produto>(false);
                resultModel.Pages = null;
                resultModel.AddMessage(ex.Message, SystemMessageTypeEnum.Error);
                return resultModel;
            }
        }
    }
}
