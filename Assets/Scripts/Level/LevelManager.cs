using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*[System.Serializable]
public class ItemFrequency {
	//public GameItem gameItem;
	public LevelItem levelItem;
	public int nMaxFront;
	public int nMaxRows;
	public float frequency;
}*/

[System.Serializable]
public class LevelData {
	public float startSpeed = 1.0f;
	public float maxSpeed = 10.0f;
	public float speedDecrease = 0.1f;

	public GameItem.ItemType lastItem = GameItem.ItemType.SMALL_SUN;
	public List<LevelItem> levelItems = new List<LevelItem>();

	public float totalFrequency = 0.0f;
	public void Init() {
		totalFrequency = 0.0f;
		foreach (LevelItem levelItem in levelItems) {
			totalFrequency += levelItem.frequency;
		}
		foreach (LevelItem levelItem in levelItems) {
			levelItem.frequency = levelItem.frequency * 100.0f / totalFrequency;
		}
	}

	public float lastPos = 0.0f;

	public List<GameObject> GetNextItems() {
		List<GameObject> listG0 = new List<GameObject> ();
		LevelItem levelItem = null;
		do {
			float randValue = UnityEngine.Random.Range(0.0f, 100.0f);
			float currValue = 0.0f;
			for (int iItem = 0; iItem < levelItems.Count && levelItem == null; iItem++) {
				currValue += levelItems [iItem].frequency;
				if (randValue <= currValue)
					levelItem = levelItems [iItem];
			}
			if(levelItem.itemType ==  lastItem)
				levelItem = null;
		} while (levelItem == null);

		lastItem = levelItem.itemType;

		int rows = UnityEngine.Random.Range(levelItem.nMinRows, levelItem.nMaxRows + 1);
		lastPos += levelItem.roomBeforeItem;
		for(int iRow = 0; iRow < rows; iRow++) {
			GameObject go = GameObject.Instantiate(levelItem.gameObject, new Vector3(0, 0, lastPos), Quaternion.identity) as GameObject;
			lastPos += levelItem.roomBetweenRows;
			listG0.Add(go);
		}

		return listG0;
	}
}

public class LevelManager : MonoBehaviour {

	public GameObject m_playerObj;
	public PlayerController m_playerController;
	public GameObject m_rope;

	public LevelData m_levelData = new LevelData();

	public static LevelManager Instance = null;
	void Awake() {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(this.gameObject);
		} else {
			Destroy (this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		if (m_playerController == null) {
			m_playerController = GameObject.FindObjectOfType<PlayerController>() as PlayerController;
		}
		if (m_playerObj == null && m_playerController != null) {
			m_playerObj = m_playerController.gameObject;
		}

		Setup ();
		Launch ();
	}

	public void Setup() {
		m_levelData.Init ();
		for (int i = 0; i < 10; i++) {
			List<GameObject> items = m_levelData.GetNextItems();
			if(items != null && items.Count > 0) {
				GameObject go = new GameObject("Items" + i.ToString());
				go.transform.position = items[0].transform.position;
				go.transform.parent = transform.parent;
				for(int iRow = 0; iRow < items.Count; iRow++) {
					items[iRow].gameObject.transform.parent = go.transform;
				}
			}
		}
	}

	public void Launch() {
		m_playerController.SetSpeed (m_levelData.startSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		m_playerController.SetSpeed (m_playerController.m_speed - m_levelData.speedDecrease * Time.deltaTime);
		m_playerController.Move (Time.deltaTime);
	}

	public static void ManageCollision(GameItem item) {
		Debug.Log ("Manage Collision");
		if (Instance == null || item == null)
			return;

		switch (item.m_type) {
		default:
			break;
		}
	}
}
