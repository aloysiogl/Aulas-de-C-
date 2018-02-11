using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Siga inicialmente para a classe Main

namespace Snake
{
    //Define uma célula (cobra, parede ou fruta)
    class Cell
    {
        //Posição da célula
        int x;
        int y;

        //Tipo da célula (cobra, parede ou fruta)
        int type;

        //Contrutor que seta os parêmetros acima
        public Cell(int x, int y, int t)
        {
            this.x = x;
            this.y = y;
            this.type = t;
        }

        //Getter para a posção x
        public int getX()
        {
            return this.x;
        }

        //Getter para a posção y
        public int getY()
        {
            return this.y;
        }

        //Getter para o tipo
        public int getType()
        {
            return this.type;
        }

        //Setter para a posção x
        public void setX(int x)
        {
            this.x = x;
        }

        //Setter para a posção y
        public void setY(int y)
        {
            this.y = y;
        }

        //Setter para o tipo
        public void setType(int t)
        {
            this.type = t;
        }

        //Voltar para Snak
    }

    //Guarda todas as células do campo
    class Field
    {
        //Tamanho do campo
        int size;

        //Campo
        Cell[,] cellMatrix;

        //COntrutor que cria um campo quadrado de tamanho n
        public Field(int n)
        {
            //Alocando a matriz de células
            this.cellMatrix = new Cell[n, n];

            //Alocando o campo
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    if (j == 0 || i == 0 || j == n-1 || i == n - 1)
                    {
                        cellMatrix[i, j] = new Cell(i, j, 1);
                    }
                    else
                    {
                        cellMatrix[i, j] = new Cell(i,j,0);
                    }
                }
            }

            //Setando a cariável de tamanho do campo
            this.size = n;

        }

        //Adicionando cobra ao campo
        public void addSnake(Snak snake)
        {
            for (int i = 0; i < snake.getSize(); ++i)
            {
                cellMatrix[snake.getBody()[i].getX(), snake.getBody()[i].getY()] = snake.getBody()[i];
            }
        }

        //Getter para uma posição genperica no campo
        public Cell getValue(int x, int y)
        {
            return cellMatrix[x, y];
        }

        //Setter para uma poisção genérica no campo
        public void setValue(Cell cell)
        {
            cellMatrix[cell.getX(), cell.getY()] = cell;
        }

        //Getter para o tamanho do campo
        public int getSize()
        {
            return size;
        }

        //Votar para Snak
    }

    //Objeto responsável por imprimir os elementos na tela
    class Printer
    {
        //Printa um campo recebido
        public void printAll(Field field)
        {
            for (int i = 0; i < field.getSize(); ++i)
            {
                for (int j = 0; j < field.getSize(); ++j)
                {
                    Console.SetCursorPosition(i, j);
                    int tipo = field.getValue(i, j).getType();
                    Console.Write(map(tipo));
                }
            }
        }

        //Mapeia os valores de cada tipo de célula para caracteres
        public static char map(int value)
        {
            switch (value)
            {
                case 0:
                    return ' ';
                case 1:
                    return '#';
                case 2:
                    return 'O';
            }
            return 'F';
        }

        //Atualiza uma única posição na tela
        public void printUpdate(Cell cell)
        {
            Console.SetCursorPosition(cell.getX(), cell.getY());
            Console.Write(map(cell.getType()));
        }

        //voltar para Snak
    }

    //Define a cobra
    class Snak
    {
        //Variáveis de tamanho da cobra
        int size;
        int maxSize;

        //Variáveis de posição da cabeça da cobra
        int headX;
        int headY;

        //Variável que detecta de o jogo acabou
        bool gameOver;

        //Lista que forma o corpo da cobra
        Cell[] body;

        //Contrutor que cria a cobra em x, y e tamanho máximo max
        public Snak(int x, int y, int max)
        {
            //Setando as variáveis descritas acima
            this.headX = x;
            this.headY = y;
            this.size = 1;
            this.maxSize = max;
            this.gameOver = false;

            this.body = new Cell[max];

            body[0] = new Cell(x, y, 2);
        }

        //Getter para o corpo da cobra
        public Cell[] getBody()
        {
            return body;
        }

        //FUnção que atulaiza a posição da cobra e retorna se o jogo acabou
        public bool moveUpdate(char keystroke, Printer printer, Field field)
        {
            //Atualizando a posição da cabeça da cobra baseado no keystroke
            if (keystroke != ' ')
            {
                if (keystroke == 'w')
                {
                    headY--;
                }
                else if (keystroke == 'a')
                {
                    headX--;
                }
                else if (keystroke == 's')
                {
                    headY++;
                }
                else
                {
                    headX++;
                }

                //Criando nova célula na nova poisção da cabeça
                //ir para a definição de Cell
                Cell newHead = new Cell(headX, headY, 2);

                //Checando no campo o que está na posição em que a cabeça deveria ir
                //ir para a definição de Field
                int newHeadValue = field.getValue(headX, headY).getType();

                //Atualizando as posição da cobra
                for (int i = size; i > 0; --i)
                {
                    body[i] = body[i - 1];
                }

                //Adicionando ao campo a nova posição da cabeça da cobra
                field.setValue(newHead);

                //Atualizando a cabeça da cobra
                body[0] = newHead;

                //Aumentando a cobra se ela encontrou uma fruta
                if(newHeadValue == 3)
                {
                    size++;

                    //Gerando uma nova fruta em uma posição aleatória
                    Random rnd = new Random();
                    Cell fruta;

                    //Garantindo que a fruta não é gerada detro da cobra
                    bool bate = false;
                    do
                    {
                        fruta = new Cell(rnd.Next(1, field.getSize() - 2), rnd.Next(1, field.getSize() - 2), 3);
                        for (int i = 0; i < size; ++i)
                        {
                            if (fruta.getX() == body[i].getX() && fruta.getY() == body[i].getY())
                            {
                                bate = true;
                            }
                        }
                    } while (bate);

                    //Adicionando a fruta ao campo
                    field.setValue(fruta);

                    //Atualizando a fruta na vizualização
                    //ir paraa a definição de Printer
                    printer.printUpdate(fruta);
                }

                //Atualizando a conbra caso ela não tenha batido em nada
                if (newHeadValue == 0)
                {
                    Cell tail = new Cell(body[size].getX(), body[size].getY(), 0);
                    field.setValue(tail);
                    printer.printUpdate(tail);
                }

                //Terminando o jogo caso a cobra bata nela mesma ou na parede
                else if (newHeadValue == 1 || newHeadValue == 2)
                {
                    gameOver = true;
                    return gameOver;
                }

                //Atualizando a posição da cabeça na tela
                printer.printUpdate(newHead);

            }

            //Se a cobra não bateu nela ou na parede o jogo não acabou
            return false;

            //ir para Control
        }

        public int getSize()
        {
            return size;
        }
    }

    class Control
    {
        //Tecla apertada padra inicialmente
        char key = ' ';

        //Detecta qual tecla fou apertada
        public char getKeystroke(Field field)
        {
            //Se for possível ler a tecla atualizar key
            if (Console.KeyAvailable)
            {
                key =  Console.ReadKey().KeyChar;
                Console.CursorLeft -= 1;
                Console.Write(Printer.map(field.getValue(Console.CursorTop,Console.CursorLeft).getType()));

            }

            //Retornando a tacla lida
            return key;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Instanciando campo de tamanho determinado (20)
            Field campo = new Field(20);

            //Instanciando objeto que printa os elelmentos na tela
            Printer tela = new Printer();

            //Instanciando o objeto que representa a cobra
            Snak snake = new Snak(10, 10, 40);

            //Intanciando o objeto controlador que realiza os movimentos da cobra
            Control controller = new Control();

            //Instanciando a primeira fruta na posição 15, 15
            Cell fruta1 = new Cell(15, 15, 3);

            //Adicionanado a fruta ao campo
            campo.setValue(fruta1);

            //Adicionando a cobra ao campo
            campo.addSnake(snake);

            //Printando todos os elementos no campo
            tela.printAll(campo);

            //Setando a posição do cursor
            Console.SetCursorPosition(0, 0);

            //Gameloop (aqui serão atulaizados os parâmetros do jogo)
            //moveUpdate atualiza a posição da cobra
            while (!snake.moveUpdate(controller.getKeystroke(campo), tela, campo))
            {
                //Essa função para a interface por 100ms p que da a taxa de refresh
                Thread.Sleep(100);
            }

            //Seguir para a classe Snak

            //Retornando o cursor à posução inicial
            Console.SetCursorPosition(0, campo.getSize());
        }
    }
}
