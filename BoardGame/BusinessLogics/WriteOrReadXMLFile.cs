using BoardGame.Domain_Model;
using BoardGame.DomainModel;
using System;
using System.IO;
using System.Xml;

namespace BoardGame.BusinessLogics
{
    static class WriteOrReadXMLFile
    {
        static string fileName = "UserSettings.xml";

        public static void ExportData(Player player, Robot robot1, Robot robot2, Robot robot3, Level level)
        {
            if (!File.Exists(fileName))
            {
                IfFileNotExistCreateXMLFile(player, robot1, robot2, robot3, level);
            }
            else
            {
                IfFileExistModifyXMLFileValues(player, robot1, robot2, robot3, level);
            }
        }

        public static void ImportData(Player player, Robot robot1, Robot robot2, Robot robot3, Level level)
        {
            if (File.Exists(fileName))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);

                player.Name = xmlDocument.SelectSingleNode("//Settings/Player/Name").InnerText;
                robot1.Name = xmlDocument.SelectSingleNode("//Settings/Robot1/Name").InnerText;
                robot2.Name = xmlDocument.SelectSingleNode("//Settings/Robot2/Name").InnerText;
                robot3.Name = xmlDocument.SelectSingleNode("//Settings/Robot3/Name").InnerText;

                player.FigureColor = (Figure)Enum.Parse(typeof(Figure), xmlDocument.SelectSingleNode("//Settings/Player/FigureColor").InnerText);
                robot1.FigureColor = (Figure)Enum.Parse(typeof(Figure), xmlDocument.SelectSingleNode("//Settings/Robot1/FigureColor").InnerText);
                robot2.FigureColor = (Figure)Enum.Parse(typeof(Figure), xmlDocument.SelectSingleNode("//Settings/Robot2/FigureColor").InnerText);
                robot3.FigureColor = (Figure)Enum.Parse(typeof(Figure), xmlDocument.SelectSingleNode("//Settings/Robot3/FigureColor").InnerText);

                player.Balance = int.Parse(xmlDocument.SelectSingleNode("//Settings/Player/Balance").InnerText);
                robot1.Balance = int.Parse(xmlDocument.SelectSingleNode("//Settings/Robot1/Balance").InnerText);
                robot2.Balance = int.Parse(xmlDocument.SelectSingleNode("//Settings/Robot2/Balance").InnerText);
                robot3.Balance = int.Parse(xmlDocument.SelectSingleNode("//Settings/Robot3/Balance").InnerText);

                level.LevelType = (Levels)Enum.Parse(typeof(Levels), xmlDocument.SelectSingleNode("//Settings/Level").InnerText);
            }
        }

        public static void IfFileNotExistCreateXMLFile(Player player, Robot robot1, Robot robot2, Robot robot3, Level level)
        {
            using (XmlWriter xmlWriter = XmlWriter.Create(fileName))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Settings");

                xmlWriter.WriteStartElement("Player");

                xmlWriter.WriteStartElement("Name");
                xmlWriter.WriteString(player.Name);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("FigureColor");
                xmlWriter.WriteString(player.FigureColor.ToString());
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("Balance");
                xmlWriter.WriteString(player.Balance.ToString());
                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("Robot1");

                xmlWriter.WriteStartElement("Name");
                xmlWriter.WriteString(robot1.Name);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("FigureColor");
                xmlWriter.WriteString(robot1.FigureColor.ToString());
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("Balance");
                xmlWriter.WriteString(robot1.Balance.ToString());
                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("Robot2");

                xmlWriter.WriteStartElement("Name");
                xmlWriter.WriteString(robot2.Name);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("FigureColor");
                xmlWriter.WriteString(robot2.FigureColor.ToString());
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("Balance");
                xmlWriter.WriteString(robot2.Balance.ToString());
                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("Robot3");

                xmlWriter.WriteStartElement("Name");
                xmlWriter.WriteString(robot3.Name);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("FigureColor");
                xmlWriter.WriteString(robot3.FigureColor.ToString());
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("Balance");
                xmlWriter.WriteString(robot3.Balance.ToString());
                xmlWriter.WriteEndElement();               

                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("Level");
                xmlWriter.WriteString(level.LevelType.ToString());
                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndElement();
            }
        }

        public static void IfFileExistModifyXMLFileValues(Player player, Robot robot1, Robot robot2, Robot robot3, Level level)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);

            xmlDocument.SelectSingleNode("//Settings/Player/Name").InnerText = player.Name;
            xmlDocument.SelectSingleNode("//Settings/Robot1/Name").InnerText = robot1.Name;
            xmlDocument.SelectSingleNode("//Settings/Robot2/Name").InnerText = robot2.Name;
            xmlDocument.SelectSingleNode("//Settings/Robot3/Name").InnerText = robot3.Name;

            xmlDocument.SelectSingleNode("//Settings/Player/FigureColor").InnerText = player.FigureColor.ToString();
            xmlDocument.SelectSingleNode("//Settings/Robot1/FigureColor").InnerText = robot1.FigureColor.ToString();
            xmlDocument.SelectSingleNode("//Settings/Robot2/FigureColor").InnerText = robot2.FigureColor.ToString();
            xmlDocument.SelectSingleNode("//Settings/Robot3/FigureColor").InnerText = robot3.FigureColor.ToString();

            xmlDocument.SelectSingleNode("//Settings/Player/Balance").InnerText = player.Balance.ToString();
            xmlDocument.SelectSingleNode("//Settings/Robot1/Balance").InnerText = robot1.Balance.ToString();
            xmlDocument.SelectSingleNode("//Settings/Robot2/Balance").InnerText = robot2.Balance.ToString();
            xmlDocument.SelectSingleNode("//Settings/Robot3/Balance").InnerText = robot3.Balance.ToString();

            xmlDocument.SelectSingleNode("//Settings/Level").InnerText = level.LevelType.ToString();

            xmlDocument.Save(fileName);
        }
    }
}
