using GrachevDistributorApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GrachevDistributorApp.ViewModel
{
    public class MainViewModel : BaseBinding
    {
        #region Fields

        private readonly Renamer _renamer;

        private int _initialNameIndex;
        private bool _inProgress;

        #endregion

        #region Properties

        public ObservableCollection<FilePair> FilePairs { get; }

        public int InitialNameIndex
        {
            get => _initialNameIndex;
            set => SetValue(() => _initialNameIndex = value);
        }

        public bool InProgress
        {
            get => _inProgress;
            set => SetValue(() => _inProgress = value);
        }

        #endregion

        #region Commands

        public ICommand SelectDirectoryCommand { get; }

        public ICommand RemaneCommand { get; }

        #endregion

        #region .ctor

        public MainViewModel()
        {
            _renamer = new Renamer();

            FilePairs = new ObservableCollection<FilePair>();
            InitialNameIndex = 1;
        }

        #endregion

        #region Command handlers



        #endregion
    }
}
