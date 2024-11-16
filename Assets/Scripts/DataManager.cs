using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonBase<DataManager>
{
    private int score = 0;

    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    public TextAsset csvFile; // Assign CSV file in the Inspector
    public List<DialogueData> dialogues = new List<DialogueData>();

    void Awake()
    {
        LoadCSV();
    }

    void LoadCSV()
    {
        var lines = csvFile.text.Split('\n');
        for (int i = 1; i < lines.Length; i++) // Skip header row
        {
            var values = lines[i].Split(',');
            if (values.Length >= 13) // Ensure sufficient columns
            {
                DialogueData data = new DialogueData
                {
                    DialogID = values[0],
                    ID = int.Parse(values[1]),
                    Speaker = values[2],
                    Line = values[3],
                    IsOption = int.Parse(values[4]) == 1,
                    Question = values[5],
                    OptionA = values[6],
                    OptionB = values[7],
                    OptionC = values[8],
                    Result1 = int.Parse(values[9]),
                    Result2 = int.Parse(values[10]),
                    Result3 = int.Parse(values[11]),
                    NextIDs = values[12] // Delimited NextIDs
                };
                dialogues.Add(data);
            }
        }
    }
}

[System.Serializable]
public class DialogueData
{
    public string DialogID; // Conversation ID
    public int ID; // Line ID within conversation
    public string Speaker;
    public string Line;
    public bool IsOption;
    public string Question;
    public string OptionA;
    public string OptionB;
    public string OptionC;
    public int Result1;
    public int Result2;
    public int Result3;
    public string NextIDs; // Delimited next IDs (e.g., "3;4;5")
}