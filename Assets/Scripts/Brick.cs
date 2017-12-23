using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

	public AudioClip crack;
	public Sprite[] hitSprites;
	public static int breakableCount = 0;
	public GameObject smoke;
	private int timesHit;
	private LevelManager levelManager;
	private bool isBreakable;

	// Use this for initialization
	void Start () {
		isBreakable = (this.tag == "Breakable");
		if (isBreakable) {
			breakableCount++;
		}
		timesHit = 0;
		levelManager = GameObject.FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter2D (Collision2D col) {
		AudioSource.PlayClipAtPoint(clip: crack, position: transform.position, volume: 0.8f);
		if (this.tag == "Breakable") {
			HandleHits();
		}
    }

	void HandleHits() {
		timesHit++;
		if (timesHit >= hitSprites.Length + 1) {
			breakableCount--;
			levelManager.BrickDestroyed();
			PuffSmoke();
			Destroy(gameObject);
		} else {
			LoadSprites();
		}
	}

	void PuffSmoke() {
		GameObject smokePuff = Instantiate(smoke, transform.position, Quaternion.identity) as GameObject;
		ParticleSystem ps = smokePuff.GetComponent<ParticleSystem>();
		var main = ps.main;
		main.startColor = gameObject.GetComponent<SpriteRenderer>().color;
	}

	void LoadSprites() {
		int spriteIndex = timesHit - 1;
		if (hitSprites[spriteIndex]) {
			this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
		} else {
			Debug.LogError("Brick sprite does not exist");
		}
	}

	// TODO Remove this method once we can actually win
	void SimulateWin() {
		levelManager.LoadNextLevel();
	} 
}
