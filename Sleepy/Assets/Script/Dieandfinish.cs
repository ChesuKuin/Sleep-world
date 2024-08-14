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
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            FinishUi.SetActive(true);
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Kill"))
        {
            DedUi.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            FinishUi.SetActive(false);
        }
    }
}
