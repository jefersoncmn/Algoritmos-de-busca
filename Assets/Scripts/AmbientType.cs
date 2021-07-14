using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enum que conterá os tipos de terreno e seus custos de movimentação que serão usados nos algoritmos de busca.
/// </summary>
public enum AmbientType
{
    Solido = 1,
    Plano = 1,
    Rochoso = 10,
    Arenoso = 4,
    Pantano = 20,
}
