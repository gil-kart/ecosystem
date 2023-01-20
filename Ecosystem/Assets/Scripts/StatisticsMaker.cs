using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

public class StatisticsMaker : MonoBehaviour
{
    int count = 0;
    private float timePassed = 0f;
    List<double> aveSheepSpeed = new List<double>();
    List<double> aveSheepLongevity = new List<double>();

    List<double> aveSheepAttractivnes = new List<double>();
    List<double> aveSheepMatingDesire = new List<double>();
    List<double> aveSheepAmuneSystemProbs = new List<double>();
    
    List<double> aveSheeolikelinessToGetSick = new List<double>();
    void Start()
    {
        timePassed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(count == 0)
        {
            Debug.Log("hello!");
            count = 1;
        }
        
        timePassed += Time.deltaTime;
        if (timePassed > 2)
        {
            timePassed = 0;
            Player[] allSheep = UnityEngine.Object.FindObjectsOfType<Player>();
            Wolf[] allWolves = UnityEngine.Object.FindObjectsOfType<Wolf>();
            appendSheepStats(allSheep);
        }
    }

    public void stopEcosystem()
    {
        SceneManager.LoadScene("startMenu");
    }

    public async void goToStats()
    {
        SceneManager.LoadScene("Statistics");
        Scene scene = SceneManager.GetSceneByName("Statistics");
        int saftyCount = 0;
        while (!scene.isLoaded && saftyCount < 50000)
        {
            await Task.Delay(5);
            saftyCount++;
        }
        List<List<double>> dataList = new()
        {
            aveSheepSpeed,
            aveSheeolikelinessToGetSick,
            aveSheepLongevity,
            aveSheepAttractivnes,
            aveSheepMatingDesire,
            aveSheepAmuneSystemProbs
        };
        FindObjectOfType<Graph>().SetData(dataList, 0);
        FindObjectOfType<Graph>().ShowData(dataList[0]);
    }
    private void appendSheepStats(Player[] allSheep)
    {
        int speedsSum = 0;
        float likeToGetSickSum = 0f;
        float longevity = 0f;
        double attractivnes = 0f;
        double matingDesire = 0f;
        double ammuneSystemProb = 0f;
        foreach (Player sheep in allSheep)
        {
            speedsSum += sheep.getSpeed();
            likeToGetSickSum += sheep.getSicknessLikelihood();
            longevity += sheep.getLongevity();
            attractivnes += sheep.getAttractivnes();
            matingDesire += sheep.getMatingDesire();
            ammuneSystemProb += sheep.getAmuneSystemProbs();
        }

        aveSheepSpeed.Add(speedsSum / allSheep.Length);
        aveSheeolikelinessToGetSick.Add((likeToGetSickSum / allSheep.Length));
        aveSheepLongevity.Add(longevity / allSheep.Length);
        aveSheepAttractivnes.Add(attractivnes / allSheep.Length);
        aveSheepMatingDesire.Add(matingDesire / allSheep.Length);
        aveSheepAmuneSystemProbs.Add(ammuneSystemProb / allSheep.Length);
    }

}
