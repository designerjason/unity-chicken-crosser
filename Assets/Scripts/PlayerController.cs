using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioSource foodSound;
    public AudioSource cluckSound;
    Animator animator;
    GameManager gameManager;
    Vector3 playerScale;
    float curScale;
    float curScaleRate = 1.05f;
    public ParticleSystem feathers;
    public ParticleSystem blood;
    public float speed = 15;
    public float horizontalInput;
    public float verticalInput;
    private Rigidbody playerRig;
    private bool playerIsDead = false;
    private float horizBounds = 35.0f;
    private float vertBoundsTop = -15.0f;
    private float vertBoundsBottom = 20.0f;

    void Start() {
        playerScale = transform.localScale;
        animator = GameObject.Find("Player").GetComponent<Animator>();
        gameManager = GameObject.Find("_GameManager").GetComponent<GameManager>();
        playerRig = gameObject.GetComponent<Rigidbody>();
        curScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo animatorState = animator.GetCurrentAnimatorStateInfo(0);

        if(!playerIsDead) 
        {
            movePlayer();
        }

        if(animatorState.IsName("Eat") && animatorState.normalizedTime >= 1.10f)
        {
            animator.SetBool("Eat", false);
        }
    }

    void movePlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if(verticalInput != 0 || horizontalInput !=0) {
            animator.SetBool("Run", true);
        } else {
            animator.SetBool("Run", false);
        }

        // move with the rotation
        Vector3 movement = new Vector3(-horizontalInput, 0.0f, -verticalInput);

        if(verticalInput != 0 || horizontalInput !=0) {
            transform.rotation = Quaternion.LookRotation(movement);
        }

        transform.Translate(movement * Time.deltaTime * speed, Space.World);

        //top bounds limit
        if(transform.position.z <= vertBoundsTop) {
            transform.position = new Vector3(transform.position.x, transform.position.y , vertBoundsTop);
        }

        // bottom bounds limit
        if(transform.position.z > vertBoundsBottom) {
            transform.position = new Vector3(transform.position.x, transform.position.y , vertBoundsBottom);
        }

        // horizontal bounds limit right
        if(transform.position.x > horizBounds) {
            transform.position = new Vector3(horizBounds, transform.position.y , transform.position.z);
        }

        // horizontal bounds limit left
        if(transform.position.x < -horizBounds) {
            transform.position = new Vector3(-horizBounds, transform.position.y , transform.position.z);
        }
    }

    // eat the food little chick
    public void EatFood()
    {
        Debug.Log("yum yum");
        curScale = curScale * curScaleRate;
        gameManager.curFoodCount++;
        gameManager.foodCountText.text = gameManager.curFoodCount.ToString();
        transform.localScale = new Vector3(playerScale.x*curScale,  playerScale.y*curScale, playerScale.z*curScale);
        animator.SetBool("Eat", true);
        foodSound.Play();
        cluckSound.Play();
    }

    // when the chick hits something
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Vehicle"))
        {
            if(playerIsDead == false) {
                gameManager.deadSound.Play();
                gameManager.impactSound.Play();
                gameManager.gameOverSound.Play();
                gameManager.musicSound.Stop();
            }

            //vehicle hit
            Debug.Log("got hit by "+ other.gameObject.name);
            feathers.Play();
            blood.Play();
            //addforce away from vehicle
            playerRig.AddForce((gameObject.transform.position - other.transform.position).normalized * 1000, ForceMode.Impulse);
            playerRig.constraints = RigidbodyConstraints.None;
            playerIsDead = true;
            
            gameManager.GameOver();
        }    
    }

    // have I touched food?
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Food"))
        {
            EatFood();
            gameManager.SpawnFood();
        }
    }
}
