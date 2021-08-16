using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class FloatValueView : MonoBehaviour
{
    [SerializeField]
    int cutSimbol = 3;

    Text text;

    float value = 0;

    public float Value
    {
        get => value;
        set
        {
            if (this.value == value)
                return;
            this.value = value;
            UpdateView();
        }
    }

    void UpdateView()
    {
        string newText = value.ToString();
        text.text = newText;
    }

    protected void Awake()
    {
        text = GetComponent<Text>();
    }
}
