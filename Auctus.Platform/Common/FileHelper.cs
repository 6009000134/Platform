using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.Common
{
    public class FileHelper
    {
        public bool IsExistDirectory(string directory)
        {
            if (Directory.Exists(directory))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
