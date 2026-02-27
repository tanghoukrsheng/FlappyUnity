using UnityEngine;
using TMPro;

public class Text : MonoBehaviour
{
    private TMP_Text _text;
    public float speed = 1000f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
      private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        float alpha = Mathf.PingPong(Time.time * speed, 1); // Calculate alpha value using PingPong for a smooth fade in and out effect
        Color color = _text.color;
        color.a = alpha;
        _text.color = color;
        
    }
}
