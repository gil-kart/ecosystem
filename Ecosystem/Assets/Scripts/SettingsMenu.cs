using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public TextMeshProUGUI wolfCount;
    public TextMeshProUGUI sheepCount;
    public TMP_Dropdown tmpDropdown;
    public Scene scene;
    public string selectedScene = "Forrest";
    public async void backToMainMenu()
    {
        SceneManager.LoadSceneAsync("startMenu");
        scene = SceneManager.GetSceneByName("startMenu");
        int count = 0;
        while (!scene.isLoaded && count < 50000)
        {
            await Task.Delay(5);
            count++;
        }
        FindObjectOfType<StartMenu>().updateData(int.Parse(sheepCount.text), int.Parse(wolfCount.text), selectedScene);

    }

    public void WolfInc()
    {
        int wolfNum = int.Parse(wolfCount.text);
        wolfNum++;
        if(wolfNum <= 10)
            wolfCount.text = wolfNum.ToString();
    }

    public void WolfDec()
    {
        int wolfNum = int.Parse(wolfCount.text);
        wolfNum--;
        if(wolfNum >= 0)
            wolfCount.text = wolfNum.ToString();
    }

    public void SheepInc()
    {
        int sheepNum = int.Parse(sheepCount.text);
        sheepNum++;
        if(sheepNum <= 15)
            sheepCount.text = sheepNum.ToString();
    }

    public void SheepDec()
    {
        int sheepNum = int.Parse(sheepCount.text);
        sheepNum--;
        if (sheepNum >= 0)
            sheepCount.text = sheepNum.ToString();
    }

    public void SetScene()
    {
        if (tmpDropdown.value == 0)
            selectedScene = "Forrest";
        if (tmpDropdown.value == 1)
            selectedScene = "Snow";
        if (tmpDropdown.value == 2)
            selectedScene = "Desert";
    }


    public void setData(int inpSheepCount, int inpWolfCount, string scene)
    {
        selectedScene = scene;
        if (scene == "Forrest")
            tmpDropdown.value = 0;
        else if (scene == "Snow")
            tmpDropdown.value = 1;
        else if (scene == "Desert")
            tmpDropdown.value = 2;
        sheepCount.text = inpSheepCount.ToString();
        wolfCount.text = inpWolfCount.ToString();
        
    }
}
