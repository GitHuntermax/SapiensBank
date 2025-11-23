using System.Collections.Generic;

namespace SapiensBank
{
    public class Banco
    {
        public List<Conta> Contas { get; private set; } = new List<Conta>();

        public Conta? EncontrarConta(int numero, string senha)
        {
            return Contas.Find(c => c.Numero == numero && c.Senha == senha);
        }

        public void AdicionarConta(Conta conta)
        {
            Contas.Add(conta);
        }
    }
}
