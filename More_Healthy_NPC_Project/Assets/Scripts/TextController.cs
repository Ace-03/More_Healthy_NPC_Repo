using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    public string textDisplay;
    
    public GameObject DamageTextPrefab, NPC, Holder;

    StandardHealth health;

    // Start is called before the first frame update
    void Start()
    {
        health = NPC.GetComponent<StandardHealth>();
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        textDisplay = "+" + health._amount; 

    }

    public void setText(string sign, int amount)
    { 
        textDisplay = sign + amount;
        CreateText();
    }

    public void CreateText()
    {
        GameObject DamageTextInstance = Instantiate(DamageTextPrefab, Holder.transform);
        DamageTextInstance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(textDisplay);
    }
}
