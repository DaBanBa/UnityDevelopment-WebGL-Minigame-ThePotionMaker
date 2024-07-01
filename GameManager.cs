using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using DG.Tweening;
[System.Serializable]
public enum CardObject
{
    CardBase,
    ThePotWater,
    jinzhanhua,
    shuweicao,
    shuijing,
    zhiliaoyaoshui,
    qiangxiaozhiliaoyaoshui,
}

public enum TraitObjects
{
    gaojilianjin,
    zhongcaipiao,
    gaobiaozhun,
    guanggao,
    maiyisongyi,
}

public struct CardInfo
{
    public CardObject cardObject;

    public CardInfo(CardObject cardObject)
    {
        this.cardObject = cardObject;
    }
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject card;
    public Sprite[] cardSprite;
    public Sprite[] traitSprites;
    public float theMaterialCount = 5;
    public float dayCount = 0;
    public float questCount = 0;
    public float questNeeded = 3;
    public GameObject traitPrefab;
    public Transform traitSelectionPanel;
    public Transform questDeck;
    public Transform potDeck;

    private List<CardControl> selectedCard = new List<CardControl>();
    private List<CardInfo> cardInfos = new List<CardInfo>();
    private Transform cardDeck;
    private List<TraitObjects> usedTraits = new List<TraitObjects>();
    private Array traitValues = Enum.GetValues(typeof(TraitObjects));


    private void Start()
    {
        instance = this;
        traitSelection();
    }

    System.Random random = new System.Random();
    private void initCardInfoList()
    {
        foreach (CardObject card in Enum.GetValues(typeof(CardObject)))
        {
            if (card != CardObject.CardBase && card != CardObject.ThePotWater && card != CardObject.zhiliaoyaoshui && card != CardObject.qiangxiaozhiliaoyaoshui)
            {
                cardInfos.Add(new CardInfo(card));
            }
        }
    }

    private void spawnCard()
    {
        foreach (Transform column in cardDeck)
        {
            for (int i = column.childCount - 1; i > 0; i--)
            {
                Destroy(column.GetChild(i).gameObject);
            }
            float posY = 15;
            for (int i = 0; i < 3; i++)
            {
                int index = Random.Range(0, cardInfos.Count);
                CardInfo cardInfo = cardInfos[index];

                Sprite sp = cardSprite[(int)cardInfo.cardObject];
                GameObject gameCard = Instantiate(card, column);
                gameCard.GetComponent<CardControl>().Init(cardInfo.cardObject);
                gameCard.transform.localPosition = new Vector3(0, posY, 0);
                gameCard.GetComponent<Image>().sprite = sp;
                posY += 15;
            }
        }
    }

    public void AddSelectedCard(CardControl cardControl)
    {
        selectedCard.Add(cardControl);
    }

    public List<CardControl> GetSelectedCard()
    {
        return selectedCard;
    }

    public void RemoveSelectedCard()
    {
        selectedCard.Clear();
    }

    public void PurhcaseCard()
    {
        if (GetSelectedCard().Count != 0)
        {
            foreach (CardControl alreadySelectedCard in GetSelectedCard())
            {
                Vector3 newPosition = alreadySelectedCard.transform.localPosition;
                newPosition.y -= 38;
                alreadySelectedCard.transform.DOLocalMove(newPosition, 0.1f);
            }
        }
        RemoveSelectedCard();
        foreach (Transform colume in cardDeck)
        {
            float posY = colume.childCount * 15;
            for (int i = 0; i < 3; i++)
            {
                int index = Random.Range(0, cardInfos.Count);
                CardInfo cardInfo = cardInfos[index];

                Sprite sp = cardSprite[(int)cardInfo.cardObject];
                GameObject gameCard = Instantiate(card, colume);
                gameCard.GetComponent<CardControl>().Init(cardInfo.cardObject);
                gameCard.transform.DOLocalMove(new Vector3(0, posY, 0), 0.1f);
                gameCard.GetComponent<Image>().sprite = sp;
                posY += 15;
            }
        }
    }

    public void NextQuest()
    {
        questCount++;
        if (questCount == questNeeded)
        {
            dayCount++;
            questCount = 0;
            questNeeded++;
            foreach (Transform trait in traitSelectionPanel)
            {
                Destroy(trait.gameObject);
            }
            foreach (Transform child in questDeck)
            {
                GameObject.Destroy(child.gameObject);
            }
            if (dayCount == 2 || dayCount == 5 || dayCount == 9 || dayCount == 14)
            {
                traitSelection();
            }
            else
            {
                noTraitTransition();
            }
            foreach (Transform child in potDeck)
            {
                if (child.GetComponent<CardControl>().cardObject != CardObject.ThePotWater)
                {
                    Destroy(child.gameObject);
                }
            }
        }
        else
        {
            QuestManager.instance.AddQuest();
        }
    }

    public void traitSelection()
    {
        cardDeck = transform.Find("CardDeck").transform;
        initCardInfoList();
        spawnCard();
        AddTrait();
        traitSelectionPanel.DOLocalMove(new Vector3(0, 0, 0), 0.3f);
    }

    public void noTraitTransition()
    {
        cardDeck = transform.Find("CardDeck").transform;
        initCardInfoList();
        spawnCard();
        Sequence sequence4 = DOTween.Sequence();
        sequence4.Append(traitSelectionPanel.DOLocalMove(new Vector3(0, 0, 0), 0.3f));
        sequence4.AppendInterval(2f);
        sequence4.Append(traitSelectionPanel.DOLocalMove(new Vector3(-1440, 0, 0), 0.3f));
        sequence4.AppendInterval(5f).OnComplete(() => QuestManager.instance.AddQuest());
    }

    public void OnTraitSelection()
    {
        Sequence sequence5 = DOTween.Sequence();
        traitSelectionPanel.DOLocalMove(new Vector3(-1440, 0, 0), 0.3f);
        sequence5.AppendInterval(5f).OnComplete(() => QuestManager.instance.AddQuest());
    }

    public void AddTrait()
    {
        for (int i = 0; i < 2; i++)
        {
            TraitObjects randomTrait = GetRandomTraitObject();
            Sprite sp = traitSprites[(int)randomTrait];
            GameObject trait = Instantiate(traitPrefab, traitSelectionPanel);
            trait.GetComponent<TraitControl>().Init(randomTrait);
            trait.transform.localPosition = new Vector3(0, 0, 0);
            trait.GetComponent<Image>().sprite = sp;
        }
    }

    public TraitObjects GetRandomTraitObject()
    {
        TraitObjects randomTrait;
        do
        {
            randomTrait = (TraitObjects)traitValues.GetValue(UnityEngine.Random.Range(0, traitValues.Length));
        } while (usedTraits.Contains(randomTrait));

        usedTraits.Add(randomTrait);
        return randomTrait;
    }
}

