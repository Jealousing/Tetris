using System;
using System.Threading;

/*
Console 테트리스 
2020-05-13 -> 2020-05-16
GitHub 주소: https://github.com/Jealousing/Tetris
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
        //최고속도유지 변수
        bool MaxSpeed = false;
        //홀드
        bool Hold=false;
        //블럭모양저장
        public int[] nextBlock = new int[4]
        { 0,0,0,0 };
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

        //도형만들기                              7가지모양,4개의방향 4x4크기의 도형배열
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

        private int count = 1;
        public int Count //체크카운트
        {
            get { return count; }
            set { count = value; }
        }
        private int blockcount = 0;//지금까지 사용한 블럭 갯수
        public int BlockCount
        {
            get { return blockcount; }
            set { blockcount = value; }
        }
        
        private int Deletecount;
        public int DeleteCount //삭제된 줄수 카운트
        {
            get { return Deletecount; }
            set { Deletecount = value; }
        }

        private int way;
        public int Way//방향
        {
            get { return way; }
            set { way = value; }
        }


        void BlockDraw(int blocktype, int way)//블럭그리기
        {
            
            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    if (TetrisBlock[blocktype, way, i, j] == 1)
                    {
                        //좌표값을 더해 실시간 위치를 그려준다.
                        TetrisBoard[m_PosY + i, m_PosX + centerX + j] = 1;
                    }
                }
            }
            TopReDraw();// 블럭이 생성되면서 사라지는 블럭복구
        }
        void BlockDown(int blocktype, int way) //블럭을 내려준다.
        {
            if (m_PosY <= 17)//바닥 전 까지 진행.
            {
                BlockDelete(blocktype, way);//내려가면서 새로 그려주기위해 삭제.
                if(Hold==false)
                m_PosY++;//Y 좌표값 증가.
            }
            BlockDraw(blocktype, way); //다시 그려준다.
            BlockCheck(blocktype, way); //다시 그리면서 불럭이 충돌하는지 체크.
            if (m_PosY==18) //바닥에 닿았을경우
            {
                BlockSave(blocktype, way);//블럭을 저장한다.
                ResetVariable();//저장 이후 초기값 설정 및 다음 블럭 지정. 
            }
        }

        void ResetVariable()// 저장 이후 초기값 설정 및 다음 블럭 지정. 
        {
            
            nextBlock[0] = nextBlock[1]; //다음블럭을 현재블럭으로 가져온다.
            nextBlock[1] = nextBlock[2]; //랜덤으로 정해지고있는 블럭에서 다음블럭으로 지정
            m_PosY = 0;//y좌표 초기화
            m_PosX = 0;//x좌표 초기화
            way = 0; //방향 초기화
            
        }
        void TopReDraw() //사라진 천장복구...
        {
            for (int i = 2; i < T_Width - 2; i++)
            {
                TetrisBoard[1, i] = 3;
            }
        }

        void BlockDelete(int blocktype, int way)//움직이는 도형의 이전 위치정보 삭제.
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

        void BlockSave(int blocktype, int way) //움직이는 도형의 블럭 정보 저장.
        {
            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    if (TetrisBlock[blocktype, way, i, j] == 1)//움직이는 도형의 블록을
                    {
                        TetrisBoard[m_PosY + i, m_PosX + centerX + j] = 2; //판에 블록으로 저장.
                    }
                }
            }
            onoff = 1;//저장할때 다음 블럭지정을 위한 변수 변경.
            BlockCount++;
        }

        void BlockCheck(int blocktype, int way)//블럭이 충돌이 일어나는지 확인Y좌표
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
        int BlockCheckX(int blocktype, int way,int x) //블럭이 충돌이 일어나는지 확인하는 함수2 X좌표
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
            return m_PosX;

        }



        void PickBlock()//블럭선택 랜덤으로 다음블럭을 정해준다.
        {
            Random num = new Random();//랜덤메서드
            int pickblock = num.Next(0, 7);
            nextBlock[2]= pickblock;
           
        }

       

        void Keybind(int blocktype, int way)//키입력 받기
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo info = Console.ReadKey();
                if (info.Key == ConsoleKey.Z)//Z키를 누르면 시계방향 회전
                {
                    BlockDelete(blocktype, way); //원래있던 도형 제거
                    Way++; //방향값 증가.
                    if (Way >= 4)//방향값 넘어가지않게 설정.
                    {
                        Way = 0;
                    }
                    BGDraw();//다시그리기

                }
                if (info.Key == ConsoleKey.X)//X키를 누르면 반시계방향 회전.
                {
                    BlockDelete(blocktype, way);//원래잇던 도형을 제거한다.
                    Way--;//방향값 제거.
                    if (Way <= 0) //방향값을 벗어나지않게 설정.
                    {
                        Way = 3;
                    }
                    BGDraw();//다시 그려주기.

                }
                if (info.Key == ConsoleKey.Spacebar||info.Key==ConsoleKey.DownArrow|| info.Key == ConsoleKey.S) //스페이스바 또는 밑 방향키 그리고 S를 누르면 진행
                {
                    if (MaxSpeed == true)//2번째 누르면 기본속도로 복귀.
                    {
                        MaxSpeed = false;
                        speed = 150;
                    }
                    else if (MaxSpeed == false)//처음 누른경우 최고속도로 진행.
                    {
                        MaxSpeed = true;
                        speed = 1;
                    }
                }
                
                if (info.Key == ConsoleKey.D || info.Key == ConsoleKey.RightArrow)//D키 또는 우측방향키로 우측으로 이동
                {
                    BlockDelete(blocktype, way);//원래 있던 그림을 삭제후
                      
                    m_PosX++; //좌표증가.
                    BlockCheckX(blocktype, way, +1);//충돌체크

                    //블럭 타입과 방향에 따른 블럭의 최소 최대좌표 설정.
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
                    BlockDraw(blocktype, way);// 그림다시그린다.
                }
                if (info.Key == ConsoleKey.A || info.Key == ConsoleKey.LeftArrow)//A키 또는 좌측방향키로 좌측으로 이동.
                {
                    BlockDelete(blocktype, way);//원래 있던 그림을 삭제후
                    m_PosX--;//좌표감소.
                    BlockCheckX(blocktype, way, -1);//충돌체크

                    //블럭 타입과 방향에 따른 블럭의 최소 최대좌표 설정.
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
                    BlockDraw(blocktype, way);//다시 이동된 그림을 그린다.

                }
                if (info.Key == ConsoleKey.H ) //멈추는 키
                {
                    if (Hold == true)
                    {
                        Hold = false;
                    }
                    else if (Hold == false)
                    {
                        Hold = true;
                    }
                }
                if (info.Key == ConsoleKey.Subtract|| info.Key == ConsoleKey.Q) //-키 또는 q키를 누르면 속도 감소
                {
                    speed -= 50;
                }
                if (info.Key == ConsoleKey.Add|| info.Key == ConsoleKey.W)//+키 또는 w키를 누르면 속도 증가
                {
                    speed += 50;
                }
                if (info.Key == ConsoleKey.Escape)//esc입력받으면 종료
                {
                    Environment.Exit(0);
                }
            }
        }

        public void Draw()//게임 진행!
        {
            PickBlock();//진행될 블럭의 모양을 선택
            Keybind(nextBlock[0], Way);//키보드입력받기
            BGDraw();//배경그림 그리기
            BlockDown(nextBlock[0], Way);//블럭이 아랫방향으로 진행
            DeleteLine();//한줄이 채워지면 제거
            GameOver();//게임오버
            GameInfo();//게임정보
            BGDraw();//다시그리기
        }

        void GameInfo()//게임 정보 출력
        {
            //설명
            Console.SetCursorPosition(32, 1);
            Console.WriteLine("  ■■■■■■■■■■■■■■■■■■■■■■■■■■■   제작시작일: 2020-05-13");
            Console.SetCursorPosition(32, 2);
            Console.WriteLine("      ■    ■            ■    ■     ■   ■  ■         최종수정일: 2020-05-16");
            Console.SetCursorPosition(32, 3);
            Console.WriteLine("      ■    ■■■■■    ■    ■■■■    ■   ■■■■  Github 주소: github.com/Jealousing/Tetris");
            Console.SetCursorPosition(32, 4);
            Console.WriteLine("      ■    ■            ■    ■    ■    ■          ■ ");
            Console.SetCursorPosition(32, 5);
            Console.WriteLine("      ■    ■■■■■    ■    ■     ■ ■■■ ■■■■  ");
            Console.SetCursorPosition(35, 8);
            Console.WriteLine("★★★★★: 좌로 이동[A,←]  우로 이동[D,→]  빠르게 내리기[SpaceBar,↓,S]");
            Console.SetCursorPosition(35, 10);
            Console.WriteLine("★단축키★: 속도 증가[+,q], 감소[-,w] 시계방향회전[Z] 반시계방향회전[X]");
            Console.SetCursorPosition(35, 12);
            Console.WriteLine("★★★★★: 종료 [ESC], 멈춤[H]");

            Console.SetCursorPosition(35, 15);
            Console.WriteLine("*Next Block*");
            Console.SetCursorPosition(35, 16);
            Console.WriteLine("*          *");
            Console.SetCursorPosition(35, 17);
            Console.WriteLine("*          *");
            Console.SetCursorPosition(35, 18);
            Console.WriteLine("*          *");
            Console.SetCursorPosition(35, 19);
            Console.WriteLine("*          *");
            Console.SetCursorPosition(35, 20);
            Console.WriteLine("*          *");
            Console.SetCursorPosition(35, 21);
            Console.WriteLine("************");

            for (int i=0;i<=3;i++)//다음블럭 출력
            {
                Console.SetCursorPosition(38, 16+i);
                for (int j=0;j<=3;j++)
                {//실질적으로 존재하는 블럭만 출력.
                    if(TetrisBlock[nextBlock[1], 0, i, j]==1)
                        Console.Write("■");
                    if (TetrisBlock[nextBlock[1], 0, i, j] == 0)
                        Console.Write("  ");
                }
                Console.WriteLine();
            }
            Console.SetCursorPosition(50, 16);
            Console.WriteLine("게임속도 :" + speed/10);//1000 = 1초
            Console.SetCursorPosition(50, 18);
            Console.WriteLine("삭제된 줄의수 :" + DeleteCount);
            Console.SetCursorPosition(50, 20);
            Console.WriteLine("Score :" + DeleteCount* BlockCount);//게임점수 = 삭제된 줄의수 * 지금까지 사용한 블럭갯수.
        }



        void BGDraw()//배경그리기
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < T_Helight; i++)
            {
                for (int j = 0; j < T_Width; j++)
                {
                    if (TetrisBoard[i, j] == 0)//빈공간
                        Console.Write("  ");
                    if (TetrisBoard[i, j] == 3)//벽
                    {
                        Console.ForegroundColor = ConsoleColor.Black;//검은색으로 설정
                        Console.Write("■");
                    }
                        
                    if (TetrisBoard[i, j] == 2)//저장된 블럭
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;//파란색으로 설정
                        Console.Write("■");
                    }
                        
                    if (TetrisBoard[i, j] == 1)//움직이는 블럭
                    {
                        Console.ForegroundColor = ConsoleColor.Red;//빨간색으로 설정
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
                    TetrisBoard[i, j] = TetrisBoard[i - 1, j];//쭈욱내린다
                }
            }
            DeleteCount++;//삭제된 줄의 숫자 증가.
        }

        void DeleteLine()//다채워진줄 삭제
        {
            for (int i = 0; i < T_Helight ; i++)
            {
                for (int j = 0; j < T_Width; j++)
                {
                    if (TetrisBoard[i, j] == 2)
                    {
                        Count++;//가로줄검사
                        if (Count == 10)//꽉차있으면 제거
                        {
                            for (int o = 1; o < T_Width-1; o++)
                            {
                                TetrisBoard[i, o] = 0;
                            }
                            DownLine(i);//위에있는 줄 땡겨주기.
                            Count = 0;
                        }
                    }
                }
                Count = 0;//초기화
            }
        }
        void GameOver()//맨위에 블럭이생기면 게임오버
        {
            for (int i = 0; i < T_Width; i++)
            {
                if (2 == TetrisBoard[1, i]|| 1 == TetrisBoard[1, i])
                {
                    
                    Console.SetCursorPosition(32 , 1);
                    Console.Write("        ■■■■■■■■■          ■■■■          ■■          ■          ■■ ■■■■■■■■■■■");
                    Console.SetCursorPosition(32, 2);
                    Console.Write("    ■■■■          ■■        ■■    ■■        ■■■      ■■■      ■■■ ■■■■■■■■■■■");
                    Console.SetCursorPosition(32, 3);
                    Console.Write("■■■■                        ■■        ■■      ■■■■  ■■■■■  ■■■■ ■■■");
                    Console.SetCursorPosition(32, 4);
                    Console.Write("■■                          ■■■■■■■■■■    ■■■■■■■  ■■■■■■■ ■■■■■■■■■■■");
                    Console.SetCursorPosition(32, 5);
                    Console.Write("■■                  ■■  ■■■■■■■■■■■■  ■■  ■■■      ■■■  ■■ ■■■■■■■■■■■");
                    Console.SetCursorPosition(32, 6);
                    Console.Write("■■■■            ■■■  ■■■            ■■■  ■■                      ■■ ■■■ ");
                    Console.SetCursorPosition(32, 7);
                    Console.Write("    ■■■■      ■■■■  ■■■            ■■■  ■■                      ■■ ■■■■■■■■■■■");
                    Console.SetCursorPosition(32, 8);
                    Console.Write("        ■■■■■■■■■  ■■■            ■■■  ■■                      ■■ ■■■■■■■■■■■");

                    
                    Console.SetCursorPosition(32, 11);
                    Console.Write("            ■■■■              ■■              ■■  ■■■■■■■■■■■■■  ■■■■■■■■■■");
                    Console.SetCursorPosition(32, 12);
                    Console.Write("        ■■■■■■■■          ■■              ■■  ■■■■■■■■■■■■■  ■■■■      ■■■■");
                    Console.SetCursorPosition(32, 13);
                    Console.Write("    ■■■■■    ■■■■■      ■■              ■■  ■■■■■■■■■■■■■  ■■■          ■■■");
                    Console.SetCursorPosition(32, 14);
                    Console.Write("■■■■■            ■■■■■  ■■              ■■  ■■■                      ■■■          ■■■");
                    Console.SetCursorPosition(32, 15);
                    Console.Write("■■■                    ■■■  ■■              ■■  ■■■■■■■■■■■■■  ■■■■      ■■■■");
                    Console.SetCursorPosition(32, 16);
                    Console.Write("■■■                    ■■■  ■■■          ■■■  ■■■■■■■■■■■■■  ■■■■■■■■■■");
                    Console.SetCursorPosition(32, 17);
                    Console.Write("■■■■■            ■■■■■    ■■■      ■■■    ■■■                      ■■■■■■■■■");
                    Console.SetCursorPosition(32, 18);
                    Console.Write("    ■■■■■    ■■■■■          ■■■  ■■■      ■■■■■■■■■■■■■  ■■          ■■");
                    Console.SetCursorPosition(32, 19);
                    Console.Write("        ■■■■■■■■                ■■■■■        ■■■■■■■■■■■■■  ■■            ■■");
                    Console.SetCursorPosition(32, 20);
                    Console.Write("            ■■■■                      ■■■          ■■■■■■■■■■■■■  ■■              ■■");

                    Console.SetCursorPosition(32, 22);
                    Console.WriteLine("Score :" + DeleteCount * BlockCount);//게임점수 = 삭제된 줄의수 * 지금까지 사용한 블럭갯수.

                    Console.SetCursorPosition(50, 22);//게임종료
                    Environment.Exit(0);
                }
            }

        }

    }


    class Mainclass
    {
        static void Main(string[] args)
        {   //게임진행
            Console.Title = "TETRIS GAME";
            Console.SetWindowSize(140,30);
            Random num = new Random();
            int pickblock = num.Next(0, 7);
            int pickblock2 = num.Next(0, 7);
            Console.BackgroundColor = ConsoleColor.White;
            Game Tetris = new Game();
            Tetris.nextBlock[0] = pickblock;
            Tetris.nextBlock[1] = pickblock2;
            while (true)
            {
                Thread.Sleep(Tetris.speed);
                Console.Clear();
                Tetris.Draw();
            }
        }
    }
}
