using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using DG.Tweening;
public class TraitControl : MonoBehaviour
{
    public TraitObjects traitObject;
    public void Init(TraitObjects traitObject)
    {
        this.traitObject = traitObject;
    }

    public void SelectedTrait()
    {
        if (traitObject == TraitObjects.gaojilianjin)
        {
            GameManager.instance.smallPotionPrice += 2;
            GameManager.instance.bigPotionPrice += 2;
            GameManager.instance.questNeeded += 2;
        }
        else if (traitObject == TraitObjects.zhongcaipiao)
        {
            GameManager.instance.moneyCount += 50;
        }
        else if (traitObject == TraitObjects.gaobiaozhun)
        {
            QuestManager.instance.maxTime -= 2;
            GameManager.instance.smallPotionPrice += 5;
            GameManager.instance.bigPotionPrice += 5;
        }
        else if (traitObject == TraitObjects.guanggao)
        {
            QuestManager.instance.maxTime += 1;
            GameManager.instance.questNeeded += 1;
        }
        else if (traitObject == TraitObjects.maiyisongyi)
        {
            GameManager.instance.maiyisongyiPermit = true;
        }
        else if (traitObject == TraitObjects.fanghuoyaojipermit)
        {
            GameManager.instance.fanghuoyaojiPermit = true;
            QuestManager.instance.maxTime += 5;
        }
        else if (traitObject == TraitObjects.shuimianyaojipermit)
        {
            GameManager.instance.shuimianyaojiPermit = true;
            QuestManager.instance.maxTime += 5;
        }
        else if (traitObject == TraitObjects.yinxingyaojipermit)
        {
            GameManager.instance.yinxingyaojiPermit = true;
            QuestManager.instance.maxTime += 5;
        }
        else if (traitObject == TraitObjects.xingyunyaojipermit)
        {
            GameManager.instance.xingyunyaojiPermit = true;
            QuestManager.instance.maxTime += 5;
        }
        else if (traitObject == TraitObjects.piliangjinhuo)
        {
            GameManager.instance.refreshIncrement -= 2;
            GameManager.instance.smallPotionPrice -= 5;
            GameManager.instance.bigPotionPrice -= 5;
        }
        else if (traitObject == TraitObjects.xiaofei)
        {
            GameManager.instance.questNeeded -= 1;
            GameManager.instance.smallPotionPrice += 2;
            GameManager.instance.bigPotionPrice += 2;
        }
        else if (traitObject == TraitObjects.shunengshengqiao)
        {
            GameManager.instance.theMaterialCount -= 1;
        }
        GameManager.instance.OnTraitSelection();
    }
}
