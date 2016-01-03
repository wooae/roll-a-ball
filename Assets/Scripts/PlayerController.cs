using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text scoreText;
    public Text winText;
    public Text clockText;
    public Text finalTimeText;

    private Rigidbody rb;
    private int score;
    private float startTime;
    private float playTime;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody>();
        score = 0;
        setScoreText();
        winText.text = "";
        startTime = Time.time;
        setClockText();
        finalTimeText.text = "";
    }

    // Update is called before each frame
    void Update()
    {
        setClockText();
    }

    // FixedUpdate is called before performing any physics calculations
    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement*speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up")) {
            other.gameObject.SetActive(false);
            score += 1;
            setScoreText();
        }
    }

    void setScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
        if (score >= 12)
        {
            winText.text = "You Win!";
            finalTimeText.text = "Total Game Time: " + clockText.text;
            clockText.gameObject.SetActive(false);
        }
    }

    void setClockText()
    {
        playTime = Time.time - startTime;
        int minutes = (int)(playTime / 60);
        int seconds = (int)(playTime - minutes*60);

        if (minutes < 1)
        {
            if (seconds < 1)
            {
                clockText.text = "00:00";
            }
            else if (seconds < 10)
            {
                clockText.text = "00:0" + seconds;
            }
            else
            {
                clockText.text = "00:" + seconds;
            }
        }
        else if (minutes < 10)
        {
            if (seconds < 1)
            {
                clockText.text = "0" + minutes + ":00";
            }
            else if (seconds < 10)
            {
                clockText.text = "0" + minutes + ":0" + seconds;
            }
            else
            {
                clockText.text = "0" + minutes + ":" + seconds;
            }
        }
        else
        {
            if (seconds < 1)
            {
                clockText.text = minutes + ":00";
            }
            else if (seconds < 10)
            {
                clockText.text = minutes + ":0" + seconds;
            }
            else
            {
                clockText.text = minutes + ":" + seconds;
            }
        }
    }
}
