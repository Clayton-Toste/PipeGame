using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public HouseHandler houseHandler;
    public FactoryHandler factoryHandler;
    public PipeHandler pipeHandler;
    public Camera cam;

    private bool running;
    public int lives = 3;

    #region Placing
    private bool force = false;
    private bool connect;
    #endregion

    public void CloseGame()
    {
        Application.Quit();
    }

    public void Start()
    {
        pipeHandler.PlaceTile(new Vector2(0, 0));                      
        factoryHandler.PlaceFactory(new Vector2(2, 7), 1);
        factoryHandler.PlaceFactory(new Vector2(7, 7), 2);
        factoryHandler.PlaceFactory(new Vector2(2, 2), 3);
        factoryHandler.PlaceFactory(new Vector2(7, 2), 4);

        PlaceTile(new Vector2(10, 10));
        PlaceTile(new Vector2(10, 0));
        PlaceTile(new Vector2(10, -10));
        PlaceTile(new Vector2(0, 10));
        PlaceTile(new Vector2(0, -10));
        PlaceTile(new Vector2(-10, 10));
        PlaceTile(new Vector2(-10, 0));
        PlaceTile(new Vector2(-10, -10));

        running = true;

        FindObjectOfType<SoundManager>().Play("BackgroundMusic");
    }

    public void PlaceTile(Vector2 location)
    {
        pipeHandler.PlaceTile(location);
        houseHandler.PlaceTile(location);
    }

    public void ResetGame()
    {
        pipeHandler.Reset();
        factoryHandler.Reset();
        houseHandler.Reset();
        running = false;
        lives = 3;
    }

    public void Hit()
    {
        lives -= 1;

        if (lives <= 0)
        {
            Lose();
        }
    }

    public void Lose()
    {
        ResetGame();
        GameOver();
    }

    public void Update()
    {
        if (running)
        {
            if (Input.GetMouseButton(0))
            {
                RaycastHit2D[] pipes = Physics2D.CircleCastAll(cam.ScreenToWorldPoint(Input.mousePosition), .05f, Vector3.zero);
                if (pipes.Length == 2)
                {
                    connect = pipes[0].transform.GetComponent<Pipe>().Connect(pipes[1].transform.GetComponent<Pipe>(), force, connect);
                    force = true;
                    pipeHandler.RefreshAll();
                }
            }
            else 
            {
                force = false;
            }
        }
    }

    #region SceneTransitions
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene2");
        // StartProtocol();
    }
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    #endregion
}
