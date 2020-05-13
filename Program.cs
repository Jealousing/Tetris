using System;
using System.Threading;
/*
Console 테트리스 
2020-05-13 -> 2020-05-

테트리스 게임시작

1.도형랜덤선택
도형 하강
맨밑줄부터 다채워져있는지 확인 -> 채워져있으면 그줄 삭제 -> 밑으로댕기기

1번 반복
도형이 더이상 못내려가면 게임오버

도형 모양 7개, 방향 4개 = 28가지
판 크기 10x20

*/

namespace Tetris_SeoDongju
{
    class Game
    {
        protected int T_Width = 12;
        protected int T_Helight = 22;
        public int m_PosX = 5;
        public int m_PosY = 0;
        public int[,] TetrisBoard = new int[22, 12]
                {
                    {3,3,3,3,3,3,3,3,3,3,3,3},
                    {3,0,0,0,0,0,0,0,0,0,0,3},
                    {3,0,0,0,0,0,0,0,0,0,0,3},
                    {3,0,0,0,0,0,0,0,0,0,0,3},
                    {3,0,0,0,0,0,0,0,0,0,0,3},
                    {3,0,0,0,0,0,0,0,0,0,0,3},
                    {3,0,0,0,0,0,0,0,0,0,0,3},
                    {3,0,0,0,0,0,0,0,0,0,0,3},
                    {3,0,0,0,0,0,0,0,0,0,0,3},
                    {3,0,0,0,0,0,0,0,0,0,0,3},
                    {3,0,0,0,0,0,0,0,0,0,0,3},
                    {3,0,0,0,0,0,0,0,0,0,0,3},
                    {3,0,0,0,0,0,0,0,0,0,0,3},
                    {3,0,0,0,0,0,0,0,0,0,0,3},
                    {3,0,0,0,0,0,0,0,0,0,0,3},
                    {3,0,0,0,0,0,0,0,0,0,0,3},
                    {3,0,0,0,0,0,0,0,0,0,0,3},
                    {3,0,0,0,0,0,0,0,0,0,0,3},
                    {3,0,0,0,0,0,0,0,0,0,0,3},
                    {3,0,0,0,0,0,0,0,0,0,0,3},
                    {3,0,0,0,0,0,0,0,0,0,0,3},
                    {3,3,3,3,3,3,3,3,3,3,3,3}
                };
        private int count;
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        void DrawShip()
        {
            Console.SetCursorPosition(m_PosX*2, m_PosY);
            Console.Write("□");//위에 어떻게넣지?
            if(TetrisBoard[m_PosY, m_PosX] != 2 || TetrisBoard[m_PosY, m_PosX] != 3)
            m_PosY++;
            if(TetrisBoard[m_PosY, m_PosX] ==2|| TetrisBoard[m_PosY, m_PosX] == 3)
            {
                TetrisBoard[m_PosY-1, m_PosX] =2;
                m_PosX = 5;
                m_PosY = 0;
            }
        }

        public void Draw()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo info = Console.ReadKey();//블러킹
                if (info.Key == ConsoleKey.D || info.Key == ConsoleKey.RightArrow)
                {
                    m_PosX++;
                    if (TetrisBoard[m_PosY, m_PosX] == 2)
                    m_PosX--;

                    if (m_PosX > 10)
                        m_PosX = 10;
                }
                if (info.Key == ConsoleKey.A || info.Key == ConsoleKey.LeftArrow)
                {
                    m_PosX --;
                    if (TetrisBoard[m_PosY, m_PosX] == 2)
                        m_PosX++;

                    if (m_PosX < 1)
                        m_PosX = 1;
                }

            }
            BGDraw();
            DrawShip();
            DeleteLine();
            GameOver();
        }

        void BGDraw()
        {
            Console.SetCursorPosition(0, 0);
            for(int i=0;i<T_Helight;i++)
            {
                for(int j=0;j<T_Width;j++)
                {
                    if (TetrisBoard[i, j] == 0)
                        Console.Write("  ");
                    if (TetrisBoard[i, j] == 3)
                        Console.Write("■");
                    if (TetrisBoard[i, j] == 2)
                        Console.Write("□");
                }
                Console.WriteLine();
            }
        }

        void DeleteLine()
        {
            for (int i = 0; i <T_Helight;i++)
            {
                for(int j=0;j<T_Width;j++)
                {
                    if (TetrisBoard[i,j]==2)
                    {
                        Count++;
                        if(Count == 10)
                        {
                            for(int o=1;o<T_Width-1;o++)
                            {
                                TetrisBoard[i, o] = 0;
                            }
                            Count = 0;
                        }
                    }
                }
                Count = 0;
            }
        }
        void GameOver()
        {
            for (int i=0;i<T_Width;i++)
            {
                if (2== TetrisBoard[0, i])
                {
                    Environment.Exit(0);
                }
            }
            
        }

    }
    class Mainclass
    {
        static void Main(string[] args)
        {
            Game Tetris = new Game();
            while (true)
            {
                Thread.Sleep(100);
                Console.Clear();

                Tetris.Draw();

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo info = Console.ReadKey();
                    if (info.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                }

            }
        }
    }
}
