using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthFirstSearch
{
    GeneralController generalController;

    public bool encontrado = false;

    bool getEncontrado()
    {
        return encontrado;
    }

    public DepthFirstSearch(GeneralController generalController)
    {
        this.generalController = generalController;
    }

    /// <summary>
    /// Função que inicializa o ambiente e variáveis para o uso do algoritmo de busca por profundidade 
    /// </summary>
    /// <param name="cells">O vetor com as celulas do mapa</param>
    /// <returns>Retorna a lista com o caminho das celulas que percorreu</returns>
    public List<Cell> BuscaProfundidade(Cell[] cells)
    {
        //Debug.Log("Busca em Profundidade inicializada!");
        List<Cell> verticesMarcados = new List<Cell>();
        List<Cell> path = new List<Cell>();
        Cell ponteiro;

        ponteiro = cells[0];
        verticesMarcados.Add(ponteiro);

        ponteiro.pathmemory.Add(ponteiro);

        DeepFindSearch(ponteiro, verticesMarcados, path);

        generalController.profundidadeMemoryCost = path.Count;

        for (int i = 0; i < verticesMarcados.Count; i++)
        {
            //Debug.Log("Posição percorrida " + i + " celula =" + verticesMarcados[i].coins);
            generalController.exploredCellsProfundidade.Add(verticesMarcados[i]);
        }

        for (int i = 0; i < path.Count; i++)
        {
            generalController.profundidadeMovimentCost += ((double)path[i].ambientType);
        }

        return path;
    }

    /// <summary>
    /// Algoritmo de busca por profundidade
    /// Ele é recursivo
    /// </summary>
    /// <param name="ponteiroAuxiliar">Celula que está explorando</param>
    /// <param name="verticesMarcados">Lista com celulas que já foram visitadas</param>
    /// <returns></returns>
    List<Cell> DeepFindSearch(Cell ponteiro, List<Cell> verticesMarcados, List<Cell> path)
    {

        Cell[] adjacente = new Cell[4];
        adjacente = PathFindingUtil.CollectAdjacentsCells(ponteiro);

        if (ponteiro.endPoint == true)
        {
            for (int x = 0; x < ponteiro.pathmemory.Count; x++)
            {
                path.Add(ponteiro.pathmemory[x]);
            }
            encontrado = true;
            return null;
        }

        for (int i = 0; i < 4; i++)//ele percorre todos as arestas
        {
            if (getEncontrado() != true)
            {
                if (adjacente[i] != null)
                {
                    if (!verticesMarcados.Contains(adjacente[i]) && !ponteiro.endPoint == true)
                    {
                        verticesMarcados.Add(adjacente[i]);

                        if (adjacente[i].pathmemory.Count == 0)
                        {
                            for (int index = 0; index < ponteiro.pathmemory.Count; index++)
                            { //pega as rotas antigas pra celula nova explorada e guarda na memoria
                                adjacente[i].pathmemory.Add(ponteiro.pathmemory[index]);
                            }
                            adjacente[i].pathmemory.Add(adjacente[i]);
                        }
                        DeepFindSearch(adjacente[i], verticesMarcados, path);
                    }

                }
            }

        }
        return null;
    }
}
