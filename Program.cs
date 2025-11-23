using System;

namespace SapiensBank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Banco banco = new Banco();
            UX ux = new UX("Sapiens Bank", banco);
            ux.Executar();
        }
    }
}
