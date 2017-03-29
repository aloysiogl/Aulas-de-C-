using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introducao
{
    //Classe
    class Vector2
    {
        //Variáves 
        private int x;
        private int y;

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void printVector()
        {
            Console.WriteLine("X:{0} Y:{1}", x, y);
        }
    }
    class Program
    {
        //Função
        static private void Hello(string palavra)
        {
            Console.WriteLine("Hello {0}", palavra);
        }
        static void Main(string[] args)
        {
            //->Namespace

            //->Tipos do C#(fortemente tipada): int, long, float, double, decimal, char, bool, string etc
            //Padrão é o double (use no final f para declarar um valor float)
            float float_ = 1.00f;

            double double_ = 1.00;

            //Entrada e saída
            //string melhorTurma = "T20";
            //string pergunta = "Qual a melhor turma?";


            //string sentenca = pergunta + " " + melhorTurma + "\n";

            //Escrevendo na tela
            //Console.Write(sentenca);

            //Lendo a tela
            //float f1;
            //float f2;

            //f1 = float.Parse(Console.ReadLine());
            //f2 = float.Parse(Console.ReadLine());

            //ttps://msdn.microsoft.com/en-us/library/241ad66z(v=vs.85).aspx
            //Console.WriteLine("{1:g} {0:c}", f1, f2);

            //string a = "aloysio";
            //Hello(a);
            //Vector2 v = new Vector2(2, 3);
            //v.printVector();

            //If-else-switch-while: iguais ao C

            //Declarando um vetor
            //int[] vetor;
            //vetor = new int[3];

            //int[] vetor2 = { 1, 2, 3, 4, 5 };

            //Iterando sobre o vetor
            //foreach(var numero in vetor2)
            //{
            //    Console.WriteLine(numero.ToString());
            //}

            //Matriz
            //float[,] matriz;
            //matriz[4, 6] = 0;
            //matriz = new float[5, 6];
            //matriz[4, 5] = 0;

            //int[][] matriz2 = new int[3][];
            //for (int i = 0; i < matriz2.Length; i++) 
            //{
            //    matriz2[i] = new int[3];
            //}

            //Coleção padrão do C#
            //List<int> lista = new List<int>();

            //lista.Add(1);
            //lista.Add(23);

            //foreach(var numero in lista)
            //{
            //    Console.WriteLine(numero);
            //}
        }
    }
}
