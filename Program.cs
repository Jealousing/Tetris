using System;
using System.Threading;
/*
Console 테트리스 
2020-05-13 -> 2020-05-
GitHub 주소: https://github.com/Jealousing/Tetris
테트리스 게임시작

1.도형랜덤선택
도형 하강
맨밑줄부터 다채워져있는지 확인 -> 채워져있으면 그줄 삭제 -> 밑으로댕기기

1번 반복
도형이 더이상 못내려가면 게임오버

도형 모양 7개, 방향 4개 = 28가지
판 크기 10x20

    현재 수정해야되는거 : 블럭통과현상, 다른도형적용 , 

*/


namespace Tetris_SeoDongju
{
    class Game
    {
        //테트리스 판의 가로
        protected int T_Width = 16;
        //테트리스 판의 높이
        protected int T_Helight = 22;
        //시작 좌표
        public int m_PosX = 8;
        public int m_PosY = 0;
        //게임속도
        public int speed = 300;
        //
        public int pick=0, way=0;
        public int onoff = 1;
        //테트리스 판
        public int[,] TetrisBoard = new int[22, 16]// 내부판크기 10x20
                {
                    {0,0,4,4,4,4,4,4,4,4,4,4,4,4,0,0},
                    {0,0,3,0,0,0,0,0,0,0,0,0,0,3,0,0},
                    {0,0,3,0,0,0,0,0,0,0,0,0,0,3,0,0},
                    {0,0,3,0,0,0,0,0,0,0,0,0,0,3,0,0},
                    {0,0,3,0,0,0,0,0,0,0,0,0,0,3,0,0},
                    {0,0,3,0,0,0,0,0,0,0,0,0,0,3,0,0},
                    {0,0,3,0,0,0,0,0,0,0,0,0,0,3,0,0},
                    {0,0,3,0,0,0,0,0,0,0,0,0,0,3,0,0},
                    {0,0,3,0,0,0,0,0,0,0,0,0,0,3,0,0},
                    {0,0,3,0,0,0,0,0,0,0,0,0,0,3,0,0},
                    {0,0,3,0,0,0,0,0,0,0,0,0,0,3,0,0},
                    {0,0,3,0,0,0,0,0,0,0,0,0,0,3,0,0},
                    {0,0,3,0,0,0,0,0,0,0,0,0,0,3,0,0},
                    {0,0,3,0,0,0,0,0,0,0,0,0,0,3,0,0},
                    {0,0,3,0,0,0,0,0,0,0,0,0,0,3,0,0},
                    {0,0,3,0,0,0,0,0,0,0,0,0,0,3,0,0},
                    {0,0,3,0,0,0,0,0,0,0,0,0,0,3,0,0},
                    {0,0,3,0,0,0,0,0,0,0,0,0,0,3,0,0},
                    {0,0,3,0,0,0,0,0,0,0,0,0,0,3,0,0},
                    {0,0,3,0,0,0,0,0,0,0,0,0,0,3,0,0},
                    {0,0,3,0,0,0,0,0,0,0,0,0,0,3,0,0},
                    {0,0,3,3,3,3,3,3,3,3,3,3,3,3,0,0}
                };//테트리스 판

        //도형만들기
        static public int[,,,] TetrisBlock = new int[7, 4, 4, 4]
        {
            {//I Block
                {
                    {0,1,0,0 },
                    {0,1,0,0 },
                    {0,1,0,0 },
                    {0,1,0,0 }
                },
                {
                    {0,0,0,0 },
                    {0,0,0,0 },
                    {1,1,1,1 },
                    {0,0,0,0 }
                },
                {
                    {0,1,0,0 },
                    {0,1,0,0 },
                    {0,1,0,0 },
                    {0,1,0,0 }
                },
                {
                    {0,0,0,0 },
                    {0,0,0,0 },
                    {1,1,1,1 },
                    {0,0,0,0 }
                }
            },

            {//O Block
                {
                    {0,1,1,0 },
                    {0,1,1,0 },
                    {0,0,0,0 },
                    {0,0,0,0 }
                },
                {
                    {0,1,1,0 },
                    {0,1,1,0 },
                    {0,0,0,0 },
                    {0,0,0,0 }
                },
                {
                    {0,1,1,0 },
                    {0,1,1,0 },
                    {0,0,0,0 },
                    {0,0,0,0 }
                },
                {
                    {0,1,1,0 },
                    {0,1,1,0 },
                    {0,0,0,0 },
                    {0,0,0,0 }
                }
            },

            {//Z Block
                {
                    {0,1,0,0 },
                    {1,1,0,0 },
                    {1,0,0,0 },
                    {0,0,0,0 }
                },
                {
                    {1,1,0,0 },
                    {0,1,1,0 },
                    {0,0,0,0 },
                    {0,0,0,0 }
                },
                {
                    {0,1,0,0 },
                    {1,1,0,0 },
                    {1,0,0,0 },
                    {0,0,0,0 }
                },
                {
                    {1,1,0,0 },
                    {0,1,1,0 },
                    {0,0,0,0 },
                    {0,0,0,0 }
                }
            },
            {//S Block
                {
                    {1,0,0,0 },
                    {1,1,0,0 },
                    {0,1,0,0 },
                    {0,0,0,0 }
                },
                {
                    {0,1,1,0 },
                    {1,1,0,0 },
                    {0,0,0,0 },
                    {0,0,0,0 }
                },
                {
                    {1,0,0,0 },
                    {1,1,0,0 },
                    {0,1,0,0 },
                    {0,0,0,0 }
                },
                {
                    {0,1,1,0 },
                    {1,1,0,0 },
                    {0,0,0,0 },
                    {0,0,0,0 }
                }
            },

            {//J Block
                {
                    {1,0,0,0 },
                    {1,1,1,0 },
                    {0,0,0,0 },
                    {0,0,0,0 }
                },
                {
                    {1,1,0,0 },
                    {1,0,0,0 },
                    {1,0,0,0 },
                    {0,0,0,0 }
                },
                {
                    {1,1,1,0 },
                    {0,0,1,0 },
                    {0,0,0,0 },
                    {0,0,0,0 }
                },
                {
                    {0,0,1,0 },
                    {0,0,1,0 },
                    {0,1,1,0 },
                    {0,0,0,0 }
                }
            },

            {//L Block
                {
                    {0,0,1,0 },
                    {1,1,1,0 },
                    {0,0,0,0 },
                    {0,0,0,0 }
                },
                {
                    {1,0,0,0 },
                    {1,0,0,0 },
                    {1,1,0,0 },
                    {0,0,0,0 }
                },
                {
                    {1,1,1,0 },
                    {1,0,0,0 },
                    {0,0,0,0 },
                    {0,0,0,0 }
                },
                {
                    {0,1,1,0 },
                    {0,0,1,0 },
                    {0,0,1,0 },
                    {0,0,0,0 }
                }
            },

            {//T Block
                {
                    {0,0,00,0 },
                    {1,1,1,0 },
                    {0,1,0,0 },
                    {0,0,0,0 }
                },
                {
                    {0,1,0,0 },
                    {1,1,0,0 },
                    {0,1,0,0 },
                    {0,0,0,0 }
                },
                {
                    {0,1,0,0 },
                    {1,1,1,0 },
                    {0,0,0,0 },
                    {0,0,0,0 }
                },
                {
                    {0,1,0,0 },
                    {0,1,1,0 },
                    {0,1,0,0 },
                    {0,0,0,0 }
                }
            }

        };

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

        void BlockDraw(int blocktype, int way)
        {

            for (int i = 3; i >= 0; i--)
            {
                for (int j = 3; j >= 0; j--)
                {
                    if (TetrisBlock[blocktype, 0, i, j] == 0)
                    {
                        Console.SetCursorPosition((m_PosX+1 ) * 2 - (j * 2), m_PosY + i);
                        Console.Write("");
                    }
                    if (TetrisBlock[blocktype, 0, i, j] == 1)
                    {
                        if (TetrisBoard[m_PosY + i, m_PosX] == 2 || TetrisBoard[m_PosY + i, m_PosX] == 3)
                        {//밑에 좌표에 블럭이있으면 그대로 저장
                            for(int p=0;p<=j;p++)
                            {
                                for (int o = i; o >= 0; o--)
                                {
                                    if (m_PosY + o - 1 <= 0)//저장되는 좌표가 0보다 작아질경우 겜오버! 
                                    {
                                        TetrisBoard[0, m_PosX] = 2;
                                        Console.Clear();
                                        BGDraw();
                                        GameOver();
                                    }
                                    else
                                    {
                                        if (TetrisBlock[blocktype, 0,o,p]==1)
                                        TetrisBoard[m_PosY + o - 1, m_PosX+p-1 ] = 2;
                                    }

                                }
                            }
                           
                            onoff = 1;
                            if (onoff == 1)
                            {
                                pick = PickBlock();
                            }
                            m_PosX = 8;
                            m_PosY = 0;
                        }
                        else
                        {
                            Console.SetCursorPosition((m_PosX+1+ blocktype) * 2 - (j * 2), m_PosY + i);
                            Console.Write("□");
                        }
                    }
                }
                Console.WriteLine("");
            }
        }

        int PickBlock()
        {
                onoff = 0;
                Random num = new Random();
                int pickblock = num.Next(0, 2);
                return pickblock;
        }
        int PickWay()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo info = Console.ReadKey();
                if (info.Key == ConsoleKey.Z)
                {
                    way++;
                    if (way == 4)
                        way = 0;
                    return way;
                }
                else return way;
            }
            return 0;
        }
        void DrawShip()//도형 내리기 더이상 못가면 저장.
        {
                way = PickWay();
            Console.SetCursorPosition(m_PosX * 2, m_PosY);
            BlockDraw(pick,way);//Console.Write("□");
            if (TetrisBoard[m_PosY, m_PosX] != 2 || TetrisBoard[m_PosY, m_PosX] != 3)//블럭이 없으면 아래로 진행
                m_PosY++;
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

                    if (m_PosX > 12)//맵안에서 활동
                        m_PosX = 12;
                }
                if (info.Key == ConsoleKey.A || info.Key == ConsoleKey.LeftArrow)
                {
                    m_PosX--;
                    if (TetrisBoard[m_PosY, m_PosX] == 2)//이동방향에 블럭이있으면 진행x
                        m_PosX++;

                    if (m_PosX < 3)//맵안에서 활동
                        m_PosX = 3;
                }
                if(info.Key==ConsoleKey.Subtract)
                {
                    speed -= 50;
                }
                if (info.Key == ConsoleKey.Add)
                {
                    speed += 50;
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
            Console.SetCursorPosition(30, 8);
            Console.WriteLine("게임속도1000=1초 :" + speed);
            Console.SetCursorPosition(30, 12);
            Console.WriteLine("onoff 확인 :" + onoff);
            Console.SetCursorPosition(30, 14);
            Console.WriteLine("block 확인 :" + PickBlock());
        }



        void BGDraw()//배경그리기
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < T_Helight; i++)
            {
                for (int j = 0; j < T_Width; j++)
                {
                    if (TetrisBoard[i, j] == 0)
                        Console.Write("  ");
                    if (TetrisBoard[i, j] == 3)
                        Console.Write("■");
                    if (TetrisBoard[i, j] == 4)
                        Console.Write("■");
                    if (TetrisBoard[i, j] == 2)
                        Console.Write("□");
                }
                Console.WriteLine();
            }
        }

        void DownLine(int Line)//줄내리기
        {
            for (int i = Line; i > 1; i--)
            {
                for (int j = 1; j < T_Width; j++)
                {
                    TetrisBoard[i, j] = TetrisBoard[i - 1, j];
                }
            }
            DeleteCount++;
        }

        void DeleteLine()//다채워진줄 삭제
        {
            for (int i = 0; i < T_Helight; i++)
            {
                for (int j = 0; j < T_Width; j++)
                {
                    if (TetrisBoard[i, j] == 2)
                    {
                        Count++;
                        if (Count == 10)
                        {
                            for (int o = 1; o < T_Width - 1; o++)
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
            for (int i = 0; i < T_Width; i++)
            {
                if (2 == TetrisBoard[0, i])
                {
                    for(int j=0; j<11;j++)
                    {
                        Console.SetCursorPosition(30, j+1);
                        Console.WriteLine("게임패배");
                    }
                    Console.SetCursorPosition(50, 22);
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
                Thread.Sleep(Tetris.speed);
                Console.Clear();
                Tetris.Draw();
            }
        }
    }
}
