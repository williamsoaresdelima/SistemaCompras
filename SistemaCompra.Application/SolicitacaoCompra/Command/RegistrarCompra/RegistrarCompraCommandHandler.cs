using AutoMapper;
using MediatR;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using SistemaCompra.Infra.Data.UoW;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProdutoAgg = SistemaCompra.Domain.ProdutoAggregate;
using SolicitacaoAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommandHandler : CommandHandler, IRequestHandler<RegistrarCompraCommand, bool>
    {
        private readonly ISolicitacaoCompraRepository _solicitacaoCompraRepostory;
        private readonly IMapper _mapper;

        public RegistrarCompraCommandHandler(ISolicitacaoCompraRepository solicitacaoCompraRepostory, IUnitOfWork uow, IMediator mediator, IMapper mapper) : base(uow, mediator)
        {
            _solicitacaoCompraRepostory = solicitacaoCompraRepostory;
            _mapper = mapper;
        }

        public Task<bool> Handle(RegistrarCompraCommand request, CancellationToken cancellationToken)
        {
            var solicitacao = new SolicitacaoAgg.SolicitacaoCompra(request.NomeSolicitante, request.NomeFornecedor);

            var itemsParaSolicitacao = new List<Item>();

            request.Itens.ForEach(item => {
               var produto = new ProdutoAgg.Produto(item.Nome, item.Descricao, item.Categoria, item.Preco);
               itemsParaSolicitacao.Add(new Item(produto, item.Qtd));
            });
            

            solicitacao.RegistrarCompra(itemsParaSolicitacao);

            _solicitacaoCompraRepostory.RegistrarCompra(solicitacao);

            Commit();
            PublishEvents(solicitacao.Events);

            return Task.FromResult(true);
        }
    }
}