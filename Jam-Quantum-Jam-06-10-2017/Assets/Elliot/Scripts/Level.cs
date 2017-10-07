using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Themes myTheme;
    public PlayerState myPlayerState;

    public GameObject myPlayer;
    public List<GameObject> mySections = new List<GameObject>();
    //public GameObject[] obstacles;
    //public GameObject[] enemies;
    //public GameObject[] pickups;

    public void LevelConstructor(Themes themes, float yPosition)
    {
        myTheme = themes;

        //this.transform.GetChild(3).GetComponent<Renderer>().material = GameManager.Instance.GetThemeColour(myTheme);

        this.transform.position = new Vector3(0, yPosition, 0);

        myPlayer = this.transform.GetChild(0).gameObject;

        GameManager.Instance.activePlayers.Add(myPlayer);

        //for (int i = 0; i < GameManager.Instance.numberOfSections; i++)
        //{
        //    GameObject newSection = Instantiate(GameManager.Instance.sectionPrefab, this.transform);
        //    newSection.transform.position = new Vector3(newSection.transform.localScale.x * i, 0, 0);
        //}

        foreach (Transform section in this.transform.GetChild(8))
        {
            mySections.Add(section.gameObject);
        }
    }


    public void AddToSection(GameObject toAdd)
    {
        for (int i = 0; i < mySections.Count - 1; i++)
        {
            // This is the object location to spawn to
            mySections[i].transform.GetChild(0);
        }
    }
}
