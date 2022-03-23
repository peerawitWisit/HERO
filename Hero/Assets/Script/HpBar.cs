using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Text hpText;

    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = Player.instance.hp + "";
        hpSlider.value = Player.instance.hp;
    }
}
