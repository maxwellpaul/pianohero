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

	public static string ResourcePath;

	public static string LocalMP3Path;
	public static string LocalNotePath;
	public static string LocalWAVPath;
	public static string AssetBundlePath;

	private static string ExeFolder = "/Scripts/";
	public static string pythonExe = ExeFolder + "hello.sh";
	public static string matlabExe = ExeFolder + "TODO";
	public static string pipelineExe = Application.streamingAssetsPath + "/pipeline.sh";

	public static string NeedleObj = "Needle";
	public static string GameManagerObj = "GameManager";
	public static string BottomNeedleObj = "BottomNeedle";
	public static string MenuManagerObj = "MenuManager";
	public static string RockMeterObj = "RockMeter";
	public static string NoteObj = "Note";

	public static int MainMenuScene = 0;
	public static int GamePlayScene = 1;
	public static int WinScreenScene = 2;
	public static int SettingsScene = 5;
}
