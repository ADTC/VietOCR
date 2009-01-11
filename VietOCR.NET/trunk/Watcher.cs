using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace VietOCR.NET
{
    public class Watcher
    {
        private FileInfo watchFolder;
        private Queue<String> queue;

        //[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public Watcher(Queue<String> q, string folder)
        {
            queue = q;

            // Create a new FileSystemWatcher and set its properties.
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = folder;
            /* Watch for changes in LastAccess and LastWrite times, and 
               the renaming of files or directories. */
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            // Only watch tif files.
            //watcher.Filter = "*.tif"; // commented out since multiple filters not possible with Filter property

            // Add event handlers.
            //watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            //watcher.Deleted += new FileSystemEventHandler(OnChanged);
            //watcher.Renamed += new RenamedEventHandler(OnRenamed);

            // Begin watching.
            watcher.EnableRaisingEvents = true;
        }

        // Define the event handlers.
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                if (e.Name.ToLower().EndsWith(".tif"))
                {
                    queue.Enqueue(e.FullPath);
                }
            }
        }

        private void OnRenamed(object source, RenamedEventArgs e)
        {
            // Specify what is done when a file is renamed.
            Console.WriteLine("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
        }
    }
}