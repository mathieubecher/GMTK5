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
    public GameObject ValidateBtn;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {



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

    public void UpdateKeyboardInput()
    {
        ValidateBtn.SetActive(true);
        var foundInputHintObjects = FindObjectsOfType<UI_InputHint>();
        foreach (UI_InputHint inputHint in foundInputHintObjects)
        {
            inputHint.GetComponent<UI_InputHint>().OnKeyboardInput();
        }
    }

    public void UpdatePsInput()
    {
        ValidateBtn.SetActive(true);
        var foundInputHintObjects = FindObjectsOfType<UI_InputHint>();
        foreach (UI_InputHint inputHint in foundInputHintObjects)
        {
            inputHint.GetComponent<UI_InputHint>().OnPsInput();
        }
    }

    public void UpdateXboxInput()
    {
        ValidateBtn.SetActive(true);
        var foundInputHintObjects = FindObjectsOfType<UI_InputHint>();
        foreach (UI_InputHint inputHint in foundInputHintObjects)
        {
            inputHint.GetComponent<UI_InputHint>().OnXboxInput();
        }
    }

    public void HideValidate()
    {
        ValidateBtn.SetActive(false);
    }

    public void DeselectAll()
    {
        GetComponent<Animator>().ResetTrigger("Instructions");
        GetComponent<Animator>().ResetTrigger("Back");
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }


}
