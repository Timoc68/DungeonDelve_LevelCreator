using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DungeonDelve_LevelCreator
{
    public class LevelMap
    {
        private int iSizeX;
        private int iSizeY;
        private MapBlock[] mapBlocks;
        private MapCoords startBlock = null;

        public int SizeX
        {
            get
            {
                return (iSizeX);
            }
            set
            {
                iSizeX = value;
            }
        }

        public int SizeY
        {
            get
            {
                return (iSizeY);
            }
            set
            {
                iSizeY = value;
            }
        }

        public int StartX
        {
            get
            {
                return (startBlock.X);
            }
            set
            {
                startBlock.X = value;
            }
        }

        public int StartY
        {
            get
            {
                return (startBlock.Y);
            }
            set
            {
                startBlock.Y = value;
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
                return(mapBlocks[y*iSizeX + x]);
            }
            set
            {
                mapBlocks[y*iSizeX + x] = value;
            }
        }

        public MapCoords StartBlock
        {
            set
            {
                startBlock = value;
            }
        }

        public LevelMap()
        {
        }

        public LevelMap(int iMapSizeX, int iMapSizeY)
        {
            iSizeX = iMapSizeX;
            iSizeY = iMapSizeY;
            // initialise level full of unused blocks
            mapBlocks = new MapBlock[iSizeX * iSizeY];
            for (int x = 0; x < iSizeX; x++)
                for (int y = 0; y < iSizeY; y++)
                    this[x, y] = new MapBlock(x, y);
        }
    }
}
