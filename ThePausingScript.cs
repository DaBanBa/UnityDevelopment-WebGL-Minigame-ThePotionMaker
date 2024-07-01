using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using DG.Tweening;

public class ThePausingScript : MonoBehaviour
{
    public Transform pauseDeck;

    public void PauseGame()
    {
        if (Time.timeScale == 1)
        {
            pauseDeck.DOLocalMove(new Vector3(0, 0, 0), 0.3f).OnComplete(() =>
            {
                Time.timeScale = 0;
            });
        }
    }

    public void ResumeGame()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            pauseDeck.DOLocalMove(new Vector3(0, 3122, 0), 0.3f);
        }
    }
}
