using System;
using System.Collections.Generic;
using System.Text;

namespace Sokoban_class
{
    internal class Rules
    {
        Player player;
        /// <summary>
        /// 박스, 골, 장애물, 벽이 들어가야 하는 맵
        /// </summary>
        Map boxMap;

        /// <summary>
        /// 플레이어, 장애물, 벽이 들어가야 하는 맵
        /// </summary>
        Map playerMap;

        bool onGoal = false;

        public Rules(Player player, Map boxMap, Map playerMap)
        {
            this.player = player;
            this.boxMap = boxMap;
            this.playerMap = playerMap;
        }

        /// <summary>
        /// 플레이어 움직임 함수 
        /// </summary>
        /// <param name="inputKey"></param>
        /// <returns>Result</returns>
        public Result PlayerMove(ConsoleKey inputKey)
        {
            // Player가 움직인 nextPos 값을 가지자.
            Define.Position nextPos = player.PlayerPos;
            Define.eMoveDir dir = Define.eMoveDir.NONE;

            switch (inputKey)
            {
                case ConsoleKey.W:
                    dir = Define.eMoveDir.UP;
                    nextPos.X--;
                    break;

                case ConsoleKey.A:
                    dir = Define.eMoveDir.LEFT;
                    nextPos.Y--;
                    break;

                case ConsoleKey.S:
                    dir = Define.eMoveDir.DOWN;
                    nextPos.X++;
                    break;

                case ConsoleKey.D:
                    dir = Define.eMoveDir.RIGHT;
                    nextPos.Y++;
                    break;

                default:
                    return Result.Fail();
            }

            // 지나갈 수 있음? -> #벽 체크
            char target_boxMap = boxMap.GetCell(nextPos.X, nextPos.Y);
            char target_playerMap = playerMap.GetCell(nextPos.X, nextPos.Y);

            // #(벽), B->G(@) 못 지나감..
            if (target_boxMap == Define.WALL || target_boxMap == Define.BOX_ON_GOAL)
                return Result.Fail();

            if (target_playerMap == Define.WALL || target_boxMap == Define.BOX_ON_GOAL)
                return Result.Fail();

            // 장애물(■)이 있다면 이동X
            if (target_boxMap == Define.OBSTACLE)
                return Result.Fail();       
            
            if (target_playerMap == Define.OBSTACLE)
                return Result.Fail();

            int goalCount = 0;

            // B(박스/폭탄) 밀기 시도
            if (target_boxMap == Define.BOX)
            {
                bool pushed = PushBox(dir, nextPos, out goalCount);

                if (!pushed)
                    return Result.Fail();
            }

            // 움직임.
            if (onGoal)
            {
                onGoal = false;
                boxMap.SetCell(player.PlayerPos.X, player.PlayerPos.Y, Define.GOAL);
                playerMap.SetCell(player.PlayerPos.X, player.PlayerPos.Y, Define.GOAL);
            }
            else
            {
                boxMap.SetCell(player.PlayerPos.X, player.PlayerPos.Y, Define.EMPTY);
                playerMap.SetCell(player.PlayerPos.X, player.PlayerPos.Y, Define.EMPTY);
            }

            if (playerMap.GetCell(nextPos.X, nextPos.Y) == Define.GOAL)
                onGoal = true;

            playerMap.SetCell(nextPos.X, nextPos.Y, Define.PLAYER);     

            player.Move(nextPos);

            return Result.Success(goalCount);
        }


        bool PushBox(Define.eMoveDir dir, Define.Position boxPos, out int goalCount)
        {
            Define.Position pos = boxPos;

            goalCount = 0;

            switch (dir)
            {
                case Define.eMoveDir.UP:
                    pos.X--;
                    break;

                case Define.eMoveDir.DOWN:
                    pos.X++;
                    break;

                case Define.eMoveDir.LEFT:
                    pos.Y--;
                    break;

                case Define.eMoveDir.RIGHT:
                    pos.Y++;
                    break;
            }

            char target = boxMap.GetCell(pos.X, pos.Y);


            // 박스(B)가 벽(#)이랑 겹치면 이동X 
            if (target == Define.WALL)
                return false;

            if (target == Define.OBSTACLE)
                return false;

            // 박스(B)가 골(G)이랑 겹치면 이동O 
            if (target == Define.GOAL)
            {
                boxMap.SetCell(boxPos.X, boxPos.Y, Define.EMPTY);
                boxMap.SetCell(pos.X, pos.Y, Define.BOX_ON_GOAL);

                // 골개수 카운팅 ++
                goalCount = 1;

                return true;
            }

            // 박스(B)가 빈칸( )이면 이동O 
            boxMap.SetCell(boxPos.X, boxPos.Y, Define.EMPTY);
            boxMap.SetCell(pos.X, pos.Y, Define.BOX);

            return true;
        }
    }
}
