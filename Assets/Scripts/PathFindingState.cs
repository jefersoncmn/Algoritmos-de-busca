using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe que será o estado que fará a operação dos algoritmos de busca
/// </summary>
public class PathFindingState : SimulatorState
{
    Cell[] cellmap = new Cell[36];

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cellmapObject"></param>
    public PathFindingState(GameObject[] cellmapObject)
    {
        Debug.Log("Estado PathFinding");
        for (int i = 0; i < cellmapObject.Length; i++)
        {
            cellmap[i] = cellmapObject[i].GetComponent<Cell>() as Cell;
            if (i == 35)
            {
                cellmap[i].endPoint = true;
            }
        }
        //BuscaLargura(cellmap);
        //BuscaProfundidade(cellmap);
    }

    /// <summary>
    /// Algoritmo de busca em largura
    /// </summary>
    /// <param name="cells">Recebe um vetor de celulas do mapa</param>
    /// <returns>Retorna uma lista com as ações tomadas até o objetivo ou nulo caso não encontre</returns>
    List<Cell> BuscaLargura(Cell[] cells)
    {
        Debug.Log("Busca em Lagura inicializada!");
        Queue<Cell> fila = new Queue<Cell>();

        List<Cell> verticesMarcados = new List<Cell>();

        Cell ponteiro, ponteiroAuxiliar;

        Cell[] adjacente = new Cell[4];

        ponteiro = cells[0];

        verticesMarcados.Add(ponteiro);//marca a raiz como visitada

        fila.Enqueue(ponteiro);//coloca raiz na fila

        while (fila.Count != 0)
        {
            ponteiroAuxiliar = fila.Peek();
            //Debug.Log("Ponteiro auxiliar =" + ponteiroAuxiliar.coins);
            if (ponteiroAuxiliar.right != ponteiroAuxiliar)
            { //Caso ele aponte para o anterior ele não vai pra trás novamente
                adjacente[0] = ponteiroAuxiliar.right;//meio que aqui era pra receber a lista de adjacencia
            }
            if (ponteiroAuxiliar.left != ponteiroAuxiliar)
            {
                adjacente[1] = ponteiroAuxiliar.left;
            }
            if (ponteiroAuxiliar.up != ponteiroAuxiliar)
            {
                adjacente[2] = ponteiroAuxiliar.up;
            }
            if (ponteiroAuxiliar.down != ponteiroAuxiliar)
            {
                adjacente[3] = ponteiroAuxiliar.down;
            }

            int i = 0;
            while (i < 4) //percorre os vertices adjacentes
            {
                if (adjacente[i] != null)
                {
                    if (!verticesMarcados.Contains(adjacente[i])) //se nÃo foram percorridos
                    {
                        //Debug.Log("Celula " + adjacente[i].coins + " marcado como percorrido!");
                        verticesMarcados.Add(adjacente[i]); //adiciona na lista de percorridos
                        fila.Enqueue(adjacente[i]); //adiciona a fila 
                    }
                    // if (fila.Contains(adjacente[i]))
                    // {

                    // }
                    if (adjacente[i].endPoint == true)
                    { //se for o nó objetivo
                        Debug.Log("Objeto encontrado");
                        for (int f = 0; f < verticesMarcados.Count; f++)
                        {
                            Debug.Log("Posição percorrida " + f + " celula =" + verticesMarcados[f].coins);
                        }
                        return verticesMarcados;
                    }
                }

                i++;
            }

            fila.Dequeue();
        }

        return null;

    }

    /// <summary>
    /// Função que inicializa o ambiente e variáveis para o uso do algoritmo de busca por profundidade 
    /// </summary>
    /// <param name="cells">O vetor com as celulas do mapa</param>
    /// <returns>Retorna a lista com o caminho das celulas que percorreu</returns>
    List<Cell> BuscaProfundidade(Cell[] cells)
    {
        Debug.Log("Busca em Profundidade inicializada!");
        List<Cell> verticesMarcados = new List<Cell>();
        Cell ponteiro;

        ponteiro = cells[0];
        verticesMarcados.Add(ponteiro);

        DeepFindSearch(ponteiro, verticesMarcados);

        for (int i = 0; i < verticesMarcados.Count; i++)
        {
            Debug.Log("Posição percorrida " + i + " celula =" + verticesMarcados[i].coins);
        }

        return verticesMarcados;
    }

    /// <summary>
    /// Algoritmo de busca por profundidade
    /// Ele é recursivo
    /// </summary>
    /// <param name="ponteiroAuxiliar">Celula que está explorando</param>
    /// <param name="verticesMarcados">Lista com celulas que já foram visitadas</param>
    /// <returns></returns>
    List<Cell> DeepFindSearch(Cell ponteiroAuxiliar, List<Cell> verticesMarcados)
    {

        Cell[] adjacente = new Cell[4];
        if (ponteiroAuxiliar.right != ponteiroAuxiliar)
        { //Caso ele aponte para o anterior ele não vai pra trás novamente
            adjacente[0] = ponteiroAuxiliar.right;//meio que aqui era pra receber a lista de adjacencia
        }
        if (ponteiroAuxiliar.left != ponteiroAuxiliar)
        {
            adjacente[1] = ponteiroAuxiliar.left;
        }
        if (ponteiroAuxiliar.up != ponteiroAuxiliar)
        {
            adjacente[2] = ponteiroAuxiliar.up;
        }
        if (ponteiroAuxiliar.down != ponteiroAuxiliar)
        {
            adjacente[3] = ponteiroAuxiliar.down;
        }

        if (ponteiroAuxiliar.endPoint == true)
        { //verifica se o cara é o objetivo
            Debug.Log("Objetivo encontrado!" + "Celula " + ponteiroAuxiliar.coins);
            return null;
        }

        for (int i = 0; i < 4; i++)//ele percorre todos as arestas
        {
            if (adjacente[i] != null)
            {
                if (!verticesMarcados.Contains(adjacente[i]) && !ponteiroAuxiliar.endPoint == true)
                {
                    verticesMarcados.Add(adjacente[i]);
                    DeepFindSearch(adjacente[i], verticesMarcados);
                }
            }
        }
        return null;
    }

    Cell[] BuscaGulosa(Cell[] cells)
    {
        return cells;
    }

    Cell[] BuscaAStar(Cell[] cells)
    {
        return cells;
    }
}
