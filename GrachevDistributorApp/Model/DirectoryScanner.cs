using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GrachevDistributorApp.Model
{
    public static class DirectoryScanner
    {
        #region Methods

        public static List<FilePair> Scan(string directoryPath)
        {
            var pairs = new List<FilePair>();
            // словарь - имя без расширения -> путь
            var archivers = new Dictionary<string, string>();
            var pictures = new Dictionary<string, string>();

            try
            {
                var allFiles = Directory.GetFiles(directoryPath, "*", SearchOption.TopDirectoryOnly);
                archivers = ToDictionary(allFiles.Where(file => IsArchive(file)));
                pictures = ToDictionary(allFiles.Where(file => IsPicture(file)));
            }
            catch
            {
                throw;
            }

            foreach (var fileName in archivers.Keys)
            {
                pictures.TryGetValue(fileName, out var picturePath);
                
                pairs.Add(new FilePair(archivers[fileName], picturePath));
            }

            foreach (var fileName in pictures.Keys)
            {
                if (archivers.ContainsKey(fileName)) continue;

                pairs.Add(new FilePair(null, pictures[fileName]));
            }

            return pairs;
        }

        #endregion

        #region Helpers

        private static Dictionary<string, string> ToDictionary(IEnumerable<string> files)
        {
            var result = new Dictionary<string, string>();

            foreach (var file in files)
            {
                var name = Path.GetFileNameWithoutExtension(file);

                if (!result.ContainsKey(name))
                {
                    result.Add(name, file);
                }
            }

            return result;
        }

        private static bool IsArchive(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLower();

            return extension == ".rar" || extension == ".zip" || extension == ".7z";
        }

        private static bool IsPicture(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLower();

            return extension == ".jpeg" || extension == ".gif" || extension == ".jpg" || extension == ".png" || extension == ".bmp";
        }

        #endregion
    }
}
