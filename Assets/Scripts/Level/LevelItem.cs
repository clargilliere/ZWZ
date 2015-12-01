using UnityEngine;
using System.Collections;

[System.Serializable]
public class LevelItem /*: MonoBehaviour*/ {

	public GameObject gameObject = null;
	public GameItem.ItemType itemType;
	public bool canBeSplit = false;
	public bool canBeRotated = false;
	public int nMinRows = 1;
	public int nMaxRows = 10;
	public float roomBetweenRows = 1.0f;
	public float roomBeforeItem = 0.0f;

	public float frequency = 1.0f;
}
