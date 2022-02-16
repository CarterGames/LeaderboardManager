![1200 x 630](https://user-images.githubusercontent.com/33253710/154334690-a848e1a9-c4d2-4ed7-9aab-a1f19587094c.jpg)

<b>Leaderboard Manager</b> is a <b>FREE</b> local leaderboard system for Unity games. 
> Version 1.1.0

## Key Features
- Easily create & update leaderboards with a name & score.
- Support for multiple leaderboards.
- Support for TextMeshProGUI.
- Support for showing score as time.

## How To Install
Either download and import the package from the releases section or the <a href="https://assetstore.unity.com/packages/tools/utilities/leaderboard-manager-cg-177291">Unity Asset Store</a> and use the package manager. Alternatively, download this repo and copy all files into your project. 

## Setup & Basic Usage
The asset doesn’t need any setup by you to function. By default there are no leaderboards in the save. The save will automatically create itself when you first create a leaderboard with the system. The leaderboard save goes to the users computer <a href="https://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html">persistent datapath</a>, under the `/Leaderboard` directory.

### Create a leaderboard called "MyBoard"
> LeaderboardManager.CreateLeaderboard("MyBoard");

### Get leaderboard data for the board called "MyBoard"
> var _data = LeaderboardManager.GetLeaderboard("MyBoard");

### Add entry to board called "MyBoard" for a player called "John" with a score of 100
> LeaderboardManager.AddEntryToBoard("MyBoard", new LeaderboardEntry("John", 100));
> LeaderboardManager.AddEntryToBoard("MyBoard", "John", 100);

### Remove entry from board called "MyBoard" for a player called "John" of a score of 100
> LeaderboardManager.DeleteEntryFromBoard("MyBoard", new LeaderboardEntry("John", 100));
> LeaderboardManager.DeleteEntryFromBoard("MyBoard", "John", 100);

### Save all leaderboards
> LeaderboardManager.Save();

### Load all leaderboards
> LeaderboardManager.Load();

## Limitations
For technical reasons, the asset doesn't support WebGL game builds at present. This system is also local ONLY.

## Documentation
You can access a online of the documentation here: <a href="https://carter.games/leaderboardmanager">Online Documentation</a>. A offline copy if provided with the package and asset if needed. 

## Authors
- <a href="https://github.com/JonathanMCarter">Jonathan Carter</a>

## Licence
MIT Licence
