using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBar : MonoBehaviour
{
    [SerializeField] private GameObject childBar;
    [SerializeField] private int score;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (childBar != null)
        {
            Instantiate(childBar, transform.position, Quaternion.identity);
        }

        GameManager.instance.scoreSFX.Play();
        GameManager.instance.AddScore(score);
        Destroy(gameObject);
    }
}
