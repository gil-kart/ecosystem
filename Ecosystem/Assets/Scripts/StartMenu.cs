using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public int sheepCount = 10;
    public int wolfCount = 3;
    public string selectedScene = "Desert";
    public Scene scene;
    public void startEcosystem()
    {
        SceneManager.LoadScene("SampleScene");
        
    }

    public async void goToSettings()
    {
        
        SceneManager.LoadSceneAsync("SettingMenu");
        scene = SceneManager.GetSceneByName("SettingMenu");
        int count = 0;
        while (!scene.isLoaded && count < 50000)
        {
            await Task.Delay(5);
            count++;
        }
        FindObjectOfType<SettingsMenu>().setData(sheepCount, wolfCount, selectedScene);


    }

    public void QuitGame()
    {
        Application.Quit(); 
    }

    public void updateData(int updatedSheepCount,int updatedWolfCount, string updatedSelectedScene)
    {
        Debug.Log("!!!!!!!!!");
        this.sheepCount = updatedSheepCount;
        this.wolfCount = updatedWolfCount;
        this.selectedScene = updatedSelectedScene;
    }
   
}
