using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Const {

	public const string scoreKey = "Score";
	public const string multKey = "Mult";
	public const string streakKey = "Streak";

	public const string maxMultKey = "MaxMult";
	public const string maxStreakKey = "MaxStreak";

	public const string highScoreKey = "HighScore";
	public const string highMultKey = "HighMult";
	public const string highStreakKey = "HighStreak";

	public const string amountOfRockKey = "amtOfRock";
	public const string songChoiceTokenKey = "SongChoiceToken";

	public const string difficultyLevelKey = "difficultyLevel";

	public const string NeedleObj = "Needle";
	public const string GameManagerObj = "GameManager";
	public const string BottomNeedleObj = "BottomNeedle";
	public const string MenuManagerObj = "MenuManager";
	public const string RockMeterObj = "RockMeter";
	public const string NoteObj = "Note";

	public const int MainMenuScene = 0;
	public const int GamePlayScene = 1;
	public const int WinScreenScene = 2;
	public const int SettingsScene = 5;

	public static readonly List<string> difficultyLevelsArray = new List<string>() { 
		"Easy",
		"Medium",
		"Hard",
		"Expert"
	};
}
