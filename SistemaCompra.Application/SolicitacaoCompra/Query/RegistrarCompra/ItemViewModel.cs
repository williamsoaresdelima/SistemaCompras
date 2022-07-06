namespace SistemaCompra.Application.SolicitacaoCompra.Query.RegistrarCompra
{
    public class ItemViewModel
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public decimal Preco { get; set; }
        public int Qtd { get; set; }
    }
}