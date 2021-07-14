using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe responsavel por ser o estado que irá gerar o mapa para simulação dos algoritmos de busca
/// </summary>
public class MapGeneratorState : MonoBehaviour, SimulatorState
{

    public GameObject[] cellmap;

    public GeneralController generalController;

    public void SetGeneralController(GeneralController generalController)
    {
        this.generalController = generalController;
    }

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
                cellmap[i] = Instantiate(generalController.cellmodel, new Vector3(x, 0, z), Quaternion.identity);//intancia um bloco
                defineTerrain(cellmap[i]);
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
