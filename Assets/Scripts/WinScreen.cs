using CGL.Data;
using CGL.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CGL.UI
{
	public class WinScreen : MonoBehaviour
	{
		[SerializeField]
		private GameObject winPanel;

		[SerializeField]
		private GameObject gameOverPanel;

		[SerializeField]
		private CanvasGroup other;

        private void Start()
        {
            winPanel.SetActive(false);
			gameOverPanel.SetActive(false);

            GameObject health = GameObject.FindWithTag("Health_Bar");
            health.GetComponent<Canvas>().enabled = false;
        }
        public void OnWinChanged()
		{
			winPanel.gameObject.SetActive(true);
			other.blocksRaycasts = false;
			Time.timeScale = 0.0f;
		}

		public void OnGameOver()
		{
			gameOverPanel?.SetActive(true);
			other.blocksRaycasts = false;
			Time.timeScale = 0.0f;
		}

		public void OnResume()
		{
			other.blocksRaycasts = true;
			Time.timeScale = 1.0f;
            winPanel.SetActive(false);
        }

		public void NewGame()
		{
			other.blocksRaycasts = true;
			Time.timeScale = 1.0f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
	}
}