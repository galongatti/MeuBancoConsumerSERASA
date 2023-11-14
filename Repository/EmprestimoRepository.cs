using MeuBancoSerasaConsumer.Connection;
using MeuBancoSerasaConsumer.Model;
using Dapper;
using System.Configuration;
using System.Data.SqlClient;

namespace MeuBancoSerasaConsumer.Repository
{
    public class EmprestimoRepository
    {
        public EmprestimoRepository() { }

        public void AtualizarEmprestimo(Emprestimo emprestimo)
        {
            string sql = "UPDATE Emprestimo SET NotaSERASA = @NotaSERASA, ScoreSERASA = @ScoreSERASA WHERE IdEmprestimo = @IdEmprestimo;";


            using (CustomSQLConnection customSQL = new CustomSQLConnection())
            {
                SqlConnection sqlConnection = customSQL.ConexaoBanco();
                var affectedRows = sqlConnection.Execute(sql, new { NotaSERASA = emprestimo.NotaSERASA, ScoreSERASA = emprestimo.ScoreSERASA, IdEmprestimo = emprestimo.IdEmprestimo });
            }
        }
    }
}