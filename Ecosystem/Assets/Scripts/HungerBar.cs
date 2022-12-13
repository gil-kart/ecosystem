using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    [SerializeField] private Image hungerBarSprite;
    public void updateHungerBar(float maxHunger, float curHunger)
    {
        hungerBarSprite.fillAmount = curHunger / maxHunger;
    }
}
