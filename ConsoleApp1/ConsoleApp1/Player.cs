using System;
using System.Collections.Generic;
using System.Text;

namespace Sokoban_class
{
    // 플레이어 입력
    internal class Player
    {
        public Define.Position PlayerPos { get; private set; }

        public void Init(int posX, int PosY)
        {
            PlayerPos = new Define.Position() { X = posX, Y = PosY };
        }

        public ConsoleKey UserInput()
        {
            ConsoleKey inputKey = default;

            while (true)
            {
                inputKey = Console.ReadKey(true).Key;

                if (ConsoleKey.W == inputKey ||
                    ConsoleKey.A == inputKey ||
                    ConsoleKey.S == inputKey ||
                    ConsoleKey.D == inputKey ||
                    ConsoleKey.Q == inputKey)
                    break;
            }

            return inputKey;
        }

        public void Move(Define.Position nextPos)
        {
            PlayerPos = nextPos;
        }
    }
}
