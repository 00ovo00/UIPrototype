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

    public TextAsset csvFile;
    public List<DialogueData> dialogues = new List<DialogueData>();

    void Start()
    {
        LoadCSV();
    }

    void LoadCSV()
    {
        var lines = csvFile.text.Split('\n');
        foreach (var line in lines)
        {
            var values = line.Split(',');
            if (values.Length >= 8)
            {
                DialogueData data = new DialogueData
                {
                    ID = int.Parse(values[0]),
                    Speaker = values[1],
                    Line = values[2],
                    IsOption = int.Parse(values[3]) == 1,
                    Question = values[4],
                    OptionA = values[5],
                    OptionB = values[6],
                    OptionC = values[7],
                    ResultA = int.Parse(values[8]),
                    ResultB = int.Parse(values[9]),
                    ResultC = int.Parse(values[10])
                };
                dialogues.Add(data);
            }
        }
    }
}

[System.Serializable]
public class DialogueData
{
    public int ID;
    public string Speaker;
    public string Line;
    public bool IsOption;
    public string Question;
    public string OptionA;
    public string OptionB;
    public string OptionC;
    public int ResultA;
    public int ResultB;
    public int ResultC;
}