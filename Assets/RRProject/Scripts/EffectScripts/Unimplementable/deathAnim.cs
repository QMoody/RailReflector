using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathAnim : MonoBehaviour
{
    public float timertomin;
    public float timertomax;
    public float sizeMulti;
    private float timer;
    Color mycol;

    // Start is called before the first frame update
    void Start()
    {
        mycol = gameObject.GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer < timertomin)
        {
            gameObject.transform.localScale = new Vector3(1 - (timer / timertomin),1 -( timer / timertomin), 1);
        }

        if (timer > timertomin)
        {
            gameObject.transform.localScale = new Vector3(sizeMulti * (timer - timertomin) / (timertomax - timertomin), sizeMulti * (timer - timertomin) / (timertomax - timertomin), 1);

            gameObject.GetComponent<SpriteRenderer>().color = new Color(mycol.r, mycol.g, mycol.b, 1-((timer-timertomin) / (timertomax-timertomin)));
        }

        if (gameObject.GetComponent<SpriteRenderer>().color.a < 0)
        {
            Destroy(gameObject);
        }
    }

 
}
