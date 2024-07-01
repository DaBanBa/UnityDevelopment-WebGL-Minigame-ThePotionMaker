using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using DG.Tweening;

public class CardControl : MonoBehaviour
{
    public CardObject cardObject;
    private Vector3 originalPosition;

    public void Init(CardObject cardObject)
    {
        this.cardObject = cardObject;
    }

    public void OnPointerDown()
    {
        if (GameManager.instance.GetSelectedCard().Count == 0 && (cardObject != CardObject.CardBase && cardObject != CardObject.ThePotWater))
        {
            IsSeries();
        }
        else if (GameManager.instance.GetSelectedCard().Count != 0 && GameManager.instance.GetSelectedCard()[0].transform.parent != transform.parent && (transform.parent.GetChild(transform.parent.childCount - 1).GetComponent<CardControl>().cardObject == GameManager.instance.GetSelectedCard()[0].cardObject || cardObject == CardObject.CardBase || (transform.parent.GetChild(0).GetComponent<CardControl>().cardObject == CardObject.ThePotWater && ChildType())))
        {
            AddToNewColumn();
        }
        else if (GameManager.instance.GetSelectedCard().Count != 0 && cardObject != CardObject.CardBase && cardObject != CardObject.ThePotWater && (transform.parent.GetChild(transform.parent.childCount - 1).GetComponent<CardControl>().cardObject != GameManager.instance.GetSelectedCard()[0].cardObject || GameManager.instance.GetSelectedCard()[0].transform.parent == transform.parent))
        {
            ReturnToPosition();
        }
    }

    public void PointDownState()
    {
        Vector3 newPosition = transform.localPosition;
        newPosition.y += 38;
        transform.DOLocalMove(newPosition, 0.1f);
    }

    public void ReturnToPosition()
    {
        foreach (CardControl alreadySelectedCard in GameManager.instance.GetSelectedCard())
        {
            Vector3 newPosition = alreadySelectedCard.transform.localPosition;
            newPosition.y -= 38;
            alreadySelectedCard.transform.DOLocalMove(newPosition, 0.1f);
        }
        GameManager.instance.RemoveSelectedCard();
    }

    public void AddToNewColumn()
    {
        float posY = transform.parent.childCount * 15;
        foreach (CardControl alreadySelectedCard in GameManager.instance.GetSelectedCard())
        {
            alreadySelectedCard.transform.SetParent(transform.parent);
            alreadySelectedCard.transform.DOLocalMove(new Vector3(0, posY, 0), 0.1f);
            posY += 15;
        }
        GameManager.instance.RemoveSelectedCard();
    }

    public void IsSeries()
    {
        CardObject tempCardObj = cardObject;
        for (int i = transform.parent.childCount - 1; i >= 0; i--)
        {
            if (transform.parent.GetChild(i).GetComponent<CardControl>().cardObject == tempCardObj)
            {
                transform.parent.GetChild(i).GetComponent<CardControl>().PointDownState();
                GameManager.instance.AddSelectedCard(transform.parent.GetChild(i).GetComponent<CardControl>());
            }
            else
            {
                break;
            }
        }
    }

    public bool ChildType()
    {
        List<CardObject> childList = new List<CardObject>();
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (!childList.Contains(transform.parent.GetChild(i).GetComponent<CardControl>().cardObject))
            {
                childList.Add(transform.parent.GetChild(i).GetComponent<CardControl>().cardObject);
            }
        }
        if (childList.Contains(cardObject))
        {
            return true;
        }
        return childList.Count < 3;
    }
}