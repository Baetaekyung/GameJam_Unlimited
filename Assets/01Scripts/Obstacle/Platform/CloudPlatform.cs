using System.Collections;
using UnityEngine;

public class CloudPlatform : MonoBehaviour
{
    [SerializeField] private float _fadeDuration = 1f; // ������� ��Ÿ���µ� �ɸ��� �ð�
    [SerializeField] private float _waitTime = 2f;     // ��ٸ��� �ð�
    private SpriteRenderer _renderer;
    private Collider2D _collider;

    private bool _isFading = false; // ������� �ְų� �̹� �������?

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isFading) // �̹� ������� ���� �ƴ϶��
        {
            _isFading = true; // ������� ���·� ����
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color color = _renderer.color;

        // ������� �ִϸ��̼�
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

        // ��Ÿ����
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

        // ����� ���� ����
        _isFading = false;
    }


}
