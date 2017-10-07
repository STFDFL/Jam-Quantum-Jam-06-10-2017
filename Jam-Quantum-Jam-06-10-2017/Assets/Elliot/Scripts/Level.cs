using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Themes myTheme;
    public PlayerState myPlayerState;

    public GameObject myPlayer;
    public GameObject[] mySections;
    public GameObject[] obstacles;
    public GameObject[] enemies;
    public GameObject[] pickups;

    public void LevelConstructor(Themes themes, float yPosition)
    {
        myTheme = themes;

        this.transform.GetChild(3).GetComponent<Renderer>().material = GameManager.Instance.GetThemeColour(myTheme);

        this.transform.position = new Vector3(0, yPosition, 0);

        //for (int i = 0; i < GameManager.Instance.numberOfSections; i++)
        //{
        //    GameObject newSection = Instantiate(GameManager.Instance.sectionPrefab, this.transform);
        //    newSection.transform.position = new Vector3(newSection.transform.localScale.x * i, 0, 0);
        //}
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
