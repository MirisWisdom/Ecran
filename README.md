<h1 align="center">Écran: HCE Resolution Editor</h1>
<p align="center">
	<i>A resolution editor for HCE that allows any resolution and resolves refresh rate locks.</i>
	<br><br>
	<img src="https://user-images.githubusercontent.com/10241434/34657903-aa4a1382-f465-11e7-9a63-a755db5a2bf0.png">
	<br><br>
	<img src="https://ci.appveyor.com/api/projects/status/isv41b4d477tid28?svg=true">
</p>

## Features

* Ability to set any custom resolution.
* Ability to choose from preset resolutions. Configuration-driven resolutions list coming soon!
* Ability to attempt auto-detection and selection of the blam.sav.
* Removes the necessity of `-vidmode x,y` argument for custom resolutions.
* Removes the locked framerate that would be used be caused by the `-vidmode` argument.
* Built-in checksum forging for the selected `blam.sav` binary.
* Output console for debugging any potential problems/issues.

## Instructions

0. Make sure you have at least [.NET 4.5](https://www.microsoft.com/en-au/download/details.aspx?id=30653) installed if you're on Windows 7!
1. Launch the tool. Click on `Detect Blam.sav` to detect your profile. If that fails, click on `Browse blam.sav` and navigate to the `blam.sav` of your HCE profile.
2. Enter your desired solution, and click on `Update Profile`. Enjoy!

## Issues

Don't hesitate to post an issue if you find any bugs, or have any suggestions/feedback!
