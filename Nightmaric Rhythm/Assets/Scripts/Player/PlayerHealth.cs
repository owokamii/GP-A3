using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    public BedTween bedTween;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public GameObject missText;

    [SerializeField] private GameObject gameOverScreen;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        if(health > numOfHearts)
        {
            health = numOfHearts;
        }

        if (health != 0)
        {
            gameOverScreen.SetActive(false);
            PauseMenu.GameIsPaused = false;
        } 
        else
        {
            gameOverScreen.SetActive(true);
            PauseMenu.GameIsPaused = true;
        }

        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;

            if(i < numOfHearts)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Note_1") || collision.gameObject.CompareTag("Note_2"))
        {
            health -= 1;
            audioManager.PlaySFX(audioManager.sfx[1]);
            ScoreManager.instance.ComboBreak();
            Destroy(collision.gameObject);
            GameObject miss = Instantiate(missText);
            bedTween.AnimateBedBounce();
        }
    }
}
