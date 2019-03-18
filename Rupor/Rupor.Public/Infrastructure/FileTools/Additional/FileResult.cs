using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Rupor.Public.Infrastructure.FileTools.Additional
{
    public class ToolsFileInfo
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public bool Picture { get; set; }
        public string Alt { get; set; }
        public string Extension { get; set; }
        public string Name { get; set; }
        public FileStream FileStream { get; set; }
    }
}