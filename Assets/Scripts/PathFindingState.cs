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

    public bool encontrado = false;

    bool getEncontrado()
    {
        return encontrado;
    }

    /// <summary>
    /// Construtor da classe que reproduzirá os algoritmos de busca
    /// </summary>
    /// <param name="cellmapObject"></param>
    public PathFindingState(GeneralController generalController)
    {
        this.generalController = generalController;

        defineGoal();

        cleanPathMemory(cellmap);
        generalController.setSucessorFuctionLargura(BuscaLargura(cellmap));
        cleanPathMemory(cellmap);
        generalController.setSucessorFuctionProfundidade(BuscaProfundidade(cellmap));
        cleanPathMemory(cellmap);
        generalController.setSucessorFuctionGulosa(BuscaGulosa(cellmap));
        cleanPathMemory(cellmap);
        generalController.setSucessorFuctionAStar(BuscaAStar(cellmap));

        generalController.simulate();

    }

    /// <summary>
    /// Função que define a celula objetivo do mapa.
    /// Que os algoritmos de busca irão encontrar uma rota pra chegar nela.
    /// /// </summary>
    void defineGoal()
    {
        for (int i = 0; i < generalController.cellmap.Length; i++)
        {
            cellmap[i] = generalController.cellmap[i].GetComponent<Cell>() as Cell;
            if (i == generalController.celulaObjetivo)
            {
                cellmap[i].endPoint = true;
            }
        }
    }

    void cleanPathMemory(Cell[] cellmap)
    {
        for (int i = 0; i < cellmap.Length; i++)
        {
            cellmap[i].pathmemory = new List<Cell>();
        }
    }


    /// <summary>
    /// Algoritmo de busca em largura
    /// </summary>
    /// <param name="cells">Recebe um vetor de celulas do mapa</param>
    /// <returns>Retorna uma lista com as ações tomadas até o objetivo ou nulo caso não encontre</returns>
    List<Cell> BuscaLargura(Cell[] cells)
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
            adjacente = CollectAdjacentsCells(ponteiro);

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

    /// <summary>
    /// Função que coleta todas as celulas vizinhas para a navegação dos algoritmos
    /// </summary>
    /// /// <param name="cell">Celula atual sendo visitada</param>
    /// <returns></returns>
    public Cell[] CollectAdjacentsCells(Cell cell)
    {
        Cell[] adjacente = new Cell[4];

        if (cell.right != null && cell.right != cell)
        { //Caso ele aponte para o anterior ele não vai pra trás novamente
            adjacente[0] = cell.right;//meio que aqui era pra receber a lista de adjacencia
        }
        if (cell.left != null && cell.left != cell)
        {
            adjacente[1] = cell.left;
        }
        if (cell.up != null && cell.up != cell)
        {
            adjacente[2] = cell.up;
        }
        if (cell.down != null && cell.down != cell)
        {
            adjacente[3] = cell.down;
        }

        return adjacente;
    }

    /// <summary>
    /// Função que inicializa o ambiente e variáveis para o uso do algoritmo de busca por profundidade 
    /// </summary>
    /// <param name="cells">O vetor com as celulas do mapa</param>
    /// <returns>Retorna a lista com o caminho das celulas que percorreu</returns>
    List<Cell> BuscaProfundidade(Cell[] cells)
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
        adjacente = CollectAdjacentsCells(ponteiro);

        if (ponteiro.endPoint == true)
        { //verifica se o cara é o objetivo
          //Debug.Log("Objetivo encontrado!" + "Celula " + ponteiroAuxiliar.coins);
            for (int x = 0; x < ponteiro.pathmemory.Count; x++)
            {
                //Debug.Log("Ponto adicionado no final =" + ponteiro.pathmemory[x].coins);
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

                        //Debug.Log("celula marcada " + adjacente[i].coins);
                        //Debug.Log("Encontrado " + getEncontrado());
                        //Debug.Log("Movimento de celula " + ponteiro.coins + " para " + adjacente[i].coins);
                        DeepFindSearch(adjacente[i], verticesMarcados, path);
                    }

                }
            }

        }
        return null;
    }



    List<Cell> BuscaGulosa(Cell[] cells)
    {
        //Debug.Log("Busca gulosa inicializada!");
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
            //Debug.Log("Posição percorrida " + i + " celula =" + path[i].coins);
            generalController.gulosaMovimentCost += ((double)path[i].ambientType);
        }

        generalController.gulosaMemoryCost = verticesMarcados.Count;

        return path;
    }


    List<Cell> BuscaGulosaAlgoritmo(Cell cell, List<Cell> verticesMarcados, List<Cell> melhoresValoresHeuristicos, List<Cell> path)
    {
        Cell[] adjacente = new Cell[4];
        adjacente = CollectAdjacentsCells(cell);

        if (cell.endPoint == true)
        { //verifica se o cara é o objetivo
            for (int index = 0; index < cell.pathmemory.Count; index++)
            { //pega as rotas antigas pra celula nova explorada e guarda na memoria
                path.Add(cell.pathmemory[index]);
            }
            encontrado = true;
            //Debug.Log("Objetivo encontrado!" + "Celula " + cell.coins);
            return null;
        }

        for (int i = 0; i < 4; i++)//ele percorre todos as arestas
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
            if (Vector3.Distance(melhoresValoresHeuristicos[i].gameObject.transform.position, cellmap[generalController.celulaObjetivo].gameObject.transform.position) < Vector3.Distance(melhorCelula.gameObject.transform.position, cellmap[generalController.celulaObjetivo].gameObject.transform.position))
            {
                //Debug.Log("Distancia da celula = " + melhorCelula.coins + " ate o objetivo é = " + Vector3.Distance(melhorCelula.gameObject.transform.position, cellmap[34].gameObject.transform.position));
                melhorCelula = melhoresValoresHeuristicos[i];
                //Debug.Log("Distancia da celula atual = " + melhorCelula.coins + " ate o objetivo é = " + Vector3.Distance(melhorCelula.gameObject.transform.position, cellmap[34].gameObject.transform.position));
            }

        }

        if (melhorCelula != null && getEncontrado() == false)
        {
            BuscaGulosaAlgoritmo(melhorCelula, verticesMarcados, melhoresValoresHeuristicos, path);
        }

        return null;
    }


    List<Cell> BuscaAStar(Cell[] cells)
    {
        //Debug.Log("Busca AStar inicializada!");
        List<Cell> verticesMarcados = new List<Cell>();
        List<Cell> melhoresValoresHeuristicos = new List<Cell>();
        List<Cell> path = new List<Cell>();
        encontrado = false;

        Cell ponteiro;

        ponteiro = cells[0];
        verticesMarcados.Add(ponteiro);

        ponteiro.pathmemory.Add(ponteiro);//adiciona a si mesmo na memória

        AStarSearch(ponteiro, verticesMarcados, melhoresValoresHeuristicos, path);

        for (int i = 0; i < verticesMarcados.Count; i++)
        {
            generalController.exploredCellsAStar.Add(verticesMarcados[i]);
        }
        for (int i = 0; i < path.Count; i++)
        {
            generalController.aStarMovimentCost += ((double)path[i].ambientType);
            //Debug.Log("Passo " + i + " celula " + path[i].coins);
        }

        generalController.aStarMemoryCost = verticesMarcados.Count;

        return path;
    }

    List<Cell> AStarSearch(Cell cell, List<Cell> verticesMarcados, List<Cell> melhoresValoresHeuristicos, List<Cell> path)
    {

        Cell[] adjacente = new Cell[4];
        adjacente = CollectAdjacentsCells(cell);


        //Debug.Log("Ponteiro =" + cell.coins);
        if (cell.endPoint == true)
        { //verifica se o cara é o objetivo
            for (int index = 0; index < cell.pathmemory.Count; index++)
            { //pega as rotas antigas pra celula nova explorada e guarda na memoria
                path.Add(cell.pathmemory[index]);
            }

            //Debug.Log("Objetivo encontrado!" + "Celula " + cell.coins);
            encontrado = true;
            return null;
        }

        for (int i = 0; i < 4; i++)//ele percorre todos as arestas
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


                    //Debug.Log("Celula percorrida " + adjacente[i].coins);
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
            if (Vector3.Distance(melhoresValoresHeuristicos[i].gameObject.transform.position, cellmap[generalController.celulaObjetivo].gameObject.transform.position) + ((float)melhoresValoresHeuristicos[i].ambientType) < Vector3.Distance(melhorCelula.gameObject.transform.position, cellmap[generalController.celulaObjetivo].gameObject.transform.position) + ((float)melhorCelula.ambientType))
            {
                //Debug.Log("Distancia da celula anterior = " + melhorCelula.coins + " ate o objetivo é = " + Vector3.Distance(melhorCelula.gameObject.transform.position, cellmap[34].gameObject.transform.position));
                melhorCelula = melhoresValoresHeuristicos[i];
                //Debug.Log("Distancia da celula atual = " + melhorCelula.coins + " ate o objetivo é = " + Vector3.Distance(melhorCelula.gameObject.transform.position, cellmap[34].gameObject.transform.position));
            }

        }

        if (melhorCelula != null && getEncontrado() == false)
        {
            AStarSearch(melhorCelula, verticesMarcados, melhoresValoresHeuristicos, path);

        }

        return null;
    }
}
