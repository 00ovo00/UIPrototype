using TMPro;
using UnityEngine;
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
    private bool isResult;

    void Start()
    {
        isResult = false;
        _dataManager = DataManager.Instance;
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
            optionAButton.GetComponentInChildren<TextMeshProUGUI>().text = data.OptionA;
            optionBButton.GetComponentInChildren<TextMeshProUGUI>().text = data.OptionB;
            optionCButton.GetComponentInChildren<TextMeshProUGUI>().text = data.OptionC;

            optionAButton.onClick.RemoveAllListeners();
            optionBButton.onClick.RemoveAllListeners();
            optionCButton.onClick.RemoveAllListeners();

            optionAButton.onClick.AddListener(() => SelectOption(data.Result1, 1));
            optionBButton.onClick.AddListener(() => SelectOption(data.Result2, 2));
            optionCButton.onClick.AddListener(() => SelectOption(data.Result3, 3));
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
        if (isResult)
        {
            scoreAlert.SetActive(false);
            isResult = false;
            StartNewConversation("대화1");            
        }
        currentID++;
        ShowDialogue();
    }

    private void SelectOption(int result, int num)
    {
        DataManager.Instance.Score += result;
        isResult = true;
        currentID += num;

        if (result != 0)
        {
            scoreChangeTxt.text = result > 0 ? $"호감도 +{result}" : $"호감도 {result}";
            scoreAlert.SetActive(true);
        }
        ShowDialogue();
    }

    public void StartNewConversation(string dialogID)
    {
        DialogueData firstDialogue = _dataManager.dialogues.Find(d => d.DialogID == dialogID && d.ID == 0);
        if (firstDialogue != null)
        {
            currentID = _dataManager.dialogues.IndexOf(firstDialogue) - 1;
            ShowDialogue();
        }
        else
        {
            Debug.LogError($"Dialog with ID {dialogID} not found.");
        }
    }
}
