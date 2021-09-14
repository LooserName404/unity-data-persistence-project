#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class MenuUIManager : MonoBehaviour
    {
        [SerializeField] private InputField nameInput;

        public void StartGame()
        {
            var name = nameInput.text;

            HighscoreManager.Instance.CurrentPlayerName = name;

            SceneManager.LoadScene(1);
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }
}