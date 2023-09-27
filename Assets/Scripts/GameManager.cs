using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public HouseHandler houseHandler;
    public FactoryHandler factoryHandler;
    public PipeHandler pipeHandler;
    public Camera cam;

    private int size = 0;
    private bool running;

    #region Placing
    private bool force = false;
    private bool connect;
    #endregion

    public void Start()
    {
        // Placeholder
        StartGame();
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        pipeHandler.Expand(1);
        pipeHandler.Expand(2);
        pipeHandler.Expand(3);
        pipeHandler.Expand(4);
        pipeHandler.Expand(5);
        pipeHandler.Expand(6);
        factoryHandler.PlaceFactory(new Vector2(2, 2), 1);
        factoryHandler.PlaceFactory(new Vector2(-3, 2), 2);
        factoryHandler.PlaceFactory(new Vector2(-3, -3), 3);
        factoryHandler.PlaceFactory(new Vector2(2, -3), 4);
        size = 6;
        running = true;
    }

    public void ResetGame()
    {
        pipeHandler.Reset();
        factoryHandler.Reset();
        houseHandler.Reset();
        running = false;
        size = 1;
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
}
