using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Classe responsável pelo controle das informações do simulador.
/// </summary>
public class GeneralController : MonoBehaviour
{

    public int sizeMap = 36;
    public int celulaObjetivo = 35;
    public int indiceDeMuros = 200;
    public GameObject[] cellmap;
    public SimulatorState simulatorState;
    public GameObject cellmodel;
    public GameObject testmodel;
    public GameObject explorationmodel;
    public List<Cell> sucessorFuctionProfundidade;
    public List<Cell> sucessorFuctionLargura;
    public List<Cell> sucessorFuctionGulosa;
    public List<Cell> sucessorFuctionAStar;
    public List<Cell> exploredCellsLargura;
    public List<Cell> exploredCellsProfundidade;
    public List<Cell> exploredCellsGulosa;
    public List<Cell> exploredCellsAStar;
    public int profundidadeMemoryCost = 0;
    public int larguraMemoryCost = 0;
    public int gulosaMemoryCost = 0;
    public int aStarMemoryCost = 0;

    public double profundidadeMovimentCost = 0;
    public double larguraMovimentCost = 0;
    public double gulosaMovimentCost = 0;
    public double aStarMovimentCost = 0;

    public Text textMemory;

    public Text textMovimentCost;
    public Text textAlgoritm;

    MapGeneratorState mapGenerationState;

    RunPathState runPathState;

    int algoritmOrder = 0;

    void Start()
    {
        mapGenerationState = gameObject.AddComponent(typeof(MapGeneratorState)) as MapGeneratorState;
        mapGenerationState.SetGeneralController(this);
        simulatorState = mapGenerationState;
    }

    public void simulate()
    {
        runPathState = gameObject.AddComponent(typeof(RunPathState)) as RunPathState;
        runPathState.SetGeneralController(this);
        simulatorState = runPathState;

    }

    /// <summary>
    /// Função que apresenta na interface informações do algoritmo
    /// </summary>
    public void showValues(string algoritmName, int memory, double movimentCost)
    {
        textAlgoritm.text = "" + algoritmName;
        textMemory.text = "Nós expandidos na memória: " + memory;
        textMovimentCost.text = "Custo de movimentação: " + movimentCost;

    }

    /// <summary>
    /// Menu simples para ir passando os testes
    /// </summary>
    public void nextAlgoritm()
    {
        if (runPathState.runningTest == false)
        {
            switch (algoritmOrder)
            {
                case 0:
                    runPathState.RunLargura();
                    showValues("Busca em Largura", larguraMemoryCost, larguraMovimentCost);
                    algoritmOrder++;
                    break;
                case 1:
                    runPathState.RunProfundidade();
                    showValues("Busca em profundidade", profundidadeMemoryCost, profundidadeMovimentCost);
                    algoritmOrder++;
                    break;
                case 2:
                    runPathState.RunGulosa();
                    showValues("Busca Gulosa", gulosaMemoryCost, gulosaMovimentCost);
                    algoritmOrder++;
                    break;
                case 3:
                    runPathState.RunAStar();
                    showValues("Busca A*", aStarMemoryCost, aStarMovimentCost);
                    algoritmOrder++;
                    break;
                default:
                    break;
            }
        }

    }
}