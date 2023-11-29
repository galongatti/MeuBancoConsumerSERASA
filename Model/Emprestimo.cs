namespace MeuBancoSerasaConsumer.Model
{
    public class Emprestimo 
    {
        public long IdEmprestimo { get; set; }
        public DateTime? DataSolicitacao { get; set; }
        public decimal? ValorSolicitado { get; set; }
        public decimal? ValorAprovado { get; set; }
        public int? NotaSERASA {get;set;}
        public int? ScoreSERASA {get;set;}
        public long? IdPessoa { get; set; }
        public Pessoa? Pessoa { get; set; }
    
    }
    
}