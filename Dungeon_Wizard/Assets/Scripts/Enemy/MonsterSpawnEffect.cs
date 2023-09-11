using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnEffect : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioClip spawnSound;
    AudioSource spawnSource;
    void OnEnable()
    {
        spawnSource = GetComponent<AudioSource>();
        spawnSource.clip = spawnSound;
        spawnSource.Play();
        StartCoroutine(DisableEffect());
    }

    IEnumerator DisableEffect(){
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
    }
}
