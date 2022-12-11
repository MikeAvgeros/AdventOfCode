using System.Collections.Generic;
using System.Linq;

namespace AoC2022.Models
{
    public class Directory
    {
        public string Name { get; set; }
        public List<FileInfo> Files { get; set; }
        public List<Directory> SubDirectories { get; set; }
        public Directory ParentDirectory { get; set; }
        public long TotalFileSize
        {
            get
            {
                return Files.Sum(file => file.FileSize) +
                       SubDirectories.Sum(subDirectory => subDirectory.TotalFileSize);
            }
        }

        public Directory(string name)
        {
            Name = name;
            Files = new List<FileInfo>();
            SubDirectories = new List<Directory>();
        }
    }
}