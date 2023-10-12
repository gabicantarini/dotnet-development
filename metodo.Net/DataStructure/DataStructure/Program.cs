using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Pilha - Last in, First out
            Stack<string> historico = new Stack<string>();

            historico.Push("www.google.com");
            historico.Push("www.metodo.net.com.br");
            historico.Push("www.metodo.net.com.br/artigo-1");

            Console.WriteLine($"Página atual da pilha: {historico.Peek()}"); //Peek() retorna página atual

            var paginaAnterior = historico.Pop(); //Pop() apaga último valor do histórico

            Console.WriteLine($"Página Anterior: {paginaAnterior}.");
            Console.WriteLine($"Página Atual: {historico.Peek()}.");

            

            //Fila Queue - First in, First out

            //Enqueue: Adds an object to the end of the Queue.
            //Dequeue: Removes and returns the object at the beginning of the Queue.
            //Peek: Returns the object at the beginning of the Queue without removing it.

            Queue<string> filaAtendimento = new Queue<string>();

            filaAtendimento.Enqueue("A-001");
            filaAtendimento.Enqueue("A-002");
            filaAtendimento.Enqueue("A-003");

            Console.WriteLine($"Próximo da fila: {filaAtendimento.Peek()}"); //Peek() retorna valor atual da fila first in, first out

            var atendido = filaAtendimento.Dequeue();

            Console.WriteLine($"Atendido: {atendido}");
            Console.WriteLine($"Próximo da fila: {filaAtendimento.Peek()}");


            Console.ReadLine();
        }
    }
}
