using System.IO;
using System.Linq;

namespace GrachevDistributorApp.Model
{
    public class FilePair
    {
        #region Properties

        public string PairName { get; private set; }

        public string ArchiveFileName { get; private set; }

        public string ArchiveFilePath { get; private set; }

        public bool ArchiveFileExists { get; private set; }

        public string PictureFileName { get; private set; }

        public string PictureFilePath { get; private set; }

        public bool PictureFileExists { get; private set; }

        public bool CanRename => ArchiveFileExists && PictureFileExists;

        #endregion

        #region .ctor

        public FilePair(string archiveFilePath = null, string pictureFilePath = null)
        {
            PairName = null;
            InitiateArchiveFileData(archiveFilePath);
            InitiatePictureFileData(pictureFilePath);
        }

        #endregion

        #region Helpers

        private void InitiateArchiveFileData(string archiveFilePath)
        {
            if (archiveFilePath == null)
            {
                ArchiveFileExists = false;
                ArchiveFilePath = null;
                ArchiveFileName = null;
            }
            else
            {
                ArchiveFileExists = true;
                ArchiveFilePath = archiveFilePath;
                ArchiveFileName = Path.GetFileName(archiveFilePath);
                PairName = Path.GetFileNameWithoutExtension(archiveFilePath);
            }
        }

        private void InitiatePictureFileData(string pictureFilePath)
        {
            if (pictureFilePath == null)
            {
                PictureFileExists = false;
                PictureFilePath = null;
                PictureFileName = null;
            }
            else
            {
                PictureFileExists = true;
                PictureFilePath = pictureFilePath;
                PictureFileName = Path.GetFileName(pictureFilePath);
                PairName = Path.GetFileNameWithoutExtension(pictureFilePath);
            }
        }

        #endregion
    }
}
