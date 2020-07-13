using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : MonoBehaviour
{
    static int _points;
    public bool gameover;
 
    public Transform tilesFolder;
    public LevelGenerator levelGenerator;
    public Life life;
    public MainMenu mainMenu;
    public Background background;

    public static int points
    {
        get => _points;
        set
        {
            _points = value;
            if (_points > highscore)
            {
                highscore = _points;
            }
        }
    }
    public static int highscore;

    public void PauseGame(bool condition)
    {
        Time.timeScale = condition ? 0f : 1f;
    }

    public void ExitGame()
    {
        SaveData(gameover);
        Application.Quit();
    }
    private void Start()
    {
        Time.timeScale = 0f;
        LoadData();
    }
    int pointsTemp;
    private void Update()
    {
        if(pointsTemp<points)
        {
            CheckMapStatus();
        }
        pointsTemp = points;

        if(Input.GetKey(KeyCode.Escape))
        {
            if(Time.deltaTime!=0)
            {
                SaveData(false);
            }
            mainMenu.SwitchPauseMenu(true);
            mainMenu.AllowContinue(!gameover);
            
        }
    }

    void CheckMapStatus()
    {
        if(tilesFolder.childCount==0)
        {
            NewGame(false);
            life.levelCompletedAnimator.Play("LevelCompleted",-1,0f);
        }
    }

    public void SaveData(bool _gameover)
    {
        gameover = _gameover;
        List<Tile> tiles = new List<Tile>();
        if (_gameover)
        {
            SaveSystem.SaveMap(tiles, 0, 0, highscore, _gameover);
        }
        else
        {
            foreach (Transform children in tilesFolder)
            {
                Tile tile = children.GetComponent<Tile>();
                if (tile != null)
                {
                    tiles.Add(tile);
                }
            }
            SaveSystem.SaveMap(tiles, life.life, points, highscore, _gameover);
        } 
    }

    public void LoadData()
    {
        ProgressData progressData= SaveSystem.LoadData();
        //LevelGenerator levelGenerator = tilesFolder.GetComponent<LevelGenerator>();
        if(progressData==null)
        {
            //NewGame();
            mainMenu.continueGameButton.interactable = false;
        }
        else
        {
            highscore = progressData.highscore;
            if (!progressData.gameover)
            {
                points = progressData.points;
                life.life = progressData.health;

                levelGenerator.ClearMap();
                foreach (SerializableTile serializableTile in progressData.serializableTiles)
                {
                    levelGenerator.LoadTile(serializableTile);
                }
            }
            else
            {
                //NewGame();
                mainMenu.continueGameButton.interactable = false;
            }
        }

    }
    
    public void NewGame(bool reset)
    {
        //LevelGenerator levelGenerator = tilesFolder.GetComponent<LevelGenerator>();

        //levelGenerator.ClearMap();
        //levelGenerator.GenerateTiles();

        StartCoroutine(levelGenerator.RegenerateMap());

        life.life = 3;
        life.LifeIndicatorUpdate();

        Ball []balls = FindObjectsOfType<Ball>();

        for(int i=1;i<balls.Length;i++)
        {
            Destroy(balls[i].gameObject);
        }
        balls[0].GetComponent<Rigidbody2D>().simulated = false;
        //life.gameOverAnimator.Play("GameOver");

        if(reset)
        {
            points = 0;
        }
        background.ChangeColor();
    }
}
