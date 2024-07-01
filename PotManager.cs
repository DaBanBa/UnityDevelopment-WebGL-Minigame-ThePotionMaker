using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using DG.Tweening;

public class PotManager : MonoBehaviour
{
    public GameObject pot;
    public GameObject cardPrefab;
    public void OnCookPot()
    {
        Dictionary<CardObject, int> destroyedCardCounts = new Dictionary<CardObject, int>();
        foreach (Transform child in pot.transform)
        {
            CardControl distroyedCardControl = child.GetComponent<CardControl>();
            if (distroyedCardControl.cardObject != CardObject.ThePotWater)
            {
                if (destroyedCardCounts.ContainsKey(distroyedCardControl.cardObject))
                {
                    destroyedCardCounts[distroyedCardControl.cardObject]++;
                }
                else
                {
                    destroyedCardCounts.Add(distroyedCardControl.cardObject, 1);
                }
                Destroy(child.gameObject);
            }
        }
        CreatingPotions(destroyedCardCounts);
    }

    public void CreatingPotions(Dictionary<CardObject, int> destroyedCardCounts)
    {
        if (destroyedCardCounts.Count > 3)
        {
            return;
        }

        if (destroyedCardCounts.ContainsKey(CardObject.jinzhanhua) && destroyedCardCounts.ContainsKey(CardObject.shuweicao) &&
            destroyedCardCounts[CardObject.jinzhanhua] >= GameManager.instance.theMaterialCount && destroyedCardCounts[CardObject.shuweicao] >= GameManager.instance.theMaterialCount)
        {
            AddCardToParent(CardObject.zhiliaoyaoshui);
        }
        else if (destroyedCardCounts.ContainsKey(CardObject.zhiliaoyaoshui) && destroyedCardCounts.ContainsKey(CardObject.shuijing) &&
            destroyedCardCounts[CardObject.zhiliaoyaoshui] >= 1 && destroyedCardCounts[CardObject.shuijing] >= GameManager.instance.theMaterialCount)
        {
            AddCardToParent(CardObject.qiangxiaozhiliaoyaoshui);
        }else if (destroyedCardCounts.ContainsKey(CardObject.honglajiao) && destroyedCardCounts.ContainsKey(CardObject.longxueshuzhi) &&
            destroyedCardCounts[CardObject.honglajiao] >= 1 && destroyedCardCounts[CardObject.longxueshuzhi] >= GameManager.instance.theMaterialCount)
        {
            AddCardToParent(CardObject.fanghuoyaoji);
        }else if (destroyedCardCounts.ContainsKey(CardObject.fanghuoyaoji) && destroyedCardCounts.ContainsKey(CardObject.liuhuang) &&
            destroyedCardCounts[CardObject.fanghuoyaoji] >= 1 && destroyedCardCounts[CardObject.liuhuang] >= GameManager.instance.theMaterialCount)
        {
            AddCardToParent(CardObject.qiangxiaofanghuoyaoji);
        }else if (destroyedCardCounts.ContainsKey(CardObject.mandelacao) && destroyedCardCounts.ContainsKey(CardObject.yingli) &&
            destroyedCardCounts[CardObject.mandelacao] >= 1 && destroyedCardCounts[CardObject.yingli] >= GameManager.instance.theMaterialCount)
        {
            AddCardToParent(CardObject.shuimianyaoji);
        }else if (destroyedCardCounts.ContainsKey(CardObject.shuimianyaoji) && destroyedCardCounts.ContainsKey(CardObject.shuweicao) &&
            destroyedCardCounts[CardObject.shuimianyaoji] >= 1 && destroyedCardCounts[CardObject.shuweicao] >= GameManager.instance.theMaterialCount)
        {
            AddCardToParent(CardObject.qiangxiaoshuimianyaoji);
        }else if (destroyedCardCounts.ContainsKey(CardObject.mandelacao) && destroyedCardCounts.ContainsKey(CardObject.yizhijian) &&
            destroyedCardCounts[CardObject.mandelacao] >= 1 && destroyedCardCounts[CardObject.yizhijian] >= GameManager.instance.theMaterialCount)
        {
            AddCardToParent(CardObject.yinxingyaoji);
        }else if (destroyedCardCounts.ContainsKey(CardObject.yinxingyaoji) && destroyedCardCounts.ContainsKey(CardObject.shuiyin) &&
            destroyedCardCounts[CardObject.yinxingyaoji] >= 1 && destroyedCardCounts[CardObject.shuiyin] >= GameManager.instance.theMaterialCount)
        {
            AddCardToParent(CardObject.qiangxiaoyinxingyaoji);
        }else if (destroyedCardCounts.ContainsKey(CardObject.siyecao) && destroyedCardCounts.ContainsKey(CardObject.jinzhanhua) &&
            destroyedCardCounts[CardObject.siyecao] >= 1 && destroyedCardCounts[CardObject.jinzhanhua] >= GameManager.instance.theMaterialCount)
        {
            AddCardToParent(CardObject.xingyunyaoji);
        }else if (destroyedCardCounts.ContainsKey(CardObject.xingyunyaoji) && destroyedCardCounts.ContainsKey(CardObject.jinfen) &&
            destroyedCardCounts[CardObject.xingyunyaoji] >= 1 && destroyedCardCounts[CardObject.jinfen] >= GameManager.instance.theMaterialCount)
        {
            AddCardToParent(CardObject.qiangxiaoxingyunyaoji);
        }
    }

    private void AddCardToParent(CardObject cardObject)
    {
        Sprite sp = GameManager.instance.cardSprite[(int)cardObject];
        GameObject newCard = Instantiate(cardPrefab, pot.transform);
        newCard.GetComponent<CardControl>().Init(cardObject);
        newCard.transform.DOLocalMove(new Vector3(0, 15, 0), 0.1f);
        newCard.GetComponent<Image>().sprite = sp;
    }
}
