using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DungeonDelve_LevelCreator
{
    public class MapBlock : MapCoords
    {
        List<BlockExit> blockExits;
        BlockTypes blockType;

        public BlockExit this[Directions dir]
        {
            get
            {
                return (GetExit(dir));
            }
        }

        private BlockExit GetExit(Directions dir)
        {
            foreach(BlockExit exit in Exits)
                if(exit.Direction == dir)
                    return(exit);
            return (null);
        }

        public BlockTypes BlockType
        {
            get
            {
                return (blockType);
            }
            set
            {
                blockType = value;
            }
        }

        public bool LevelEntrance
        {
            get
            {
                return (blockType == BlockTypes.Entrance);
            }
        }

        public bool LevelExit
        {
            get
            {
                return (blockType == BlockTypes.Exit);
            }
        }

        public List<BlockExit> Exits
        {
            get
            {
                return (blockExits);
            }
            set
            {
                blockExits = value;
            }
        }

        public bool Unused
        {
            get
            {
                return (blockType == BlockTypes.Unused);
            }
        }

        public MapBlock() : this(0, 0)
        {
        }

        public MapBlock(int iX, int iY)
        {
            X = iX;
            Y = iY;
            blockType = BlockTypes.Unused;
            Exits = new List<BlockExit>();
        }
    }

    public class BlockExit
    {
        private Directions direction;
        private ExitTypes exitType;
        private ExitDoor bDoor;

        public Directions Direction
        {
            get
            {
                return (direction);
            }
            set
            {
                direction = value;
            }
        }

        public ExitTypes ExitType
        {
            get
            {
                return (exitType);
            }
            set
            {
                exitType = value;
            }
        }

        public BlockExit()
        {
        }

        public BlockExit(Directions dir)
        {
            direction = dir;
            bDoor = new ExitDoor();
        }
        public BlockExit(Directions dir, ExitTypes type)
        {
            direction = dir;
            exitType = type;
            bDoor = new ExitDoor();
        }
    }

    public class ExitDoor
    {
        private bool bDoor;
        private bool bLocked;
        private bool bGuarded;

        public bool Door
        {
            get
            {
                return (bDoor);
            }
            set
            {
                bDoor = value;
            }
        }

        public bool Locked
        {
            get
            {
                return (bLocked);
            }
            set
            {
                bLocked = value;
            }
        }

        public bool Guarded
        {
            get
            {
                return (bGuarded);
            }
            set
            {
                bGuarded = value;
            }
        }

        public ExitDoor()
        {
            bDoor = false;
            bLocked = false;
            bGuarded = false;
        }
    }

    public class MapCoords
    {
        int iX = 0;
        int iY = 0;

        public int X
        {
            get
            {
                return (iX);
            }
            set
            {
                iX = value;
            }
        }

        public int Y
        {
            get
            {
                return (iY);
            }
            set
            {
                iY = value;
            }
        }

        public MapCoords()
        {
        }

        public MapCoords(int iValX, int iValY)
        {
            iX = iValX;
            iY = iValY;
        }
    }
}
