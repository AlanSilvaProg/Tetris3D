using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesData : ScriptableObject
{
    public List<GameObject> pieceType = new List<GameObject>();

    public PieceOfPuzzle GetNewPiece()
    {
        int randomIndex = Random.Range(0, pieceType.Count);
        return Instantiate(pieceType[randomIndex]).GetComponent<PieceOfPuzzle>();
    }
}
