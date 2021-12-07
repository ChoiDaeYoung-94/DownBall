using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    public GameManager GM;

    public float Ballspeed;
    public float BallJumpPower;
    public float timer;

    int jumpCount = 0;

    public AudioClip JumpSound;
    public AudioClip DeathSound;

    bool isTouchR = false;
    bool isTouchL = false;

    public GameObject Gball;
    public GameObject Rball;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Gball.SetActive(true);
        Rball.SetActive(false);
    }

    void Update()
    {
        if (GM.end == false)
        move();
        Phonemove();

        FirstClickCheck(0.15f);
        if (Application.platform == RuntimePlatform.Android)
        {
            Vector3 tpos = Input.GetTouch(0).position;
            if (Input.GetMouseButtonDown(0) && tpos.x >= Screen.width / 2)
            {
                DoubleClickR();
            }
            else if (Input.GetMouseButtonDown(0) && tpos.x <= Screen.width / 2)
            {
                DoubleClickL();
            }
        }
    }

    void DoubleClickR()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            Vector3 tpos = Input.GetTouch(0).position;
            if (isTouchR == true && jumpCount == 4 && tpos.x >= Screen.width / 2)
            {
                GetComponent<AudioSource>().Play();
                rb.velocity = new Vector2(2, 3);
                jumpCount = 0;
                GM.Hit(-5);
                ResetR();
                Gball.SetActive(true);
                Rball.SetActive(false);
            }
            else
            {
                isTouchR = true;
                ResetL();
            }   
        }
    }

    void DoubleClickL()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            Vector3 tpos = Input.GetTouch(0).position;
            if (isTouchL == true && jumpCount == 4 && tpos.x <= Screen.width / 2)
            {
                GetComponent<AudioSource>().Play();
                rb.velocity = new Vector2(-2, 3);
                jumpCount = 0;
                GM.Hit(-5);
                ResetL();
                Gball.SetActive(true);
                Rball.SetActive(false);
            }
            else
            {
                isTouchL = true;
                ResetR();
            }
        }
    }

    void FirstClickCheck(float timercount)
    {
        if (isTouchR == true)
        {
            timer += Time.deltaTime;

            if (timer > timercount)
            {
                ResetR();
            }
        } else if (isTouchL == true)
        {
            timer += Time.deltaTime;

            if (timer > timercount)
            {
                ResetL();
            }
        }
    }

    void ResetR()
    {
        timer = 0;
        isTouchR = false;
    }

    void ResetL()
    {
        timer = 0;
        isTouchL = false;
    }


    void jump()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * BallJumpPower, ForceMode2D.Force);
        jumpCount += 1;

        if (jumpCount >= 4)
        {
            jumpCount = 4;
            Gball.SetActive(false);
            Rball.SetActive(true);
        }
    }

    void Phonemove()
    {   
        if (Application.platform == RuntimePlatform.Android)
        {
            float Ballspeed = Time.deltaTime * 4;
            Vector3 tpos = Input.GetTouch(0).position;

            if (tpos.x <= Screen.width / 2)
            {
                this.transform.Translate(-Vector3.right * Ballspeed, Space.World);
            }

            if (tpos.x >= Screen.width / 2)
            {
                this.transform.Translate(Vector3.right * Ballspeed, Space.World);
            }
        }
    }

    void move()
    {
        float xMove = Input.GetAxis("Horizontal") * Ballspeed * Time.deltaTime;
        this.transform.Translate(new Vector2(xMove, 0), Space.World);

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount == 4 && Input.GetKey(KeyCode.RightArrow))
        {
            GetComponent<AudioSource>().Play();
            rb.velocity = new Vector2(2, 3);
            jumpCount = 0;
            GM.Hit(-5);
            Gball.SetActive(true);
            Rball.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount == 4 && Input.GetKey(KeyCode.LeftArrow))
        {
            GetComponent<AudioSource>().Play();
            rb.velocity = new Vector2(-2, 3);
            jumpCount = 0;
            GM.Hit(-5);
            Gball.SetActive(true);
            Rball.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            AudioSource.PlayClipAtPoint(JumpSound, transform.position);
            jump();
        }
        else if (col.gameObject.tag == "DeathGround")
        {
            AudioSource.PlayClipAtPoint(DeathSound, transform.position);
            jump();
            GM.Hit(10);
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Deathdown")
        {
            GM.Hit(101);
        }
        else if (target.tag == "Deathup")
        {
            GM.Hit(101);
        }
        else if (target.tag == "Score" && GM.end != true)
        {
            GM.GetScore();
        }
    }
}
                                                                                                                                                    