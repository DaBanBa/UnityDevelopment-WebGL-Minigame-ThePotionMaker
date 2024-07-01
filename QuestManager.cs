using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]

public enum QuestObject
{
    zhiliaoyaoshui,
    qiangxiaozhiliaoyaoshui,
    shuimianyaoji,
    qiangxiaoshuimianyaoji,
    fanghuoyaoji,
    qiangxiaofanghuoyaoji,
    xingyunyaoji,
    qiangxiaoxingyunyaoji,
    yinxingyaoji,
    qiangxiaoyinxingyaoji
}

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public GameObject questPrefab;
    public GameObject dialoguePrefab;
    public List<Sprite> questSprites = new List<Sprite>();
    public List<Sprite> dialogueSprites = new List<Sprite>();
    public RectTransform timerBar;
    public bool isTimerActive = false;
    public float maxTime = 15f;
    private readonly Color blueishGreen = new Color(26f / 255f, 255f / 255f, 179f / 255f);
    private readonly Color darkPurple = new Color(181f / 255f, 35f / 255f, 69f / 255f);
    private float timeRemaining;
    private float initialWidth = 1440f;

    private QuestObject questObject;

    private void Start()
    {
        timeRemaining = maxTime;
        instance = this;
        initialWidth = timerBar.sizeDelta.x;
    }

    public void AddQuest()
    {
        GameManager.instance.moneyCountButtonText.text = "$ " + GameManager.instance.moneyCount.ToString();
        this.questObject = GetRandomQuestObject();
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        GameObject questCard = Instantiate(questPrefab, transform);
        questCard.GetComponent<QuestPerson>().Init(questObject);
        questCard.transform.localPosition = new Vector3(-1200, -150, 0);
        questCard.GetComponent<Image>().sprite = questSprites[Random.Range(0, questSprites.Count)];
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(0.5f);
        sequence.Append(questCard.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-300, -150), 0.5f).SetEase(Ease.OutBack));

        GameObject dialogueCard = Instantiate(dialoguePrefab, transform);
        dialogueCard.GetComponent<DialogueControl>().Init(questObject);
        dialogueCard.transform.localPosition = new Vector3(-1200, -50, 0);
        dialogueCard.GetComponent<Image>().sprite = dialogueSprites[(int)questObject];
        Sequence sequence2 = DOTween.Sequence();
        sequence2.AppendInterval(0.5f);
        sequence2.Append(dialogueCard.GetComponent<RectTransform>().DOAnchorPos(new Vector2(300, -50), 0.5f).SetEase(Ease.OutBack));
        StartTimer();
    }

    void Update()
    {
        if (isTimerActive)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                Vector2 sizeDelta = timerBar.sizeDelta;
                sizeDelta.x = initialWidth * (timeRemaining / maxTime);
                timerBar.sizeDelta = sizeDelta;
                float t = 1 - (timeRemaining / maxTime);
                Color currentColor = Color.Lerp(blueishGreen, darkPurple, t);
                timerBar.GetComponent<Image>().color = currentColor;
            }
            else
            {
                OnTimerEnd();
            }
        }
    }

    public void StartTimer()
    {
        isTimerActive = true;
        timeRemaining = maxTime;
    }

    private void OnTimerEnd()
    {
        isTimerActive = false;
        Vector2 sizeDelta = timerBar.sizeDelta;
        sizeDelta.x = initialWidth;
        timerBar.sizeDelta = sizeDelta;
        timerBar.GetComponent<Image>().color = blueishGreen;
        transform.GetChild(0).GetComponent<RectTransform>().DOAnchorPos(new Vector2(-1200, -150), 0.5f).SetEase(Ease.InBack);
        transform.GetChild(1).GetComponent<RectTransform>().DOAnchorPos(new Vector2(-1200, -50), 0.5f).SetEase(Ease.InBack).OnComplete(() => SmoothEndingTransition());
    }

    public void SmoothEndingTransition()
    {
        GameManager.instance.NextQuest();
    }

    public void SubmitPotion()
    {
        if (GameManager.instance.GetSelectedCard().Count != 0 && GameManager.instance.GetSelectedCard()[0].GetComponent<CardControl>().cardObject.ToString() == questObject.ToString())
        {
            if(GameManager.instance.GetSelectedCard()[0].GetComponent<CardControl>().cardObject == CardObject.zhiliaoyaoshui)
            {
                GameManager.instance.moneyCount += GameManager.instance.smallPotionPrice;
            }
            else if(GameManager.instance.GetSelectedCard()[0].GetComponent<CardControl>().cardObject == CardObject.qiangxiaozhiliaoyaoshui)
            {
                GameManager.instance.moneyCount += GameManager.instance.bigPotionPrice;
            }
            foreach (CardControl card in GameManager.instance.GetSelectedCard())
            {
                Destroy(card.gameObject);
            }
            GameManager.instance.RemoveSelectedCard();
            OnTimerEnd();
        }
    }

    public QuestObject GetRandomQuestObject()
    {
        Array values = Enum.GetValues(typeof(QuestObject));
        QuestObject randomQuest = (QuestObject)values.GetValue(Random.Range(0, values.Length));
        if ((GameManager.instance.fanghuoyaojiPermit && 
            (randomQuest == QuestObject.fanghuoyaoji || randomQuest == QuestObject.qiangxiaofanghuoyaoji)) ||
            (GameManager.instance.shuimianyaojiPermit && 
            (randomQuest == QuestObject.shuimianyaoji || randomQuest == QuestObject.qiangxiaoshuimianyaoji)) ||
            (GameManager.instance.yinxingyaojiPermit && 
            (randomQuest == QuestObject.yinxingyaoji || randomQuest == QuestObject.qiangxiaoyinxingyaoji)) ||
            (GameManager.instance.xingyunyaojiPermit && 
            (randomQuest == QuestObject.xingyunyaoji || randomQuest == QuestObject.qiangxiaoxingyunyaoji)) ||
            (randomQuest == QuestObject.zhiliaoyaoshui || randomQuest == QuestObject.qiangxiaozhiliaoyaoshui))
        {
            return randomQuest;
        }
        else
        {
            return GetRandomQuestObject();
        }
    }
}
