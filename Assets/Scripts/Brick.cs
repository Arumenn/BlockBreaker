using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

	public static int breakableCount = 0;
	public AudioClip crack;
	public Sprite[] hitSprites;
	public GameObject smoke;
	
	private int timesHit;
	private bool isBreakable;
	private LevelManager levelManager;

	// Use this for initialization
	void Start () {
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		timesHit = 0;
		isBreakable = (this.tag == "Breakable");
		if (isBreakable) { 
			breakableCount++;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter2D(Collision2D col){
		AudioSource.PlayClipAtPoint(crack, transform.position);
		if (isBreakable) {
			handleHits();
		}
	}
	
	void handleHits (){
		int maxHits = hitSprites.Length + 1;
		timesHit++;
		if (timesHit >= maxHits) {
			breakableCount--;
			levelManager.BrickDestroyed();
			PuffSmoke();
			Destroy(gameObject);
		} else {
			LoadSprites();
		}
	}
	
	void PuffSmoke(){
		GameObject smokePuff = Instantiate(smoke, transform.position, Quaternion.identity) as GameObject;
		smokePuff.particleSystem.startColor = gameObject.GetComponent<SpriteRenderer>().color;
	}
	
	void LoadSprites(){
		int spriteIndex = timesHit - 1;
		if (hitSprites[spriteIndex]) {
			this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
		}
	}
}
