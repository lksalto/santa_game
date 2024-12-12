using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine : MonoBehaviour
{
    IBossState currentState;
    [SerializeField] AudioClip theme;
    public int life = 100;
    public float flashCd = 1;
    public bool canAttack = true;
    public Phase1State phase1 = new Phase1State(); 
    public Phase2State phase2 = new Phase2State();
    public Phase3State phase3 = new Phase3State();
    public Phase4State phase4 = new Phase4State();

    [SerializeField] public GeniusAim geniusAim;

    [SerializeField] public CircularBomb bomb;
    [SerializeField] public EnemySpin spin;
    //[SerializeField] public FollowPlayerWithDistance follow;
    [SerializeField] public List<EnemyMono> shotguns;



    [SerializeField] SpriteRenderer sr;
    public List<Sprite> sprites;
    public Sprite currentSprite;
 
    private void Start()
    {
        
        currentState = phase1;
        currentState.EnterState(this);
        
        sr.sprite = sprites[0];
        currentSprite = sprites[0];
        canAttack = true;
        geniusAim.gameObject.SetActive(true);

        //bomb.gameObject.SetActive(false);
        //spin.gameObject.SetActive(false);
        //follow = GetComponent<FollowPlayerWithDistance>();
        StartCoroutine(ChooseSong());

    }

    public IEnumerator ChooseSong() {

        yield return new WaitForSeconds(0.1f);
        if (FindObjectOfType<AudioManager>().GetComponent<AudioSource>().clip.name != theme.name) {
            FindObjectOfType<AudioManager>().PlayMusic(theme);
            FindObjectOfType<AudioManager>().GetComponent<AudioSource>().PlayScheduled(AudioSettings.dspTime + theme.length);
        }



    }

    private void Update()
    {
        currentState?.CheckStateTransition(this);
    }

    public void TransitionToState(IBossState state)
    {
        currentState?.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    public void TakeDamage(int dmg)
    {
        life -= dmg;
        StartCoroutine(Flash(flashCd));
        currentState?.CheckStateTransition(this);

    }

    IEnumerator Flash(float invulnerabilityCd)
    {
        canAttack = false;
        //ManageCanAttack(canAttack);
        sr.sprite = sprites[4];
        yield return new WaitForSeconds(invulnerabilityCd);
        sr.sprite = currentSprite;
        canAttack = true;
        //ManageCanAttack(canAttack);
    }

    public void ManageCanAttack(bool b)
    {
        canAttack = b;
        geniusAim.gameObject.SetActive(b);

    }

    public void Die()
    {
        Destroy(gameObject);
    }


}
