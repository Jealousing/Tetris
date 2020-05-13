using System;
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
        protected int[,] TetrisBoard = new int[22, 12]
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
        public Game()
        {
            

        }

        public void BGDraw()
        {
            Game TetrisBoard = new Game();
            for(int i=0;i<T_Helight;i++)
            {
                for(int j=0;j<T_Width;j++)
                {
                    if (TetrisBoard.TetrisBoard[i, j] == 0)
                        Console.Write("  ");
                    else if (TetrisBoard.TetrisBoard[i, j] == 3)
                        Console.Write("■");
                }
                Console.WriteLine();
            }
        }

    }
    class Mainclass
    {
        static void Main(string[] args)
        {
            Game Tetris = new Game();
            Tetris.BGDraw();
            Console.WriteLine("테스트");
        }
    }
}
