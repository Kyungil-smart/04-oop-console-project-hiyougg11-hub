using System;
using System.Collections.Generic;
using System.Text;

namespace Sokoban_class
{
    internal struct Result
    {
        public bool Moved;
        public int GoalCount;

        public static Result Fail() => new Result() { Moved = false, GoalCount = 0 };
        public static Result Success(int goalCount = 0) => new Result() { Moved = true, GoalCount = goalCount };
    }
}
