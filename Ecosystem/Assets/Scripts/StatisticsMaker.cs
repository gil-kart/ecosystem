using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

public class StatisticsMaker : MonoBehaviour
{
    private float timePassed = 0f;
    List<double> aveSheepSpeed = new List<double>();
    List<double> aveSheepLongevity = new List<double>();
    List<double> aveSheepAttractivnes = new List<double>();
    List<double> aveSheepMatingDesire = new List<double>();
    List<double> aveSheepAmuneSystemProbs = new List<double>();
    List<double> aveSheeolikelinessToGetSick = new List<double>();

    List<double> aveWolfSpeed = new List<double>();
    List<double> aveWolfLongevity = new List<double>();
    List<double> aveWolfAttractivnes = new List<double>();
    List<double> aveWolfMatingDesire = new List<double>();
    List<double> aveWolfAmuneSystemProbs = new List<double>();
    List<double> aveWolflikelinessToGetSick = new List<double>();
    void Start()
    {
        timePassed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > 2)
        {
            timePassed = 0;
            Player[] allSheep = UnityEngine.Object.FindObjectsOfType<Player>();
            Wolf[] allWolves = UnityEngine.Object.FindObjectsOfType<Wolf>();
            appendSheepStats(allSheep);
            appendWoldStats(allWolves);

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
            aveSheepAmuneSystemProbs,
            aveWolfSpeed,
            aveWolflikelinessToGetSick,
            aveWolfLongevity,
            aveWolfAttractivnes,
            aveWolfMatingDesire,
            aveWolfAmuneSystemProbs,
    };
        FindObjectOfType<Graph>().SetData(dataList, 0);
        //FindObjectOfType<Graph>().ShowData(dataList[0]);
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

    private void appendWoldStats(Wolf[] allWolves)
    {
        float speedsSum = 0;
        float likeToGetSickSum = 0f;
        float longevity = 0f;
        double attractivnes = 0f;
        double matingDesire = 0f;
        double ammuneSystemProb = 0f;

        foreach(Wolf wolf in allWolves)
        {
            speedsSum += wolf.getSpeed();
            likeToGetSickSum += wolf.getSicknessLikelihood();
            longevity += wolf.getLongevity();
            attractivnes += wolf.getAttractivnes();
            matingDesire += wolf.getMatingDesire();
            ammuneSystemProb += wolf.getAmuneSystemProbs();
        }

        aveWolfSpeed.Add(speedsSum / allWolves.Length);
        aveWolflikelinessToGetSick.Add((likeToGetSickSum / allWolves.Length));
        aveWolfLongevity.Add(longevity / allWolves.Length);
        aveWolfAttractivnes.Add(attractivnes / allWolves.Length);
        aveWolfMatingDesire.Add(matingDesire / allWolves.Length);
        aveWolfAmuneSystemProbs.Add(ammuneSystemProb / allWolves.Length);
    }

}
