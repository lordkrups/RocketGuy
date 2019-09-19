using UnityEngine;


[AddComponentMenu("S2/S2Button Scale")]
public class S2ButtonScale : MonoBehaviour
{
    public Vector3 toScale = new Vector3(1.05f, 1.05f, 1f);
    public float time = 0.1f;
    private Vector3 _scale;
    private bool _isPlaying;

    void OnPress(bool isPressed)
    {
        if (isPressed)
        {
            Play();
        }
    }
    private void Play()
    {
        if (_isPlaying) return;
        _scale = transform.localScale;
        LeanTween.scale(gameObject, new Vector3(_scale.x*toScale.x, _scale.y*toScale.y, _scale.z*toScale.z), time)
            .setOnComplete(Stop);
    }
    private void Stop()
    {
        transform.localScale = _scale;
        _isPlaying = false;
    }

}
