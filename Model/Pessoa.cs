namespace MeuBancoSerasaConsumer.Model
{
    public class Pessoa
    {
        public long? IdPessoa { get; set; }
        public string? PrimeiroNome { get; set; }
        public string? Sobrenome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? CPF { get; set; }
        public string? RG { get; set; }
        public decimal RendaBruta { get; set; }
        public string? Email { get; set; }
        public ICollection<Emprestimo>? Emprestimos { get; set; }
    }
    
}