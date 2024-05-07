using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    EndGameScoreManager endGameScoreManager;
    AudioSource snd;
    public AudioClip jumpp;
    public AudioClip move;
    //Variables
    public float movespeed;
    public float speed;
    public float jumpSpeed;
    public bool isGrounded;
    public bool powerupsheild =false;
    public float max;
    public float speedMultiplier = 600;
   

    public Rigidbody rb;

    public GameObject sheild;

    private void Start()
    {
        snd = GetComponent<AudioSource>();
        endGameScoreManager = FindObjectOfType<EndGameScoreManager>();

        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0, 0, 5), ForceMode.Impulse);

        
    }
    void Update()
    {


        if (powerupsheild)
        {
            sheild.SetActive(true);
        }
        else
        { sheild.SetActive(false); }

        speed = (Input.GetAxis("Horizontal") * speedMultiplier);

        if (Time.timeScale != 0f) //
        { //
            //right 
            if (Input.GetAxis("Horizontal") > 0)
            {
                // transform.Rotate(Vector3.forward * -Input.GetAxis("Horizontal"));
                rb.AddForce(transform.right * speed);
            }
            if (Input.GetAxis("Horizontal") == 0)
            {
                if (rb.velocity.x > 0)
                {
                    // transform.Rotate(transform.rotation.x *0.5f, transform.rotation.y, transform.rotation.z, Space.Self);
                    rb.AddForce(transform.right * -speed);
                }
                //left
                if (rb.velocity.x < 0)
                {

                    rb.AddForce(transform.right * -speed);
                }
            }


            if (Input.GetAxis("Horizontal") < 0)
            {
                //transform.Rotate(Vector3.forward * -Input.GetAxis("Horizontal"));
                rb.AddForce(transform.right * speed);
            }


            if (isGrounded)
            {
                //Jumping
                if (Input.GetButtonUp("Jump") || Input.GetKeyDown(KeyCode.Joystick1Button1))
                {
                    snd.PlayOneShot(jumpp);
                    rb.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode.Impulse);
                    isGrounded = false;
                }

            }
            else
            {
                rb.AddForce(0, -55, 0);
            }

        } //
    }

        void OnCollisionEnter(Collision other)
        {
        if (other.gameObject.tag == "floor")
        {
            isGrounded = true;
        }

        if (other.gameObject.tag == "sheild")
        {
            powerupsheild = true;
        }

        if (other.gameObject.tag == "cube")
        {
            Debug.Log(powerupsheild);
            if (powerupsheild)
            {
                powerupsheild = false;
                Destroy(other.gameObject);
            }
            else
            {
                if (this.gameObject.tag == "Player" && !powerupsheild)
                {
                    //SceneManager.LoadScene("GameOver");
                   endGameScoreManager.Crash();
                }
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "sheild")
        {
            powerupsheild = true;
        }

        if (other.gameObject.tag == "cube")
        {
            Debug.Log(powerupsheild);
            if (powerupsheild)
            {
                //powerupsheild = false;
                Destroy(other.gameObject);
                StartCoroutine(DelayShield());
            }
            else
            {
                if (this.gameObject.tag == "Player" && !powerupsheild)
                {
                    //SceneManager.LoadScene("GameOver");
                    endGameScoreManager.Crash();
                }
            }
        }
    }

    IEnumerator DelayShield()
    {
        yield return new WaitForSeconds(1);
        powerupsheild = false;
    }



    public void speedUpgrade(int addValue)
    {
        speedMultiplier += addValue;
    }

}