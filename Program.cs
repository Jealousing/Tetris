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
        protected int T_Helight = 23;
        //중심 좌표
        int centerX = 6;
        //시작 좌표
        public int m_PosX = 0;
        public int m_PosY = 0;
        //게임속도
        public int speed = 150;
        bool MaxSpeed = false;
        //블럭모양저장
        int[] nextBlock = new int[3]
        { 0,0,0 };
        public int onoff = 1;
        //테트리스 판
        public int[,] TetrisBoard = new int[23, 16]// 내부판크기 10x20
                {
                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                    {0,0,3,3,3,3,3,3,3,3,3,3,3,3,0,0},
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
                    {0,0,0,0 },
                    {1,1,1,1 } 
                    
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
                    {0,0,0,0 },
                    {1,1,1,1 }
                    
                }
            },

            {//O Block
                {
                    {0,0,0,0 },
                    {0,0,0,0 },
                    {0,1,1,0 },
                    {0,1,1,0 }

                },
                {
                    {0,0,0,0 },
                    {0,0,0,0 },
                    {0,1,1,0 },
                    {0,1,1,0 }
                },
                {
                    {0,0,0,0 },
                    {0,0,0,0 },
                    {0,1,1,0 },
                    {0,1,1,0 }
                },
                {
                    {0,0,0,0 },
                    {0,0,0,0 },
                    {0,1,1,0 },
                    {0,1,1,0 }
                }
            },

            {//Z Block
                {
                    {0,0,0,0 },
                    {0,1,0,0 },
                    {1,1,0,0 },
                    {1,0,0,0 }
                },
                {
                    {0,0,0,0 },
                    {0,0,0,0 },
                    {1,1,0,0 },
                    {0,1,1,0 }
                },
                {
                    {0,0,0,0 },
                    {0,1,0,0 },
                    {1,1,0,0 },
                    {1,0,0,0 }
                },
                {
                    {0,0,0,0 },
                    {0,0,0,0 },
                    {1,1,0,0 },
                    {0,1,1,0 }
                }
            },
            {//S Block
                {
                    {0,0,0,0 },
                    {1,0,0,0 },
                    {1,1,0,0 },
                    {0,1,0,0 }
                },
                {
                    {0,0,0,0 },
                    {0,0,0,0 },
                    {0,1,1,0 },
                    {1,1,0,0 }
                },
                {
                    {0,0,0,0 },
                    {1,0,0,0 },
                    {1,1,0,0 },
                    {0,1,0,0 }
                },
                {
                    {0,0,0,0 },
                    {0,0,0,0 },
                    {0,1,1,0 },
                    {1,1,0,0 }
                }
            },

            {//J Block
                {
                    {0,0,0,0 },
                    {0,0,0,0 },
                    {1,0,0,0 },
                    {1,1,1,0 }
                },
                {
                    {0,0,0,0 },
                    {1,1,0,0 },
                    {1,0,0,0 },
                    {1,0,0,0 }
                },
                {
                    {0,0,0,0 },
                    {0,0,0,0 },
                    {1,1,1,0 },
                    {0,0,1,0 }
                },
                {
                    {0,0,0,0 },
                    {0,0,1,0 },
                    {0,0,1,0 },
                    {0,1,1,0 }
                }
            },

            {//L Block
                {
                    {0,0,0,0 },
                    {0,0,0,0 },
                    {0,0,1,0 },
                    {1,1,1,0 }
                },
                {
                    {0,0,0,0 },
                    {1,0,0,0 },
                    {1,0,0,0 },
                    {1,1,0,0 }
                },
                {
                    {0,0,0,0 },
                    {0,0,0,0 },
                    {1,1,1,0 },
                    {1,0,0,0 }

                },
                {
                    {0,0,0,0 },
                    {0,1,1,0 },
                    {0,0,1,0 },
                    {0,0,1,0 }
                }
            },

            {//T Block
                {
                    {0,0,0,0 },
                    {0,0,0,0 },
                    {1,1,1,0 },
                    {0,1,0,0 }
                },
                {
                    {0,0,0,0 },
                    {0,0,1,0 },
                    {0,1,1,0 },
                    {0,0,1,0 }
                },
                {
                    {0,0,0,0 },
                    {0,0,0,0 },
                    {0,1,0,0 },
                    {1,1,1,0 }
                },
                {
                    {0,0,0,0 },
                    {0,1,0,0 },
                    {0,1,1,0 },
                    {0,1,0,0 }
                }
            }

        };

        private int count;
        public int Count //체크카운트
        {
            get { return count; }
            set { count = value; }
        }
        private int count2;
        public int Count2 //체크카운트
        {
            get { return count2; }
            set { count2 = value; }
        }
        private int Deletecount;
        public int DeleteCount //삭제된 줄수 카운트
        {
            get { return Deletecount; }
            set { Deletecount = value; }
        }

        private int way;
        public int Way
        {
            get { return way; }
            set { way = value; }
        }


        void BlockDraw(int blocktype, int way)
        {
            
            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    if (TetrisBlock[blocktype, way, i, j] == 1)
                    {
                        TetrisBoard[m_PosY + i, m_PosX + centerX + j] = 1;
                    }
                }
            }
            TopReDraw();
        }
        void BlockDown(int blocktype, int way)
        {
            
            if (m_PosY <= 17)
            {
                BlockDelete(blocktype, way);
                m_PosY++;
            }
            BlockDraw(blocktype, way);
            BlockCheck(blocktype, way);
            if (m_PosY==18)
            {
                BlockSave(blocktype, way);
                ResetVariable();
            }
        }

        void ResetVariable()
        {
            onoff = 1;
            nextBlock[0] = nextBlock[1];
            m_PosY = 0;
            m_PosX = 0;
            Count2 = 0;
            way = 0;
            
        }
        void TopReDraw()
        {
            for (int i = 2; i < T_Width - 2; i++)
            {
                TetrisBoard[1, i] = 3;
            }
        }

        void BlockDelete(int blocktype, int way)
        {
            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    if (TetrisBlock[blocktype, way, i, j] == 1)
                    {
                        TetrisBoard[m_PosY + i, m_PosX + centerX + j] = 0;

                    }

                }
            }
        }

        void BlockSave(int blocktype, int way)
        {
            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    if (TetrisBlock[blocktype, way, i, j] == 1)
                    {
                        TetrisBoard[m_PosY + i, m_PosX + centerX + j] = 2;

                    }

                }
            }
        }

        void BlockCheck(int blocktype, int way)
        {
            for (int i=0;i<T_Helight;i++)
            {
                for(int j=0;j<T_Width;j++)
                {
                    if (TetrisBoard[i, j] == 1 &&TetrisBoard[i+1, j] == 2)
                    {
                        BlockSave(blocktype, way);
                        ResetVariable();
                    }
                }
            }
            
        }
        int BlockCheckX(int blocktype, int way,int x)
        {
            for (int j = 0; j < T_Width; j++)
            {
                for (int i = 0; i < T_Helight; i++)
                {
                    if (TetrisBoard[i, j] == 1 && TetrisBoard[i , j+x] == 2)
                    {
                        return m_PosX += x;
                    }
                }
            }
            return m_PosY;

        }



        int PickBlock()
        {
            onoff = 0;
            Random num = new Random();
            int pickblock = num.Next(0, 7);
            return pickblock;
        }

       

        void Keybind(int blocktype, int way)//키입력 받기
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo info = Console.ReadKey();
                if (info.Key == ConsoleKey.Z)
                {
                    BlockDelete(blocktype, way);
                    Way++;
                    if (Way >= 4)
                    {
                        Way = 0;
                    }
                    BGDraw();

                }
                if (info.Key == ConsoleKey.X)
                {
                    BlockDelete(blocktype, way);
                    Way--;
                    if (Way <= 0)
                    {
                        Way = 3;
                    }
                    BGDraw();

                }
                if (info.Key == ConsoleKey.Spacebar)
                {
                    if (MaxSpeed == true)
                    {
                        MaxSpeed = false;
                        speed = 150;
                    }
                    else if (MaxSpeed == false)
                    {
                        MaxSpeed = true;
                        speed = 1;
                    }
                }
                
                if (info.Key == ConsoleKey.D || info.Key == ConsoleKey.RightArrow)
                {
                    BlockDelete(blocktype, way);
                      
                    m_PosX++;
                    BlockCheckX(blocktype, way, +1);
                    if (blocktype == 0)
                    {
                        switch (way)
                        {
                            case 0:
                            case 2:
                                if (m_PosX > -1 + centerX)//맵안에서 활동
                                    m_PosX = 5;
                                break;
                            case 1:
                            case 3:
                                if (m_PosX > -3 + centerX)//맵안에서 활동
                                    m_PosX = 3;
                                break;
                        }
                    }
                    else if (blocktype == 1)
                    {
                        switch (way)
                        {
                            case 0:
                            case 1:
                            case 2:
                            case 3:
                                if (m_PosX > -2 + centerX)//맵안에서 활동
                                    m_PosX = 4;
                                break;
                            
                        }
                    }
                    else if (blocktype == 2)
                    {
                        switch (way)
                        {
                            case 0:
                            case 2:
                                if (m_PosX > -1 + centerX)//맵안에서 활동
                                    m_PosX = 5;
                                break;
                            case 1:
                            case 3:
                                if (m_PosX > -2 + centerX)//맵안에서 활동
                                    m_PosX = 4;
                                break;
                        }
                    }
                    else if (blocktype == 3)
                    {
                        switch (way)
                        {
                            case 0:
                            case 2:
                                if (m_PosX > -1 + centerX)//맵안에서 활동
                                    m_PosX = 5;
                                break;
                            case 1:
                            case 3:
                                if (m_PosX > -2 + centerX)//맵안에서 활동
                                    m_PosX = 4;
                                break;
                        }
                    }
                    else if (blocktype == 4)
                    {
                        switch (way)
                        {
                            case 0:
                            case 2:
                            case 3:
                                if (m_PosX > -2 + centerX)//맵안에서 활동
                                    m_PosX = 4;
                                break;
                            case 1:
                                if (m_PosX > -1 + centerX)//맵안에서 활동
                                    m_PosX = 5;
                                break;
                            
                        }
                    }
                    else if (blocktype == 5)
                    {
                        switch (way)
                        {
                            case 0:
                            case 2:
                            case 3:
                                if (m_PosX > -2 + centerX)//맵안에서 활동
                                    m_PosX = 4;
                                break;
                            case 1:
                                if (m_PosX > -1 + centerX)//맵안에서 활동
                                    m_PosX = 5;
                                break;
                        }
                    }
                    else if (blocktype == 6)
                    {
                        switch (way)
                        {
                            case 0:
                            case 1:
                            case 2:
                            case 3:
                                if (m_PosX > -2 + centerX)//맵안에서 활동
                                    m_PosX = 4;
                                break;
                        }
                    }
                    BlockDraw(blocktype, way);
                }
                if (info.Key == ConsoleKey.A || info.Key == ConsoleKey.LeftArrow)
                {
                    BlockDelete(blocktype, way);
                    m_PosX--;
                    BlockCheckX(blocktype, way, -1);

                    if (blocktype == 0)
                    {
                        switch(way)
                        {
                            case 0:
                            case 2:
                                if (m_PosX < 2 - centerX)//맵안에서 활동
                                    m_PosX = -4;
                                break;
                            case 1:
                            case 3:
                                if (m_PosX < 3- centerX)//맵안에서 활동
                                    m_PosX = -3;
                                break;
                        }
                    }
                    else if (blocktype == 1)
                    {
                        switch (way)
                        {
                            case 0:
                            case 1:
                            case 2:
                            case 3:
                                if (m_PosX < 2- centerX)//맵안에서 활동
                                    m_PosX = -4;
                                break;
                        }
                    }
                    else if (blocktype == 2)
                    {
                        switch (way)
                        {
                            case 0:
                            case 1:
                            case 2:
                            case 3:
                                if (m_PosX < 3 - centerX)//맵안에서 활동
                                    m_PosX = -3;
                                break;
                        }
                    }
                    else if (blocktype == 3)
                    {
                        switch (way)
                        {
                            case 0:
                            case 1:
                            case 2:
                            case 3:
                                if (m_PosX < 3 - centerX)//맵안에서 활동
                                    m_PosX = -3;
                                break;
                        }
                    }
                    else if (blocktype == 4)
                    {
                        switch (way)
                        {
                            case 0:
                            case 1:
                            case 2:
                                if (m_PosX < 4 - centerX)//맵안에서 활동
                                    m_PosX = -3;
                                break;
                            case 3:
                                if (m_PosX < 2 - centerX)//맵안에서 활동
                                    m_PosX = -4;
                                break;
                        }
                    }
                    else if (blocktype == 5)
                    {
                        switch (way)
                        {
                            case 0:
                            case 1:
                            case 2:
                                if (m_PosX < 3 - centerX)//맵안에서 활동
                                    m_PosX = -3;
                                break;
                            case 3:
                                if (m_PosX < 2 - centerX)//맵안에서 활동
                                    m_PosX = -4;
                                break;
                        }
                    }
                    else if (blocktype == 6)
                    {
                        switch (way)
                        {
                            case 0:
                            case 2:
                                if (m_PosX < 3 - centerX)//맵안에서 활동
                                    m_PosX = -3;
                                break;
                            case 1:
                            case 3:
                                if (m_PosX < 2 - centerX)//맵안에서 활동
                                    m_PosX = -4;
                                break;
                        }
                    }
                    BlockDraw(blocktype, way);

                }

                if (info.Key == ConsoleKey.Subtract)
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
            Keybind(nextBlock[0], Way);
            BGDraw();
            nextBlock[1] = PickBlock();
            BlockDown(nextBlock[0], Way);
            DeleteLine();
            GameOver();
            GameInfo();
            BGDraw();
        }

        void GameInfo()//게임 정보 출력
        {
            Console.SetCursorPosition(30, 1);
            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
            Console.SetCursorPosition(30, 2);
            Console.WriteLine("      ■    ■            ■    ■     ■   ■  ■  ");
            Console.SetCursorPosition(30, 3);
            Console.WriteLine("      ■    ■■■■■    ■    ■■■■    ■   ■■■■");
            Console.SetCursorPosition(30, 4);
            Console.WriteLine("      ■    ■            ■    ■    ■    ■          ■");
            Console.SetCursorPosition(30, 5);
            Console.WriteLine("      ■    ■■■■■    ■    ■     ■ ■■■ ■■■■");
            Console.SetCursorPosition(30, 8);
            Console.WriteLine("ESC:게임종료");
            Console.SetCursorPosition(30, 10);
            Console.WriteLine("삭제된 줄의수 :" + DeleteCount);
            Console.SetCursorPosition(30, 8);
            Console.WriteLine("게임속도1000=1초 :" + speed);
            Console.SetCursorPosition(30, 12);
            Console.WriteLine("onoff 확인 :" + onoff);
            Console.SetCursorPosition(30, 14);
            Console.WriteLine("block 확인 :" + PickBlock());
            Console.SetCursorPosition(30, 16);
            Console.Write("y좌표 x좌표 :" + m_PosY);
            Console.WriteLine(" " + m_PosX + centerX);
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
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("■");
                    }
                        
                    if (TetrisBoard[i, j] == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("■");
                    }
                        
                    if (TetrisBoard[i, j] == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("■");
                    }
                        
                }
                Console.WriteLine();
            }
        }

        void DownLine(int Line)//줄내리기
        {
            for (int i = Line; i > 2; i--)
            {
                for (int j = 1; j < T_Width-1; j++)
                {
                    TetrisBoard[i, j] = TetrisBoard[i - 1, j];
                }
            }
            DeleteCount++;
        }

        void DeleteLine()//다채워진줄 삭제
        {
            for (int i = 0; i < T_Helight ; i++)
            {
                for (int j = 0; j < T_Width; j++)
                {
                    if (TetrisBoard[i, j] == 2)
                    {
                        Count++;
                        if (Count == 10)
                        {
                            for (int o = 1; o < T_Width-1; o++)
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
                if (2 == TetrisBoard[1, i]|| 1 == TetrisBoard[1, i])
                {
                    for (int j = 0; j < 11; j++)
                    {
                        Console.SetCursorPosition(30, j + 1);
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
            Console.BackgroundColor = ConsoleColor.White;
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
