using UnityEngine;
using UnityEngine.UI;
using System.Collections;
 
public class PlayerController : MonoBehaviour {

    // physics properties
    private Vector2 velocity = Vector2.zero;  // player speed
    private Vector2 OriginalVelocity;         // vector for velocity backup
    private Vector2 OriginalGravity;          // vector for gravity backup
    public  Vector2 gravity;                  // Custom gravity, we won't use unity's physics
    public  Vector2 jumpVelocity;             // jump velocity of the player

    // logic and animations
    public float maxRotationVelocity;      // jump animation max rotation
    private bool canJump = false;          // Player can jump if screen was touched or space pressed
    private bool gameOver = false;         // Is player dead ??
    private bool stop = false;             // Is gameplay stopped ??
    private int currentObstacleId = 0;     // current obstacle we are trying to jump
    private int prevObstacleId = 0;        // prev obstacle we are trying to jump
    public static bool debug = false;              // should we display debug info

    // references to other objects
    public GameObject score_go;       // ref to the score counter
    public GameObject portal;         // ref to the portal GameObject
    private Fader fader;              // ref to Fader script inside the canvas
    public GameObject explosion;      // explosion prefab

    // Camera properties
    public GameObject cam;            // camera reference
    private float targetZRotation;    // camera rotation on z axis
    public float RotationSpeed = 20;  // camera rotation speed

    void Start()
    {
        targetZRotation = 0;       // initial cam rotation
        OriginalGravity = gravity; // backup inital gravity
        OriginalVelocity = velocity;
        disablePortal();
        fader = GameObject.FindObjectOfType<Fader>();
    }

    void Update (){
        if ((Input.GetKeyDown(KeyCode.Space) || // if "Space" is pressed OR
             Input.GetMouseButtonDown(0)) &&    // screen is touched AND
             !gameOver && !stop) {              // game is not over or stopped, then
            canJump = true;                     // player can jump
        }
    }
 
    // update method called a fixed number of times per second
    void FixedUpdate () { playerMovement(); }

    // logic for moving the player
    void playerMovement()
    {
        velocity += gravity * Time.deltaTime; // player falls
        if (canJump == true)
        {
            velocity.y = jumpVelocity.y;  // Get impulse from jump velocity
            canJump = false;              // Player cannot jump again if key/screen is not pressed again
        }
        Vector2 newPosition = velocity * Time.deltaTime; // apply impulse to player
        transform.position += new Vector3(newPosition.x, newPosition.y, transform.position.z);
        
        // Animate player rotation
        float angle = 0;
        if (velocity.y >= 0) {
            // if going up, look upwards
            angle = Mathf.Lerp (0, 25, velocity.y/maxRotationVelocity);
        }
        else {
            // if going down, look downwards
            angle = Mathf.Lerp (0, -75, -velocity.y/maxRotationVelocity);
        }
        // apply rotation
        transform.rotation = Quaternion.Euler (0, 0, angle);
    }

    IEnumerator onGameOver(Collision2D collision)
    {
        GetComponent<SpriteRenderer>().color = Color.red; // set player color to red
        // play explosion
        if (collision.contacts.Length > 0)
        {
            Vector2 collisionPoint = collision.contacts[0].point;
            Vector3 explosionPoint = new Vector3(collisionPoint.x, collisionPoint.y, 0);
            GameObject explosion_go = Instantiate(explosion, explosionPoint, Quaternion.identity) as GameObject;
            explosion_go.GetComponent<Animator>().Play(0);
        }
        bool playSFX = true;
        if (PlayerPrefs.HasKey("SFXToggle") && PlayerPrefs.GetInt("SFXToggle") == 0)
            playSFX = false;
        
        if(playSFX) GetComponent<AudioSource>().Play();

        stopObstacles();
        stopBackground();
        score_go.GetComponent<ScoreCounter>().saveScore();
        
        yield return new WaitForSeconds(1.5f);   // wait 1.5 seconds

        /*float choice = Random.Range(0, 1);
        if(choice > 0.5)*/ GameObject.FindObjectOfType<IntersticialController>().showAd();

        yield return StartCoroutine(fader.FadeToOpaque());
        Application.LoadLevel("gameover");       // Go the game over scene
    }

    void onCollideWithGround(Collision2D collision)
    {
        // backup gravity before collision and set gravity to 0
        OriginalGravity = gravity;
        gravity.Set(0, 0);

        if(!gameOver) StartCoroutine(onGameOver(collision));
        gameOver = true;
    }

    void onCollideWithObstacle(Collision2D collision)
    {
        collision.collider.isTrigger = true;  // No more collision with this obstacle
        setObstacleColor(Color.red, collision.transform);
        if(!gameOver) StartCoroutine(onGameOver(collision));
        gameOver = true;
    }

    void onCollideWithScorePoint(Collider2D collider)
    {
        // set blue color on obstacles
        setObstacleColor(Color.blue, collider.transform.parent);
        // update score counter
        score_go.GetComponent<ScoreCounter>().updateScore();
        
        // rotate camera
        targetZRotation += 5;
        targetZRotation %= 360;

        float currentZRot = cam.transform.eulerAngles.z;
        float newZRot = Mathf.MoveTowardsAngle(currentZRot,                     // from
                                               targetZRotation,                 // to
                                               RotationSpeed * Time.deltaTime); // speed
        cam.transform.eulerAngles = new Vector3(0,0, newZRot);

        // spawn portal every ten points
        if(currentObstacleId > 1)
        {
            if(currentObstacleId % 10 == 9){ enablePortal(); }
            if(currentObstacleId % 10 == 0){ StartCoroutine("OnPortal"); }
            if(currentObstacleId % 10 == 1){ disablePortal(); }
        }
    }

    void enablePortal() { portal.SetActive(true); }
    void disablePortal() { portal.SetActive(false); }

    void unlockSkin()
    {
        int nextSkinUnlocked = 0;
        int currentSkinsUnlocked = 0;
        int skinsUnlocked = 0;
        
        switch (currentObstacleId)
        {
            case 10:
                nextSkinUnlocked = 1; break;
            case 20:
                nextSkinUnlocked = 2; break;
            case 30:
                nextSkinUnlocked = 3; break;
            case 40:
                nextSkinUnlocked = 4; break;
            case 50:
                nextSkinUnlocked = 5; break;
            default:
                break;
        }
        if (PlayerPrefs.HasKey("unlockedSkins"))
        {
            currentSkinsUnlocked = PlayerPrefs.GetInt("unlockedSkins");
        }

        if (nextSkinUnlocked > currentSkinsUnlocked)
        {
            PlayerPrefs.SetString("newSkinUnlocked", "true");            
            skinsUnlocked = nextSkinUnlocked;
        }
        else
        {
            PlayerPrefs.SetString("newSkinUnlocked", "false");
            skinsUnlocked = currentSkinsUnlocked;
        }
        PlayerPrefs.SetInt("unlockedSkins", skinsUnlocked);
        PlayerPrefs.Save();
    }

    IEnumerator OnPortal()
    {
        // check for unlocking skins
        unlockSkin();
        // stop everything
        stop = true;
        //stopPlayer();
        //stopObstacles();
        //stopBackground();

        
        // fade to white
        if(fader) yield return StartCoroutine(fader.FadeToOpaque());
        Time.timeScale = 0.001f;

        // flip camera
        bool mirror = cam.GetComponent<CameraMirror>().mirror;
        cam.GetComponent<CameraMirror>().mirror = !mirror;

        // swap sprites
        if (currentObstacleId % 20 == 0) swapSprites("bg_original", "portal1");
        else swapSprites("bg_verde", "portal2");

        // move obstacles so player has time to react
        moveObstacles(-1);
        GameObject portal = GameObject.FindGameObjectWithTag("Portal");
        transform.position = new Vector3(transform.position.x, portal.transform.position.y, transform.position.z);
        // fade to clear
        if(fader) yield return StartCoroutine(fader.FadeToClear());
        Time.timeScale = 0.001f;

        // unstop everything
        stop = false;
        //unstopPlayer();
        //unstopObstacles();
        //unstopBackgorund();
        Time.timeScale = 1;
        yield return null;
    }

    void moveObstacles(float moveX)
    {
        ObstacleController[] ocs = FindObjectsOfType<ObstacleController>();
        foreach(ObstacleController oc in ocs){
            oc.MoveX(moveX);
        }
    }

    void swapSprites(string bg_spriteName, string portal_spriteName)
    {
        // Load sprites
        Sprite portal_sprite = Resources.Load<Sprite>(portal_spriteName);
        Sprite bg_sprite = Resources.Load<Sprite>(bg_spriteName);

        // swap sprites
        portal.GetComponent<SpriteRenderer>().sprite = portal_sprite; // swap portal sprite
        GameObject[] bgs = GameObject.FindGameObjectsWithTag("Background");
        foreach (GameObject bg in bgs)
        {
            bg.GetComponent<SpriteRenderer>().sprite = bg_sprite; // swap background (bg) sprite
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")  { onCollideWithGround(collision); }
        if (collision.gameObject.tag  == "ObstacleGroup"){ onCollideWithObstacle(collision); }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // restore gravity on collision end
        gravity = OriginalGravity;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "ScoreTrigger")
        {
            // get obstacle id
            GameObject obstacleGroup = other.transform.parent.gameObject;
            int id = obstacleGroup.GetComponent<ObstacleController>().id;

            // set id properties
            prevObstacleId = currentObstacleId;
            currentObstacleId = id;

            // are both ids the same number ??
            bool same = prevObstacleId == currentObstacleId;
            
            if(debug) Debug.Log(id + ": prevId = " + prevObstacleId + ", currentId: " + currentObstacleId + ", same: " + same);
            
            if (!same && !gameOver && !stop)
            {
                onCollideWithScorePoint(other);
            }
        }
    }

    void setObstacleColor(Color newColor, Transform groupTransform)
    {
        // Get obstacle group renderer components
        Component[] renderers = groupTransform.GetComponentsInChildren(typeof(SpriteRenderer));
        foreach (Component r in renderers)              // for each component
        {
            if (r.gameObject.tag == "Obstacle")
            {
                SpriteRenderer r_cast = (SpriteRenderer)r; // cast component to renderer
                r_cast.color = newColor;                   // Set color to new color
            }
        }
    }

    // player movement toggle
    void stopPlayer()
    {
        OriginalGravity = gravity;
        OriginalVelocity = velocity;
        gravity.Set(0, 0);
        velocity.Set(0, 0);
    }
    void unstopPlayer()
    {
        gravity = OriginalGravity;
        velocity = OriginalVelocity;
    }

    // obstacles moving control
    void unstopObstacles(){ setObstaclesVelocity(ObstacleController.initialXVel); }
    void stopObstacles(){ setObstaclesVelocity(0); }
    void setObstaclesVelocity(float newVel)
    {
        ObstacleController.xVelocity = newVel;
    }

    // background scrolling control
    void stopBackground(){ stopBackground(false); }
    void unstopBackgorund(){ stopBackground(true); }
    void stopBackground(bool scrollerEnabled)
    {
        GameObject[] bgs = GameObject.FindGameObjectsWithTag("Background");
        foreach (GameObject bg in bgs)
        {
            bg.GetComponent<BackgroundScroller>().enabled = scrollerEnabled;
        }
    }
}