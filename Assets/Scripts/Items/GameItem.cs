using UnityEngine;
using System.Collections;

public class GameItem : MonoBehaviour {

	[System.Serializable]
	public enum ItemType {
		SMALL_SUN,
		BIG_SUN,
		SKULL,
		BONUS_A,
		BONUS_B,
		BONUS_C,
		EMPTY
	}

	public ItemType m_type;
	public float m_value;
}
