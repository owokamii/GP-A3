using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static bool BonusLevelUnlocked = false;
    public GameObject BonusLevelButton;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        if(BonusLevelUnlocked)
        {
            BonusLevelButton.SetActive(true);
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Select()
    {
        audioManager.PlaySFX(audioManager.sfx[0]);
    }

    public void Cancel()
    {
        audioManager.PlaySFX(audioManager.sfx[1]);
    }

    public void PlayBonusLevel()
    {
        SceneManager.LoadScene("LevelBonus");
    }

    public void UnlockBonusLevel()
    {
        BonusLevelUnlocked = true;
    }
}
