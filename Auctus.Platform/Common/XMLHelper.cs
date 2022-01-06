using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MyPlatform.Common
{
    public class XMLHelper
    {
        /// <summary>
        /// xml文件的绝对路径
        /// </summary>
        public string FilePath { get; set; }
        public XmlDocument LoadFile()
        {
            if (!File.Exists(FilePath))
            {
                throw new Exception(FilePath+"不存在！");
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(FilePath);
            return xmlDoc;
        }
        /// <summary>
        /// 获取节点列表
        /// </summary>
        /// <param name="nodeName">节点名称</param>
        /// <returns></returns>
        public XmlNodeList GetNodeList(string nodeName)
        {
            XmlDocument xmlDoc = LoadFile();
            return xmlDoc.SelectNodes(nodeName);
        }
        /// <summary>
        /// 获取符合条件的第一个节点的InnerText
        /// </summary>
        /// <param name="root">根节点名</param>
        /// <param name="attr">属性</param>
        /// <param name="attrValue">属性值</param>
        /// <returns></returns>
        public string GetAttr(string root,string attr,string attrValue)
        {
            XmlDocument xmlDoc = LoadFile();
            XmlNode  node = xmlDoc.SelectSingleNode(root);
            return Fn(node, attr, attrValue);         
        }
  
        public string Fn(XmlNode node,string attr,string attrValue)
        {
            XmlNodeList nodeList = node.ChildNodes;
            foreach (XmlNode xn in nodeList)
            {
                XmlElement xe = (XmlElement)xn;
                if (xe.GetAttribute(attr) == attrValue)
                {
                    return xe.InnerText;
                }
                else
                {
                    Fn(xn, attr, attrValue);
                }
            }
            return "";
        }
    }
}
