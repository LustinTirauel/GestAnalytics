using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

[AddComponentMenu("GetButtonName")]
public class getButtonName : MonoBehaviour {

	public List<Answers> answer	 = new List<Answers>();
	public static List<CollectedAnswers> collectedAnswers = new List<CollectedAnswers>();
	public MoviePlay questionName;
	string answerNumber;
	public string participantName;
	string participantControl = "";
    public int phase = 1;
    public GameObject phaseWarning;
    public GameObject exit;
    public InputField participantNumber;
    public InputField occupation;
    public InputField age;
    public Dropdown sex;
	public Dropdown dominant;
    public Toggle phaseToggle;
	string sexString;
	string dominantString;
    string filePath;
    string filePath2;
    //string filePath3;
    string filePath4;
    string filePath5;



    // Use this for initialization
    void Start () {


       
        /*foreach(Answers button in answer)
		{
			print (button.name + " " + button.number);
		}*/
        //This is how you create a list. Notice how the type
        //is specified in the angle brackets (< >).

    }

	// Update is called once per frame
	void Update () {

      

    }

    public void changeParticipantName()
    {
        participantName = participantNumber.text;

        if (phase == 2)
        {
            participantName = participantName + "_innovative"; //add innovative suffix to name if the phase2 is running
        }

    }

	public void collectAnswers (){
        //Debug.Log ("control " + questionName.taskNames [questionName.taskNumber]);

		//following 4 if functions are for changing the dropdown menu values to logical words

		if (sex.value == 1)
			sexString = "Female";
		
		if (sex.value == 2)
			sexString = "Male";

		//if (dominant.value == 1)
		//	dominantString = "Right";

		//if (dominant.value == 2)
		//	dominantString = "Left";
		//4 if functions are finished


        if (participantControl != participantName) { //if a new participant started to fill the questionnaire, then these fucntions will be available

           		collectedAnswers.Add (new CollectedAnswers (participantName));
                collectedAnswers.Add(new CollectedAnswers(occupation.text));
                collectedAnswers.Add(new CollectedAnswers(age.text));
				collectedAnswers.Add(new CollectedAnswers(dominantString));
				collectedAnswers.Add(new CollectedAnswers(sexString));

           		participantControl = participantName;
		}
		
        if (questionName.taskNumber>0)	
		collectedAnswers.Add (new CollectedAnswers(questionName.taskNames[questionName.taskNumber-1]));

		foreach (Answers marked in answer) {
			answerNumber = marked.number.ToString();
			collectedAnswers.Add (new CollectedAnswers ((answerNumber)));
		}


		//Debug.Log (collectedAnswers);
		Savecsv();
}
	//writing to file
    
    public void nameSaveFiles()
    {
    
         
    filePath = Application.dataPath + "/SavedData/" + participantName + "_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
    filePath2 = Application.dataPath + "/SavedData/All_data.csv";
    //filePath3 = Application.dataPath + "/SavedData/" + participantName + "_innovative_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
    filePath4 = Application.dataPath + "/SavedData/" + participantName + "_taskList" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
    filePath5 = Application.dataPath + "/taskData.csv";
        //string delimiter = ",";  


    }


    public void createTaskList()
    {

        StringBuilder taskListString = new StringBuilder();
        taskListString.AppendLine();
        for (int i = 0; i < questionName.taskNames.Length; i++)
        {

            taskListString.AppendLine(questionName.taskNames[i]+";"+questionName.videoNames[i]);

        }

        File.AppendAllText(filePath4, taskListString.ToString());
        File.WriteAllText(filePath5, taskListString.ToString());
    }

    public void manuallyChangePhase()
    {
        if (phaseToggle.isOn)
            phase = 2;

        if (!phaseToggle.isOn)
            phase = 1;
    }

void Savecsv() {
        

        StringBuilder sb = new StringBuilder();

        //string[] collectedAnswersTxt = collectedAnswers.ToString ();
        foreach (CollectedAnswers answer in collectedAnswers)
        {
            sb.Append(answer.input + ", ");
        }
            

        

		//Debug.Log ("csv exported");
        if (phase == 1)
		File.WriteAllText(filePath, sb.ToString());

        if (phase == 2)
        File.WriteAllText(filePath, sb.ToString());

        
        Debug.Log ("questionTaskNumber=" + questionName.taskNumber + " questiontaskNamesLength=" + questionName.taskNames.Length);

        // this function will write all collected data to a mutual file.
		if (questionName.taskNumber == questionName.taskNames.Length) {
            sb.AppendLine();
            File.AppendAllText(filePath2, sb.ToString());
            collectedAnswers.Clear();

        // these lines will activate different phases of the study if the all tasks are completed.

            if (phase < 3)
            {

                phase++; // start to phase2
            }

            if (phase == 2)
            {
                phaseWarning.SetActive(true); //activate phase 2 screen

            }

            if (phase == 3)
            {
                exit.SetActive(true); //activate phase 2 screen

            }
        }
	
	}

    public void defineArray()
    {
        questionName.taskNames = new string[26];
    }
}




