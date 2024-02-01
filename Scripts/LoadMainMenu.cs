using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour
{
	public void ButtonReturnToMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
		AudioManager.Instance.PlayMenuAudio();
	}
}
