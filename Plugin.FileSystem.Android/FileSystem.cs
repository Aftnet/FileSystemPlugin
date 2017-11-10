﻿using Plugin.Filesystem;
using Plugin.Filesystem.Abstractions;
using System;

namespace Plugin.FileSystem
{
    public class FileSystem : FileSystemBase
    {
        public override IDirectoryInfo LocalStorage => GetSpecialFolder(Environment.SpecialFolder.LocalApplicationData);

        public override IDirectoryInfo RoamingStorage => GetSpecialFolder(Environment.SpecialFolder.ApplicationData);

        public override IDirectoryInfo InstallLocation => new DirectoryInfo(new System.IO.DirectoryInfo(Android.App.Application.Context.DataDir.AbsolutePath));

        private IDirectoryInfo GetSpecialFolder(Environment.SpecialFolder specialFolder)
        {
            var path = Environment.GetFolderPath(specialFolder);
            var folder = new System.IO.DirectoryInfo(path);
            return new DirectoryInfo(folder);
        }
    }
}