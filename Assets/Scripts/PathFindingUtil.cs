using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingUtil : MonoBehaviour
{
    /// <summary>
    /// Função que coleta todas as celulas vizinhas para a navegação dos algoritmos
    /// </summary>
    /// /// <param name="cell">Celula atual sendo visitada</param>
    /// <returns></returns>
    static public Cell[] CollectAdjacentsCells(Cell cell)
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
}
