using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace t3ampo.mixit.utils
{
    public class SafeLoader : MonoBehaviour
    {
        void Awake()
        {
#if UNITY_EDITOR
            gameObject.DontDestroyOnLoad();
            if (!GameManager.Instance)
            {
                string old = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene("MXT-Intro");
                StartCoroutine(WaitUntilScene(old));
            }
#else
            Destroy(gameObject);
#endif
        }
#if UNITY_EDITOR
        IEnumerator WaitUntilScene(string old)
        {
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "MXT-Menu");
            SceneManager.LoadScene(old);
            Destroy(gameObject);
        }
#endif
    }
}
