/**
 * Copyright (c) 2019 Emilian Roman
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would be
 *    appreciated but is not required.
 * 2. Altered source versions must be plainly marked as such, and must not be
 *    misrepresented as being the original software.
 * 3. This notice may not be removed or altered from any source distribution.
 */

using System.IO;
using System.IO.Compression;
using System.Linq;
using static System.Diagnostics.Process;
using static System.IO.Compression.CompressionLevel;
using static System.IO.Compression.ZipArchiveMode;
using static System.IO.Compression.ZipFile;
using static System.IO.Directory;
using static System.IO.Path;
using static System.IO.SearchOption;
using static SPV3.CLI.Console;

namespace SPV3.CLI
{
  /// <summary>
  ///   Conducts core and data compilation to manifest and compressed packages.
  /// </summary>
  public static class Compiler
  {
    /// <summary>
    ///   Compiles the data in the source directory to packages and a manifest in the target directory.
    /// </summary>
    /// <param name="source">
    ///   Source directory, expected to contain SPV3 data such as the executables and maps.
    /// </param>
    /// <param name="target">
    ///   Target directory, which is expected to be distributed to the end-user.
    /// </param>
    public static void Compile(string source, string target)
    {
      if (!Exists(target))
        CreateDirectory(target);

      /**
       * We declare the manifest file to be in the target directory, because the target directory is expected to be the
       * distributed with the installer.
       * 
       * We recursively retrieve a list of directories in the source directory, for the purpose of creating packages and
       * manifest entries for each of them.
       * 
       * We must also add the source directory itself to the list, to ensure that the top-level SPV3/HCE binaries are
       * packaged!
       * 
       * Given that the manifest is represented by 0x00, the subsequent packages should be represented by a >=1 ID. 
       */

      var manifest    = (Manifest) Combine(target, Paths.Files.Manifest);
      var directories = GetDirectories(source, "*", AllDirectories).ToList();
      var i           = 0x1;

      directories.Add(source);

      /**
       * For any subdirectory found in the source, we create a DEFLATE package for it containing its files. We also
       * declare an entry for it in the manifest. Given that the files will be verified by the LOADER, we also declare
       * entries for them in the manifest, with necessary information such as the name and size.
       * 
       * The manifest information seeks to be compatible and portable, by recording only relative paths to the source
       * directory.
       *
       * Each package represents a subdirectory in the source directory; hence, we must declare an entry for the
       * respective directory's path relative to the source.
       *
       * We also declare an entry for each file within the respective subdirectory, by recording its name and size.
       * This effectively permits two scenarios:
       *
       * -   the INSTALLER can lay out the directories when installing the packages, by combining the particular
       *     target path with the relative path declared for the package.
       * -   the LOADER can infer the absolute path of a file and then match its size against the one declared in the
       *     manifest, as part of its asset verification routine.
       */

      foreach (var directory in directories)
      {
        Debug("Preparing to compile directory - " + directory);

        /**
         * We record the package's name on the filesystem to the manifest. This permits the INSTALLER to seek the
         * package within the source directory, and then extract its data to the target directory.
         * 
         * The packages are conventionally identified by the hexadecimal equivalent of the inbound integer, with ".bin" as
         * the prefix, just like the manifest file.
         */

        var name    = $"0x{i:x2}.bin";
        var files   = GetFiles(directory, "*.*", TopDirectoryOnly);
        var package = (Manifest.Package) name;
        var archive = (File) Combine(target, name);

        if (archive.Exists())
        {
          archive.Delete();
          Info("Deleted existing archive: " + archive);
        }

        /**
         * We create an archive & package entry for the files. Note that the path does not need to be declared for the
         * file's entry in the package, because it does not reside within a subdirectory.
         */

        using (var deflate = Open(archive.Path, Create))
        {
          foreach (var file in files)
          {
            var fileName = GetFileName(file);

            deflate.CreateEntryFromFile(file, fileName, Optimal);

            Debug("Created archive entry for file - " + file);

            /*
             * For the LOADER's asset verification routine, we must create an entry for the file in the manifest. The
             * respective entry must represent the filename and file size on the filesystem. This allows the LOADER to
             * infer the full path on the filesystem, and compare the file's actual size versus the declared one!
             */

            package.Entries.Add(new Manifest.Package.Entry
            {
              Name = fileName,
              Size = new FileInfo(file).Length
            });

            Debug("Created package entry for file - " + file);
          }
        }

        /*
         * We MUST declare the RELATIVE path as the value, by inferring the relative path from the absolute path
         * which belongs to the source directory.
         *
         * X:\Development\SPV3                 - source absolute path
         * X:\Development\SPV3\content\Gallery - current directory path
         *                    |--------------| - relative path to source
         * 
         * With the above diagram in mind, we can essentially remove the source path portion (and trailing slash)
         * from the full absolute path, and thus declare the relative path as the package's path.
         *
         * NOTE: The aforementioned procedure is not carried out if this iteration's directory is the source directory
         * itself.
         */

        package.Path = directory.Equals(source)
          ? string.Empty
          : directory.Substring(source.TrimEnd('/', '\\').Length + 1);

        manifest.Packages.Add(package);

        Debug("Added package for the current directory to the manifest.");

        i++;
      }

      if (manifest.Exists())
      {
        manifest.Delete();
        Info("Deleted existing manifest binary.");
      }

      manifest.Save();

      Debug("Manifest successfully saved. The loader can now install and verify the SPV3.2 assets.");

      /**
       * For subsequent installation convenience, we will make a copy of the current CLI to the target directory.
       */

      var cli = (File) GetCurrentProcess().MainModule.FileName;
      cli.CopyTo(target);

      Debug("Loader executable has been successfully copied. The packages can now be distributed and installed!");
    }
  }
}