using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe responsável pelo controle das informações do simulador.
/// </summary>
public class GeneralController : MonoBehaviour
{
    public GameObject[] cellmap;
    public SimulatorState simulatorState;
    public GameObject cellmodel;
    public List<Cell> sucessorFuctionProfundidade;
    public List<Cell> sucessorFuctionLargura;
    public List<Cell> sucessorFuctionGulosa;
    public List<Cell> sucessorFuctionAStar;
    double profundidadeTime;
    double larguraTime;
    double gulosaTime;
    double aStarTime;
    int profundidadeMemoryCost;
    int larguraMemoryCost;
    int gulosaMemoryCost;
    int aStarMemoryCost;

    MapGeneratorState mapGenerationState;
    PathFindingState pathFindingState;
    RunPathState runPathState;

    void Start()
    {

        mapGenerationState = gameObject.AddComponent(typeof(MapGeneratorState)) as MapGeneratorState;
        mapGenerationState.SetGeneralController(this);

        simulatorState = mapGenerationState;
    }
}