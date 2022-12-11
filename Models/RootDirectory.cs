using System.Collections.Generic;
using System.Linq;

namespace AoC2022.Models
{
    public class RootDirectory : Directory
    {
        public long CalculateTotalSize()
        {
            return GetDirectoriesToDelete().Sum(x => x.TotalFileSize);
        }

        private IEnumerable<Directory> GetDirectoriesToDelete()
        {
            var directories = new List<Directory>();
            foreach (var subDirectory in SubDirectories)
            {
                directories.AddRange(GetAllDirectories(subDirectory));
            }

            return directories.Where(x => x.TotalFileSize <= 100000);
        }

        public static IEnumerable<Directory> GetAllDirectories(Directory directory)
        {
            var result = new List<Directory> {directory};

            foreach (var sub in directory.SubDirectories)      
            {
                result.AddRange(GetAllDirectories(sub));
            }

            return result;
        }

        public RootDirectory(string name) : base(name)
        {
        }
    }
}