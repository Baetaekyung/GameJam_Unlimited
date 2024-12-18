using System.Collections;
using UnityEngine;

public class CloudPlatform : MonoBehaviour
{
    [SerializeField] private float _fadeDuration = 1f; // 사라지고 나타나는데 걸리는 시간
    [SerializeField] private float _waitTime = 2f;     // 기다리는 시간
    private SpriteRenderer _renderer;
    private Collider2D _collider;

    private bool _isFading = false; // 사라지고 있거나 이미 사라졌니?

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isFading) // 이미 사라지는 중이 아니라면
        {
            _isFading = true; // 사라지는 상태로 설정
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color color = _renderer.color;

        // 사라지는 애니메이션
        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / _fadeDuration);
            color.a = alpha;
            _renderer.color = color;
            yield return null;
        }

        color.a = 0f;
        _renderer.color = color;

        _collider.enabled = false;

        yield return new WaitForSeconds(_waitTime);

        // 나타나기
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = _renderer.color;

        while (elapsedTime < 2)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / 2);
            color.a = alpha;
            _renderer.color = color;   
            yield return null;
        }

        color.a = 1f;
        _renderer.color = color;

        _collider.enabled = true;

        // 사라짐 상태 해제
        _isFading = false;
    }


}
