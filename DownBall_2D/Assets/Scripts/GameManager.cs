using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    public float waitingTime;

    public bool ready = true;
    public bool end = false;

    public GameObject ground;
    public GameObject deathground;
    public GameObject StartGround;
    public GameObject player;


    private int score;
    public TextMesh scoretext;
    public TextMesh FinalScoreText;
    public TextMesh BestScoreText;

    private float mTotalHp;
    private float mNowHp;
    public Transform mBar;

    public GameObject readyImage1;
    public GameObject readyImage2;
    public GameObject gameOverImage;
    public GameObject finalWindow;
    public GameObject imageNew;

    public AudioClip GameOverSound;

    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(700, 1280, true);
    }

    void Start()
    {
        score = 0;
        SetScore();
        SetHp(100);
        GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        if (Input.anyKeyDown && ready == true)
        {
            ready = false;
            iTween.FadeTo(readyImage1, iTween.Hash("alpha", 0, "time", 0.5f));
            iTween.FadeTo(readyImage2, iTween.Hash("alpha", 0, "time", 0.5f));
            InvokeRepeating("MakeGround", 0, waitingTime);
            InvokeRepeating("MakeDeathGround", 0, 3);
            InvokeRepeating("MakeDeathGroundd", 0, 10);
            InvokeRepeating("MakeDeathGrounddd", 0, 7);
            InvokeRepeating("MakeDeathGroundddd", 0, 5);
            InvokeRepeating("HPdown", 0, 1);
            player.GetComponent<Rigidbody2D>().gravityScale = 1;
            MakeStartGround();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void HPdown()
    {
        Hit(1);
    }

    void MakeStartGround()
    {
        Instantiate(StartGround);
    }

    void MakeGround()
    {
        Instantiate(ground);
    }

    void MakeDeathGround()
    {
        if (score > 100)
            Instantiate(deathground);
    }

    void MakeDeathGroundd()
    {
        if (score > 500)
            Instantiate(deathground);
    }

    void MakeDeathGrounddd()
    {
        if (score > 800)
            Instantiate(deathground);
    }

    void MakeDeathGroundddd()
    {
        if (score > 1100)
            Instantiate(deathground);
    }

    public void GameOver()
    {
        if (end == true)
            return;
        end = true;

        player.SetActive(false);
        CancelInvoke("MakeGround");
        CancelInvoke("MakeDeathGround");
        CancelInvoke("MakeDeathGroundd");
        CancelInvoke("MakeDeathGrounddd");
        CancelInvoke("MakeDeathGroundddd");
        CancelInvoke("HPdown");
        iTween.ShakePosition(Camera.main.gameObject, iTween.Hash("x", 0.2, "y", 0.2, "time", 0.5f));
        iTween.FadeTo(gameOverImage, iTween.Hash("alpha", 255, "delay", 1f, "time", 0.5f));
        iTween.MoveTo(finalWindow, iTween.Hash("y", 0, "delay", 1f, "time", 0.5f));
        AudioSource.PlayClipAtPoint(GameOverSound, transform.position);

        if (score > PlayerPrefs.GetInt("BS"))
        {
            PlayerPrefs.SetInt("BS", score);
            imageNew.SetActive(true);
        }
        else if (score <= PlayerPrefs.GetInt("BS"))
        {
            imageNew.SetActive(false);
        }

        FinalScoreText.text = score.ToString();
        BestScoreText.text = PlayerPrefs.GetInt("BS").ToString();
    }


    void SetScore()
    {
        scoretext.text = "Score " + score.ToString();
    }

    public void GetScore()
    {
        score += 10;
        SetScore();
    }

    public void SetHp(int hp)
    {
        mNowHp = mTotalHp = hp;
    }

    public void Hit(float damage)
    {
        mNowHp -= damage;
    
        if (mNowHp > 100)
        {
            mNowHp = 100;
        }
        
        if (mNowHp <= 0)
        {
            mNowHp = 0;
            GameOver();
        }
       
        mBar.transform.localScale = new Vector3((mNowHp / mTotalHp) * 4, 4, 1);
    }
}
