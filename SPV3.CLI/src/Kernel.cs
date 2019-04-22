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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SPV3.CLI.Exceptions;
using static System.Environment;
using static SPV3.CLI.Console;
using static SPV3.CLI.Paths;

namespace SPV3.CLI
{
  /// <summary>
  ///   Class used for bootstrapping the SPV3 loading procedure.
  /// </summary>
  public static class Kernel
  {
    /**
     * Inic.txt can be located across multiple locations on the filesystem; however, SPV3 only deals with the one in
     * the working directory -- hence the name!
     */
    private static readonly Initiation RootInitc = (Initiation) Path.Combine(CurrentDirectory, Files.Initiation);

    /// <summary>
    ///   Invokes the SPV3 loading procedure.
    /// </summary>
    public static void Bootstrap(Executable executable)
    {
      var configuration = (Configuration) Files.Configuration;

      if (!configuration.Exists())
        configuration.Save(); /* gracefully create new configuration */

      configuration.Load();

      if (!configuration.Kernel.SkipVerifyMainAssets)
        VerifyMainAssets();
      else
        Info("Skipping Kernel.VerifyMainAssets");

      if (!configuration.Kernel.SkipInvokeCoreTweaks)
        InvokeCoreTweaks(executable);
      else
        Info("Skipping Kernel.InvokeCoreTweaks");

      if (!configuration.Kernel.SkipResumeCheckpoint)
        ResumeCheckpoint(executable);
      else
        Info("Skipping Kernel.ResumeCheckpoint");

      if (!configuration.Kernel.SkipSetShadersConfig)
        SetShadersConfig(configuration);
      else
        Info("Skipping Kernel.SkipSetShadersConfig");

      if (!configuration.Kernel.SkipPatchLargeAAware)
        PatchLargeAAware(executable);
      else
        Info("Skipping Kernel.SkipPatchLargeAAware");

      if (!configuration.Kernel.SkipInvokeExecutable)
        InvokeExecutable(executable);
      else
        Info("Skipping Kernel.InvokeExecutable");
    }

    /// <summary>
    ///   Invokes the SPV3.2 data verification routines.
    /// </summary>
    private static void VerifyMainAssets()
    {
      /**
       * It is preferable to whitelist the type of files we would like to verify. The focus would be to skip any files
       * which are expected to be changed.
       *
       * For example, if SPV3.2 were to be distributed with a configuration file, then changing its contents would
       * result in an asset verification error. Additionally, changing the CLI executable by updating it could result in
       * the same error.
       */

      var whitelist = new List<string>
      {
        ".map",      /* map resources */
        "haloce.exe" /* game executable */
      };

      /**
       * Through the use of the manifest that was copied to the installation directory, the loader can infer the list of
       * SPV3 files on the filesystem that it must verify. Verification is done through a simple size comparison between
       * the size of the file on the filesystem and the one declared in the manifest.
       *
       * This routine relies on combining...
       *
       * - the path of the working directory; with
       * - the package's declared relative path; with
       * - the filename of the respective file
       *
       * ... to determine the absolute path of the file on the filesystem:
       * 
       * X:\Installations\SPV3\gallery\Content\editbox1024.PNG
       * |----------+---------|-------+-------|-------+-------|
       *            |                 |               |
       *            |                 |               + - File on the filesystem
       *            |                 + ----------------- Package relative path
       *            + ----------------------------------- Working directory
       */

      var manifest = (Manifest) Path.Combine(CurrentDirectory, Files.Manifest);

      /**
       * This shouldn't be an issue in conventional SPV3 installations; however, for existing/current SPV3 installations
       * OR installations that weren't conducted by the installer, the manifest will likely not be present. As such, we
       * have no choice but to skip the verification mechanism.
       */
      if (!manifest.Exists()) return;

      manifest.Load();

      Info("Found manifest file - proceeding with data verification ...");

      foreach (var package in manifest.Packages)
      foreach (var entry in package.Entries)
      {
        if (!whitelist.Any(entry.Name.Contains)) /* skip verification if current file isn't in the whitelist */
          continue;

        var absolutePath = Path.Combine(CurrentDirectory, package.Path, entry.Name);
        var expectedSize = entry.Size;
        var actualSize   = new FileInfo(absolutePath).Length;

        if (expectedSize == actualSize) continue;

        Info($"Size mismatch {entry.Name} (expect: {expectedSize}, actual: {actualSize}).");
        throw new AssetException($"Size mismatch {entry.Name} (expect: {expectedSize}, actual: {actualSize}).");
      }
    }

    /// <summary>
    ///   Invokes core improvements to the auto-detected profile, such as auto max resolution and gamma fixes. This is
    ///   NOT done when a profile does not exist/cannot be found!
    /// </summary>
    /// <param name="executable"></param>
    private static void InvokeCoreTweaks(Executable executable)
    {
      try
      {
        var lastprof = (LastProfile) Path.Combine(executable.Profile.Path, Files.LastProfile);

        if (!lastprof.Exists()) return;

        lastprof.Load();

        var profile = (Profile) Path.Combine(
          executable.Profile.Path,
          Directories.Profiles,
          lastprof.Profile,
          Files.Profile
        );

        if (!profile.Exists()) return;

        profile.Load();

        Info("Auto-loaded HCE profile. Proceeding to apply core tweaks ...");

        profile.Video.Resolution.Width  = (ushort) Screen.PrimaryScreen.Bounds.Width;
        profile.Video.Resolution.Height = (ushort) Screen.PrimaryScreen.Bounds.Height;
        profile.Video.FrameRate         = Profile.ProfileVideo.VideoFrameRate.VsyncOff; /* ensure no FPS locking */
        profile.Video.Particles         = Profile.ProfileVideo.VideoParticles.High;
        profile.Video.Quality           = Profile.ProfileVideo.VideoQuality.High;
        profile.Video.Effects.Specular  = true;
        profile.Video.Effects.Shadows   = true;
        profile.Video.Effects.Decals    = true;

        profile.Save();

        Info("Patched video resolution width  - " + profile.Video.Resolution.Width);
        Info("Patched video resolution height - " + profile.Video.Resolution.Height);
        Info("Patched video frame rate        - " + profile.Video.FrameRate);
        Info("Patched video quality           - " + profile.Video.Particles);
        Info("Patched video texture           - " + profile.Video.Quality);
        Info("Patched video effect - specular - " + profile.Video.Effects.Specular);
        Info("Patched video effect - shadows  - " + profile.Video.Effects.Shadows);
        Info("Patched video effect - decals   - " + profile.Video.Effects.Decals);
      }
      catch (Exception e)
      {
        Error(e.Message + " -- CORE TWEAKS WILL NOT BE APPLIED.");
      }
    }

    /// <summary>
    ///   Invokes the profile & campaign auto-detection mechanism.
    /// </summary>
    private static void ResumeCheckpoint(Executable executable)
    {
      var lastprof = (LastProfile) Path.Combine(executable.Profile.Path, Files.LastProfile);

      if (!lastprof.Exists()) return;

      lastprof.Load();

      Info("Found lastprof file - proceeding with checkpoint detection ...");

      var playerDat = (Progress) Path.Combine(
        executable.Profile.Path,
        Directories.Profiles,
        lastprof.Profile,
        Files.Progress
      );

      if (!playerDat.Exists()) return;

      Info("Found checkpoint file - proceeding with resuming campaign ...");

      playerDat.Load();

      try
      {
        RootInitc.Mission    = playerDat.Mission;
        RootInitc.Difficulty = playerDat.Difficulty;
        RootInitc.Save();

        Info("Resumed campaign MISSION    - " + playerDat.Mission);
        Info("Resumed campaign DIFFICULTY - " + playerDat.Difficulty);
      }
      catch (UnauthorizedAccessException e)
      {
        Error(e.Message + " -- CAMPAIGN WILL NOT RESUME!");
      }
    }

    /// <summary>
    ///   Applies the post-processing settings.
    /// </summary>
    private static void SetShadersConfig(Configuration configuration)
    {
      try
      {
        RootInitc.PostProcessing = configuration.PostProcessing;
        RootInitc.Save();

        Info("Applied PP settings for MXAO        - " + RootInitc.PostProcessing.Mxao);
        Info("Applied PP settings for DOF         - " + RootInitc.PostProcessing.Dof);
        Info("Applied PP settings for Motion Blur - " + RootInitc.PostProcessing.MotionBlur);
        Info("Applied PP settings for Lens Flares - " + RootInitc.PostProcessing.DynamicLensFlares);
        Info("Applied PP settings for Volumetrics - " + RootInitc.PostProcessing.Volumetrics);
        Info("Applied PP settings for Lens Dirt   - " + RootInitc.PostProcessing.LensDirt);
      }
      catch (UnauthorizedAccessException e)
      {
        Error(e.Message + " -- POST PROCESSING WILL NOT BE APPLIED!");
      }
    }

    /// <summary>
    ///   Patches HCE executable for Large Address Aware.
    /// </summary>
    private static void PatchLargeAAware(Executable executable)
    {
      try
      {
        Info("Attempting LAA patching on the HCE executable ...");

        using (var fs = new FileStream(executable.Path, FileMode.Open, FileAccess.ReadWrite))
        using (var bw = new BinaryWriter(fs))
        {
          fs.Position = 0x136;
          bw.Write((byte) 0x2F);
        }

        Info("Successfully conducted LAA patching on the HCE executable!");
      }
      catch (Exception e)
      {
        Error(e.Message + " -- LAA PATCH WILL NOT BE APPLIED!");
      }
    }

    /// <summary>
    ///   Invokes the HCE executable.
    /// </summary>
    private static void InvokeExecutable(Executable executable)
    {
      Info("Attempting to start executable with the following parameters:");

      if (executable.Video.Width > 0)
        Info("+   Video.Width      - " + executable.Video.Width);
      if (executable.Video.Height > 0)
        Info("+   Video.Height     - " + executable.Video.Height);

      if (executable.Video.Refresh > 0)
        Info("+   Video.Refresh    - " + executable.Video.Refresh);

      if (executable.Video.Adapter > 0)
        Info("+   Video.Adapter    - " + executable.Video.Adapter);

      if (executable.Video.Window)
        Info("+   Video.Window     - " + executable.Video.Window);

      if (executable.Debug.Console)
        Info("+   Debug.Console    - " + executable.Debug.Console);

      if (executable.Debug.Developer)
        Info("+   Debug.Developer  - " + executable.Debug.Developer);

      if (executable.Debug.Developer)
        Info("+   Debug.Screenshot - " + executable.Debug.Developer);

      if (!string.IsNullOrWhiteSpace(executable.Profile.Path))
        Info("+   Profile.Path     - " + executable.Profile.Path);

      executable.Start();

      Info("And... we're done!");
    }
  }
}