using UnityEngine;

public class Coin : MonoBehaviour
{
    public Score _score;
    public  int CoinValue = 10;
    private void Awake()
    {
        _score = FindFirstObjectByType<Score>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CollectedCoin();
        }
            
        else
        {
            Debug.Log(collision.tag);
        }
    }

    public void CollectedCoin()
    {
        _score._score += CoinValue;
        _score._ringCount++;
        SoundManager.PlayEFXRandomSoundPitch(SoundType.RING, .7f, true, .3f, .7f);
        Destroy(gameObject);
    }
}
