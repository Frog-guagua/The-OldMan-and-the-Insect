using UnityEngine;
using UnityEngine.SceneManagement;

public class StartAndEnd : MonoBehaviour
{
     private string targetSceneName="HomeScene";
     public GameObject settingMenu;
    

    public void SwitchToScene()
    {
        
        SceneManager.LoadScene(targetSceneName, LoadSceneMode.Additive);

       
        

        // 卸载当前场景
        SceneManager.UnloadSceneAsync("StartMenu");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    public void SettingMenuOn()
    {
        settingMenu.SetActive(true);
    }
    public void SettingMenuOff()
    {
        settingMenu.SetActive(false);
    }
}
