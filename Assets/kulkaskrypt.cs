using UnityEngine;

public class Card : MonoBehaviour
{
	

	public AudioClip fire;

	
	void Start(){
 Debug.Log("napis");
 SoundScript.Instance.PlaySound(fire);
}
 

}
