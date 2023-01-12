using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Graph : MonoBehaviour
{
    [SerializeField] public Sprite circleSprite;
    private RectTransform graphContainer;
    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;
    private void Awake()
    {
        graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();
        labelTemplateX = graphContainer.Find("LabelTemplateX").GetComponent<RectTransform>();
        labelTemplateY = graphContainer.Find("LabelTemplateY").GetComponent<RectTransform>();
        //CreateCircle(new Vector2(200, 200));
        List<int> values = new List<int>() { 13, 4, 55, 42, 88, 4, 156, 53, 157, 22, 88, 300, 321, 2, 55, 0, 12};
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

    private void ShowGraph(List<int> valueList)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float yMax = 100f;
        float xSize = 50f;
        GameObject lastCircle = null;
        for(int i = 0; i < valueList.Count; i++)
        {
            float xPosition = i * xSize + 35;
            float yPosition = (valueList[i] / yMax) * graphHeight + 45;
            GameObject circle = CreateCircle(new Vector2(xPosition, yPosition));
            if(lastCircle != null)
            {
                CreateDotConnection(lastCircle.GetComponent<RectTransform>().anchoredPosition, circle.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircle = circle;


            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graphContainer, false);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(xPosition + 11, 20f);
            labelX.GetComponent<Text>().text = i.ToString();
        }

        int seperatorCount = 10;
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

}
