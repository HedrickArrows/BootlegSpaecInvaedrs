using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {

    public Text[] options;
    public int iter;
	// Use this for initialization
	void Start () {
        iter = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (!options[iter].text.Contains("►")) {
            options[iter].text = "► " + options[iter].text.Substring(4);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            options[iter].text = "    " + options[iter].text.Substring(2);
            iter = ++iter % options.Length;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            options[iter].text = "    " + options[iter].text.Substring(2);
            iter = (--iter + options.Length) % options.Length;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (options[iter].text.Contains("New game"))
            {
                SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
            }
            if (options[iter].text.Contains("Control"))
            {
                SceneManager.LoadScene("ControlsScene", LoadSceneMode.Single);
            }
            if (options[iter].text.Contains("Quit")) {
                //if (Application.isEditor)
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                Application.Quit();
            }
        }


    }
}
