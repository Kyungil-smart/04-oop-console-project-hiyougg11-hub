using System;
using System.Collections.Generic;

namespace Sokoban_class
{
    internal class Map
    {
        readonly Random random = new Random();

        char[,] map;
        int width;
        int height;

        // [추가] 박스들의 위치를 관리할 리스트
        public List<Define.Position> BoxList = new List<Define.Position>();

        public void Init()
        {
            width = 10;
            height = 20;

            map = new char[width, height];

            // [추가] 리스트 초기화
            BoxList.Clear();

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (i == 0 || j == 0 || i == width - 1 || j == height - 1)
                        map[i, j] = Define.WALL;
                    else
                        map[i, j] = Define.EMPTY;
                }
            }
        }

        public void PrintMap()
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }
                Console.WriteLine();
            }
        }

        public char GetCell(int x, int y) => map[x, y];
        public void SetCell(int x, int y, char value) => map[x, y] = value;

        public void SpawnPlayer(Player player)
        {
            SpawnEmptyCell(out int x, out int y);
            SetCell(x, y, Define.PLAYER);
            player.Init(x, y);
        }

        public void SpawnObject(char objChar)
        {
            SpawnEmptyCell(out int x, out int y);
            SetCell(x, y, objChar);

            // [추가] 박스라면 리스트에 위치 저장
            if (objChar == Define.BOX)
            {
                BoxList.Add(new Define.Position() { X = x, Y = y });
            }
        }

        void SpawnEmptyCell(out int posX, out int posY)
        {
            while (true)
            {
                posX = random.Next(2, width - 2);
                posY = random.Next(2, height - 2);

                if (map[posX, posY] == Define.EMPTY)
                    break;
            }
        }

        // [추가] 박스 이동 후, 맵 배열(char[,])에 그림을 다시 그려주는 함수
        public void UpdateBoxMapVisuals()
        {
            // 1. 맵 전체에서 기존 박스 그림 지우기 (빈칸 or 골)
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (map[i, j] == Define.BOX) map[i, j] = Define.EMPTY;
                    if (map[i, j] == Define.BOX_ON_GOAL) map[i, j] = Define.GOAL;
                }
            }

            // 2. 리스트에 있는 좌표에 박스 다시 그리기
            foreach (var pos in BoxList)
            {
                if (map[pos.X, pos.Y] == Define.GOAL)
                    map[pos.X, pos.Y] = Define.BOX_ON_GOAL;
                else
                    map[pos.X, pos.Y] = Define.BOX;
            }
        }
    }
}