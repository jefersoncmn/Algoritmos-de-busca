using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe responsável pelo controle da apresentação do funcionamento dos algoritmos de busca, operando os itens da tela.
/// </summary>
public class RunPathState : MonoBehaviour, SimulatorState
{
    GeneralController generalController;
    public bool runningTest = false;
    public void SetGeneralController(GeneralController generalController)
    {
        this.generalController = generalController;

    }

    /// <summary>
    /// Função que executará a apresentação do algoritmo de busca em largura
    /// </summary>
    public void RunLargura()
    {
        if (runningTest == false)
        {
            if (generalController.exploredCellsLargura != null)
            {
                Debug.Log("tamanho da funcao sucessora do largura =" + generalController.sucessorFuctionLargura.Count);
                StartCoroutine(ExploreRoutes(generalController.exploredCellsLargura, generalController.sucessorFuctionLargura));
            }
            else
            {
                Debug.Log("Lista de exploracao vazia");
            }

        }
    }

    /// <summary>
    /// Função que executará a apresentação do algoritmo de busca em profundidade
    /// </summary>
    public void RunProfundidade()
    {
        if (runningTest == false)
        {
            if (generalController.exploredCellsProfundidade != null)
            {
                StartCoroutine(ExploreRoutes(generalController.exploredCellsProfundidade, generalController.sucessorFuctionProfundidade));
            }
            else
            {
                Debug.Log("Lista de exploracao vazia");
            }

        }

    }

    /// <summary>
    /// Função que executará a apresentação do algoritmo de busca gulosa
    /// </summary>
    public void RunGulosa()
    {
        if (runningTest == false)
        {
            if (generalController.exploredCellsGulosa != null)
            {
                StartCoroutine(ExploreRoutes(generalController.exploredCellsGulosa, generalController.sucessorFuctionGulosa));
            }
            else
            {
                Debug.Log("Lista de exploracao vazia");
            }

        }

    }

    /// <summary>
    /// Função que executará a apresentação do algoritmo de busca A estrela
    /// </summary>
    public void RunAStar()
    {
        if (runningTest == false)
        {
            if (generalController.exploredCellsAStar != null)
            {
                StartCoroutine(ExploreRoutes(generalController.exploredCellsAStar, generalController.sucessorFuctionAStar));
            }
            else
            {
                Debug.Log("Lista de exploracao vazia");
            }

        }

    }

    /// <summary>
    /// Função que irá fazer a sequencia de todos os movimentos realizados nas rotas retornadas pelos algoritmos de busca
    /// </summary>
    /// <param name="path">Lista com as celulas do caminho a ser percorrido</param>
    /// <returns></returns>
    IEnumerator TestRoute(List<Cell> path)
    {
        GameObject dummy = Instantiate(generalController.testmodel, new Vector3(0, 1.5f, 0), Quaternion.identity);
        yield return new WaitForSeconds(1);

        for (int i = 0; i < path.Count - 1; i++)
        {
            move(dummy, path[i].gameObject.transform.position, path[i + 1].gameObject.transform.position);
            yield return new WaitForSeconds(1);
        }

        yield return new WaitForSeconds(1);
        Destroy(dummy);

        runningTest = false;
    }

    /// <summary>
    /// Função que realiza a movimentação do boneco entre os pontos do mapa
    /// </summary>
    /// <param name="dummy">Boneco de testes</param>
    /// <param name="startPosition">Posição incial</param>
    /// <param name="target">Posição que quer ir</param>
    void move(GameObject dummy, Vector3 startPosition, Vector3 target)
    {
        startPosition.y = 1.5f;
        target.y = 1.5f;

        dummy.transform.position = Vector3.Lerp(startPosition, target, Time.deltaTime * 1000);
    }

    /// <summary>
    /// Função responsável por instanciar os blocos que representam como é feito a exploracao pelo algoritmo de busca
    /// Depois chama a função que mostrará a rota encotrada até o objetivo
    /// </summary>
    /// <param name="exploredCells">Lista com as celulas exploradas</param>
    /// <param name="path">Lista com as celulas do caminho até o objetivo final</param>
    /// <returns></returns>
    IEnumerator ExploreRoutes(List<Cell> exploredCells, List<Cell> path)
    {
        List<GameObject> exploredRoutesObjects = new List<GameObject>();
        runningTest = true;

        for (int i = 0; i < exploredCells.Count; i++)
        {
            GameObject exploreRouteObj = Instantiate(generalController.explorationmodel, new Vector3(exploredCells[i].gameObject.transform.position.x, 0.5f, exploredCells[i].gameObject.transform.position.z), Quaternion.identity);
            exploredRoutesObjects.Add(exploreRouteObj);
            yield return new WaitForSeconds(1);
        }
        yield return new WaitForSeconds(1);

        for (int i = 0; i < exploredRoutesObjects.Count; i++)
        {
            Destroy(exploredRoutesObjects[i]);
        }

        if (path != null || path.Count == 0)
        {
            StartCoroutine(TestRoute(path));
        }
        else
        {
            Debug.Log("Não foi possível encontrar um caminho");
        }
    }

}