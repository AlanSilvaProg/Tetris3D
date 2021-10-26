using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PieceOfPuzzle : MonoBehaviour
{
    public int pieceID = 0;
    public List<GameObject> slots = new List<GameObject>();
    public List<GameObject> deletedSlots = new List<GameObject>();
    bool current = true;
    bool speedMode = false;

    void OnEnable()
    {
        InitializePiece();
        StartCoroutine(MovimentDelay());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && current) TurnAroundPiece();
        speedMode = Input.GetKey(KeyCode.DownArrow) && current;
        if (Input.GetKeyDown(KeyCode.RightArrow) && current) MovePiece();
        if (Input.GetKeyDown(KeyCode.LeftArrow) && current) MovePiece(-1);
    }

    public void MovePiece(int direction = 1)
    {
        transform.position += Vector3.right * direction; 
        CheckPiecePosition();
    }

    public void TurnAroundPiece()
    {
        transform.DORotate(transform.rotation.eulerAngles + Vector3.forward * 90, 0).OnComplete(() => {
            CheckPiecePosition();
        });
    }

    public void CheckPiecePosition()
    {
        foreach(GameObject slot in slots)
        {
            if(slot.transform.position.x < GameManager.Instance.puzzleAcess.GetMinimumLenght)
            {
                MovePiece();
            }
            else if (slot.transform.position.x > GameManager.Instance.puzzleAcess.GetMaximumLenght)
            {
                MovePiece(-1);
            }
        }
    }

    public void InitializePiece()
    {
        current = true;
        if (deletedSlots.Count > 0)
        {
            foreach(GameObject slot in deletedSlots)
            {
                slots.Add(slot);
                slot.SetActive(true);
            }
        }
        deletedSlots.Clear();
    }

    IEnumerator MovimentDelay()
    {
        yield return new WaitForSeconds(speedMode ? 0.2f : GameManager.Instance.puzzleAcess.pieceVelocity);
        transform.DOKill(true);
        if (CheckNextMoviment())
        {
            transform.DOMoveY(transform.position.y - 1, speedMode ? 0.2f : GameManager.Instance.puzzleAcess.pieceVelocity / 2);
            StartCoroutine(MovimentDelay());
        }
        else
        {
            current = false;
            GameManager.Instance.puzzleAcess.CheckLineAfterFit(this);
        }
    }

    Vector3 tempPos = Vector3.zero;
    Vector3 tempPosCheck = Vector3.zero;
    public void DeleteSlotsInPosition(int yPos)
    {
        foreach (GameObject slot in slots)
        {
            tempPos = tempPosCheck = Vector3.up;
            tempPos.y *= yPos;
            tempPosCheck *= slot.transform.position.y;
            if (Vector3.Distance(tempPos, tempPosCheck) < 0.2f)
            {
                deletedSlots.Add(slot);
                slot.SetActive(false);
            }
        }
        foreach (GameObject obj in deletedSlots)
        {
            if(slots.Contains(obj))
                slots.Remove(obj);
        }
        if (slots.Count <= 0)
            RemovePieceFromScene();
    }

    public void ApplyMoveCheck()
    {
        checkTimes = 5;
        StartCoroutine(MoveAfterClearLine());
    }

    int checkTimes = 5;
    public IEnumerator MoveAfterClearLine()
    {
        yield return new WaitForSeconds(0.2f );
        if (CheckNextMoviment())
        {
            transform.DOMoveY(transform.position.y - 1, 0).OnComplete(() =>
            {
                transform.DOKill(true);
                if (checkTimes > 0) { checkTimes--; StartCoroutine(MoveAfterClearLine()); }
            });
        }
        else
        {
            if (checkTimes > 0) { checkTimes--; StartCoroutine(MoveAfterClearLine()); }
        }
    }

    public void RemovePieceFromScene()
    {
        GameManager.Instance.poolAcess.PutOnPool(this);
    }

    public bool CheckNextMoviment()
    {
        return GameManager.Instance.puzzleAcess.CheckNextStep(this);
    }

}
