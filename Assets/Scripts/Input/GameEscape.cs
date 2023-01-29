using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Inputs
{
    public class GameEscape : MonoBehaviour
    {
        [SerializeField]
        private KeyCode escapeKey = KeyCode.Escape;

        [SerializeField]
        private SceneLoader sceneLoader;

        private void Update()
        {
            if(Input.GetKeyDown(escapeKey))
            {
                sceneLoader.LoadScene("MainMenu");
            }
        }
    }
}
