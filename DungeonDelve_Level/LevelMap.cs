using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DungeonDelve_Level
{
    public class LevelMap
    {
        int iMapSeed = 0;
        int iLevelNo = 0;
        string strLevelName = string.Empty;
        private MapCoords mapSize = null;
        private MapCoords startBlock = null;
        private MapBlock[] mapBlocks;

        public int MapSeed
        {
            get
            {
                return (iMapSeed);
            }
            set
            {
                iMapSeed = value;
            }
        }

        public int LevelNo
        {
            get
            {
                return (iLevelNo);
            }
            set
            {
                iLevelNo = value;
            }
        }

        public string LevelName
        {
            get
            {
                return (strLevelName);
            }
            set
            {
                strLevelName = value;
            }
        }

        public MapCoords MapSize
        {
            get
            {
                return (mapSize);
            }
            set
            {
                mapSize = value;
            }
        }

        public int SizeX
        {
            get
            {
                return (mapSize.X);
            }
        }

        public int SizeY
        {
            get
            {
                return (mapSize.Y);
            }
        }

        public MapCoords StartBlock
        {
            get
            {
                return (startBlock);
            }
            set
            {
                startBlock = value;
            }
        }

        public int StartX
        {
            get
            {
                return (startBlock.X);
            }
        }

        public int StartY
        {
            get
            {
                return (startBlock.Y);
            }
        }

        public MapBlock[] MapBlocks
        {
            get
            {
                return (mapBlocks);
            }
            set
            {
                mapBlocks = value;
            }
        }

        [XmlIgnore]
        public MapBlock this[int x, int y]
        {
            get
            {
                return(mapBlocks[y*SizeX + x]);
            }
            set
            {
                mapBlocks[y*SizeX + x] = value;
            }
        }

        public LevelMap()
        {
        }

        public LevelMap(int iMapSizeX, int iMapSizeY)
        {
            mapSize = new MapCoords(iMapSizeX, iMapSizeY);
            // initialise level full of unused blocks
            mapBlocks = new MapBlock[SizeX * SizeY];
            for (int x = 0; x < SizeX; x++)
                for (int y = 0; y < SizeY; y++)
                    this[x, y] = new MapBlock(x, y);
        }
    }
}
