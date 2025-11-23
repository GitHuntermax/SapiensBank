using System;

namespace SapiensBank
{
    public class UX
    {
        private readonly Banco _banco;
        private readonly string _titulo;

        public UX(string titulo, Banco banco)
        {
            _titulo = titulo;
            _banco = banco;
        }

        public void Executar()
        {
            Console.Clear();
            Console.WriteLine("------ " + _titulo + " ------");
            Console.WriteLine(" [1] Criar Conta");
            Console.WriteLine(" [2] Listar Contas");
            Console.WriteLine(" [3] Sacar");
            Console.WriteLine(" [4] Depositar");
            Console.WriteLine(" [5] Aumentar Limite");
            Console.WriteLine(" [6] Diminuir Limite");
            Console.WriteLine(" [9] Sair");
            Console.Write("Digite a opção: ");
            var opcao = Console.ReadLine()?.Trim();

            switch (opcao)
            {
                case "1": CriarConta(); break;
                case "2": ListarContas(); break;
                case "3": Sacar(); break;
                case "4": Depositar(); break;
                case "5": AumentarLimite(); break;
                case "6": DiminuirLimite(); break;
                case "9": return;
                default: break;
            }

            Executar();
        }

        private void CriarConta()
        {
            Console.Clear();

            Console.Write("Número da Conta: ");
            int numero = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Nome do Cliente: ");
            string cliente = Console.ReadLine() ?? "";

            Console.Write("CPF: ");
            string cpf = Console.ReadLine() ?? "";

            Console.Write("Senha: ");
            string senha = Console.ReadLine() ?? "";

            Console.Write("Limite inicial: ");
            decimal limite = decimal.Parse(Console.ReadLine() ?? "0");

            Conta conta = new Conta(numero, cliente, cpf, senha, limite);
            _banco.AdicionarConta(conta);

            Console.WriteLine("Conta criada com sucesso!");
            Console.ReadLine();
        }

        private void ListarContas()
        {
            Console.Clear();

            foreach (var conta in _banco.Contas)
            {
                Console.WriteLine($"Conta: {conta.Numero}");
                Console.WriteLine($"Cliente: {conta.Cliente}");
                Console.WriteLine($"Saldo: {conta.Saldo:C}");
                Console.WriteLine($"Limite: {conta.Limite:C}");
                Console.WriteLine($"Saldo Disponível: {conta.SaldoDisponivel:C}");
                Console.WriteLine("-----------------------------");
            }

            Console.ReadLine();
        }

        private Conta? AutenticarUsuario()
        {
            Console.Write("Número da Conta: ");
            int numero = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Senha: ");
            string senha = Console.ReadLine() ?? "";

            return _banco.EncontrarConta(numero, senha);
        }

        private void Sacar()
        {
            Console.Clear();
            var conta = AutenticarUsuario();

            if (conta == null)
            {
                Console.WriteLine("Conta ou senha inválida!");
                Console.ReadLine();
                return;
            }

            Console.Write("Valor do saque: ");
            decimal valor = decimal.Parse(Console.ReadLine() ?? "0");

            if (valor <= conta.SaldoDisponivel)
            {
                if (valor <= conta.Saldo)
                    conta.Saldo -= valor;
                else
                {
                    decimal restante = valor - conta.Saldo;
                    conta.Saldo = 0;
                    conta.Limite -= restante;
                }

                Console.WriteLine("Saque realizado com sucesso!");
            }
            else
            {
                Console.WriteLine("Saldo insuficiente!");
            }

            Console.ReadLine();
        }

        private void Depositar()
        {
            Console.Clear();
            var conta = AutenticarUsuario();

            if (conta == null)
            {
                Console.WriteLine("Conta ou senha inválida!");
                Console.ReadLine();
                return;
            }

            Console.Write("Valor do depósito: ");
            decimal valor = decimal.Parse(Console.ReadLine() ?? "0");

            conta.Saldo += valor;

            Console.WriteLine("Depósito realizado!");
            Console.ReadLine();
        }

        private void AumentarLimite()
        {
            Console.Clear();
            var conta = AutenticarUsuario();

            if (conta == null)
            {
                Console.WriteLine("Conta ou senha inválida!");
                Console.ReadLine();
                return;
            }

            Console.Write("Valor para aumentar o limite: ");
            decimal valor = decimal.Parse(Console.ReadLine() ?? "0");

            conta.Limite += valor;

            Console.WriteLine("Limite aumentado!");
            Console.ReadLine();
        }

        private void DiminuirLimite()
        {
            Console.Clear();
            var conta = AutenticarUsuario();

            if (conta == null)
            {
                Console.WriteLine("Conta ou senha inválida!");
                Console.ReadLine();
                return;
            }

            Console.Write("Valor para diminuir o limite: ");
            decimal valor = decimal.Parse(Console.ReadLine() ?? "0");

            if (valor <= conta.Limite)
                conta.Limite -= valor;
            else
                Console.WriteLine("Não é possível diminuir além do limite atual!");

            Console.WriteLine("Operação concluída!");
            Console.ReadLine();
        }
    }
}
