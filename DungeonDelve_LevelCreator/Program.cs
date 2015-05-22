using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using DungeonDelve_Level;

namespace DungeonDelve_LevelCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            string strParamsFile = System.Configuration.ConfigurationSettings.AppSettings["paramsFile"];
            string strOutputFile = System.Configuration.ConfigurationSettings.AppSettings["outputFile"];
            
            if (args.Length > 0)
                if(File.Exists(args[0]))
                    strParamsFile = args[0];

            if (!string.IsNullOrEmpty(strParamsFile))
            {
                try
                {
                    // read parameters file for name and level and determine output file name
                    XmlDocument xmlParams = new XmlDocument();
                    xmlParams.Load(strParamsFile);
                    string strLevelName = GetXmlAttribute(xmlParams, "DungeonLevelParams", "name");
                    string strLevelNo = GetXmlAttribute(xmlParams, "DungeonLevelParams", "level");
                    strOutputFile = strOutputFile.Replace("%N", strLevelName);
                    strOutputFile = strOutputFile.Replace("%L", strLevelNo);

                    // read parameters file for level size details
                    int iMinX = Convert.ToInt16(GetXmlAttribute(xmlParams, "DungeonLevelParams/Blocks", "minX"));
                    int iMinY = Convert.ToInt16(GetXmlAttribute(xmlParams, "DungeonLevelParams/Blocks", "minY"));
                    int iMaxX = Convert.ToInt16(GetXmlAttribute(xmlParams, "DungeonLevelParams/Blocks", "maxX"));
                    int iMaxY = Convert.ToInt16(GetXmlAttribute(xmlParams, "DungeonLevelParams/Blocks", "maxY"));

                    // create dungeon level and initialise
                    int iMapSeed = 0;
                    string sMapSeed = GetXmlAttribute(xmlParams, "DungeonLevelParams/MapSeed", "value");
                    if (sMapSeed != string.Empty)
                        iMapSeed = Convert.ToInt16(sMapSeed);
                    LevelCreator dungeonLevel = new LevelCreator(strOutputFile);
                    dungeonLevel.InitialiseLevel(iMinX, iMinY, iMaxX, iMaxY, iMapSeed);
                    dungeonLevel.Map.LevelNo = Convert.ToInt16(strLevelNo);
                    dungeonLevel.Map.LevelName = strLevelName;

                    // determine corridor parameters
                    int iCorrSmall = Convert.ToInt16(GetXmlAttribute(xmlParams, "DungeonLevelParams/Corridors/short", "length"));
                    int iCorrMedium = Convert.ToInt16(GetXmlAttribute(xmlParams, "DungeonLevelParams/Corridors/medium", "length"));
                    int iCorrLong = Convert.ToInt16(GetXmlAttribute(xmlParams, "DungeonLevelParams/Corridors/long", "length"));
                    int iBlocksBetweenCorridorChecks = Convert.ToInt16(GetXmlAttribute(xmlParams, "DungeonLevelParams/Corridors/blocksBetweenCorridorChecks", "value"));
                    dungeonLevel.ShortCorrLength = iCorrSmall;
                    dungeonLevel.MediumCorrLength = iCorrMedium;
                    dungeonLevel.LongCorrLength = iCorrLong;
                    dungeonLevel.BlocksBetweenCorridorChecks = iBlocksBetweenCorridorChecks;

                    // generate level map
                    dungeonLevel.GenerateMap();

                    ////// write level map to output xml file
                    XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
                    {
                        Indent = true,
                        OmitXmlDeclaration = true
                    };
                    XmlSerializerNamespaces xmlnsWriter = new XmlSerializerNamespaces();
                    xmlnsWriter.Add("", "");
                    using (XmlWriter xmlWriter = XmlWriter.Create(strOutputFile))
                    {
                        XmlSerializer ser = new XmlSerializer(typeof(LevelMap));
                        ser.Serialize(xmlWriter, dungeonLevel.Map, xmlnsWriter);
                        xmlWriter.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid level parameters XML file '" + strParamsFile + "'.");
                }
            }
            else
                Console.WriteLine("A valid level parameters XML file was not provided.");
        }

        private static string GetXmlAttribute(XmlDocument xmlDoc, string strXmlNode, string strXmlAttr)
        {
            string strValue = string.Empty;

            try
            {
                XmlNode node = xmlDoc.SelectSingleNode(strXmlNode);
                if (node != null)
                    if(node.Attributes[strXmlAttr] != null)
                        strValue = node.Attributes[strXmlAttr].Value;
            }
            catch (Exception ex)
            {
            }

            return (strValue);
        }
    }
}
