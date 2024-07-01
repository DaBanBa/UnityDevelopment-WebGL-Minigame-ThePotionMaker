using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using DG.Tweening;

public class QuestPerson : MonoBehaviour
{
    public QuestObject questObject;
    public void Init(QuestObject questObject)
    {
        this.questObject = questObject;
    }
}
