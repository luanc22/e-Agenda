using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.ConsoleApp.Compartilhado
{
    public class Notificador
    {
        public void ApresentarMensagem(string mensagem, TipoMensagem tipoMensagem)
        {
            switch (tipoMensagem)
            {
                case TipoMensagem.Sucesso:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case TipoMensagem.Atencao:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case TipoMensagem.Erro:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }

            Console.WriteLine();
            Console.WriteLine(mensagem);
            Console.WriteLine();
            Console.ResetColor();

            Console.Write("Aperte ENTER para prosseguir.");
            Console.ReadLine();

        }
    }
}
