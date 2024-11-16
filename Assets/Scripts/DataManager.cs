using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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

    void Awake()
    {
        LoadCSV();
    }

    void LoadCSV()
    {
        var lines = csvFile.text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            var values = lines[i].Split(',');
            if (values.Length >= 8)
            {
                Debug.Log(csvFile);
                Debug.Log(csvFile.text);
                DialogueData data = new DialogueData
                {
                    ID = int.Parse(values[0]),
                    Speaker = values[1],
                    Line = values[2],
                    IsOption = int.Parse(values[3]) == 1,
                    Question = values[4],
                    Option1 = values[5],
                    Option2 = values[6],
                    Option3 = values[7],
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
    public string Option1;
    public string Option2;
    public string Option3;
    public int ResultA;
    public int ResultB;
    public int ResultC;
}