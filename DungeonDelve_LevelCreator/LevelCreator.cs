using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using DungeonDelve_Level;

namespace DungeonDelve_LevelCreator
{
    public class LevelCreator
    {
        private bool bInitialised = false;
        private string strLevelOutputFile = string.Empty;
        private string strLevelLogFile = string.Empty;

        private Random levelRandomiser = null;
        private MapCoords LevelSize = null;

        Stack stackExitsToProcess = null;

        private int iShortCorrLength = 4;
        private int iMediumCorrLength = 8;
        private int iLongCorrLength = 12;

        private int iBlocksBetweenCorridorChecks = 1;

        private int iNumCorridors = 0;

        private LevelMap levelMap;

        private Logging levelLog;

        public LevelMap Map
        {
            get
            {
                return (levelMap);
            }
        }

        public int ShortCorrLength
        {
            get
            {
                return (iShortCorrLength);
            }
            set
            {
                iShortCorrLength = value;
            }
        }

        public int MediumCorrLength
        {
            get
            {
                return (iMediumCorrLength);
            }
            set
            {
                iMediumCorrLength = value;
            }
        }

        public int LongCorrLength
        {
            get
            {
                return (iLongCorrLength);
            }
            set
            {
                iLongCorrLength = value;
            }
        }

        public int BlocksBetweenCorridorChecks
        {
            get
            {
                return (iBlocksBetweenCorridorChecks);
            }
            set
            {
                iBlocksBetweenCorridorChecks = value;
            }
        }

        public LevelCreator(string strOutputFile)
        {
            strLevelOutputFile = strOutputFile;
            strLevelLogFile = Path.GetFileNameWithoutExtension(strOutputFile) + ".log";
            levelLog = new Logging(strLevelLogFile);
            stackExitsToProcess = new Stack();

            levelLog.LogInfo("New level file generation started for '" + strOutputFile + "'.");
        }

        public void GenerateMap()
        {
            if (bInitialised)
            {
                // CurrBlock = new MapCoords(levelMap.StartX, levelMap.StartY);
                int iStartX = levelMap.StartX;
                int iStartY = levelMap.StartY;
                GenerateExits(levelMap, levelMap[iStartX, iStartY], false);

                HandleExitsToProcess(levelMap);
            }
        }

        private void HandleExitsToProcess(LevelMap map)
        {
            while (stackExitsToProcess.Count > 0)
            {
                ExitToProcess exitToProcess = (ExitToProcess)stackExitsToProcess.Pop();
                MapBlock block = exitToProcess.Block;
                BlockExit exit = exitToProcess.Exit;
                int iCorrLength = GetCorridorLength(map, map[block.X, block.Y], exit.Direction);
                if (iCorrLength > 0)
                {
                    ExitTypes exitType = GetExitType(iCorrLength);
                    map[block.X, block.Y][exit.Direction].ExitType = exitType;
                    GenerateCorridor(map, map[block.X, block.Y], exit.Direction, iCorrLength, exitType);
                }
                else
                    map[block.X, block.Y].Exits.Remove(map[block.X, block.Y][exit.Direction]);
            }
        }

        private int GenerateCorridor(LevelMap map, MapBlock curr, Directions corridorDirection, int iLength, ExitTypes exitType)
        {
            int iNumNewExits = 0;
            int iSkipBlocksForCorridor = 0;

            // keep track of number of corridors
            iNumCorridors++;

            // create a new block in the relevant direction for the length of the new corridor
            for (int iBlock = 0; iBlock < iLength; iBlock++)
            {
                MapBlock next = null;
                switch (corridorDirection)
                {
                    case Directions.North:
                        curr = map[curr.X, curr.Y + 1];
                        if(curr.Y < map.SizeY-1)
                            next = map[curr.X, curr.Y + 1];
                        break;
                    case Directions.South:
                        curr = map[curr.X, curr.Y - 1];
                        if(curr.Y > 1)
                            next = map[curr.X, curr.Y - 1];
                        break;
                    case Directions.East:
                        curr = map[curr.X + 1, curr.Y];
                        if(curr.X < map.SizeX-1)
                            next = map[curr.X + 1, curr.Y];
                        break;
                    case Directions.West:
                        curr = map[curr.X - 1, curr.Y];
                        if(curr.X > 1)
                            next = map[curr.X - 1, curr.Y];
                        break;
                }

                // create exit from new block back to previous block in opposite direction
                AddExitToBlock(curr, DirectionOpposite(corridorDirection), exitType);
                // create exit from new block back to next block (except for last block in corridor)
                if (iBlock < iLength-1)
                    AddExitToBlock(curr, corridorDirection, exitType);
                else if ((next != null) && !next.Unused)
                {
                    // adjust next block when two corridors intersect
                    AddExitToBlock(curr, corridorDirection, exitType);
                    AddExitToBlock(next, DirectionOpposite(corridorDirection), exitType);
                    next.BlockType = GetBlockType(next);
                }

                // set block type
                curr.BlockType = GetBlockType(curr);

                // check for new branch off corridor
                if (exitType != ExitTypes.ShortCorridor)
                {
                    if (iSkipBlocksForCorridor == 0)
                    {
                        if (NumNSEWExits(curr) < 3)
                        {
                            iNumNewExits = GenerateExits(map, curr, false);
                            if (iNumNewExits > 0)
                                iSkipBlocksForCorridor = BlocksBetweenCorridorChecks;
                        }
                    }
                    else
                        iSkipBlocksForCorridor--;
                }
            }

            if (iSkipBlocksForCorridor == 0)
                if(NumNSEWExits(map[curr.X,curr.Y]) == 1)
                    if((iNumCorridors < 10) || (levelRandomiser.Next(4) == 1))
                        iNumNewExits = GenerateExits(map, map[curr.X,curr.Y], true);

            return (iLength);
        }

        private int NumNSEWExits(MapBlock block)
        {
            return(block.Exits.Count - (block.LevelEntrance ? 1 : 0));
        }

        private bool AddExitToBlock(MapBlock block, Directions dir, ExitTypes type)
        {
            bool bAddExit = true;
            for(int i=0;i<block.Exits.Count;i++)
            {
                if(block.Exits[i].Direction == dir)
                    bAddExit = false;
            }
            if(bAddExit)
            {
                BlockExit exit = new BlockExit(dir, type);
                block.Exits.Add(exit);
            }

            return (bAddExit);
        }

        private BlockTypes GetBlockType(MapBlock block)
        {
            BlockTypes blockType = BlockTypes.Unused;
            int iNumExits = NumNSEWExits(block);
            switch (iNumExits)
            {
                case 1:
                    blockType = BlockTypes.Deadend;
                    break;
                case 2:
                    blockType = BlockTypes.Corridor;
                    break;
                case 3:
                    blockType = BlockTypes.TIntersection;
                    break;
                case 4:
                    blockType = BlockTypes.Crossroad;
                    break;
            }

            return (blockType);
        }

        private int GetCorridorLength(LevelMap map, MapBlock curr, Directions dir)
        {
            int iLength = levelRandomiser.Next(6);
            if (iLength < 2)
                iLength = levelRandomiser.Next(1, ShortCorrLength);
            else if (iLength < 5)
                iLength = levelRandomiser.Next(ShortCorrLength + 1, MediumCorrLength);
            else
                iLength = levelRandomiser.Next(MediumCorrLength + 1, LongCorrLength);
            iLength = levelRandomiser.Next(1, LongCorrLength); //// **** ////
            iLength = CheckCorridor(map, curr, dir, iLength);

            return (iLength);
        }

        private ExitTypes GetExitType(int iLength)
        {
            ExitTypes exitType;
            if (iLength < ShortCorrLength)
                exitType = ExitTypes.ShortCorridor;
            else if (iLength < MediumCorrLength)
                exitType = ExitTypes.MediumCorridor;
            else
                exitType = ExitTypes.LongCorridor;

            return (exitType);
        }

        private int CheckCorridor(LevelMap map, MapBlock curr, Directions dir, int iTargetLength)
        {
            bool bValidBlock = true;
            int iX = curr.X;
            int iY = curr.Y;
            int iLen = 1;
            int iDX = (dir == Directions.West ? -1 : (dir == Directions.East ? 1 : 0));
            int iDY = (dir == Directions.South ? -1 : (dir == Directions.North ? 1 : 0));

            if (bValidBlock)
            {
                do
                {
                    // check there are no adjacent corridors in the relevant direction
                    // -- North
                    if (dir == Directions.North)
                    {
                        if (iY+iLen < map.SizeY-1)
                        {
                            if ((iX > 0) && (iX < map.SizeX - 1))
                                if (!map[iX - 1, iY + iLen].Unused || !map[iX + 1, iY + iLen].Unused)
                                    bValidBlock = false;
                        }
                        else
                            bValidBlock = false;
                    }
                    // -- South
                    if (dir == Directions.South)
                    {
                        if (iY - iLen > 0)
                        {
                            if ((iX > 0) && (iX < map.SizeX - 1))
                                if (!map[iX - 1, iY - iLen].Unused || !map[iX + 1, iY - iLen].Unused)
                                    bValidBlock = false;
                        }
                        else
                            bValidBlock = false;
                    }
                    // -- East
                    if (dir == Directions.East)
                    {
                        if (iX + iLen < map.SizeX - 1)
                        {
                            if ((iY > 0) && (iY < map.SizeY - 1))
                                if (!map[iX + iLen, iY - 1].Unused || !map[iX + iLen, iY + 1].Unused)
                                    bValidBlock = false;
                        }
                        else
                            bValidBlock = false;
                    }
                    // -- West
                    if (dir == Directions.West)
                    {
                        if (iX - iLen > 0)
                        {
                            if ((iY > 0) && (iY < map.SizeY - 1))
                                if (!map[iX - iLen, iY - 1].Unused || !map[iX - iLen, iY + 1].Unused)
                                    bValidBlock = false;
                        }
                        else
                            bValidBlock = false;
                    }
                    
                    /*
                    if ((iX + (iLen + 1) * iDX < map.SizeX) && (iY + (iLen + 1) * iDY < map.SizeY))
                        if ((iX + (iLen + 1) * iDX >= 0) && (iY + (iLen + 1) * iDY >= 0))
                            if (map[iX + iLen * iDX, iY + iLen * iDY].Unused)
                                bValidBlock = true;
                    */

                    if(bValidBlock)
                        iLen++;
                } while (bValidBlock && (iLen <= iTargetLength));
            }

            return (iLen - 1);
        }

        private Directions DirectionOpposite(Directions dir)
        {
            Directions newDir = Directions.North;
            switch (dir)
            {
                case Directions.North:
                    newDir = Directions.South;
                    break;
                case Directions.South:
                    newDir = Directions.North;
                    break;
                case Directions.East:
                    newDir = Directions.West;
                    break;
                case Directions.West:
                    newDir = Directions.East;
                    break;
            }

            return (newDir);
        }

        private Directions DirectionTurn(Directions dir, Turns turn)
        {
            Directions newDir = Directions.North;
            switch (dir)
            {
                case Directions.North:
                    if (turn == Turns.Right)
                        newDir = Directions.East;
                    else
                        newDir = Directions.West;
                    break;
                case Directions.South:
                    if(turn == Turns.Right)
                        newDir = Directions.West;
                    else
                        newDir = Directions.East;
                    break;
                case Directions.East:
                    if(turn == Turns.Right)
                        newDir = Directions.South;
                    else
                        newDir = Directions.North;
                    break;
                case Directions.West:
                    if(turn == Turns.Right)
                        newDir = Directions.North;
                    else
                        newDir = Directions.South;
                    break;
            }

            return (newDir);
        }

        private int GenerateExits(LevelMap map, MapBlock block, bool bNoDeadEnd)
        {
            // determine random number of exits - different for entrance block
            int iNumNewExits = 0;
            int iTemp = levelRandomiser.Next(20);
            if (block.LevelEntrance || bNoDeadEnd)
                // avoid deadend for entrance block, or if instructed
                iTemp = levelRandomiser.Next(9);
            if (iTemp == 0)
                // crossroads
                iNumNewExits = Math.Min(3, 4 - NumNSEWExits(block));
            else if ((iTemp >= 1) && (iTemp <= 2))
                // t-intersection
                iNumNewExits = Math.Min(2, 4 - NumNSEWExits(block));
            else if ((iTemp >= 3) && (iTemp <= 8))
                // corridor
                iNumNewExits = Math.Min(1, 4 - NumNSEWExits(block));
            else
                // deadend
                iNumNewExits = 0;

            for (int i = 0; i < iNumNewExits; i++)
            {
                Directions newExitDir = (Directions)levelRandomiser.Next(4);
                bool bValidExit = true;
                for (int j = 0; j < block.Exits.Count; j++)
                {
                    if (block.Exits[j].Direction == newExitDir)
                    {
                        // exit already exists, re-roll
                        bValidExit = false;
                        i--;
                        // iNumNewExits--;
                    }
                    else
                    {
                        // check if exit leads off edge of map in the relvant direction
                        // -- North
                        if ((newExitDir == Directions.North) && (block.Y == map.SizeY - 1))
                            bValidExit = false;
                        // -- South
                        if ((newExitDir == Directions.South) && (block.Y == 0))
                            bValidExit = false;
                        // -- East
                        if ((newExitDir == Directions.East) && (block.X == map.SizeX - 1))
                            bValidExit = false;
                        // -- West
                        if ((newExitDir == Directions.West) && (block.Y == 0))
                            bValidExit = false;
                    }
                }
                if (bValidExit)
                {
                    block.Exits.Add(new BlockExit(newExitDir));
                    ExitToProcess exitToProcess = new ExitToProcess(block, block.Exits[block.Exits.Count-1]);
                    stackExitsToProcess.Push(exitToProcess);
                }
            }

            // set block type
            if (block.BlockType != BlockTypes.Entrance)
                block.BlockType = GetBlockType(block);

            return (iNumNewExits);
        }

        public void InitialiseLevel(int iMinX, int iMinY, int iMaxX, int iMaxY, int iMapSeed)
        {
            // initialise randomiser
            if(iMapSeed == 0)
                iMapSeed = DateTime.Now.Millisecond;
            levelRandomiser = new Random(iMapSeed);

            // generate level size (X,Y)
            LevelSize = new MapCoords();
            int iSizeX = levelRandomiser.Next(iMinX, iMaxX);
            int iSizeY = levelRandomiser.Next(iMinY, iMaxY);
            
            // create level full of unused blocks
            levelMap = new LevelMap(iSizeX, iSizeY);
            levelMap.MapSeed = iMapSeed;

            levelLog.LogInfo("Level map initialised - " + levelMap.SizeX.ToString() + " x " + levelMap.SizeY.ToString() + " blocks.");

            // determine start block (X,Y)
            int iStartX = levelRandomiser.Next(1,levelMap.SizeX-1);
            int iStartY = levelRandomiser.Next(1,levelMap.SizeY-1);
            levelMap.StartBlock = new MapCoords(iStartX, iStartY);
            levelMap[levelMap.StartX, levelMap.StartY].BlockType = BlockTypes.Entrance;
            levelMap[levelMap.StartX, levelMap.StartY].Exits.Add(new BlockExit(Directions.Up));
            levelMap[levelMap.StartX, levelMap.StartY][Directions.Up].ExitType = ExitTypes.PrevLevel;

            levelLog.LogInfo("Level entry located at block " + levelMap.StartX.ToString() + "," + levelMap.StartY.ToString() + ".");

            bInitialised = true;
        }
    }
}
