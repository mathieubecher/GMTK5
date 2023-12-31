using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_ButtonAC : MonoBehaviour, IDeselectHandler, ISelectHandler, IPointerExitHandler, IPointerEnterHandler
{

    public TMPro.TMP_Text Text_Btn;
    public Image Checkmark_Object;
    public GameObject Focus_Bkg;
    public Sprite Checkmark_True;
    public Sprite Checkmark_False;
    public Sprite Checkmark_True_Focused;
    public Sprite Checkmark_False_Focused;
    public bool On;
    private bool isFocused;
    public GameObject Twin_Button;


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
        DeselectAll();
        OnFocused();
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        DeselectAll();
        OnUnfocused();
    }



    public void OnSelect(BaseEventData eventData) => OnFocused();


    private void OnFocused()
    {
        isFocused = true;
        Text_Btn.color = Color.white;
        Focus_Bkg.SetActive(true);
        if (On == true)
        {
            Checkmark_Object.sprite = Checkmark_True_Focused;

        } else
        {
            Checkmark_Object.sprite = Checkmark_False_Focused;
        }


    }


    public void OnDeselect(BaseEventData eventData) => OnUnfocused();


    private void OnUnfocused()
    {
        isFocused = false;
        Text_Btn.color = Color.black;
        Focus_Bkg.SetActive(false);
        if (On == true)
        {
            Checkmark_Object.sprite = Checkmark_True;

        } else
        {
            Checkmark_Object.sprite = Checkmark_False;
        }


    }

    public void UpdateCheckmark(bool Active, bool isImperialButton)
    {
        On = Active;
        UI_MetricsInformation.instance.IsImperial = isImperialButton;

        if (isFocused == true)
        {
            
            if (On == true)
            {
                Twin_Button.GetComponent<UI_ButtonAC>().UpdateCheckmark(false, isImperialButton);
                Checkmark_Object.sprite = Checkmark_True_Focused;

            }
            else
            {
                Checkmark_Object.sprite = Checkmark_False_Focused;
            }

        } else
        {
            if (On == true)
            {
                Twin_Button.GetComponent<UI_ButtonAC>().UpdateCheckmark(false, isImperialButton);
                Checkmark_Object.sprite = Checkmark_True;

            }
            else
            {
                Checkmark_Object.sprite = Checkmark_False;
            }
        }
    }

    public void OnButtonCheckmarkClick(bool isImperialButton)
    {
        UpdateCheckmark(true, isImperialButton);
    }


    public void DeselectAll()
    {
        var eventSystem = FindObjectOfType<EventSystem>();
        if(eventSystem) eventSystem.SetSelectedGameObject(null);
    }
}
