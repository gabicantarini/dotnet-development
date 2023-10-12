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

            //Matriz

            Console.WriteLine("Notas id array 1 dimensão:\n");
            int[] notasID = new int[2]; // array em uma dimensão com índice 0 => 85 e indice 1 => 90

            notasID[0] = 85;
            notasID[1] = 90;

            for (var i = 0; i < notasID.Length; i++)
            {
                Console.WriteLine($"Notas id array 1 dimensão: {notasID[i]}");
            }

            Console.WriteLine("Notas array 2 dimensões:\n");

            int[,] notas2d = new int[3, 2]; //array em duas dimensões 

            notas2d[0, 0] = 85;  //A, Primeiro Bimestre
            notas2d[0, 1] = 90;  //A, Segundo Bimestre
            notas2d[1, 0] = 55;  //B, Primeiro Bimestre
            notas2d[1, 1] = 100; //B, Segundo Bimestre
            notas2d[2, 0] = 60;  //C, Primeiro Bimestre
            notas2d[2, 2] = 90;  //C, Segundo Bimestre


            for (var i = 0; i < notas2d.GetLength(0); i++) //GetLength(0) => percorre a primeira dimensão
            {
                Console.WriteLine($"Notas array 2 dimensões: {i}");
                for (var j = 0; j < notas2d.GetLength(1); j++) //GetLength(1) => percorre a segunda dimensão
                {
                    Console.WriteLine($"Notas array 2 dimensões: {notas2d[i, j]}");
                }
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
