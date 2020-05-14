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
        //테트리스 판의 가로
        protected int T_Width = 12;
        //테트리스 판의 높이
        protected int T_Helight = 22;
        //시작 좌표
        public int m_PosX = 5;
        public int m_PosY = 0;
        //테트리스 판
        public int[,] TetrisBoard = new int[22, 12]// 내부판크기 10x20
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
                };//테트리스 판

        //도형만들기
        /*public int(,,,) TetrisBlock = new int(7, 4, 4, 4);
        {
            { //I Block
                { 
                    {1,1,1,1 },
                    { 0,0,0,0},
                    {0,0,0,0 },
                    {0,0,0,0 }
                },
                
                { 
                    { 0,1,0,0},
                    { 0,1,0,0},
                    {0,1,0,0 },
                    {0,1,0,0 }
                },
            
                { 
                    {1,1,1,1 },
                    { 0,0,0,0},
                    {0,0,0,0 },
                    {0,0,0,0 }
                },
                
                {
                    { 0,1,0,0},
                    { 0,1,0,0},
                    {0,1,0,0 },
                    {0,1,0,0 }
                }
            },
            
            { //O Block
                { 
                    {1,1,1,1 },
                    { 0,0,0,0},
                    {0,0,0,0 },
                    {0,0,0,0 }
                },
                
                { 
                    { 0,1,0,0},
                    { 0,1,0,0},
                    {0,1,0,0 },
                    {0,1,0,0 }
                },
            
                { 
                    {1,1,1,1 },
                    { 0,0,0,0},
                    {0,0,0,0 },
                    {0,0,0,0 }
                },
                
                {
                    { 0,1,0,0},
                    { 0,1,0,0},
                    {0,1,0,0 },
                    {0,1,0,0 }
                }
            },
            
            { //Z Block
                { 
                    {1,1,1,1 },
                    { 0,0,0,0},
                    {0,0,0,0 },
                    {0,0,0,0 }
                },
                
                { 
                    { 0,1,0,0},
                    { 0,1,0,0},
                    {0,1,0,0 },
                    {0,1,0,0 }
                },
            
                { 
                    {1,1,1,1 },
                    { 0,0,0,0},
                    {0,0,0,0 },
                    {0,0,0,0 }
                },
                
                {
                    { 0,1,0,0},
                    { 0,1,0,0},
                    {0,1,0,0 },
                    {0,1,0,0 }
                }
            },
            
            { //S Block
               { 
                    {1,1,1,1 },
                    { 0,0,0,0},
                    {0,0,0,0 },
                    {0,0,0,0 }
                },
                
                { 
                    { 0,1,0,0},
                    { 0,1,0,0},
                    {0,1,0,0 },
                    {0,1,0,0 }
                },
            
                { 
                    {1,1,1,1 },
                    { 0,0,0,0},
                    {0,0,0,0 },
                    {0,0,0,0 }
                },
                
                {
                    { 0,1,0,0},
                    { 0,1,0,0},
                    {0,1,0,0 },
                    {0,1,0,0 }
                }
            },
            
            { //J Block
                { 
                    {1,1,1,1 },
                    { 0,0,0,0},
                    {0,0,0,0 },
                    {0,0,0,0 }
                },
                
                { 
                    { 0,1,0,0},
                    { 0,1,0,0}, 
                    {0,1,0,0 },
                    {0,1,0,0 }
                },
            
                { 
                    {1,1,1,1 },
                    { 0,0,0,0},
                    {0,0,0,0 },
                    {0,0,0,0 }
                },
                
                {
                    { 0,1,0,0},
                    { 0,1,0,0},
                    {0,1,0,0 },
                    {0,1,0,0 }
                }
            },
            
            { //L Block
                { 
                    {1,1,1,1 },
                    { 0,0,0,0},
                    {0,0,0,0 },
                    {0,0,0,0 }
                },
                
                { 
                    { 0,1,0,0},
                    { 0,1,0,0},
                    {0,1,0,0 },
                    {0,1,0,0 }
                },
            
                { 
                    {1,1,1,1 },
                    { 0,0,0,0},
                    {0,0,0,0 },
                    {0,0,0,0 }
                },
                
                {
                    { 0,1,0,0},
                    { 0,1,0,0},
                    {0,1,0,0 },
                    {0,1,0,0 }
                }
            },
            
            { //T Block
                { 
                    {1,1,1,1 },
                    { 0,0,0,0},
                    {0,0,0,0 },
                    {0,0,0,0 }
                },
                
                { 
                    { 0,1,0,0},
                    { 0,1,0,0},
                    {0,1,0,0 },
                    {0,1,0,0 }
                },
            
                { 
                    {1,1,1,1 },
                    { 0,0,0,0},
                    {0,0,0,0 },
                    {0,0,0,0 }
                },
                
                {
                    { 0,1,0,0},
                    { 0,1,0,0},
                    {0,1,0,0 },
                    {0,1,0,0 }
                }
            }
        };*/

        private int count;
        public int Count //체크카운트
        {
            get { return count; }
            set { count = value; }
        }
        private int Deletecount;
        public int DeleteCount //삭제된 줄수 카운트
        {
            get { return Deletecount; }
            set { Deletecount = value; }
        }
        void DrawShip()//도형 내리기 더이상 못가면 저장.
        {
            Console.SetCursorPosition(m_PosX*2, m_PosY);
            Console.Write("□");
            if(TetrisBoard[m_PosY, m_PosX] != 2 || TetrisBoard[m_PosY, m_PosX] != 3)//블럭이 없으면 아래로 진행
            m_PosY++;
            if(TetrisBoard[m_PosY, m_PosX] ==2|| TetrisBoard[m_PosY, m_PosX] == 3)//블럭이 있으면 블럭저장
            {
                TetrisBoard[m_PosY-1, m_PosX] =2;
                m_PosX = 5;
                m_PosY = 0;
            }
        }
        void Keybind()//키입력 받기
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo info = Console.ReadKey();
                if (info.Key == ConsoleKey.D || info.Key == ConsoleKey.RightArrow)
                {
                    m_PosX++;
                    if (TetrisBoard[m_PosY, m_PosX] == 2)//이동방향에 블럭이있으면 진행x
                        m_PosX--;

                    if (m_PosX > 10)//맵안에서 활동
                        m_PosX = 10;
                }
                if (info.Key == ConsoleKey.A || info.Key == ConsoleKey.LeftArrow)
                {
                    m_PosX--;
                    if (TetrisBoard[m_PosY, m_PosX] == 2)//이동방향에 블럭이있으면 진행x
                        m_PosX++;

                    if (m_PosX < 1)//맵안에서 활동
                        m_PosX = 1;
                }
                if (info.Key == ConsoleKey.Escape)//esc입력받으면 종료
                {
                    Environment.Exit(0);
                }

            }
        }

        public void Draw()//그리기
        {
            Keybind();
            BGDraw();
            DrawShip();
            DeleteLine();
            GameOver();
            GameInfo();
        }
        
        void GameInfo()//게임 정보 출력
        {
            Console.SetCursorPosition(30, 3);
            Console.WriteLine("게임정보");
            Console.SetCursorPosition(30, 4);
            Console.WriteLine("ESC:게임종료");
            Console.SetCursorPosition(30, 10);
            Console.WriteLine("삭제된 줄의수 :" + DeleteCount);
        }



        void BGDraw()//배경그리기
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

        void DownLine(int Line)//줄내리기
        {
            for(int i=Line;i>1;i--)
            {
                for(int j=1;j<T_Width;j++)
                {
                    TetrisBoard[i, j] = TetrisBoard[i - 1, j];
                }
            }
            DeleteCount++;
        }

        void DeleteLine()//다채워진줄 삭제
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
                            DownLine(i);
                            Count = 0;
                        }
                    }
                }
                Count = 0;
            }
        }
        void GameOver()//맨위에 블럭이생기면 게임오버
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
            //게임진행
            Game Tetris = new Game();
            while (true)
            {
                Thread.Sleep(100);
                Console.Clear();
                Tetris.Draw();
            }
        }
    }
}
