using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject columnAmbientTypes;
    public GameObject ambientTypesUIPrefab;

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

    private void Start()
    {
        InstantiateAmbientTypesTiles(columnAmbientTypes);
    }

    public void ChangeButtonShowColumnAmbientTypes(Text buttonText)
    {
        SwitchActiveUI(columnAmbientTypes);
        if (columnAmbientTypes.activeSelf)
        {
            buttonText.text = "-";
        }
        else
        {
            buttonText.text = "+";
        }

    }

    public void InstantiateAmbientTypesTiles(GameObject _columnAmbientTypes)
    {
        Color[] colors = { Color.cyan, Color.green, Color.yellow, Color.grey, Color.black };

        if (!_columnAmbientTypes)
        {
            return;
        }
        int index = 0;
        foreach (int ambientType in Enum.GetValues(typeof(AmbientType)))
        {

            GameObject instantiatedTile = Instantiate(ambientTypesUIPrefab);
            Text text = instantiatedTile.GetComponentInChildren<Text>();
            Image image = instantiatedTile.GetComponentInChildren<Image>();

            text.text = Enum.GetNames(typeof(AmbientType))[index].ToString() + " " + ambientType.ToString();
            image.color = colors[index];

            if (colors[index] == text.color)
            {
                text.color = Color.white;
            }

            index++;
            instantiatedTile.transform.SetParent(_columnAmbientTypes.transform);
        }
    }

    public void SwitchActiveUI(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

}
