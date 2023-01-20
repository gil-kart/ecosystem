using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;
using TMPro;

public class Graph : MonoBehaviour
{
    [SerializeField] public Sprite circleSprite;
    private RectTransform graphContainer;
    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;
    static List<List<double>> data;
    static int curListIndex;
    public TextMeshProUGUI Header;

    Dictionary<int, string> keyValuePairs = new Dictionary<int, string>
    {
        { 0, "Sheep Average Speed" },
        { 1, "Sheep Average Likeliness To Get Sick" },
        { 2, "Sheep Average Longevity" },
        { 3, "Sheep Average Attractivnes" },
        { 4, "Sheep Average Mating Desire" },
        { 5, "Sheep Average Amune System Strength" },

    };
    private void Awake()
    {
        graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();
        labelTemplateX = graphContainer.Find("LabelTemplateX").GetComponent<RectTransform>();
        labelTemplateY = graphContainer.Find("LabelTemplateY").GetComponent<RectTransform>();
        SetData(data, curListIndex);
    }

    public void SetData(List<List<double>> dataList, int index)
    {
        curListIndex = index;
        data = dataList;
        Header.SetText(keyValuePairs[curListIndex]);
        ShowData(data[curListIndex]);
    }


    public void ShowData(List<double> values)
    {
        ShowGraph(values);
    }

    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject circle = new GameObject("Circle", typeof(Image));
        circle.transform.SetParent(graphContainer, false);
        circle.GetComponent<Image>().sprite = circleSprite; 
        RectTransform rectTransform = circle.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return circle;
    }

    private void ShowGraph(List<double> valueList)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float graphWidth = 860f;
        float yMax = (float)valueList.Max();
        float xSize = 50f;
        int seperatorCount = 10;
        GameObject lastCircle = null;
        for(int i = 0; i < valueList.Count; i++)
        {
            float xPosition = i * xSize + 35;
            double yPosition = (valueList[i] / yMax) * graphHeight + 45;
            GameObject circle = CreateCircle(new Vector2(35 + i * graphWidth / valueList.Count, (float)yPosition));
            if(lastCircle != null)
            {
                CreateDotConnection(lastCircle.GetComponent<RectTransform>().anchoredPosition, circle.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircle = circle;


            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graphContainer, false);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(45 + i * graphWidth / valueList.Count, 20f);
            labelX.GetComponent<Text>().text = i.ToString();
        }

        
        graphHeight = 410;
        for (int i=0; i<=seperatorCount; i++)
        {
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(graphContainer, false);
            labelY.gameObject.SetActive(true);
            float normalizedValue = i * 1f / seperatorCount;
            labelY.anchoredPosition = new Vector2(20f, 40 + normalizedValue * graphHeight);
            labelY.GetComponent<Text>().text = Mathf.RoundToInt(normalizedValue * yMax).ToString();
        }
    }

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB) 
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        Vector2 direction = (dotPositionA - dotPositionB).normalized;
        float dist = Vector2.Distance(dotPositionA, dotPositionB);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(dist, 3f);
        rectTransform.anchoredPosition = dotPositionB + direction * dist * 0.5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(direction));
    }

    float GetAngleFromVectorFloat(Vector2 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    public async void ShowNextStat()
    {
        curListIndex++;
        if (curListIndex % data.Count == 0)
            curListIndex = 0;

        SceneManager.LoadScene("Statistics");
        Scene scene = SceneManager.GetSceneByName("Statistics");
        int saftyCount = 0;
        while (!scene.isLoaded && saftyCount < 50000)
        {
            await Task.Delay(5);
            saftyCount++;
        }

        FindObjectOfType<Graph>().SetData(data, curListIndex);
    }

    public void BackToEcosystem()
    {
        data = null;
        SceneManager.LoadScene("startMenu");
    }

}
