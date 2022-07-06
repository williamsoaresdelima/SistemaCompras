using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.Core.Model;
using SistemaCompra.Domain.ProdutoAggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
    public class SolicitacaoCompra : Entity
    {
        public UsuarioSolicitante UsuarioSolicitante { get; private set; }
        public NomeFornecedor NomeFornecedor { get; private set; }
        public IList<Item> Itens { get; private set; }
        public DateTime Data { get; private set; }
        public Money TotalGeral { get; private set; }
        public Situacao Situacao { get; private set; }
        public CondicaoPagamento CondicaoPagamento { get; set; }

        private SolicitacaoCompra() { }

        public SolicitacaoCompra(string usuarioSolicitante, string nomeFornecedor)
        {
            Id = Guid.NewGuid();
            UsuarioSolicitante = new UsuarioSolicitante(usuarioSolicitante);
            NomeFornecedor = new NomeFornecedor(nomeFornecedor);
            Itens = new List<Item>();
            TotalGeral = new Money();
            Data = DateTime.Now;
            Situacao = Situacao.Solicitado;
        }

        public void AdicionarItem(Produto produto, int qtde)
        {
            Itens.Add(new Item(produto, qtde));
        }

        public void RegistrarCompra(IEnumerable<Item> itens)
        {

            if (itens.Count() == 0) 
                throw new BusinessRuleException("A solicitação de compra deve possuir itens!");

            int prazoPagamento = 0;
            decimal totalGeral = decimal.Zero;

            itens.ToList().ForEach(x => {
                totalGeral += x.Subtotal.Value;
            });

            SetTotalGeral(new Money(totalGeral));

            prazoPagamento = (TotalGeral.Value > 50000) ? 30 : prazoPagamento;

            CondicaoPagamento = new CondicaoPagamento(prazoPagamento);
        }

        private void SetTotalGeral(Money money)
        {
            this.TotalGeral = money;
        }
    }
}