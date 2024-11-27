<div align="center">

![RHLauncher logo](rhlauncher.png)

# Rusty Hearts Launcher
[![License](https://img.shields.io/github/license/JuniorDark/RustyHearts-Launcher?color=green)](LICENSE)
[![Build](https://github.com/JuniorDark/RustyHearts-Launcher/actions/workflows/build.yml/badge.svg)](https://github.com/JuniorDark/RustyHearts-Launcher/actions/workflows/build.yml)
[![GitHub release (latest by date)](https://img.shields.io/github/v/release/JuniorDark/RustyHearts-Launcher)](https://github.com/JuniorDark/RustyHearts-Launcher/releases/latest) <a href="https://github.com/JuniorDark/RustyHearts-Launcher/releases">
 
## Introduction
Rusty Hearts Launcher is a custom launcher for the Rusty Hearts game client. It provides several features, including self-updating, automatic game updates, account registration, and a news window.

## Preview
![image](screenshoots/preview-01.png)
![image](screenshoots/preview-02.png)
![image](screenshoots/preview-03.png)
![image](screenshoots/preview-04.png)
![image](screenshoots/preview-ko.png)

## Table of Contents
* [Features](#features)
* [Setup](#setup)
* [Prerequisites for Building Locally/Development](#prerequisites-for-building-locallydevelopment)
* [System Requirements for Ready-to-use build](#system-requirements-for-ready-to-use-build)
* [License](#license)
* [Building](#building)
* [Credits](#credits)

## Features
* Game Download: The launcher can download and install the client.
* Game Update: The launcher can automatically download and install game updates.
* Self-updating: The launcher can automatically update itself to the latest version.
* Register account: Users can register a new account/change the password directly from the launcher.
* News window: The launcher displays the latest news and updates about the game.

## Setup
The launcher require the [Rusty Hearts API](https://github.com/JuniorDark/RustyHearts-API) to work. See the api documentation for instructions on setup.

### API URL
In order for the launcher to work it need to be conected to the api. To change the URL address of the launcher API open the Config.ini (it will be created when opening the launcher for the first time).

The default URL for the api can be changed on IniFile.cs

### Client region
The client region can be set on Service on Config.ini 

* **usa** (PWE) - Full api support
* **chn** (Xunlei) - Only launcher support

### Launcher self-update
In order for the launcher to automatically update itself, you need to use the launcher_info.ini in the `launcher_update` directory of the api. This file specifies the version of the launcher. After each update of the launcher, you need to change the version in the ini, as well in the launcher executable file.

### Client download
In order to download the client, you need to use the `client\download` directory of the api.

The tool for creating the client files is available in the repository: https://github.com/JuniorDark/RustyHearts-MIPTool

### Client patch
In order to create client patches, you need to use the `patch` directory of the api.

The tool for creating the patch files is available in the repository: https://github.com/JuniorDark/RustyHearts-MIPTool

### Launcher Language
The launcher language can be changed on `Lang` on Config.ini or in the Config menu

* **en** - English (en-US) default language
* **ko** - Korean ("ko-KR) (Machine Translated)

### Launcher customization
If you want to add a new language create a LocalizedStrings.<language>.resx with for the desired language. The english strings can be found on LocalizedStrings.resx resource file.

If you want to change the text on the buttons/images used in the launcher you can use the Photoshop .psd files included in the PSD Resources.rar

## Prerequisites for Development
* Visual Studio 2022 (Any Edition - 17.12 or later)
* Windows 10 SDK or Windows 11 SDK via Visual Studio Installer
* .NET Core 9 SDK (9.0.100 or later)

## Building

If you wish to build the project yourself, follow these steps:

### Step 1

Install the [.NET 9.0 (or higher) SDK](https://dotnet.microsoft.com/download/dotnet/9.0).
Make sure your SDK version is higher or equal to the required version specified. 

### Step 2

Either use `git clone https://github.com/JuniorDark/RustyHearts-Launcher` on the command line to clone the repository or use Code --> Download zip button to get the files.

### Step 3

To build Rusty Hearts Launcher, open a command prompt inside the project directory.
You can quickly access it on Windows by holding shift in File Explorer, then right clicking and selecting `Open command window here`.
Then type the following command: `dotnet build -c Release`.
 
The built files will be found in the newly created `bin` build directory.

## License
This project is licensed under the terms found in [`LICENSE-0BSD`](LICENSE).

## Credits
The following third-party libraries, tools, and resources are used in this project:
* [Microsoft.Web.WebView2](https://www.nuget.org/packages/Microsoft.Web.WebView2)
* [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json)
* [WindowsAPICodePack](https://www.nuget.org/packages/WindowsAPICodePack)
