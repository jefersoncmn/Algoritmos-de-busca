using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe que será o estado que fará a operação dos algoritmos de busca
/// </summary>
public class PathFindingState : SimulatorState
{
    Cell[] cellmap = new Cell[36];

    GeneralController generalController;

    Cell goalCell;

    /// <summary>
    /// Construtor da classe que reproduzirá os algoritmos de busca
    /// </summary>
    /// <param name="cellmapObject"></param>
    public PathFindingState(GeneralController generalController)
    {
        this.generalController = generalController;

        goalCell = defineGoal();
        if (goalCell == null)
        {
            Debug.Log("Error: goalCell is null! I need a goal");
            return;
        }

        BreadthFirstSearch breadthFirstSearch = new BreadthFirstSearch(this.generalController);
        DepthFirstSearch depthFirstSearch = new DepthFirstSearch(this.generalController);
        BestFirstSearch bestFirstSearch = new BestFirstSearch(this.generalController, goalCell);
        AStarSearch aStarSearch = new AStarSearch(this.generalController, goalCell);


        cleanPathMemory(cellmap);
        generalController.setSucessorFuctionLargura(breadthFirstSearch.BuscaLargura(cellmap));
        cleanPathMemory(cellmap);
        generalController.setSucessorFuctionProfundidade(depthFirstSearch.BuscaProfundidade(cellmap));
        cleanPathMemory(cellmap);
        generalController.setSucessorFuctionGulosa(bestFirstSearch.BuscaGulosa(cellmap));
        cleanPathMemory(cellmap);
        generalController.setSucessorFuctionAStar(aStarSearch.BuscaAStar(cellmap));

        generalController.simulate();

    }

    /// <summary>
    /// Função que define a celula objetivo do mapa.
    /// Que os algoritmos de busca irão encontrar uma rota pra chegar nela.
    /// /// </summary>
    Cell defineGoal()
    {
        for (int i = 0; i < generalController.cellmap.Length; i++)
        {
            cellmap[i] = generalController.cellmap[i].GetComponent<Cell>() as Cell;
            if (i == generalController.celulaObjetivo)
            {
                cellmap[i].endPoint = true;
                return cellmap[i];
            }
        }
        return null;
    }

    void cleanPathMemory(Cell[] cellmap)
    {
        for (int i = 0; i < cellmap.Length; i++)
        {
            cellmap[i].pathmemory = new List<Cell>();
        }
    }
}
