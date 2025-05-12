using Unity.VisualScripting;
using UnityEngine;

public class AudioSource : MonoBehaviour
{
    private UnityEngine.AudioSource _audioSource; // ����� �ҽ� ������Ʈ

    public void Play(AudioClip clip, float soundEffectVolume, float soundEffectPitchVariance)
    {
        if (_audioSource == null)
            _audioSource = gameObject.GetOrAddComponent<UnityEngine.AudioSource>(); // ����� �ҽ� �������� (�ʱ�ȭ)

        CancelInvoke(); // ������ ����� Invoke ���
        _audioSource.clip = clip; // ����� ����� Ŭ�� ����
        _audioSource.volume = soundEffectVolume; // ���� ����
        _audioSource.Play(); // ����� ���

        // ��ġ(�� ������) ���� (���� ���� ����)
        _audioSource.pitch = 1f + Random.Range(-soundEffectPitchVariance, soundEffectPitchVariance);

        // ����� Ŭ�� ���� + 2�� �� �ڵ� ��Ȱ��ȭ
        Invoke(nameof(Disable), clip.length + 2);
    }

    public void Disable()
    {
        _audioSource.Stop(); // ����� ����
        Destroy(this.gameObject); // ������Ʈ ����
    }
}
