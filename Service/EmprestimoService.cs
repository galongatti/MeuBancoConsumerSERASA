using MeuBancoSerasaConsumer.Model;
using MeuBancoSerasaConsumer.Repository;

namespace MeuBancoSerasaConsumer.Service
{
    public class EmprestimoService
    {

        public Emprestimo ConsultarSerasa(Emprestimo emprestimo)
        {
            Emprestimo newEmprestimo = emprestimo;
            Random rnd = new Random();
            int score = rnd.Next(0, 1000);
            int nota = 0;

            if (score < 100)
            {
                nota = 1;
            }
            else if (score < 200)
            {
                nota = 2;
            }
            else if (score < 300)
            {
                nota = 3;
            }
            else if (score < 400)
            {
                nota = 4;
            }
            else if (score < 500)
            {
                nota = 5;
            }
            else if (score < 600)
            {
                nota = 6;
            }
            else if (score < 700)
            {
                nota = 7;
            }
            else if (score < 800)
            {
                nota = 8;
            }
            else if (score < 900)
            {
                nota = 9;
            }
            else if (score < 1000)
            {
                nota = 3;
            }

            emprestimo.NotaSERASA = nota;
            emprestimo.ScoreSERASA = score;

            return emprestimo;
        }

        public void AtualarEmprestimo(Emprestimo emprestimo)
        {
            EmprestimoRepository repository = new EmprestimoRepository();
            repository.AtualizarEmprestimo(emprestimo);
        }



    }
}