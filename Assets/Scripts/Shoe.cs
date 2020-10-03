using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoe : MonoBehaviour
{
    public ShoePolishMiniGame game;
    public Color minColor;
    public Color maxColor;
    public SpriteRenderer dirt1, dirt2;
    public float hp;
    public float maxHp;
    public bool dried;
    public bool toggleCottonOnce;

    private void Update()
    {
        if (!toggleCottonOnce)
        {
            if (hp < 0 && game.cottonSelected)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    game.toggleCotton();
                    //game.movingShoe = this;
                    toggleCottonOnce = true;
                }
            }
        }
    }
    void dry()
    {
        dried = true;
    }
    void Start()
    {
        game = FindObjectOfType<ShoePolishMiniGame>();
        Color color = Color.Lerp(minColor, maxColor, Random.Range(0f, 1f));
        for (int i = 0; i < this.GetComponentsInChildren<SpriteRenderer>().Length; i++)
        {
            this.GetComponentsInChildren<SpriteRenderer>()[i].color = color;

        }

        int a = Random.Range(0, 2);
        if (a == 0)
        {
            dirt1.gameObject.SetActive(true);
            hp += 100;
        }
        a = Random.Range(0, 2);
        if (a == 0)
        {
            dirt2.gameObject.SetActive(true);
            hp += 100;
        }
        maxHp = hp;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.name);
        if (collision.collider.name == "ShoeArea")
        {
            game.currentShoe = this;
        }
        else if (collision.collider.name == "Cotton")
        {
            game.cottonColliding = true;
        }
        else if(collision.collider.name == "EndArea")
        {
            if(dried && hp <= 0)
            {
                OnMouseUp();
                game.done(this);
            }
        }
        else if(collision.collider.name == "DryArea")
        {
            OnMouseUp();
            Invoke("dry", 0.5f);
            game.dry();
        }
    }

    public void clean(float Amount)
    {
        hp -= Amount;
        dirt1.color = Color32.Lerp(Color.white, Color.clear, (maxHp - hp) / maxHp);
        dirt2.color = Color32.Lerp(Color.white, Color.clear, (maxHp - hp) / maxHp);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log(collision.collider.name);
        if (collision.collider.name == "ShoeArea")
        {
            game.currentShoe = null;
        }
        else if (collision.collider.name == "Cotton")
        {
            game.cottonColliding = false;
        }
    }

    private void OnMouseDown()
    {
        game.movingShoe = this;
        game.offset = this.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("MouseDown");
    }

    private void OnMouseUp()
    {
        game.movingShoe = null;
    }
}
