﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrachevDistributorApp.Model
{
    public sealed class Renamer
    {
        #region Fields

        private readonly List<FilePair> _pairs;

        #endregion

        #region Properties

        public IEnumerable<FilePair> Pairs => _pairs;

        #endregion

        #region .ctor

        public Renamer()
        {
            _pairs = new List<FilePair>();
        }

        #endregion

        #region Methods

        public void LoadFiles(string directoryPath)
        {
            if (string.IsNullOrEmpty(directoryPath)) return;

            _pairs.Clear();

            var newPairs = DirectoryScanner.Scan(directoryPath);

            if (newPairs.Any())
            {
                _pairs.AddRange(newPairs.OrderBy(p => p.PairName));
            }
        }

        public IEnumerable<FilePair> GetFilePairs() => _pairs;

        public int RenameFiles(int initialNameIndex)
        {
            if (!_pairs.Any()) return initialNameIndex;

            foreach (var pair in _pairs)
            {
                if (!pair.CanRename) continue;

                try
                {
                    File.Move(pair.ArchiveFilePath, pair.ArchiveFilePath.Replace(pair.PairName, initialNameIndex.ToString()));
                }
                catch
                {
                    continue;
                }

                try
                {
                    File.Move(pair.PictureFilePath, pair.PictureFilePath.Replace(pair.PairName, initialNameIndex.ToString()));
                }
                catch
                {
                    File.Move(pair.ArchiveFilePath.Replace(pair.PairName, initialNameIndex.ToString()), pair.ArchiveFilePath);
                    continue;
                }

                initialNameIndex++;
            }

            return initialNameIndex;
        }

        #endregion
    }
}
