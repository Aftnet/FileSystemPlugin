[![Build status](https://ci.appveyor.com/api/projects/status/6wab6l0x8rctc5w2?svg=true)](https://ci.appveyor.com/project/Aftnet/filesystemplugin)
[![NuGet version](https://img.shields.io/nuget/v/Xam.Plugin.FileSystem.svg)](https://badge.fury.io/nu/Xam.Plugin.FileSystem)

# File System Xamarin Plugin

Provides a unified API to interface with a platform's native file system
The API is modeled on System.IO and uses it where it makes sense to do so, with the addition of making IO operations asynchronous.

## Features

- Provides wrappers for native file system objects (StorageItem for UWP or File/FolderInfo) which expose most of their functionality and can be used from shared .net standard assemblies
- Supports getting the original file system objects from abstractions in the native assemblies that created them.
- Provides standardized access to key file system areas that all platforms provide (app install folder, app exclusive data store folder)
- ToDo: Provides standardized ways to prompt the user to pick files/folders using each platform's native UI

## Supported platforms

- UWP
- .Net 4.6.1
- iOS
- Android

## Usage
