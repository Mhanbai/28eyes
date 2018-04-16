using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSelection : MonoBehaviour {
    public GameObject head1;
    public GameObject head2;

    private void Start()
    {
        head1.SetActive(false);
        head2.SetActive(false);
    }

    private void Update()
    {
        if (PlayerInfo.Instance.headPart == 0)
        {
            head1.SetActive(true);
            head2.SetActive(false);
        }
        if (PlayerInfo.Instance.headPart == 1)
        {
            head1.SetActive(false);
            head2.SetActive(true);
        }
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void ChangeHead() {
        if (PlayerInfo.Instance.headPart == 1)
        {
            PlayerInfo.Instance.headPart = 0;
        }
        else
        {
            PlayerInfo.Instance.headPart = 1;
        }
    }

    public void Reset()
    {
        DataManager.Instance.Reset();
    }
}

