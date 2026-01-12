using System;
using System.Collections.Generic;
using System.Text;

namespace Sokoban_class
{
    // 게임상태 텍스트 출력
    internal class PrintText
    {
        public void PrintGuide()
        {
            Console.WriteLine("W 위, S 아래, A 왼쪽, D 오른쪽, Q 종료");
            Console.WriteLine("모든 박스를 골로 옮기세요");
            Console.WriteLine();
        }


        public void PrintGameState(int moveCount, int curGoalCount, int goalCount)
        {
            Console.WriteLine($"이동 횟수:  {moveCount}");
            Console.WriteLine($"골 횟수:  {curGoalCount} / {goalCount}");
            Console.WriteLine();
        }

        public void PrintGameClear()
        {
            // 축하글
            Console.WriteLine();
        }
    }
}
