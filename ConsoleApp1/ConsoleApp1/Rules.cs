using System;
using System.Collections.Generic;
using System.Text;

namespace Sokoban_class
{
    internal class Rules
    {
        Player player;
        Map boxMap;
        Map playerMap;

        bool onGoal = false;

        public Rules(Player player, Map boxMap, Map playerMap)
        {
            this.player = player;
            this.boxMap = boxMap;
            this.playerMap = playerMap;
        }


        public Result PlayerMove(ConsoleKey inputKey)
        {
            // 1. 방향 설정
            int dx = 0;
            int dy = 0;
            
            switch (inputKey)
            {
                case ConsoleKey.W: dx = -1; break;
                case ConsoleKey.S: dx = 1; break;
                case ConsoleKey.A: dy = -1; break;
                case ConsoleKey.D: dy = 1; break;
                default: return Result.Fail();
            }

            // 박스 로직
            for (int i = 0; i < boxMap.BoxList.Count; i++)
            {
                Define.Position currentPos = boxMap.BoxList[i];
                int nextX = currentPos.X + dx;
                int nextY = currentPos.Y + dy;

                char target = boxMap.GetCell(nextX, nextY);

                if (target != Define.WALL && target != Define.OBSTACLE)
                {
                    Define.Position newPos = new Define.Position { X = nextX, Y = nextY };
                    boxMap.BoxList[i] = newPos;
                }
            }

            boxMap.UpdateBoxMapVisuals();

            int currentGoalCount = 0;
            // 박스가 골에 들어가면 골 카운트 plus
            foreach (var box in boxMap.BoxList)
            {
                if (boxMap.GetCell(box.X, box.Y) == Define.BOX_ON_GOAL)
                    currentGoalCount++;
            }


            // 플레이어 로직
            Define.Position nextPPos = player.PlayerPos;
            nextPPos.X += dx;
            nextPPos.Y += dy;

            char target_boxMap = boxMap.GetCell(nextPPos.X, nextPPos.Y);
            char target_playerMap = playerMap.GetCell(nextPPos.X, nextPPos.Y);

            bool canMove = true;

            // 벽, 장애물 체크
            if (target_boxMap == Define.WALL || target_boxMap == Define.OBSTACLE || target_boxMap == Define.BOX_ON_GOAL)
                canMove = false;

            if (target_playerMap == Define.WALL || target_playerMap == Define.BOX_ON_GOAL || target_playerMap == Define.OBSTACLE)
                canMove = false;

            if (canMove)
            {
                // 원래 있던 자리 처리 (골이었으면 골 복구, 아니면 빈칸)
                playerMap.SetCell(player.PlayerPos.X, player.PlayerPos.Y, onGoal ? Define.GOAL : Define.EMPTY);

                if (playerMap.GetCell(nextPPos.X, nextPPos.Y) == Define.GOAL)
                    onGoal = true;
                else
                    onGoal = false;

                // 새 자리로 이동
                playerMap.SetCell(nextPPos.X, nextPPos.Y, Define.PLAYER);
                player.Move(nextPPos);

                return Result.Success(currentGoalCount);
            }
            return Result.Fail();
        }
    }
}