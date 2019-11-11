using GrachevDistributorApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace GrachevDistributorApp.ViewModel
{
    public class MainViewModel : BaseBinding
    {
        #region Fields

        private readonly Renamer _renamer;

        private int _initialNameIndex;
        private bool _notInProgress;

        #endregion

        #region Properties

        public ObservableCollection<FilePair> FilePairs { get; }

        public int InitialNameIndex
        {
            get => _initialNameIndex;
            set => SetValue(() => _initialNameIndex = value);
        }

        public bool NotInProgress
        {
            get => _notInProgress;
            set => SetValue(() => _notInProgress = value);
        }

        #endregion

        #region Commands

        public ICommand SelectDirectoryCommand { get; }

        public ICommand RenameCommand { get; }

        #endregion

        #region .ctor

        public MainViewModel()
        {
            _renamer = new Renamer();

            FilePairs = new ObservableCollection<FilePair>();
            InitialNameIndex = 1;

            SelectDirectoryCommand = CreateCommand(ExecuteSelectDirectoryCommand);
            RenameCommand = CreateCommand(ExecuteRenameCommand, CanExecuteRenameCommand);

            NotInProgress = true;
        }

        #endregion

        #region Command handlers

        private async void ExecuteSelectDirectoryCommand(object _)
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FilePairs.Clear();

                try
                {
                    await Task.Run(() => _renamer.LoadFiles(dialog.SelectedPath));
                }
                catch (Exception e)
                {
                    MessageBox.Show("Побробности: " + e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                foreach (var pair in _renamer.GetFilePairs())
                    FilePairs.Add(pair);
            }
        }

        private bool CanExecuteRenameCommand(object _) => FilePairs.Any();

        private async void ExecuteRenameCommand(object _)
        {
            NotInProgress = false;

            InitialNameIndex = await Task.Run(() => _renamer.RenameFiles(InitialNameIndex));

            NotInProgress = true;

            MessageBox.Show("Переименование завершено", "Финиш", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion
    }
}
