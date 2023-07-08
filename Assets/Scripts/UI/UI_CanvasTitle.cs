using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UI_CanvasTitle : MonoBehaviour
{
    public int phase = 0;
    public Button PlayBtn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            Debug.Log("A key or mouse click has been detected");

            if (phase == 0)
            {
                GetComponent<Animator>().SetTrigger("ClickSkip");

            } else if (phase == 1)
            {
                phase = 2;
                GetComponent<Animator>().SetTrigger("PressAnyKey");

            } 
        }
    }

    public void SelectPlay()
    {
        PlayBtn.Select();
    }

    public void LauchGame()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }


}
