namespace OrexApp.Features.ManterProduto.Produto
{
    public class Produtos
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public DateTime DtCadastro { get; set; }
        public DateTime? DtAtualizacao { get; set; }

        public Produtos()
        {
        }
        public Produtos (string descricao, bool ativo)
        {
            Descricao = descricao;
            Ativo = ativo;
            DtCadastro = DateTime.UtcNow;
        }
    }
}