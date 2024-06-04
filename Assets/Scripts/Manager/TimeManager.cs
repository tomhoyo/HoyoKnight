using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Manager
{

    internal class TimeManager : MonoBehaviour
    {

        private bool _isGameOnPause = false;
        public bool IsGameOnPause { get { return _isGameOnPause; } private set { _isGameOnPause = value; } }

        private static TimeManager instance = null;
        public static TimeManager Instance => instance;
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                instance = this;
            }
        }

        public void TimeStopChangeState()
        {
            if (IsGameOnPause)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        public void PauseGame()
        {
            Time.timeScale = 0.0f;
            IsGameOnPause = true;
        }

        public void ResumeGame()
        {
            Time.timeScale = 1.0f;
            IsGameOnPause = false;
        }


    }
}
