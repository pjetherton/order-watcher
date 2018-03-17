using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace PJE.WatchOrders
{
    public class FolderInput : IInput
    {
        private ICollection<string> _files = new List<string>();

        private readonly ICollection<string> _newFiles = new List<string>();

        private readonly string _path;

        private readonly bool _recursive;

        public FolderInput(string path, bool recursive)
        {
            _path = path;

            _recursive = recursive;
        }

        public InputFile GetNext()
        {
            while (true)
            {
                if (_newFiles.Any())
                {
                    var file = new InputFile {FileName = _newFiles.First()};
                    file.Contents = File.ReadAllText(file.FileName);
                    _newFiles.Remove(file.FileName);
                    return file;
                }

                var files = Directory.GetFiles(_path, "*.json",
                    _recursive
                        ? SearchOption.AllDirectories
                        : SearchOption.TopDirectoryOnly);

                foreach (var newFile in files.Where(f => !_files.Contains(f)))
                {
                    _newFiles.Add(newFile);
                }

                _files = files;

                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
        }
    }
}
