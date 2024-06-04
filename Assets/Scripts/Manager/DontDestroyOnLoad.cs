using Assets.Scripts.StringConstant;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    internal class DontDestroyOnLoad : MonoBehaviour
    {


        private void Start()
        {
            DontDestroyOnLoad(gameObject);
           /* GameObject SceneSelector = GameObject.Find(GameObjectsName.SCENEMANAGER);

            if (SceneSelector != null)
            {
                SceneSelector.GetComponent<SceneSelector>().EventLoadMainMenu.AddListener(Destroy);
            }*/
                     
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

    }
}
