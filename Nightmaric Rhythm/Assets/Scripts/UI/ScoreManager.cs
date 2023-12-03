using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Conductor conductor;
    public TMP_Text highscoreText;
    public TMP_Text scoreText;

    public TMP_Text perfectTextLose;
    public TMP_Text goodTextLose;
    public TMP_Text badTextLose;
    public TMP_Text perfectTextWin;
    public TMP_Text goodTextWin;
    public TMP_Text badTextWin;

    public TMP_Text comboText;
    public TMP_Text totalTextLose;
    public TMP_Text totalTextWin;

    int highscore = 0;
    int score = 0;
    int combo = 0;
    int total = 0;

    int perfect = 0;
    int good = 0;
    int bad = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //highscore
        highscore = PlayerPrefs.GetInt("highscore", 0);
        highscoreText.text = highscore.ToString();

        scoreText.text = score.ToString();

        //perfect, good, bad
        perfectTextLose.text = "Perfect: " + perfect.ToString();
        perfectTextWin.text = "Perfect " + perfect.ToString();
        goodTextLose.text = "Good: " + good.ToString();
        goodTextWin.text = "Good: " + good.ToString();
        badTextLose.text = "Bad: " + bad.ToString();
        badTextWin.text = "Bad: " + bad.ToString();

        //combo
        comboText.text = "X " + combo.ToString();

        //total notes hit win/lose
        totalTextLose.text = "You beat " + total.ToString() + "/" + (conductor.notes.Length - 1).ToString() + " monsters!";
        totalTextWin.text = "You beat " + total.ToString() + "/" + (conductor.notes.Length - 1).ToString() + " monsters!";
    }

    public void AddCombo()
    {
        combo += 1;
        comboText.text = "X " + combo.ToString();
    }

    public void ComboBreak()
    {
        combo = 0;
        comboText.text = "X " + combo.ToString();
    }

    public void AddPoint(int val)
    {
        score = score + val * combo;
        scoreText.text = score.ToString();
        if (highscore < score)
            PlayerPrefs.SetInt("highscore", score);
    }

    public void AddTotal()
    {
        total += 1;
        totalTextLose.text = "You beat " + total.ToString() + "/" + (conductor.notes.Length - 1).ToString() + " monsters!";
        totalTextWin.text = "You beat " + total.ToString() + "/" + (conductor.notes.Length - 1).ToString() + " monsters!";
    }

    public void AddPerfect()
    {
        perfect += 1;
        perfectTextLose.text = "Perfect: " + perfect.ToString();
        perfectTextWin.text = "Perfect " + perfect.ToString();
    }

    public void AddGood()
    {
        good += 1;
        goodTextLose.text = "Good: " + good.ToString();
        goodTextWin.text = "Good: " + good.ToString();
    }

    public void AddBad()
    {
        bad += 1;
        badTextLose.text = "Bad: " + bad.ToString();
        badTextWin.text = "Bad: " + bad.ToString();
    }
}
