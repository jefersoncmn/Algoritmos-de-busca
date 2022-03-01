using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadthFirstSearch
{
    GeneralController generalController;
    public BreadthFirstSearch(GeneralController generalController)
    {
        this.generalController = generalController;
    }

    /// <summary>
    /// Algoritmo de busca em largura
    /// </summary>
    /// <param name="cells">Recebe um vetor de celulas do mapa</param>
    /// <returns>Retorna uma lista com as ações tomadas até o objetivo ou nulo caso não encontre</returns>
    public List<Cell> BuscaLargura(Cell[] cells)
    {
        //Debug.Log("Busca em Lagura inicializada!");
        Queue<Cell> fila = new Queue<Cell>();

        List<Cell> path = new List<Cell>();

        List<Cell> verticesMarcados = new List<Cell>();

        Cell ponteiro;

        Cell[] adjacente = new Cell[4];

        ponteiro = cells[0];

        verticesMarcados.Add(ponteiro);//marca a raiz como visitada

        fila.Enqueue(ponteiro);//coloca raiz na fila

        ponteiro.pathmemory.Add(ponteiro);

        while (fila.Count != 0)
        {
            ponteiro = fila.Peek();
            //Debug.Log("Ponteiro auxili =" + ponteiro.coins);
            adjacente = PathFindingUtil.CollectAdjacentsCells(ponteiro);

            int i = 0;
            while (i < 4) //percorre os vertices adjacentes
            {
                if (adjacente[i] != null)
                {
                    if (adjacente[i].pathmemory.Count == 0)
                    {
                        for (int index = 0; index < ponteiro.pathmemory.Count; index++)
                        { //pega as rotas antigas pra celula nova explorada e guarda na memoria

                            adjacente[i].pathmemory.Add(ponteiro.pathmemory[index]);
                        }
                        adjacente[i].pathmemory.Add(adjacente[i]);
                    }


                    if (!verticesMarcados.Contains(adjacente[i])) //se nÃo foram percorridos
                    {
                        //Debug.Log("Celula " + adjacente[i].coins + " marcado como percorrido!");
                        verticesMarcados.Add(adjacente[i]); //adiciona na lista de percorridos
                        fila.Enqueue(adjacente[i]); //adiciona a fila 
                        generalController.larguraMemoryCost++;
                        generalController.exploredCellsLargura.Add(adjacente[i]);
                    }

                    if (adjacente[i].endPoint == true)
                    {

                        for (int x = 0; x < adjacente[i].pathmemory.Count; x++)
                        {
                            generalController.larguraMovimentCost += ((double)adjacente[i].pathmemory[x].ambientType); //custo de movimentacao
                        }

                        return adjacente[i].pathmemory;
                    }
                }

                i++;
            }

            fila.Dequeue();
        }

        return null;

    }
}
