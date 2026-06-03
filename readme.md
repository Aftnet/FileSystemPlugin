# File System Plugin

Provides a unified API to interface with a platform's native file system
The API is modeled on System.IO and uses it where it makes sense to do so, with the addition of making IO operations asynchronous.

## Features

- Provides wrappers for native file system objects (StorageItem for UWP or File/FolderInfo) which expose most of their functionality and can be used from shared .net standard assemblies
- Supports getting the original file system objects from abstractions in the native assemblies that created them.
- Provides standardized access to key file system areas that all platforms provide (app install folder, app exclusive data store folder)
- Provides standardized ways to prompt the user to pick files/folders using each platform's native UI, complying with sandboxing restrictions

## Supported platforms

.NET10 on Windows/Android/macOS/iOS

## Examples

### Open a file in app install folder

```
var files = await CrossFileSystem.Current.LocalStorage().EnumerateFilesAsync();
var someFile = files.First();

var stream = await fileOne.OpenAsync(System.IO.FileAccess.Read);

```
