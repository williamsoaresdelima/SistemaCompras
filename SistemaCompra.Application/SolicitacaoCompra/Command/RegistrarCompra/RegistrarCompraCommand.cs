using MediatR;
using SistemaCompra.Application.Produto.Query.ObterProduto;
using SistemaCompra.Application.SolicitacaoCompra.Query.RegistrarCompra;
using System.Collections.Generic;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommand : IRequest<bool>
    {
        public string NomeSolicitante { get; set; }
        public string NomeFornecedor { get; set; }
        public List<ItemViewModel> Itens { get; set; }
    }
}