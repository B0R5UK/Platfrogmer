using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Text score;
    [SerializeField] Text levelScore;
    [SerializeField] Image shurikenImage;
    [SerializeField] GameObject levelCompletePanel;
    [SerializeField] GameObject pauseMenuPanel;
    [SerializeField] GameObject tipPanel;
    [SerializeField] Text tipText;

    public void Initialize()
    {
        shurikenImage.CrossFadeAlpha(0, 0f, false);
        levelScore.text = GameController.Instance.maxLevelScore.ToString();
    }

    public void PauseMenu(bool state)
    {
        pauseMenuPanel.SetActive(state);
    }

    public void UpdateScore()
    {
        score.text = GameController.Instance.Score.ToString();
    }

    public void UpdateShuriken()
    {
        if (GameController.Instance.Player.canShoot)
            shurikenImage.CrossFadeAlpha(1, 0.2f,false);
        else
            shurikenImage.CrossFadeAlpha(0, 0.2f, false);
    }

    public void ShowTip(bool active, string description = "")
    {
        tipText.text = description;
        tipPanel.SetActive(active);
    }

    public void LevelClear()
    {
        levelCompletePanel.SetActive(true);
    }
}
