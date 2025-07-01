using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public GameObject tutorialPanel;
    public Button continueButton;
    public Button helpButton;

    private bool tutorialActive = false;

    private void Start()
    {
        ShowTutorial(true);
        continueButton.onClick.AddListener(HideTutorial);
        helpButton.onClick.AddListener(() => ShowTutorial(false));
    }

    void ShowTutorial(bool pause)
    {
        tutorialPanel.SetActive(true);
        tutorialActive = true;

        if (pause)
            Time.timeScale = 0;
    }

    void HideTutorial()
    {
        tutorialPanel.SetActive(false);
        tutorialActive = false;

        Time.timeScale = 1;
    }
}