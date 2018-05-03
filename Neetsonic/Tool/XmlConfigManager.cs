using System.Configuration;
using System.IO;
using System.Xml;

namespace Neetsonic.Tool
{
    /// <summary>
    /// XML文件类型的配置文件管理器
    /// Neetsonic
    /// 2018.5.3
    /// </summary>
    public sealed class XmlConfigManager
    {
        private const string RootNodeName = @"Root"; // 根节点名称

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="filePath">配置文件路径</param>
        public XmlConfigManager(string filePath)
        {
            FilePath = filePath;
            if(!File.Exists(FilePath))
            {
                XmlDocument xmlDoc = new ConfigXmlDocument();
                xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration(@"1.0", @"gb2312", null));
                xmlDoc.AppendChild(xmlDoc.CreateElement(RootNodeName));
                xmlDoc.Save(FilePath);
            }
        }

        /// <summary>
        /// 配置文件路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 保存配置项目
        /// </summary>
        /// <param name="elementName">项目名称</param>
        /// <param name="value">项目值</param>
        public void SaveConfig(string elementName, string value)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(FilePath);
            XmlNodeList elems = xmlDoc.GetElementsByTagName(elementName);
            if(elems.Count > 0) elems[0].InnerText = value; // 存在项目，替换文本
            else xmlDoc.SelectSingleNode(RootNodeName)?.AppendChild(xmlDoc.CreateSingleElement(elementName, value)); // 不存在项目，向根节点添加该项目
            xmlDoc.Save(FilePath);
        }
        /// <summary>
        /// 读取配置信息文件
        /// </summary>
        /// <param name="elementName">项目名称</param>
        /// <param name="defVal">默认值</param>
        /// <returns>项目的值</returns>
        public string ReadConfig(string elementName, string defVal)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(FilePath);
            return xmlDoc.GetElementValue(elementName, defVal);
        }
    }
}