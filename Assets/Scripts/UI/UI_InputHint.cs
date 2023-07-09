using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_InputHint : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{

    public GameObject GamepadIcon;
    public GameObject KeyboardIcon;
    public Image Bkg;
    public Sprite PsInput;
    public Sprite XboxInput;
    public bool isClickable;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (isClickable == true)
        {
            GetComponent<Button>().interactable = true;
            Bkg.color = Color.white;
        }
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Button>().interactable = false;
        Bkg.color = new Color32(255, 255, 255, 50);
    }

    public void OnKeyboardInput()
    {
        GamepadIcon.SetActive(false);
        KeyboardIcon.SetActive(true);
        
    }

    public void OnPsInput()
    {
        GamepadIcon.SetActive(true);
        KeyboardIcon.SetActive(false);
        GamepadIcon.GetComponent<Image>().sprite = PsInput;

    }

    public void OnXboxInput()
    {
        GamepadIcon.SetActive(true);
        KeyboardIcon.SetActive(false);
        GamepadIcon.GetComponent<Image>().sprite = XboxInput;

    }


}
