using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioSource gameOverSound;
    public AudioSource ambientSound;
    public AudioSource musicSound;
    public AudioSource deadSound;
    public AudioSource impactSound;
    public GameObject food;
    public GameObject ScreenGameOver;
    public TextMeshProUGUI foodCountText;
    public float[] pathZPos;
    public int foodCount = 0;
    public int curFoodCount;
    
    // Start is called before the first frame update
    void Start()
    {
        curFoodCount = 0;
        SpawnFood();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            SpawnFood();
        }
    }

    public void SpawnFood() 
    {
        float xPos = Random.Range(-35f, 35f);
        int zPosRandom = Random.Range(0, pathZPos.Length);
        float zPos = pathZPos[zPosRandom];
        Vector3 foodPos = new Vector3(xPos, -0.85f, zPos);
        
        food.transform.position = foodPos;
    }

    public void GameOver()
    {
        ScreenGameOver.SetActive(true);
        Debug.Log("Game Over");
    }

    public void Restart()
    {
        Debug.Log("Restart Game");
        SceneManager.LoadScene("GameScene");
    }

    public void Quit()
    {
        Debug.Log("Quit Game");
        SceneManager.LoadScene("MenuScene");
    }
}
