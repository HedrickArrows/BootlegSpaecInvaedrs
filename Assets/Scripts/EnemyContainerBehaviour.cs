using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyContainerBehaviour : MonoBehaviour {
    public int enemyrow = 13;
    public int enemycol = 4;
    public float dir, backupdir;         //enemy moving direction and speed, backup for when paused
    private System.DateTime quittimer;         //to measure time needed to quit
    public bool d, scr, commencingScramble, canEventSpawn, isPaused, quitting;  //can enemies change dir, can they scramble, do they scramble, can ufo spawn at top, is the game paused, are we done yet
    public int stage;   //which stage we're in
    int extra, frames, scrUp, scrDwn; //random amt of frames before ufo spawns, frame counter, extra ints for scramble
    //System.DateTime timeframe;

    int wep = 0; //just in case for optimization of code for weapon swaps

    public GameObject Enemenemy;
    public GameObject Playar;
    public GameObject LaserEnemy;
    public GameObject BombEnemy;
    public GameObject EventEnemy;
    public GameObject ArmorEnemy;
    public GameObject ArmorlessEnemy;

    public GameObject barriero;

    public GameObject showcase;

    Vector3 playerposition = new Vector3(0.0f, -4.55f, 0.0f);

    public Text t, h, r, no, exr, tim; //score, health, result text, stage no, extra text;

    public float height;  //scramble height

    char[][][] levels;

    public System.Random rand;

    //public List<GameObject> ene; //list of enemies in level //REMOVALD! now it gonna be childrens
	// Use this for initialization
	void Start () {
        //quittimer = 999;
        isPaused = false;
        rand = new System.Random();
        frames = 0;//timeframe = System.DateTime.Now;
        extra = rand.Next(1000, 1500);

        Playar = Instantiate(Playar, playerposition, Quaternion.identity) as GameObject;

        levels = new char[8][][];
        for (int i = 0; i < levels.Length; levels[i++] = new char[4][]) ;

        levels[0][0] = new char[]{ '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1' };
        levels[0][1] = new char[] { '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1' };
        levels[0][2] = new char[] { '1','1','1','1','1','1','1', '1', '1', '1', '1' };
        levels[0][3] = new char[] { '1','1','1','1','1','1','1', '1', '1', '1', '1' };

        levels[1][0] = new char[] { '1','2','1','2','1','2','1', '2', '1', '2', '1' };
        levels[1][1] = new char[] { '1','2','1','2','1','2','1', '2', '1', '2', '1' };
        levels[1][2] = new char[] { '1','2','1','2','1','2','1', '2', '1', '2', '1' };
        levels[1][3] = new char[] { '1','2','1','2','1','2','1', '2', '1', '2', '1' };

        levels[2][0] = new char[] { '1', '1', '2', '1', '1', '2', '1', '1', '2', '1', '1' };
        levels[2][1] = new char[] { '1', '1', '2', '1', '1', '2', '1', '1', '2', '1', '1' };
        levels[2][2] = new char[] { '2', '2', '2', '1', '2', '2', '2' , '1', '2', '2', '2' };
        levels[2][3] = new char[] { '2', '2', '2', '1', '2', '2', '2' , '1', '2', '2', '2' };

        levels[3][0] = new char[] { '1', '1', '2', '1', '1', '2', '1', '1', '2', '1', '1' };
        levels[3][1] = new char[] { '1', '1', '2', '1', '1', '2', '1', '1', '2', '1', '1' };
        levels[3][2] = new char[] { '3', '2', '3', '1', '2', '2', '2', '1', '3', '2', '3' };
        levels[3][3] = new char[] { '1', '1', '1', '1', '1', '2', '1', '1', '1', '1', '1' };

        levels[4][0] = new char[] { '1', '1', '2', '1', '1', '2', '1', '1', '2', '1', '1' };
        levels[4][1] = new char[] { '3', '3', '3', '3', '3', '3', '3', '3', '3', '3', '3' };
        levels[4][2] = new char[] { '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1' };
        levels[4][3] = new char[] { '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1' };

        levels[5][0] = new char[] { '3', '3', '3', '3', '3', '3', '3', '3', '3', '3', '3' };
        levels[5][1] = new char[] { '2', '2', '2', '2', '2', '2', '2', '2', '2', '2', '2' };
        levels[5][2] = new char[] { '2', '2', '2', '2', '2', '2', '2', '2', '2', '2', '2' };
        levels[5][3] = new char[] { '4', '2', '4', '2', '4', '2', '4', '2', '4', '2', '4' };

        levels[6][0] = new char[] { '3', '3', '3', '3', '3', '3', '3', '3', '3', '3', '3' };
        levels[6][1] = new char[] { '4', '4', '2', '4', '4', '2', '4', '4', '2', '4', '4' };
        levels[6][2] = new char[] { '3', '3', '3', '3', '3', '3', '3', '3', '3', '3', '3' };
        levels[6][3] = new char[] { '2', '2', '2', '1', '2', '2', '2', '1', '2', '2', '2' };

        for (int i = 0; i < levels[7].Length; levels[7][i++] = new char[11]) ; //randomly generated shidddd yeeeee
        GenerateEnemies();
        stage = 0;
        StartStage();
	}
	
	// Update is called once per frame
	void Update () {
        t.text = Playar.GetComponent<PlayerBehaviour>().score.ToString(); //show score
        string s = "";
        int i;
        for(i=0;i<Playar.GetComponent<PlayerBehaviour>().health;i++) //show health
            s += "█";
        s += "\0";
        h.text = s;

        if (Input.GetKey(KeyCode.Escape) && isPaused) {
            if (quitting == false) { quittimer = System.DateTime.Now;  quitting = true; }
            System.TimeSpan hohoho = System.DateTime.Now - quittimer;
            exr.text = "quitin in " + ((5 - (float)(hohoho.Seconds + hohoho.Milliseconds / 1000.0f)) > 0 ? (5 - (float)(hohoho.Seconds + hohoho.Milliseconds / 1000.0f)).ToString("0.000") : "now!!!1");
            if (hohoho.Seconds == 4 && quitting) r.text = "bye :(";
            if (hohoho.Seconds == 5 && quitting) {
                //if (Application.isEditor)
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                Application.Quit();
            }
        }
        if (Input.GetKeyUp(KeyCode.Escape) && isPaused)
        {
            r.text = "(paused)";
            exr.text = "press entr to contenue \n hold esc to quit";
            quitting = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused){
            Time.timeScale = 0.0f;
            isPaused = true;
            backupdir = dir; dir = 0;
            Playar.GetComponent<PlayerBehaviour>().moveL = Playar.GetComponent<PlayerBehaviour>().moveR = Playar.GetComponent<PlayerBehaviour>().shoot = false;
            r.text = "(paused)";
            exr.text = "press entr to contenue \n hold esc to quit";
            quitting = false;
        }

        if (Input.GetKeyDown(KeyCode.Return) && isPaused)
        {
            Time.timeScale = 1.0f;
            isPaused = false;
            dir = backupdir;
            Playar.GetComponent<PlayerBehaviour>().moveL = Playar.GetComponent<PlayerBehaviour>().moveR = Playar.GetComponent<PlayerBehaviour>().shoot = true;
            r.text = exr.text = "";
            quitting = false;
        }
        if (!isPaused) frames++;
        else frames--;
        if (frames / (75 * Time.deltaTime) >= extra && r.text.Equals("")) { //should it spawn that ufo
            frames = 0;
            extra = rand.Next(1000 / (int)((75 * Time.deltaTime) + 1) , 2000 / (int)((75 * Time.deltaTime) + 1));
            Vector3 ve = transform.position;
            ve.x += (float)9.7; 
            GameObject eventos = Instantiate(EventEnemy, ve, Quaternion.identity) as GameObject;
            //eventos.GetComponent<EnemyBehaviour>().container = this.gameObject;
            eventos.transform.parent = transform;//ene.Add(eventos);
        }


        if (Playar.GetComponent<PlayerBehaviour>().health <= 0) //does player have health
        {
            dir = 0;
            r.text = "Killed :(";
            exr.text = "Ya score: " + t.text + "\n press EnterR to restart";
            t.text = "";
            canEventSpawn = false;
            Playar.GetComponent<PlayerBehaviour>().moveL = Playar.GetComponent<PlayerBehaviour>().moveR = Playar.GetComponent<PlayerBehaviour>().shoot = false;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                no.text = "Stage 0";
                stage = 0;
                dir = 1;
                foreach (Transform child in transform)//var v in ene)
                    Destroy(child.gameObject);
                Playar.GetComponent<PlayerBehaviour>().score = 0;
                Playar.GetComponent<PlayerBehaviour>().health = 7;
                Playar.transform.position = playerposition;
              StartStage();
                r.text = exr.text = "";
            }
        }
        if (GameObject.FindGameObjectsWithTag("Tagged").Length.Equals(0)) { //is there no enemies/bullets/pickups
            r.text = "Completeded";
            frames--;
            exr.text = "Press ENTERe to continueue";
            canEventSpawn = false;
            Playar.GetComponent<PlayerBehaviour>().shoot = false;
            if (Input.GetKeyDown(KeyCode.Return)) {
                if (stage + 1 < levels.Length)
                    stage++;
                else GenerateEnemies();

                var bl = GameObject.FindGameObjectsWithTag("Boolet");
                foreach (var v in bl)
                    Destroy(v);

                StartStage();
                r.text = exr.text = "";
            }
        }
        if (transform.childCount < enemyrow * enemycol * scrUp / scrDwn && !scr) {
            scrUp--;
            scr = true;
            Scramble();
            commencingScramble = true;
        }
        else scr = false;

        if (commencingScramble && !isPaused) {
            if (!AreTheyThereYet())
                foreach (Transform child in transform){
                    if(!child.gameObject.name.Contains("Event"))
                        child.position = Vector3.MoveTowards(child.position, new Vector3(child.GetComponent<EnemyBehaviour>().scatterX, child.GetComponent<EnemyBehaviour>().scatterY), 0.15f * (75 * Time.deltaTime));
                   }

            else
            {

                foreach (Transform child in transform)
                {
                    child.GetComponent<EnemyBehaviour>().killable = true;
                    if(!child.gameObject.name.Contains("Event"))
                        Destroy(child.GetChild(0).gameObject);
                }
                commencingScramble = false;

            }
         }

        

        if (Playar.GetComponent<PlayerBehaviour>().weapontype == 0)
        {
            if (wep != 0)
            {
                tim.text = "";
                wep = 0;
                showcase.GetComponent<ShowcaseBehaviour>().sr.sprite = showcase.GetComponent<ShowcaseBehaviour>().weaponSprites[0];
            }
        }
        else
        {
            float timem = Playar.GetComponent<PlayerBehaviour>().limit - (Time.time - Playar.GetComponent<PlayerBehaviour>().time);
            tim.text = (timem > 0 ? timem : 0).ToString("0.000");
            if (wep != Playar.GetComponent<PlayerBehaviour>().weapontype)
            {
                showcase.GetComponent<ShowcaseBehaviour>().sr.sprite = showcase.GetComponent<ShowcaseBehaviour>().weaponSprites[Playar.GetComponent<PlayerBehaviour>().weapontype];
                wep = Playar.GetComponent<PlayerBehaviour>().weapontype;
            }
        }
	}

    bool IsPositionTaken(float x, float y) {    //scramble function, so enemies don't take the same position
        foreach (Transform child in transform)
            if (x == child.gameObject.GetComponent<EnemyBehaviour>().scatterX && y == child.gameObject.GetComponent<EnemyBehaviour>().scatterY)
                return true;
        return false;
    }

    bool AreTheyThereYet() {    //scramble function, where they check if they are in the target positioning
        foreach (Transform child in transform) {
            Vector3 comper = new Vector3(child.GetComponent<EnemyBehaviour>().scatterX, child.GetComponent<EnemyBehaviour>().scatterY);
            if (child.position != comper && !child.gameObject.name.Contains("Event"))
                return false;
        }
                
        return true;
    }

    void Scramble() {   //enemies PLAN TO swap places
        foreach (Transform child in transform)
            child.gameObject.GetComponent<EnemyBehaviour>().scatterX = child.gameObject.GetComponent<EnemyBehaviour>().scatterY = 999;
        foreach (Transform child in transform)
        {
            
            if (!child.gameObject.name.Contains("Event"))
            {
                float x, y;
                do
                {
                    x = rand.Next(-5, 6) * 1.2f;
                    y = height - rand.Next(0, 4);
                } while (IsPositionTaken(x, y));
                child.gameObject.GetComponent<EnemyBehaviour>().scatterX = x;
                child.gameObject.GetComponent<EnemyBehaviour>().scatterY = y;
                child.gameObject.GetComponent<EnemyBehaviour>().killable = false;
                GameObject j; j = Instantiate(barriero, child) as GameObject;
                j.transform.localPosition = new Vector3(0, 0);
                j.transform.localScale = new Vector3(2 / child.lossyScale.x, 2 / child.lossyScale.y);

                //Vector3 v = child.position;
                //v.x = x * (float)1.2;
                //v.y = height - y;
                //child.position = v;
            }
        }
    }

    void GenerateEnemies() {    //fill levels[7] with random enemies
        for (int i = 0; i < levels[7].Length; i++)
            for (int j = 0; j < levels[7][i].Length; j++) {
                string s = rand.Next(1, 5).ToString();
                levels[7][i][j] = s.ToCharArray()[0];
            }
            
    }

    void StartStage() { 
        no.text = no.text.Split(' ')[0] + ' ' + (System.Int32.Parse(no.text.Split(' ')[1]) + 1).ToString();
        tim.text = "";
        commencingScramble = false;
        scrUp = System.Int32.Parse(no.text.Split(' ')[1]) / 7 + 1;
        scrUp = (scrUp > 3 ? 3 : scrUp);
        scrDwn = scrUp + 1;
        dir = 1;
        canEventSpawn = true;
        d = true;
        scr = false;
        enemyrow = levels[stage].Length;
        enemycol = levels[stage][0].Length;
        Playar.GetComponent<PlayerBehaviour>().shoot = true;
        Playar.GetComponent<PlayerBehaviour>().limit = 0;
        r.text = "";
        height = this.transform.position.y - 1;
        GameObject g = null;
        for (int j = 0; j++ < levels[stage].Length;)
            for (int i = 0; levels[stage][j-1].Length > i++; g.transform.parent = transform)
            {
                Vector3 position = this.transform.position;
                position.y = this.transform.position.y - (float)j;
                position.x = (float)(this.transform.position.x + (-enemyrow / 2 + i - 2.4) * 1.2);
                if (levels[stage][j-1][i-1].Equals('2'))
                    g = Instantiate(LaserEnemy, position, Quaternion.identity) as GameObject;
                else if (levels[stage][j - 1][i - 1].Equals('3'))
                    g = Instantiate(BombEnemy, position, Quaternion.identity) as GameObject;
                else if (levels[stage][j - 1][i - 1].Equals('4'))
                    g = Instantiate(ArmorEnemy, position, Quaternion.identity) as GameObject;
                else 
                    g = Instantiate(Enemenemy, position, Quaternion.identity) as GameObject;
                g.GetComponent<EnemyBehaviour>().container = gameObject;

            }
        Playar.GetComponent<PlayerBehaviour>().moveL = Playar.GetComponent<PlayerBehaviour>().moveR = Playar.GetComponent<PlayerBehaviour>().shoot = true;
    }

    
}
