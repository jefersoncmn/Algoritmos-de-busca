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
    public List<Cell> sucessorFuctionProfundidade;
    public List<Cell> sucessorFuctionLargura;
    public List<Cell> sucessorFuctionGulosa;
    public List<Cell> sucessorFuctionAStar;
    public double profundidadeTime;
    public double larguraTime;
    public double gulosaTime;
    public double aStarTime;
    public int profundidadeMemoryCost;
    public int larguraMemoryCost;
    public int gulosaMemoryCost;
    public int aStarMemoryCost;

    public double profundidadeMovimentCost;
    public double larguraMovimentCost;
    public double gulosaMovimentCost;
    public double aStarMovimentCost;

    public Text textMemory;
    public Text textExecutionTime;
    public Text textMovimentCost;
    public Text textAlgoritm;

    MapGeneratorState mapGenerationState;

    RunPathState runPathState;

    int algoritmOrder = 2;

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
    public void showValues(string algoritmName, int memory, double executionTime, double movimentCost)
    {
        textAlgoritm.text = "" + algoritmName;
        textMemory.text = "Nós expandidos na memória: " + memory;
        textMovimentCost.text = "Custo de movimentação: " + movimentCost;
        textExecutionTime.text = "Tempo de execução: " + executionTime;
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
                    showValues("Busca em Largura", larguraMemoryCost, larguraTime, larguraMovimentCost);
                    algoritmOrder++;
                    break;
                case 1:
                    runPathState.RunProfundidade();
                    showValues("Busca em profundidade", profundidadeMemoryCost, profundidadeTime, profundidadeMovimentCost);
                    algoritmOrder++;
                    break;
                case 2:
                    runPathState.RunGulosa();
                    showValues("Busca Gulosa", gulosaMemoryCost, gulosaTime, gulosaMovimentCost);
                    algoritmOrder++;
                    break;
                case 3:
                    runPathState.RunAStar();
                    showValues("Busca A*", aStarMemoryCost, aStarTime, aStarMovimentCost);
                    algoritmOrder++;
                    break;
                default:
                    break;
            }
        }

    }
}