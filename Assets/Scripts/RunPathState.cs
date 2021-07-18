using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe responsável pelo controle da apresentação do funcionamento dos algoritmos de busca, operando os itens da tela.
/// </summary>
public class RunPathState : MonoBehaviour, SimulatorState
{
    GeneralController generalController;
    public void SetGeneralController(GeneralController generalController)
    {
        this.generalController = generalController;

    }

    /// <summary>
    /// Função que executará a apresentação do algoritmo de busca em largura
    /// </summary>
    public void RunLargura()
    {
        StartCoroutine(TestRoute(generalController.sucessorFuctionLargura));

    }

    /// <summary>
    /// Função que executará a apresentação do algoritmo de busca em profundidade
    /// </summary>
    public void RunProfundidade()
    {
        StartCoroutine(TestRoute(generalController.sucessorFuctionProfundidade));

    }

    /// <summary>
    /// Função que executará a apresentação do algoritmo de busca gulosa
    /// </summary>
    public void RunGulosa()
    {
        StartCoroutine(TestRoute(generalController.sucessorFuctionGulosa));

    }

    /// <summary>
    /// Função que executará a apresentação do algoritmo de busca A estrela
    /// </summary>
    public void RunAStar()
    {
        StartCoroutine(TestRoute(generalController.sucessorFuctionAStar));

    }

    /// <summary>
    /// Função que irá fazer a sequencia de todos os movimentos realizados nas rotas retornadas pelos algoritmos de busca
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    IEnumerator TestRoute(List<Cell> path)
    {
        GameObject dummy = Instantiate(generalController.testmodel, new Vector3(0, 1.5f, 0), Quaternion.identity);

        for (int i = 0; i < path.Count - 1; i++)
        {
            move(dummy, path[i].gameObject.transform.position, path[i + 1].gameObject.transform.position);
            yield return new WaitForSeconds(1);
        }

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

        dummy.transform.position = Vector3.Lerp(startPosition, target, Time.deltaTime);
    }




}