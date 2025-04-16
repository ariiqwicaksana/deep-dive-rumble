using UnityEngine;

public class LoopBackgroundMusci : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        // Ambil atau tambahkan AudioSource ke GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Pastikan opsi loop aktif
        audioSource.loop = true;
    }

    void Start()
    {
        // Mainkan musik
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource atau AudioClip belum disetel.");
        }
    }

    // Fungsi untuk mengganti lagu di runtime
    public void SetMusic(AudioClip newClip)
    {
        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.clip = newClip;
            audioSource.Play();
        }
    }
}
