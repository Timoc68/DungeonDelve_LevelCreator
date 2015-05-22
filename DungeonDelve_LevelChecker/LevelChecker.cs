using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Drawing;
using DungeonDelve_Level;

namespace DungeonDelve_LevelChecker
{
    public partial class LevelChecker : Form
    {
        private LevelMap levelMap = null;

        public LevelChecker()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtLevelFile_TextChanged(object sender, EventArgs e)
        {
            EnableLoad();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string sLevelFile = txtLevelFile.Text.Trim();
            if (File.Exists(sLevelFile))
            {
                XmlSerializer ser = new XmlSerializer(typeof(LevelMap));
                StreamReader reader = new StreamReader(sLevelFile);
                levelMap = (LevelMap)ser.Deserialize(reader);
                reader.Close();

                lblLevelName.Text = levelMap.LevelName;
                lblLevelNo.Text = levelMap.LevelNo.ToString();
                lblLevelSize.Text = levelMap.SizeX.ToString() + " x " + levelMap.SizeY.ToString();
                lblLevelEntrance.Text = levelMap.StartX.ToString() + " , " + levelMap.StartY.ToString();

                picLevel.Refresh();

                int iErrors = 0;
                foreach (MapBlock block in levelMap.MapBlocks)
                {
                    if (!block.LevelEntrance && !block.Unused)
                    {
                        if ((block.Exits.Count == 1) && (block.BlockType != BlockTypes.Deadend))
                            iErrors++;
                        if ((block.Exits.Count == 2) && (block.BlockType != BlockTypes.Corridor))
                            iErrors++;
                        if ((block.Exits.Count == 3) && (block.BlockType != BlockTypes.TIntersection))
                            iErrors++;
                        if ((block.Exits.Count == 4) && (block.BlockType != BlockTypes.Crossroad))
                            iErrors++;
                    }
                }
                lblLevelErrors.Text = iErrors.ToString();
            }
        }

        private void LevelChecker_Load(object sender, EventArgs e)
        {
            EnableLoad();
        }

        private void EnableLoad()
        {
            btnLoad.Enabled = !string.IsNullOrEmpty(txtLevelFile.Text);
        }

        private void picLevel_Paint(object sender, PaintEventArgs e)
        {
            if (levelMap != null)
            {
                DrawMap(levelMap, e.Graphics);
            }
        }

        private void DrawMap(LevelMap map, Graphics gr)
        {
            int iScaleX = 5;
            int iScaleY = 5;
            int iCenX = picLevel.Width / 2;
            int iCenY = picLevel.Height / 2;
            int iInitX = iCenX - map.SizeX * iScaleX / 2;
            int iInitY = iCenY + map.SizeY * iScaleY / 2;
            for (int mapX = 0; mapX < map.SizeX; mapX++)
            {
                for (int mapY = 0; mapY < map.SizeY; mapY++)
                {
                    if (map[mapX, mapY].Unused)
                    {
                        Pen pen = new Pen(Color.Black);
                        gr.DrawRectangle(pen, iInitX + mapX * iScaleX, iInitY - mapY * iScaleY, iScaleY, iScaleY);
                    }
                    else
                        if (map[mapX, mapY].LevelEntrance)
                            gr.FillRectangle(new SolidBrush(Color.Red), iInitX + mapX * iScaleX, iInitY - mapY * iScaleY, iScaleY, iScaleY);
                }
            }
        }
    }
}
