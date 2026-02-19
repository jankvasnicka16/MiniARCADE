using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public void PlayClick()
    {
        if (SoundManager.Instance)
            SoundManager.Instance.PlayClick();
    }
}
