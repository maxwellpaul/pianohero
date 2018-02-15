using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Const {

	public static string scoreKey = "Score";
	public static string multKey = "Mult";
	public static string streakKey = "Streak";

	public static string maxMultKey = "MaxMult";
	public static string maxStreakKey = "MaxStreak";

	public static string highScoreKey = "HighScore";
	public static string highMultKey = "HighMult";
	public static string highStreakKey = "HighStreak";

	public static string amountOfRockKey = "amtOfRock";
	public static string songChoiceTokenKey = "SongChoiceToken";

	private static string SongFolder = "/Songs/";
	public static string LocalMP3Path = SongFolder + "MP3Files/";
	public static string LocalNotePath = SongFolder + "NoteFiles/";
	public static string LocalWAVPath = SongFolder + "WAVFiles/";

	private static string ExeFolder = "/Executables/";
	public static string pythonExe = ExeFolder + "TODO";
	public static string matlabExe = ExeFolder + "TODO";

	public static string NeedleObj = "Needle";
	public static string GameManagerObj = "GameManager";
	public static string BottomNeedleObj = "BottomNeedle";
	public static string MenuManagerObj = "MenuManager";
	public static string RockMeterObj = "RockMeter";
	public static string NoteObj = "Note";

	public static int MainMenuScene = 0;
	public static int GamePlayScene = 1;
	public static int WinScreenScene = 2;
}
