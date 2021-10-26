using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{

   Dictionary<int, Queue<PieceOfPuzzle>> objectQueue = new Dictionary<int, Queue<PieceOfPuzzle>>();

    public PieceOfPuzzle GetFromPool(int pieceID)
    {
        if(objectQueue.ContainsKey(pieceID) && objectQueue[pieceID].Count > 0)
        {
            return objectQueue[pieceID].Dequeue();
        }
        return null;
    }

    public void PutOnPool(PieceOfPuzzle objectToPool)
    {
        objectToPool.gameObject.SetActive(false);
        if (!objectQueue.ContainsKey(objectToPool.pieceID)) objectQueue.Add(objectToPool.pieceID, new Queue<PieceOfPuzzle>());
        objectQueue[objectToPool.pieceID].Enqueue(objectToPool);
    }

}
