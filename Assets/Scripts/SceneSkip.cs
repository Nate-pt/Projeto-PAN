using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSkip : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
