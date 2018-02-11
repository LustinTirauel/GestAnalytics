using UnityEngine;
#if UNITY_EDITOR 
using UnityEditor;
#endif
using System.Collections;
using System.IO;

public class createFolder : MonoBehaviour {


	string participantName;
	int participantNumber;
	string guid;
	string newFolderPath;

	// Use this for initialization
	void Start () {
		participantName = "p";
		participantNumber = 1;

	}
	
	// Update is called once per frame
	void Update () {

		if (participantNumber <= 16){
			Debug.Log ("folder name= " + participantName + participantNumber.ToString ());

			if (participantNumber < 10) {
				guid = AssetDatabase.CreateFolder ("Assets/Resources/AnalysisVideos", (participantName + "0" + participantNumber.ToString () + "_" + UnityEngine.Random.Range (0, 1000000).ToString ()));
				newFolderPath = AssetDatabase.GUIDToAssetPath (guid);

				guid = AssetDatabase.CreateFolder ("Assets/Resources/AnalysisVideos", (participantName + "0" + participantNumber.ToString () + "_innovative" + "_" + UnityEngine.Random.Range (0, 1000000).ToString ()));
				newFolderPath = AssetDatabase.GUIDToAssetPath (guid);
			}

			if (participantNumber >= 10) {
				guid = AssetDatabase.CreateFolder ("Assets/Resources/AnalysisVideos", (participantName +  participantNumber.ToString () + "_" +  UnityEngine.Random.Range (0, 1000000).ToString ()));
				newFolderPath = AssetDatabase.GUIDToAssetPath (guid);

				guid = AssetDatabase.CreateFolder ("Assets/Resources/AnalysisVideos", (participantName + participantNumber.ToString() + "_innovative" + "_" + UnityEngine.Random.Range(0,1000000).ToString()));
				newFolderPath = AssetDatabase.GUIDToAssetPath(guid);
			}



			participantNumber++;
		}
	
	}
}
