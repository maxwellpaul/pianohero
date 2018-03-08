using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility {

	public static List<string> songTokens = new List<string>();

	public static string LocalNotePath;

	public static int amountOfRock;
	public static string songChoiceToken;
	public static string difficultyLevel;
	
	public static string textToToken(string filename) {
		return filename.Split ('.')[0];
	}

	public static string tokenToDisplay(string token) {
		return token.Replace ('_', ' ');
	}

	public static string makeHighScoreKey(string songToken, string type, string diffLevel) {
		return songToken + "-" + type + "-" + diffLevel;
	}

	public static string DisplayToToken(string name) {
		return name.Replace (' ', '_');
	}
}
