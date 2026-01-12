using System;
using System.Collections.Generic;
using System.Text;

namespace Sokoban_class
{
    // 맵(오브젝트) 초기화/출력
    internal class Map
    {
        readonly Random random = new Random();

        char[,] map;
        int width;
        int height;

        public void Init()
        {
            width = 10;
            height = 20;

            map = new char[width, height];

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

        public void CopyPrintMap(Map copyMap)
        {
            width = 10;
            height = 20;

            map = new char[width, height];

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    char _copyMap = copyMap.GetCell(i, j);

                    // 벽이나 장애물인 경우에만 맵1에 복사
                    if (_copyMap == Define.WALL || _copyMap == Define.OBSTACLE)
                        this.map[i, j] = _copyMap;
                    else
                        this.map[i, j] = Define.EMPTY; // 나머지는 빈칸
                }
            }
        }
    }
}
