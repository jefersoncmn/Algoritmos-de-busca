using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject columnAmbientTypes;
    public GameObject ButtonShowAmbientTypes;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void ShowcolumnAmbientTypes(Text buttonText)
    {
        columnAmbientTypes.SetActive(!columnAmbientTypes.activeSelf);
        if (columnAmbientTypes.activeSelf)
        {
            buttonText.text = "-";
        }
        else
        {
            buttonText.text = "+";
        }

    }
}
