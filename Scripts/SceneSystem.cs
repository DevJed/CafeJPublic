using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSystem : MonoBehaviour
{
	public void StartGame()
	{
		SceneManager.LoadScene("Cafe");
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void RestartGameProgress()
	{
		if (ES3.KeyExists("dayIndex"))
		{
			ES3.Save("dayIndex", 0);
		}
		
		StartGame();
	}
}
