using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility {
	public static string textToToken(string filename) {
		return filename.Split ('.')[0];
	}

	public static string tokenToDisplay(string token) {
		return token.Replace ('_', ' ');
	}

	public static string makeHighScoreKey(string songToken, string type) {
		return songToken + "-" + type;
	}
}
