using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleConfigure : MonoBehaviour
{
    #region constant
    public const float minimumLenght = -7.5f;
    public float GetMinimumLenght => minimumLenght;
    public const float maximumLenght = 7.5f;
    public float GetMaximumLenght => maximumLenght;
    public const float spawnHeight = 13;
    public const float endPoint = 10;
    #endregion

    public PiecesData piecesData;
    public float pieceVelocity = 1;
    List<PieceOfPuzzle> allPieces = new List<PieceOfPuzzle>();

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => GameManager.Instance != null);
        GameManager.Instance.puzzleAcess = this;
        InitializePuzzle();
    }

    public void InitializePuzzle()
    {
        SpawnNewPiece();
    }

    public void SpawnNewPiece()
    {
        PieceOfPuzzle newPiece = GameManager.Instance.poolAcess.GetFromPool(Random.Range(0, piecesData.pieceType.Count)) ?? piecesData.GetNewPiece();
        allPieces.Add(newPiece);
        newPiece.InitializePiece();
        newPiece.gameObject.SetActive(true);
        Vector3 spawnPosition = Vector3.zero;
        do
        {
            spawnPosition = new Vector3(Random.Range(-7, 7) + (Random.Range(0, 2) == 1 ? 0.5f : -0.5f), spawnHeight, 0);
            newPiece.transform.position = spawnPosition;
        } while (!CheckIfPieceHasInside(newPiece));
    }

    public bool CheckIfPieceHasInside(PieceOfPuzzle piece)
    {
        bool result = true;
        foreach(GameObject obj in piece.slots)
        {
            if(obj.transform.position.x < -7.5f || obj.transform.position.x > 7.5f)
            {
                result = false;
                break;
            }
        }
        return result;
    }

    public void IncreaseTime(bool inverse = false)
    {
        pieceVelocity = Mathf.Clamp(inverse ? pieceVelocity + 0.1f : pieceVelocity - 0.1f, 0.2f, 1);
    }

    List<PieceOfPuzzle> tempPieceList = new List<PieceOfPuzzle>();
    public void CleanLine(int line)
    {
        foreach (PieceOfPuzzle piece in allPieces)
        {
            piece.DeleteSlotsInPosition(line);
            tempPieceList.Add(piece);
        }
        foreach (PieceOfPuzzle piece in tempPieceList)
        {
            if(piece.slots.Count == 0)
                allPieces.Remove(piece);
        }
    }

    Vector3 checkPos = Vector3.zero;
    Vector3 tempPos = Vector3.zero;
    Vector3 tempPosCheck = Vector3.zero;
    public bool CheckNextStep(PieceOfPuzzle piece)
    {
        foreach (PieceOfPuzzle all in allPieces)
        {
            if (!all.Equals(piece))
            {
                foreach (GameObject obj in all.slots)
                {
                    foreach (GameObject check in piece.slots)
                    {
                        checkPos = check.transform.position;
                        tempPos = check.transform.position;
                        tempPosCheck = obj.transform.position;
                        tempPos.y = tempPosCheck.y = 0;
                        tempPos.z = tempPosCheck.z = 0;
                        checkPos.y -= 1;
                        if (Vector3.Distance(tempPos, tempPosCheck) < 0.2f)
                        {
                            if(Vector3.Distance(checkPos, obj.transform.position) < 0.2f)
                                return false;
                        }
                    }
                }
            }
            else
            {
                foreach (GameObject obj in all.slots)
                {
                    if (obj.transform.position.y <= -4) return false;
                }
            }
        }
        return true;
    }

    int piecesCount = 0;
    public void CheckLineAfterFit(PieceOfPuzzle piece)
    {
        bool lineDeleted = false;
        for (int index = -4; index <= 12; index++)
        {
            piecesCount = 0;
            foreach (PieceOfPuzzle check in allPieces)
            {
                foreach (GameObject obj in check.slots)
                {
                    if(CheckDistance(index, obj)) piecesCount++;
                }
            }
            if (piecesCount.Equals(16))
            {
                CleanLine(index);
                GameManager.Instance.statsAcess.IncreaseScore(100);
                lineDeleted = true;
            }
        }
        if(lineDeleted)
        {
            foreach (PieceOfPuzzle pieces in allPieces) pieces.ApplyMoveCheck();
        }
        else
        {
            if (CheckEndGame())
            {
                GameManager.Instance.EndGame();
                return;
            }
        }
        SpawnNewPiece();
    }

    public bool CheckEndGame()
    {
        foreach (PieceOfPuzzle pieces in allPieces)
        {
            foreach (GameObject obj in pieces.slots)
            {
                if (CheckDistance(10, obj))
                    return true;
            }
        }
        return false;
    }

    public bool CheckDistance(int index, GameObject obj)
    {
        tempPos = Vector3.up * index;
        tempPosCheck = obj.transform.position;
        tempPosCheck.x = tempPosCheck.z = 0;
        if (Vector3.Distance(tempPos, tempPosCheck) < 0.2f) return true; else return false;
    }

}
