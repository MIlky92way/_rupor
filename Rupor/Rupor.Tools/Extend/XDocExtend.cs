﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Rupor.Tools.Extend
{
    /// <summary>
    /// Helpers for XDocument.
    /// All methods from: 
    /// <see cref="https://github.com/codehollow/FeedReader"/>
    /// </summary>
    public static class XDocExtend
    {        
        /// <summary>
        /// Gets the value of an xml element encoded as utf8
        /// </summary>
        /// <param name="element">the xml element</param>
        /// <returns>value of the element utf8 encoded</returns>
        public static string GetValue(this XElement element)
        {
            if (element == null)
                return null;

            return element.Value;
        }

        /// <summary>
        /// Gets the value of the element "name"
        /// </summary>
        /// <param name="element">xml element</param>
        /// <param name="name">name of the element</param>
        /// <returns>the value of the XElement</returns>
        public static string GetValue(this XElement element, string name)
        {
            return element?.GetElement(name).GetValue();
        }

        /// <summary>
        /// Gets the value of the element "name"
        /// </summary>
        /// <param name="element">xml element</param>
        /// <param name="namespacePrefix">the namespace prefix of the element that should be returned</param>
        /// <param name="name">name of the element</param>
        /// <returns>the value of the XElement</returns>
        public static string GetValue(this XElement element, string namespacePrefix, string name)
        {
            return element?.GetElement(namespacePrefix, name).GetValue();
        }

        /// <summary>
        /// Gets the value of the given attribute
        /// </summary>
        /// <param name="attribute">the xml attribute</param>
        /// <returns>value</returns>
        public static string GetValue(this XAttribute attribute)
        {
            if (attribute == null)
                return null;

            return attribute.Value;
        }

        /// <summary>
        /// Gets the value of the attribute <paramref name="name"/>
        /// </summary>
        /// <param name="element">the xml element</param>
        /// <param name="name">the name of the attribute</param>
        /// <returns>value of the attribute</returns>
        public static string GetAttributeValue(this XElement element, string name)
        {
            return element.GetAttribute(name)?.GetValue();
        }

        /// <summary>
        /// Gets the attribute <paramref name="name"/> of the given XElement
        /// </summary>
        /// <param name="element">the xml element</param>
        /// <param name="name">the name of the attribute</param>
        /// <returns>the xml attribute</returns>
        public static XAttribute GetAttribute(this XElement element, string name)
        {
            var splitted = SplitName(name);
            return element?.GetAttribute(splitted.Item1, splitted.Item2);
        }

        /// <summary>
        /// Gets the attribute with the namespace <paramref name="namespacePrefix"/> and name <paramref name="name"/> of the given XElement
        /// </summary>
        /// <param name="element">the xml element</param>
        /// <param name="namespacePrefix">the namespace prefix of the attribute</param>
        /// <param name="name">the name of the attribute</param>
        /// <returns>the xml attribute</returns>
        public static XAttribute GetAttribute(this XElement element, string namespacePrefix, string name)
        {
            if (string.IsNullOrEmpty(namespacePrefix))
                return element.Attribute(name);

            var namesp = element.GetNamespacePrefix(namespacePrefix);
            return element.Attribute(namesp + name);
        }

        /// <summary>
        /// Gets the element of the given XElement
        /// </summary>
        /// <param name="element">the xml element</param>
        /// <param name="name">Name of the element that should be returned</param>
        /// <returns>the "name" element of the XElement</returns>
        public static XElement GetElement(this XElement element, string name)
        {
            var splitted = SplitName(name);
            return element?.GetElement(splitted.Item1, splitted.Item2);
        }

        /// <summary>
        /// Gets the element of the given XElement
        /// </summary>
        /// <param name="element">the xml element</param>
        /// <param name="namespacePrefix">the namespace prefix of the element that should be returned</param>
        /// <param name="name">Name of the element that should be returned</param>
        /// <returns>the "name" element with the prefix "namespacePrefix" of the XElement</returns>
        public static XElement GetElement(this XElement element, string namespacePrefix, string name)
        {
            var namesp = element.GetNamespacePrefix(namespacePrefix);
            if (namesp == null) return null;
            return element.Element(namesp + name);
        }

        /// <summary>
        /// Gets all elements of the given XElement
        /// </summary>
        /// <param name="element">the xml element</param>
        /// <param name="name">Name of the elements that should be returned</param>
        /// <returns>all "name" elements of the given XElement</returns>
        public static IEnumerable<XElement> GetElements(this XElement element, string name)
        {
            var splitted = SplitName(name);
            return element.GetElements(splitted.Item1, splitted.Item2);
        }

        /// <summary>
        /// Gets all elements of the given XElement
        /// </summary>
        /// <param name="element">the xml element</param>
        /// <param name="namespacePrefix">the namespace prefix of the elements that should be returned</param>
        /// <param name="name">Name of the elements that should be returned</param>
        /// <returns>all "name" elements of the given XElement</returns>
        public static IEnumerable<XElement> GetElements(this XElement element, string namespacePrefix, string name)
        {
            var namesp = element.GetNamespacePrefix(namespacePrefix);
            if (namesp == null) return null;
            return element.Elements(namesp + name);
        }

        /// <summary>
        /// Gets the namespace prefix of the given XElement
        /// </summary>
        /// <param name="element">the xml element</param>
        /// <returns>the namespace prefix</returns>
        public static XNamespace GetNamespacePrefix(this XElement element)
        {
            return element.GetNamespacePrefix(null);
        }

        /// <summary>
        /// Gets the namespace prefix of the given XElement, if namespacePrefix is null or empty, it returns the default namespace.
        /// </summary>
        /// <param name="element">the xml element</param>
        /// <param name="namespacePrefix">the namespace prefix</param>
        /// <returns>the namespace prefix or default namespace if the <paramref name="namespacePrefix"/> is null or empty</returns>
        public static XNamespace GetNamespacePrefix(this XElement element, string namespacePrefix)
        {
            var namesp = string.IsNullOrWhiteSpace(namespacePrefix) ? element.GetDefaultNamespace() : element.GetNamespaceOfPrefix(namespacePrefix);
            return namesp;
        }

        /// <summary>
        /// splits the name into namespace and name if it contains a :
        /// if it does not contain a namespace, item1 is null and item2 is the original name
        /// </summary>
        /// <param name="name">the input name</param>
        /// <returns>splitted namespace and name, item1 is null if namespace is empty</returns>
        private static Tuple<string, string> SplitName(string name)
        {
            string namesp = null;
            if (name.Contains(":"))
            {
                int pos = name.IndexOf(':');
                namesp = name.Substring(0, pos);
                name = name.Substring(pos + 1);
            }

            return new Tuple<string, string>(namesp, name);
        }
    }
}
