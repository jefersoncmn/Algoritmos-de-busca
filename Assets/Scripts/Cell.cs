using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe da célula que irá compor o mapa
/// </summary>
public class Cell : MonoBehaviour
{
    GameObject gameObject;
    Cell left, right, up, down;
    AmbientType ambientType;
    int coins;
    bool endPoint;

}
