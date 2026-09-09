namespace OrexApp.Features.MantainProduct.Product
{
    public class Products
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public DateTime DtCadastro { get; set; }
        public DateTime? DtAtualizacao { get; set; }

        public Products()
        {
        }
        public Products (string descricao, bool ativo)
        {
            Descricao = descricao;
            Ativo = ativo;
            DtCadastro = DateTime.UtcNow;
        }
    }
}