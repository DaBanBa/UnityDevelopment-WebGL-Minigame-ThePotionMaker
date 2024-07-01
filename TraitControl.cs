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
            Debug.Log("Gaojilianjin");
        }
        else if (traitObject == TraitObjects.zhongcaipiao)
        {
            Debug.Log("Zhongcaipiao");
        }
        else if (traitObject == TraitObjects.gaobiaozhun)
        {
            Debug.Log("Gaobiaozhun");
        }
        else if (traitObject == TraitObjects.guanggao)
        {
            Debug.Log("Guanggao");
        }
        else if (traitObject == TraitObjects.maiyisongyi)
        {
            Debug.Log("Maiyisongyi");
        }
        GameManager.instance.OnTraitSelection();
    }
}
