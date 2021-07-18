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

    int celulaobjetivo = 35;

    /// <summary>
    /// Construtor da classe que reproduzirá os algoritmos de busca
    /// </summary>
    /// <param name="cellmapObject"></param>
    public PathFindingState(GeneralController generalController)
    {
        this.generalController = generalController;

        //Debug.Log("Estado PathFinding");
        for (int i = 0; i < generalController.cellmap.Length; i++)
        {
            cellmap[i] = generalController.cellmap[i].GetComponent<Cell>() as Cell;
            if (i == celulaobjetivo)
            {
                cellmap[i].endPoint = true;
            }
        }

        generalController.sucessorFuctionLargura = BuscaLargura(cellmap);
        generalController.sucessorFuctionProfundidade = BuscaProfundidade(cellmap);
        generalController.sucessorFuctionGulosa = BuscaGulosa(cellmap);
        generalController.sucessorFuctionAStar = BuscaAStar(cellmap);

        // for (int x = 0; x < generalController.sucessorFuctionProfundidade.Count; x++)
        // {
        //     Debug.Log("Passo = " + x + " Celula = " + generalController.sucessorFuctionProfundidade[x].coins);
        // }

        generalController.simulate();

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
                        //Debug.Log("Objeto encontrado");
                        generalController.larguraMemoryCost = verticesMarcados.Count;//Quantidade de nós guardados na memória
                        generalController.larguraTime = 0;

                        for (int x = 0; x < verticesMarcados.Count; x++)
                        {
                            generalController.larguraMovimentCost += ((double)verticesMarcados[x].ambientType); //custo de movimentacao
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
        //Debug.Log("Busca em Profundidade inicializada!");
        List<Cell> verticesMarcados = new List<Cell>();
        Cell ponteiro;

        ponteiro = cells[0];
        verticesMarcados.Add(ponteiro);

        DeepFindSearch(ponteiro, verticesMarcados);

        generalController.profundidadeMemoryCost = verticesMarcados.Count;

        generalController.profundidadeTime = 0;

        for (int i = 0; i < verticesMarcados.Count; i++)
        {
            //Debug.Log("Posição percorrida " + i + " celula =" + verticesMarcados[i].coins);
            generalController.profundidadeMovimentCost += ((double)verticesMarcados[i].ambientType);
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
            //Debug.Log("Objetivo encontrado!" + "Celula " + ponteiroAuxiliar.coins);
            encontrado = true;
            //encontrado = true;
            return null;
        }


        for (int i = 0; i < 4; i++)//ele percorre todos as arestas
        {
            if (getEncontrado() != true)
            {
                if (adjacente[i] != null)
                {
                    if (!verticesMarcados.Contains(adjacente[i]) && !ponteiroAuxiliar.endPoint == true)
                    {
                        verticesMarcados.Add(adjacente[i]);
                        //Debug.Log("celula marcada " + adjacente[i].coins);
                        //Debug.Log("Encontrado " + getEncontrado());
                        //Debug.Log("Movimento de celula " + ponteiroAuxiliar.coins + " para " + adjacente[i].coins);
                        DeepFindSearch(adjacente[i], verticesMarcados);
                    }

                }
            }


        }



        return null;
    }

    public bool encontrado = false;

    bool getEncontrado()
    {
        return encontrado;
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

        BuscaGulosaAlgoritmo(ponteiro, verticesMarcados, melhoresValoresHeuristicos, path);

        // for (int i = 0; i < path.Count; i++)
        // {
        //     Debug.Log("Posição percorrida " + i + " celula =" + path[i].coins);
        // }

        return path;
    }


    List<Cell> BuscaGulosaAlgoritmo(Cell cell, List<Cell> verticesMarcados, List<Cell> melhoresValoresHeuristicos, List<Cell> path)
    {
        Cell[] adjacente = new Cell[4];

        if (cell.right != cell)
        { //Caso ele aponte para o anterior ele não vai pra trás novamente
            adjacente[0] = cell.right;//meio que aqui era pra receber a lista de adjacencia
        }
        if (cell.left != cell)
        {
            adjacente[1] = cell.left;
        }
        if (cell.up != cell)
        {
            adjacente[2] = cell.up;
        }
        if (cell.down != cell)
        {
            adjacente[3] = cell.down;
        }
        //Debug.Log("Ponteiro =" + cell.coins);
        path.Add(cell);
        if (cell.endPoint == true)
        { //verifica se o cara é o objetivo
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
                    //Debug.Log("Celula percorrida " + adjacente[i].coins);
                    melhoresValoresHeuristicos.Add(adjacente[i]);
                }
            }
        }
        Cell melhorCelula = cell;
        for (int i = 0; i < melhoresValoresHeuristicos.Count; i++) //isso vai assegurar que ele só vai andar pelo que tem a melhor heurística
        {
            if (Vector3.Distance(melhoresValoresHeuristicos[i].gameObject.transform.position, cellmap[celulaobjetivo].gameObject.transform.position) < Vector3.Distance(melhorCelula.gameObject.transform.position, cellmap[celulaobjetivo].gameObject.transform.position))
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
        Debug.Log("Busca AStar inicializada!");
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
            generalController.aStarMovimentCost += ((float)verticesMarcados[i].ambientType);
        }
        for (int i = 0; i < path.Count; i++)
        {
            Debug.Log("Passo " + i + " celula " + path[i].coins);
        }

        generalController.aStarMemoryCost = verticesMarcados.Count;

        generalController.aStarTime = 0;

        return path;
    }

    List<Cell> AStarSearch(Cell cell, List<Cell> verticesMarcados, List<Cell> melhoresValoresHeuristicos, List<Cell> path)
    {

        Cell[] adjacente = new Cell[4];

        if (cell.right != cell)
        { //Caso ele aponte para o anterior ele não vai pra trás novamente
            adjacente[0] = cell.right;//meio que aqui era pra receber a lista de adjacencia
        }
        if (cell.left != cell)
        {
            adjacente[1] = cell.left;
        }
        if (cell.up != cell)
        {
            adjacente[2] = cell.up;
        }
        if (cell.down != cell)
        {
            adjacente[3] = cell.down;
        }


        //Debug.Log("Ponteiro =" + cell.coins);
        if (cell.endPoint == true)
        { //verifica se o cara é o objetivo
            for (int index = 0; index < cell.pathmemory.Count; index++)
            { //pega as rotas antigas pra celula nova explorada e guarda na memoria
                path.Add(cell.pathmemory[index]);
            }



            Debug.Log("Objetivo encontrado!" + "Celula " + cell.coins);
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
        Cell melhorCelula = melhoresValoresHeuristicos[0];
        melhoresValoresHeuristicos.Remove(cell);

        for (int i = 0; i < melhoresValoresHeuristicos.Count; i++) //isso vai assegurar que ele só vai andar pelo que tem a melhor heurística
        {
            if (Vector3.Distance(melhoresValoresHeuristicos[i].gameObject.transform.position, cellmap[celulaobjetivo].gameObject.transform.position) + ((float)melhoresValoresHeuristicos[i].ambientType) < Vector3.Distance(melhorCelula.gameObject.transform.position, cellmap[celulaobjetivo].gameObject.transform.position) + ((float)melhorCelula.ambientType))
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
