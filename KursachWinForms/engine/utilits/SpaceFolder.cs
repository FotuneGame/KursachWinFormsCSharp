using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Engine.Utilits
{
    public static class SpaceFolder
    {
        
       public static string[]  getDefaultModels(string folder_path = "./DefaultModel")
        {
           
            DirectoryInfo dir_info = new DirectoryInfo(folder_path);
            if (dir_info.Exists)
            {
                var list_file = dir_info.GetFiles();
                int i = 0;
                string[] str = new string[list_file.Length];
                foreach (var file in list_file)
                {
                    if (file.Extension == ".obj")
                        str[i++] =file.Name.Split('.')[0];
                }

                return str;
            }
            return new string[0];
        }
    }
}
