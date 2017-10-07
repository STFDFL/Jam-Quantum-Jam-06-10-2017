using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Themes { Red, Blue, Yellow, White, Black }
public enum PlayerState { Alive, Dead }
public enum Obstacles { Pit, Zigzag, Looping, Bouncing, Crushing }
public enum Pickups { ExtraLife, ObstacleRemover }


public class GameManager : MonoBehaviour
{


    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }

    }

    #region Camera Variables

    [HeaderAttribute("Camera Variables")]

    public GameObject player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    #endregion

    #region Character Variables

    [HeaderAttribute("Player Variables")]

    public float runSpeed = 3;
    public float runSpeedDamping = 0.5f;
    public float jumpSpeed = 4;
    public float fallSpeed = 4;

    #endregion

    #region Game Variables

    [HeaderAttribute("Game Variables")]

    public int numberOfPlayers = 0;

    public int numberOfSections = 0;

    public GameObject levelPrefab;

    public GameObject sectionPrefab;

    [Header("Active Levels")]
    public List<Level> levels = new List<Level>();

    [Header("Active Players")]
    public List<GameObject> activePlayers;

    public Material[] colours;

    public bool stopMovement = false;

    public GameObject LosePanel;

    public GameObject WinPanel;

    #endregion
    #region Sound Variables

    [HeaderAttribute("Sound Variables")]

    //public AudioClip audioClip_Jump;
    public AudioClip audioClip_Die;
    public AudioClip audioClip_LoseGame;
    public AudioClip audioClip_WinGame;

    #endregion
    // Use this for initialization
    void Start()
    {
        levels.Clear();
        activePlayers.Clear();

        for (int i = 0; i < numberOfPlayers; i++)
        {
            GameObject newLevelObject = Instantiate(levelPrefab);
            Level newLevel = newLevelObject.AddComponent<Level>();

            int rng = UnityEngine.Random.Range(0, 5);

            switch (rng)
            {
                case 0:
                    newLevel.LevelConstructor(Themes.Red, -6 * i);
                    break;

                case 1:
                    newLevel.LevelConstructor(Themes.Blue, -6 * i);
                    break;

                case 2:
                    newLevel.LevelConstructor(Themes.Yellow, -6 * i);
                    break;

                case 3:
                    newLevel.LevelConstructor(Themes.White, -6 * i);
                    break;

                case 4:
                    newLevel.LevelConstructor(Themes.Black, -6 * i);
                    break;

                default:
                    break;
            }

            levels.Add(newLevel);
        }

        if (player == null)
        {
            int playerToUse = Mathf.FloorToInt(numberOfPlayers / 2) - 1;
            player = levels[playerToUse].transform.GetChild(0).gameObject;
            Camera.main.gameObject.AddComponent<CharacterController2D>();
            Camera.main.GetComponent<CharacterController2D>().CharacterControllerConstructor(player);
            offset = new Vector3(20, ((playerToUse - 1) * -6) + 2f, -40);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Material GetThemeColour(Themes themes)
    {
        Material colourToUse;
        switch (themes)
        {
            case Themes.Red:
                colourToUse = colours[0];
                break;
            case Themes.Blue:
                colourToUse = colours[1];
                break;
            case Themes.Yellow:
                colourToUse = colours[2];
                break;
            case Themes.White:
                colourToUse = colours[3];
                break;
            case Themes.Black:
                colourToUse = colours[4];
                break;
            default:
                colourToUse = colours[colours.Length - 1];
                break;
        }

        return colourToUse;
    }

    public void KillPlayer(GameObject playerToKill)
    {
        if (activePlayers.Contains(playerToKill))
        {
            playerToKill.SetActive(false);
            playerToKill.transform.parent.GetChild(3).position = new Vector3(playerToKill.transform.parent.GetChild(3).position.x, playerToKill.transform.parent.GetChild(3).position.y, 0);
            playerToKill.transform.parent.GetChild(3).localScale = new Vector3(80, 5, 2);
            activePlayers.Remove(playerToKill);
            CheckIfAnyAlive();
        }
    }

    private void CheckIfAnyAlive()
    {
        if(activePlayers.Count < 0)
        {
            // End Game - Lose
            AudioSource aS = GetComponent<AudioSource>();
            aS.Stop();
            aS.clip = audioClip_LoseGame;
            aS.Play();
            LosePanel.SetActive(true);
            
        }
    }

    public void WinGame()
    {
        // End Game - Win
        AudioSource aS = GetComponent<AudioSource>();
        aS.Stop();
        aS.clip = audioClip_WinGame;
        aS.Play();
        WinPanel.SetActive(true);

    }

    public void LoadLevel(string levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
