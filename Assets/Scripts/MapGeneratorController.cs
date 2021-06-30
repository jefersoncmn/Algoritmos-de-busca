using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneratorController : MonoBehaviour
{
    public GameObject cellmodel;
    public GameObject[] cellmap;


    // Start is called before the first frame update
    void Start()
    {
        cellmap = new GameObject[36];
        spawnPaths(6, 6);
    }

    /// <summary>
    /// Código responsável por instanciar os caminhos do mapa
    /// </summary>
    /// <param name="columns">Quantidade de colunas</param>
    /// <param name="lines">Quantidade de linhas</param>
    void spawnPaths(int columns, int lines)
    {
        int i = 0;

        for (int x = 0; x < columns; x++)
        {
            for (int z = 0; z < lines; z++)
            {
                cellmap[i] = Instantiate(cellmodel, new Vector3(x, 0, z), Quaternion.identity);

                i++;
            }
        }
    }

    /// <summary>
    /// Função básica para definir que tipo de terreno o bloco de caminho irá ser
    /// </summary>
    /// <param name="gameObject">O objeto do caminho</param>
    void defineTerrain(GameObject gameObject)
    {

        if (Mathf.PerlinNoise(0, 4) > 0.3)
        {

        }

    }



}
