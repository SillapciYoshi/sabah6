using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenuscrip : MonoBehaviour
{
    public void play()
    {
        SceneManager.LoadScene(SceneManager.GetSceneByName("1").name);
    }
}
