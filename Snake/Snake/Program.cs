using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Snake
{
    class Cell
    {
        int x;
        int y;
        int type;

        public Cell(int x, int y, int t)
        {
            this.x = x;
            this.y = y;
            this.type = t;
        }

        public int getX()
        {
            return this.x;
        }

        public int getY()
        {
            return this.y;
        }

        public int getType()
        {
            return this.type;
        }

        public void setX(int x)
        {
            this.x = x;
        }

        public void setY(int y)
        {
            this.y = y;
        }

        public void setType(int t)
        {
            this.type = t;
        }
    }

    class Field
    {
        int size;

        Cell[,] cellMatrix;

        public Field(int n)
        {
            this.cellMatrix = new Cell[n, n];

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

            this.size = n;
            
        }

        public void addSnake(Snak snake)
        {
            for (int i = 0; i < snake.getSize(); ++i)
            {
                cellMatrix[snake.getBody()[i].getX(), snake.getBody()[i].getY()] = snake.getBody()[i];
            }
        }

        public Cell getValue(int x, int y)
        {
            return cellMatrix[x, y];
        }

        public void setValue(Cell cell)
        {
            cellMatrix[cell.getX(), cell.getY()] = cell;
        }

        public int getSize()
        {
            return size;
        }
    }

    class Printer
    {
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

        public void printUpdate(Cell cell)
        {
            Console.SetCursorPosition(cell.getX(), cell.getY());
            Console.Write(map(cell.getType()));
        }
    }

    class Snak
    {
        int size;
        int maxSize;

        int headX;
        int headY;

        bool gameOver;

        Cell[] body;

        public Snak(int x, int y, int max)
        {
            this.headX = x;
            this.headY = y;
            this.size = 1;
            this.maxSize = max;
            this.gameOver = false;

            this.body = new Cell[max];

            body[0] = new Cell(x, y, 2);
        }

        public Cell[] getBody()
        {
            return body;
        }

        public bool moveUpdate(char keystroke, Printer printer, Field field)
        {
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

                Cell newHead = new Cell(headX, headY, 2);

                int newHeadValue = field.getValue(headX, headY).getType();

                for (int i = size; i > 0; --i)
                {
                    body[i] = body[i - 1];
                }

                field.setValue(newHead);
                body[0] = newHead;

                if(newHeadValue == 3)
                {
                    size++;
                    Random rnd = new Random();
                    Cell fruta;
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
                    field.setValue(fruta);
                    printer.printUpdate(fruta);
                }
                if (newHeadValue == 0)
                {
                    Cell tail = new Cell(body[size].getX(), body[size].getY(), 0);
                    field.setValue(tail);
                    printer.printUpdate(tail);
                }

                else if (newHeadValue == 1 || newHeadValue == 2)
                {
                    gameOver = true;
                    return gameOver;
                }

                printer.printUpdate(newHead);
               
            }

            return false;
        }

        public int getSize()
        {
            return size;
        }
    }

    class Control
    {
        char key = ' ';
        public char getKeystroke(Field field)
        {
            if (Console.KeyAvailable)
            {
                key =  Console.ReadKey().KeyChar;
                Console.CursorLeft -= 1;
                Console.Write(Printer.map(field.getValue(Console.CursorTop,Console.CursorLeft).getType()));

            }
            return key;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Field campo = new Field(20);
            Printer tela = new Printer();
            Snak snake = new Snak(10, 10, 40);
            Control controller = new Control();
            Cell fruta1 = new Cell(15, 15, 3);
            campo.setValue(fruta1);
            campo.addSnake(snake);
            tela.printAll(campo);
            Console.SetCursorPosition(0, 0);
            //Gameloop
            while (!snake.moveUpdate(controller.getKeystroke(campo), tela, campo))
            {
                //tela.printAll(campo);
                Thread.Sleep(100);
            }

            Console.SetCursorPosition(0, campo.getSize());
        }
    }
}
