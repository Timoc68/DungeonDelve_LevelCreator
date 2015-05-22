using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DungeonDelve_LevelCreator
{
    public enum Directions
    {
        North = 0,
        South = 1,
        East = 2,
        West = 3,
        Up = 4,
        Down = 5
    }

    public enum Turns
    {
        Right = 0,
        Left = 1
    }

    public enum CorridorTypes
    {
        Short = 0,
        Medium = 1,
        Long = 2
    }

    public enum BlockTypes
    {
        Unused = 0,
        Entrance = 1,
        Exit = 2,
        Corridor = 3,
        Room = 4
    }

    public enum ExitTypes
    {
        ShortCorridor = 1,
        MediumCorridor = 2,
        LongCorridor = 3,
        SmallRoom = 4,
        MediumRoom = 5,
        LargeRoom = 6,
        PrevLevel = 7,
        NextLevel = 8
    }
}
