using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using DG.Tweening;

public class TheDictionaryScript : MonoBehaviour
{
    public Transform dictionaryDeck;
    public Image imgDictionary;
    public Sprite newHPSprite;
    public Sprite newSPSprite;
    public Sprite newIPSprite;
    public Sprite newFPSprite;
    public Sprite newLPSprite;

    public void PauseDicGame()
    {
        if (Time.timeScale == 1)
        {
            dictionaryDeck.DOLocalMove(new Vector3(0, 0, 0), 0.3f).OnComplete(() =>
            {
                Time.timeScale = 0;
            });
        }
    }

    public void ResumeDicGame()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            dictionaryDeck.DOLocalMove(new Vector3(1440, 0, 0), 0.3f);
        }
    }

    public void HPChangeSprite()
    {
        imgDictionary.sprite = newHPSprite;
    }
        public void IPChangeSprite()
    {
        imgDictionary.sprite = newSPSprite;
    }
        public void LPChangeSprite()
    {
        imgDictionary.sprite = newIPSprite;
    }
        public void FPChangeSprite()
    {
        imgDictionary.sprite = newFPSprite;
    }
        public void SPChangeSprite()
    {
        imgDictionary.sprite = newLPSprite;
    }
}
