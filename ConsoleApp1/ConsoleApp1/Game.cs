using System;

using System.Collections.Generic;

using System.Text;

namespace Sokoban_class
{
    // 게임의 Update (루프/룰 체크) + 데이터 관리
    internal class Game
    {
        Random random = new Random();

        Map boxMap;
        Map playerMap;
        Player player;
        Rules rules;
        PrintText printText;

        int moveCount = 0;
        int goalCount = 0;
        int curGoalCount = 0;
        int obstacle = 0;

        public void Run()
        {
            Init();
            printText.PrintGuide();

            while (true)
            {
                // 맵 초기화/업데이트
                Console.SetCursorPosition(0, 4);
                printText.PrintGameState(moveCount, curGoalCount, goalCount);
                boxMap.PrintMap();
                playerMap.PrintMap();

                // 게임 클리어 체크
                if (curGoalCount >= goalCount)
                {
                    break;
                }

                // 플레이어 이동 (Core)
                ConsoleKey inputKey = player.UserInput(); // 입력 들어왔네. WASD
                if (inputKey == ConsoleKey.Q)
                    break;

                // 플레이어가 Move O? X? -> 게임 클리어 할 수도 있고 아닐 수도 있고
                Result result = rules.PlayerMove(inputKey);

                if (result.Moved)
                    moveCount++;

                if (result.GoalCount != 0)
                    curGoalCount += 1;
            }
            Console.WriteLine("게임이 종료됐습니다!");
        }

        void Init()
        {
            moveCount = 0;
            curGoalCount = 0;
            goalCount = 1;
            obstacle = random.Next(5, 10);


            boxMap = new Map();
            playerMap = new Map();
            player = new Player();
            rules = new Rules(player, boxMap, playerMap);
            printText = new PrintText();

            boxMap.Init();
            playerMap.Init();
            boxMap.SpawnPlayer(player);

            boxMap.SetCell(player.PlayerPos.X, player.PlayerPos.Y, Define.EMPTY);
            playerMap.SetCell(player.PlayerPos.X, player.PlayerPos.Y, Define.PLAYER);

            // 오브젝트 초기화
            for (int i = 0; i < goalCount; i++)
            {
                boxMap.SpawnObject(Define.BOX);
                boxMap.SpawnObject(Define.GOAL);
            }

            for (int i = 0; i < obstacle; i++)
            {
                boxMap.SpawnObject(Define.OBSTACLE);
                playerMap.SpawnObject(Define.OBSTACLE);
            }
        }
    }
}