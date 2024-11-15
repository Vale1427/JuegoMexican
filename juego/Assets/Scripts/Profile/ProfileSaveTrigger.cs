using UnityEngine;

public class ProfileSaveTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other){
        ProfileStorage.StorePlayerProfile(other.gameObject);
    }
}
