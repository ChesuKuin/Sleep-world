using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dieandfinish : MonoBehaviour
{

    [SerializeField] GameObject DedUi;
    [SerializeField] GameObject FinishUi;

    void Start()
    {
        DedUi.SetActive(false);
            FinishUi.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // assuming your player has the "Player" tag
        if (collision.gameObject.CompareTag("Kill"))
        {
            DedUi.SetActive(true);
            FindObjectOfType<AudioManager>().Play("Die");
            FindObjectOfType<AudioManager>().Stop("MainMusic");
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            FinishUi.SetActive(true);
            FindObjectOfType<AudioManager>().Play("End");
            FindObjectOfType<AudioManager>().Stop("MainMusic");
        }
    }
}
