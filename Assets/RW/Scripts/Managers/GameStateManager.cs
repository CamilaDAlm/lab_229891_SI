using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance; // 1

    [HideInInspector] //Unity won't show the variable it's assigned to in the editor, but it can still be accessed from other scripts. 
    public int sheepSaved; // 2

    [HideInInspector]
    public int sheepDropped; // 3

    public int sheepDroppedBeforeGameOver; // 4
    public SheepSpawner sheepSpawner; // 5

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;

    }
    public void SavedSheep()
    {
        sheepSaved++;//+1
        UIManager.Instance.UpdateSheepSaved();

    }

    public void CyanSavedSheep()
    {
        sheepSaved += 2;
        if (sheepDropped > 0)
        {
            sheepDropped--; // 1
            UIManager.Instance.UpdateSheepDropped();
        }
        UIManager.Instance.UpdateSheepSaved();
    }

    public void MagSavedSheep()
    {
        sheepSaved += 3;
        UIManager.Instance.UpdateSheepSaved();

    }

    public void DroppedSheep()
    {
        sheepDropped++; // 1
        UIManager.Instance.UpdateSheepDropped();

        CheckGameOver();
    }

    public void WoodHitSheep()
    {
        sheepDropped+=2; // 1
        UIManager.Instance.UpdateSheepDropped();

        CheckGameOver();
    }

    public void MagDroppedSheep()
    {
        sheepDropped +=3; // 1
        UIManager.Instance.UpdateSheepDropped();

        CheckGameOver();
    }


    private void CheckGameOver()
    {
        if (sheepDropped >= sheepDroppedBeforeGameOver) // 2
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        sheepSpawner.canSpawn = false; // 1
        sheepSpawner.DestroyAllSheep(); // 2
        UIManager.Instance.ShowGameOverWindow();

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
        }

    }
}
