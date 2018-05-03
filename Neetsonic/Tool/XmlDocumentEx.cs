using System.Xml;

namespace Neetsonic.Tool
{
    /// <summary>
    /// XML文档操作扩展
    /// Neetsonic 
    /// 2018.5.3
    /// </summary>
    public static class XmlDocumentExtension
    {
        /// <summary>
        /// 获取第一个匹配的指定名称的元素文本
        /// </summary>
        /// <param name="doc">xml文档对象</param>
        /// <param name="elementName">元素名称</param>
        /// <param name="defVal">默认值</param>
        /// <returns>找到则返回对应值，否则返回默认值</returns>
        public static string GetElementValue(this XmlDocument doc, string elementName, string defVal)
        {
            XmlNodeList elems = doc.GetElementsByTagName(elementName);
            return elems.Count > 0 ? elems[0].InnerText : defVal;
        }
        /// <summary>
        /// 创建仅带有文本的简单元素节点
        /// </summary>
        /// <param name="doc">xml文档对象</param>
        /// <param name="elementName">元素名称</param>
        /// <param name="innerText">元素文本</param>
        /// <returns>创建好的元素</returns>
        public static XmlElement CreateSingleElement(this XmlDocument doc, string elementName, string innerText)
        {
            XmlElement elem = doc.CreateElement(elementName);
            elem.InnerText = innerText;
            return elem;
        }
    }
}