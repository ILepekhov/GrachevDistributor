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
                archivers = allFiles.Where(file => IsArchive(file)).ToDictionary(file => Path.GetFileNameWithoutExtension(file));
                pictures = allFiles.Where(file => IsPicture(file)).ToDictionary(file => Path.GetFileNameWithoutExtension(file));
            }
            catch { }

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

        private static bool IsArchive(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLower();

            return extension == ".rar" || extension == ".zip" || extension == "7z";
        }

        private static bool IsPicture(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLower();

            return extension == ".jpeg" || extension == ".gif" || extension == ".jpg" || extension == ".png" || extension == ".bmp";
        }

        #endregion
    }
}
