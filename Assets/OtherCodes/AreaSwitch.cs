using UnityEngine;
using UnityEngine.UI;

public class AreaSwitch : MonoBehaviour
{
    public GameObject kitchenPanel;
    public GameObject customerPanel;
    public GameObject minigamePanel;

    public enum ViewState { Customer, Kitchen, Minigame }

    public void SwitchTo(int viewIndex)
    {
        ViewState targetView = (ViewState)viewIndex;
        SwitchTo(targetView);
    }

    // Keeps the enum logic clean
    private void SwitchTo(ViewState targetView)
    {
        kitchenPanel.SetActive(targetView == ViewState.Kitchen);
        customerPanel.SetActive(targetView == ViewState.Customer);
        minigamePanel.SetActive(targetView == ViewState.Minigame);
    }
}
