using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : SingletonBase<UIManager>
{
    [SerializeField] private GameObject dialogUI;
    [SerializeField] private TextMeshProUGUI lineTxt;
    [SerializeField] private TextMeshProUGUI nameTxt;
    [SerializeField] private Button nextBtn;

    [SerializeField] private GameObject optionUI;
    [SerializeField] private TextMeshProUGUI questionTxt;
    [SerializeField] private Button optionAButton;
    [SerializeField] private Button optionBButton;
    [SerializeField] private Button optionCButton;

    [SerializeField] private GameObject scoreAlert;
    [SerializeField] private TextMeshProUGUI scoreChangeTxt;

    private int currentID = 0;
    private DataManager _dataManager;

    void Start()
    {
        _dataManager = GetComponent<DataManager>();
        ShowDialogue();
    }

    public void ShowDialogue()
    {
        DialogueData data = _dataManager.dialogues[currentID];
        if (data.IsOption)
        {
            dialogUI.SetActive(false);
            optionUI.SetActive(true);
            questionTxt.text = data.Question;
            optionAButton.GetComponentInChildren<Text>().text = data.OptionA;
            optionBButton.GetComponentInChildren<Text>().text = data.OptionB;
            optionCButton.GetComponentInChildren<Text>().text = data.OptionC;

            optionAButton.onClick.AddListener(() => SelectOption(data.ResultA));
            optionBButton.onClick.AddListener(() => SelectOption(data.ResultB));
            optionCButton.onClick.AddListener(() => SelectOption(data.ResultC));
        }
        else
        {
            dialogUI.SetActive(true);
            optionUI.SetActive(false);
            nameTxt.text = data.Speaker;
            lineTxt.text = data.Line;
        }
    }

    public void OnNextButton()
    {
        currentID++;
        ShowDialogue();
    }

    private void SelectOption(int result)
    {
        DataManager.Instance.Score += result;

        currentID++;
        ShowDialogue();

        if (result == 0) return;
        if (result > 0)
            scoreChangeTxt.text = $"호감도 + {result}";
        else
            scoreChangeTxt.text = $"호감도 {result}";

        scoreAlert.SetActive(true);
    }
}
