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
        generalController.cellmap = new GameObject[36];

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
                cellmap[i].gameObject.AddComponent(typeof(Cell));//Coloca a classe Cell no bloco
                Cell cell = cellmap[i].GetComponent(typeof(Cell)) as Cell;//Pega a classe Cell que foi colocada no bloco (feita na linha anterior)
                cell.gameObject = cellmap[i];//E dentro da classe Cell define o gameObject pra ele saber quem é o objeto dele
                generalController.cellmap[i] = cellmap[i];//Armazena os objetos na classe GeneraController
                defineTerrain(generalController.cellmap[i]);//Define o tipo de terreno
                i++;//Vamos para o próximo bloco
            }
        }


    }

    /// <summary>
    /// Função para definir que tipo de terreno o bloco de caminho irá ser
    /// </summary>
    /// <param name="cell">O objeto do caminho</param>
    void defineTerrain(GameObject cellObject)
    {
        Cell cell = cellObject.GetComponent(typeof(Cell)) as Cell;

        float perlinResult = Mathf.PerlinNoise((float)cellObject.gameObject.transform.position.x / 6 * Random.Range(0, 10), (float)cellObject.gameObject.transform.position.z / 6 * Random.Range(0, 10));

        if (perlinResult < 0.2f)
        {
            cell.ambientType = AmbientType.Solido;
            var cubeRenderer = cellObject.GetComponent<Renderer>();
            cubeRenderer.material.color = Color.gray;
        }
        else if (perlinResult >= 0.2f && perlinResult < 0.4f)
        {
            cell.ambientType = AmbientType.Plano;
            var cubeRenderer = cellObject.GetComponent<Renderer>();
            cubeRenderer.material.color = Color.green;
        }
        else if (perlinResult >= 0.4f && perlinResult < 0.6f)
        {
            cell.ambientType = AmbientType.Arenoso;
            var cubeRenderer = cellObject.GetComponent<Renderer>();
            cubeRenderer.material.color = Color.red;
        }
        else if (perlinResult >= 0.6f && perlinResult < 0.8f)
        {
            cell.ambientType = AmbientType.Rochoso;
            var cubeRenderer = cellObject.GetComponent<Renderer>();
            cubeRenderer.material.color = Color.grey;
        }
        else
        {
            cell.ambientType = AmbientType.Pantano;
            var cubeRenderer = cellObject.GetComponent<Renderer>();
            cubeRenderer.material.color = Color.black;
        }

    }



}
