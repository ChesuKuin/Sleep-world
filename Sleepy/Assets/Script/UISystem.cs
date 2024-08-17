using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISystem : MonoBehaviour
{
    [SerializeField] GameObject Esc;
    public void StartOver()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        FindObjectOfType<AudioManager>().Play("MainMusic");
    }

    void Start()
    {
        Esc.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Esc.SetActive(true);
        }
        if (Input.GetKey(KeyCode.E))
        {
            Esc.SetActive(false);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
