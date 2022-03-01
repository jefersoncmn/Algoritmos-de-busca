using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestFirstSearch
{

    GeneralController generalController;

    Cell goalCell;

    public bool encontrado = false;

    bool getEncontrado()
    {
        return encontrado;
    }

    public BestFirstSearch(GeneralController generalController, Cell goalCell)
    {
        this.generalController = generalController;
        this.goalCell = goalCell;
    }

    public List<Cell> BuscaGulosa(Cell[] cells)
    {
        List<Cell> verticesMarcados = new List<Cell>();
        List<Cell> melhoresValoresHeuristicos = new List<Cell>();
        List<Cell> path = new List<Cell>();
        encontrado = false;
        Cell ponteiro;

        ponteiro = cells[0];
        verticesMarcados.Add(ponteiro);

        ponteiro.pathmemory.Add(ponteiro);

        BuscaGulosaAlgoritmo(ponteiro, verticesMarcados, melhoresValoresHeuristicos, path);

        for (int i = 0; i < verticesMarcados.Count; i++)
        {
            generalController.exploredCellsGulosa.Add(verticesMarcados[i]);
        }

        for (int i = 0; i < path.Count; i++)
        {
            generalController.gulosaMovimentCost += ((double)path[i].ambientType);
        }

        generalController.gulosaMemoryCost = verticesMarcados.Count;

        return path;
    }


    List<Cell> BuscaGulosaAlgoritmo(Cell cell, List<Cell> verticesMarcados, List<Cell> melhoresValoresHeuristicos, List<Cell> path)
    {
        Cell[] adjacente = new Cell[4];
        adjacente = PathFindingUtil.CollectAdjacentsCells(cell);

        if (cell.endPoint == true)
        {
            for (int index = 0; index < cell.pathmemory.Count; index++)
            { //pega as rotas antigas pra celula nova explorada e guarda na memoria
                path.Add(cell.pathmemory[index]);
            }
            encontrado = true;

            return null;
        }

        for (int i = 0; i < 4; i++)
        {

            if (adjacente[i] != null)
            {

                if (!verticesMarcados.Contains(adjacente[i]) && !cell.endPoint == true)
                {
                    verticesMarcados.Add(adjacente[i]);

                    if (adjacente[i].pathmemory.Count == 0)
                    {
                        for (int index = 0; index < cell.pathmemory.Count; index++)
                        { //pega as rotas antigas pra celula nova explorada e guarda na memoria
                            adjacente[i].pathmemory.Add(cell.pathmemory[index]);
                        }
                        adjacente[i].pathmemory.Add(adjacente[i]);
                    }

                    melhoresValoresHeuristicos.Add(adjacente[i]);
                }
            }
        }
        if (melhoresValoresHeuristicos.Count == 0)
        {
            return null;
        }
        Cell melhorCelula = melhoresValoresHeuristicos[0];
        melhoresValoresHeuristicos.Remove(cell);
        for (int i = 0; i < melhoresValoresHeuristicos.Count; i++) //isso vai assegurar que ele só vai andar pelo que tem a melhor heurística
        {
            if (Vector3.Distance(melhoresValoresHeuristicos[i].gameObject.transform.position, goalCell.transform.position) < Vector3.Distance(melhorCelula.gameObject.transform.position, goalCell.transform.position))
            {
                melhorCelula = melhoresValoresHeuristicos[i];
            }

        }

        if (melhorCelula != null && getEncontrado() == false)
        {
            BuscaGulosaAlgoritmo(melhorCelula, verticesMarcados, melhoresValoresHeuristicos, path);
        }

        return null;
    }
}
