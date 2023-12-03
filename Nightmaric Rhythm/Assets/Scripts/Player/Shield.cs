using UnityEngine;

public class Shield : MonoBehaviour
{
    public PlayerController controller;
    public PlayerHealth health;
    public LayerMask layersToHit;
    public GameObject[] text;
    public Transform[] offset;
    public BoxCollider2D[] shield;
    public Sprite[] plush;
    public SpriteRenderer sp;
    public ParticleSystem confetti;

    private Vector3 originalScale;
    private float bounceScaleFactor = 1.4f;
    private float animationDuration = 0.2f;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        originalScale = transform.localScale;

        if (OptionsMenu.IsEasyMode)
        {
            shield[0].enabled = true;
        }
        else
        {
            shield[1].enabled = true;
        }
    }

    private void Update()
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D badHit = Physics2D.Raycast(offset[0].position, controller.raycastAngle * offset[0].localScale.x, 2.5f, layersToHit);
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D goodHit = Physics2D.Raycast(offset[1].position, controller.raycastAngle * offset[1].localScale.x, 3f, layersToHit);
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D perfectHit = Physics2D.Raycast(offset[2].position, controller.raycastAngle * offset[2].localScale.x, 1.5f, layersToHit);

        if(!OptionsMenu.IsEasyMode)
        {
            if(Input.GetButtonDown("Hit"))
            {
                if (perfectHit.collider != null)
                {
                    Destroy(perfectHit.transform.gameObject);
                    GameObject perfect = Instantiate(text[2]);
                    Hit();
                    ScoreManager.instance.AddPoint(20);
                    ScoreManager.instance.AddPerfect();
                }
                else if (goodHit.collider != null)
                {
                    Destroy(goodHit.transform.gameObject);
                    GameObject good = Instantiate(text[1]);
                    Hit();
                    ScoreManager.instance.AddPoint(10);
                    ScoreManager.instance.AddGood();
                }
                else if (badHit.collider != null)
                {
                    Destroy(badHit.transform.gameObject);
                    GameObject bad = Instantiate(text[0]);
                    Hit();
                    ScoreManager.instance.AddPoint(5);
                    ScoreManager.instance.AddBad();
                }
            }
        }
        if(!OptionsMenu.IsEasyMode)
        {
            if (Input.GetButtonDown("Hit"))
            {
                sp.sprite = plush[1];
                if (sp.sprite == plush[1])
                {
                    Invoke("ShieldDownSprite", 0.15f);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(OptionsMenu.IsEasyMode)
        {
            if (collision.gameObject.CompareTag("Note_1") || collision.gameObject.CompareTag("Note_2"))
            {
                PlayConfetti();
                Destroy(collision.gameObject);
                GameObject perfect = Instantiate(text[2]);
                Hit();
                ScoreManager.instance.AddPoint(10);
                ScoreManager.instance.AddPerfect();
            }
        }
    }

    void ShieldDownSprite()
    {
        sp.sprite = plush[0];
    }

    void Hit()
    {
        PlayConfetti();
        AnimatePlayerBounce();
        ScoreManager.instance.AddCombo();
        ScoreManager.instance.AddTotal();
        audioManager.PlaySFX(audioManager.sfx[0]);
    }

    private void AnimatePlayerBounce()
    {
        LeanTween.scale(gameObject, originalScale * bounceScaleFactor, animationDuration / 2).setEase(LeanTweenType.easeOutQuad).setOnComplete(AnimatePlayerShrink);
    }

    private void AnimatePlayerShrink()
    {
        LeanTween.scale(gameObject, originalScale, animationDuration / 2).setEase(LeanTweenType.easeInQuad);
    }

    void PlayConfetti()
    {
        confetti.Play();
    }
}
