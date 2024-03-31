![LBM](https://github.com/CarterGames/LeaderboardManager/assets/33253710/d5ee73b1-951f-48e8-ab58-e6344792ef40)


<b>Leaderboard Manager</b> is a <b>FREE</b> local leaderboard system for Unity games. 

## Badges
![CodeFactor](https://www.codefactor.io/repository/github/cartergames/LeaderboardManager/badge?style=for-the-badge)
![GitHub all releases](https://img.shields.io/github/downloads/CarterGames/LeaderboardManager/total?style=for-the-badge)
![GitHub release (latest by date)](https://img.shields.io/github/v/release/CarterGames/LeaderboardManager?style=for-the-badge)
![GitHub repo size](https://img.shields.io/github/repo-size/CarterGames/LeaderboardManager?style=for-the-badge)

## Key Features
- Easily create & update leaderboards with a name & score.
- Support for multiple leaderboards.
- Support for TextMeshProGUI.
- Support for showing score as time.

## How To Install
Either download and import the package from the releases section or the <a href="https://assetstore.unity.com/packages/tools/utilities/leaderboard-manager-cg-177291">Unity Asset Store</a> and use the package manager. Alternatively, download this repo and copy all files into your project. 

## Setup
The asset doesn’t need any setup by you to function. By default there are no leaderboards in the save data. The save will automatically create itself when you first create a leaderboard with the system. The leaderboard save goes to the users computer <a href="https://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html">persistent datapath</a>.

### 2.0.x save updating
You can port legacy data in editor and setup the porting. In older versions users could save time as a number, this has been updated into a custom class. If used it'll need to ported to a time entry board. The controls to this and the ability to view your current boards can be found under ```Tools/Carter Games/Leaderboard Manager/Leaderboard Editor``` An example setup to port a leaderboard called "Arcade" to a score leaderboard. 

![lbm-legacyport-runtime-01](https://github.com/CarterGames/LeaderboardManager/assets/33253710/faed0d7d-4e89-49c7-8e0c-92bf83d1bfa9)

## Basic API Usage

### Create a leaderboard called "MyBoard"
> LeaderboardManager.CreateLeaderboard("MyBoard", LeaderboardType.Score);

### Get leaderboard data for the board called "MyBoard"
> var data = LeaderboardManager.GetLeaderboard("MyBoard");

### Add entry to board called "MyBoard" for a player called "John" with a score of 100
> LeaderboardManager.AddEntryToBoard("MyBoard", new LeaderboardEntryScore("John", 100));

### Remove entry from board called "MyBoard" for a player called "John" of a score of 100
> LeaderboardManager.DeleteEntryFromBoard("MyBoard", new LeaderboardEntryScore("John", 100));

### Save all leaderboards
> LeaderboardManager.Save();

### Load all leaderboards
> LeaderboardManager.Load();

## Documentation
You can access a online of the documentation here: <a href="https://carter.games/docs/leaderboardmanager">Online Documentation</a>. A offline copy if provided with the package and asset if needed. 

## Authors
- <a href="https://github.com/JonathanMCarter">Jonathan Carter</a>

## Licence
MIT Licence
