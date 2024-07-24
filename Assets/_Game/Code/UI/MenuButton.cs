using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Audio;

public class MenuButton : MonoBehaviour, IPointerEnterHandler
{
    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ClickButton);
    }

    public void ClickButton()
    {
        print("Click " + this.name);
        AudioBox.Instance.Play("MenuClick");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        print("Enter " + this.name);
        AudioBox.Instance.Play("MenuMove");
        //throw new System.NotImplementedException();
    }
}
