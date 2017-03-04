using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class script_popups : MonoBehaviour {

    int magicnumber = 500;
    Image[] images = new Image[10];
    Image[] bads = new Image[10];
    Image[] goods = new Image[10];
    Image[] combos = new Image[10];
    Image[] totalscore = new Image[10];
    int imgcount = 0;
    int badscount = 0;
    int goodscount = 0;
    int combocount = 0;
    int totalscorecount = 0;
    Sprite[] spriteSheetSprites;

   //ist<Image> sads;
   //st<Image> imgs;
   //ist<Image> happys;
   //List<Image> combos;
    Image activeImg;
    int timer = 0;
    // Use this for initialization
    void Start() {

        spriteSheetSprites = Resources.LoadAll<Sprite>("totalscore numbers");
        //for( int i =0; i < spriteSheetSprites.Length; i++)
        //{
           // print(spriteSheetSprites[i].name);
      //  }
       // print(spriteSheetSprites.Length);
       // print(spriteSheetSprites);
        //print("it started");
        // Populate arrays and set to inactive
        //images = GameObject.FindObjectsOfType<Image>();

        //  imgs.AddRange(GameObject.FindObjectsOfType<Image>());
        // GameObject[] temp1 = GameObject.FindGameObjectsWithTag("image_sad");
        Image[] temp = GameObject.FindObjectsOfType<Image>();
        for(int i=0; i < temp.Length; i++)
        {
            Image img = temp[i];
           // print(temp[i].name);
            
            img.gameObject.SetActive(false);
            if (img.gameObject.tag == "image_sad")
            {
                //ads.Add(img);
                bads[badscount] = img;
                badscount++;
            }
            else if (img.gameObject.tag == "image_happy")
            {
               // print(temp[i].name);
                goods[goodscount] = img;
                goodscount++;

            }
            else if (img.gameObject.tag == "image_combo")
            {
                combos[combocount] = img;
                combocount++;

            } else if (img.gameObject.tag == "image_totalscore")
            {
                img.gameObject.SetActive(true);
                totalscore[totalscorecount] = img;
                totalscorecount++;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {

        //lifecycle of a popup
       if(timer > 1)
        {
           // print("timer counting down");
            timer--;
        } else if(timer ==1)
        {
            //reset
            activeImg.gameObject.SetActive(false);
            timer = 0;
        } else if(timer == 0)
        {
            //do nothing
        }
        resetCounter();
        int score = GameObject.FindObjectOfType<sceneManager>().getScore();
        if(score >= 0)
        {
            counterthing(score);
        }
        resetCounter();
	}

    void resetCounter()
    {
        counterthing(0000000000);
    }

    public static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        System.Array.Reverse(charArray);
        return new string(charArray);
    }

    void counterthing(int num)
    {
       

        for (int i = num.ToString().Length-1 ; i >=0; i--)
        //for(int i = 0; i < num.ToString().Length; i++)
        {
            string temp = num.ToString();
            string wow = Reverse(temp);
            int pos = int.Parse(wow.Substring(i, 1));
            //int pos = int.Parse(num.ToString().Substring(i, 1));
            //int pos = int.Parse(num.ToString()[i]);
         //   print("pos is: " +pos);
            for (int j = 0; j < 9; j++)
            //for(int j = 9; j >0; j--)
            {

                //print("Image (" + i + ")");
                if (totalscore[j].name == "Image (" + i + ")")
                {
                   // print("matches");
                    totalscore[j].sprite = spriteSheetSprites[pos]; //load lowest digit into lowest ditigt on board
                }
            }
           // totalscore[2].sprite = spriteSheetSprites[9];
        }
    }

    public void randomHappy()
    {
       // print("passing to passing");
        passing(goods[Random.Range(0, goodscount)]);
        timer = 20;
    }

    public void randomSad()
    {
       // print("passing");
        passing(bads[Random.Range(0, badscount)]);
        timer = 20;
    }

    public void combo(int num) //just use the number for the combo you want
    {
        if(num > 3)
        {
            num = 3;
        }
        passing(combos[num - 2]);
        timer = 30;
    }

    private void passing(Image ita) //image to active
    {
        //print("passing");
        float firstsample = Random.value;
        float secondsample = Random.value;
        float x = (firstsample > 0.5f) ? 0.5f * Screen.width/5 : -0.5f * Screen.width/5;
        float y = (secondsample > 0.5f) ? 0.5f * Screen.height/5 : -0.5f * Screen.height/5;
        ita.gameObject.transform.localPosition = new Vector3(x, y, 0);
        ita.gameObject.SetActive(true);
        activeImg = ita;
        //timer = 20;
    }


}
