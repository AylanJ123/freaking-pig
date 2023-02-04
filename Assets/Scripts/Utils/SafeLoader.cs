using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace freakingpig.utils
{
    public class SafeLoader : MonoBehaviour
    {
        void Awake()
        {
#if UNITY_EDITOR
            if (!GameManager.Instance)
            {
                DontDestroyOnLoad(gameObject);
                string old = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(0);
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
