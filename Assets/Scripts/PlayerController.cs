using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    GameManager gameManager;
    public float speed = 15;
    private Rigidbody playerRig;
    private bool playerIsDead = false;
    public float horizontalInput;
    public float verticalInput;
    private float horizBounds = 35.0f;
    private float vertBoundsTop = -15.0f;
    private float vertBoundsBottom = 20.0f;

    void Start() {
        animator = GameObject.Find("Player").GetComponent<Animator>();
        gameManager = GameObject.Find("_GameManager").GetComponent<GameManager>();;
        playerRig = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerIsDead) 
        {
            movePlayer();
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
        //transform.Translate(Vector3.back * verticalInput * Time.deltaTime * speed);

        //top bounds limit
        if(transform.position.z <= vertBoundsTop) {
            //transform.Translate(Vector3.forward / 9);
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

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Vehicle"))
        {
            //vehicle hit
            Debug.Log("got hit by "+ other.gameObject.name);

            //addforce away from vehicle
            playerRig.AddForce((gameObject.transform.position - other.transform.position).normalized * 1000, ForceMode.Impulse);
            playerRig.constraints = RigidbodyConstraints.None;
            playerIsDead = true;
            
            //destroy player with particle effects
        }    
    }
}
