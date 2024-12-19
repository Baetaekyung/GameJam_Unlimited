using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public enum SfxType
{
    BGM = 0, EFFECT = 1, BALLDETECT, BALLJUMP
}

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] private Slider vfxController;
    [SerializeField] private Slider bgmController;

    [SerializeField] private TMP_Text vfxVolumeTxt;
    [SerializeField] private TMP_Text bgmVolumeTxt;

    public AudioMixer audioMixer;
    public AudioMixerGroup MasterGroup;
    public AudioMixerGroup BGMGroup;
    public AudioMixerGroup SFXGroup;
    //�ϴ���

    [Header("BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;

    private AudioSource bgmPlayer;

    [Header("SFX")]
    public SfxSO[] sfxClipSO;
    public float sfxVolum;
    public int channels;

    private AudioSource[] _sfxPlayers;
    private int _channelIndex;

    protected override void Awake()
    {
        base.Awake();
        Init();
        bgmController.value = bgmVolume;
        vfxController.value = sfxVolum;
        bgmVolume = bgmController.value;
        sfxVolum = vfxController.value;
        SetVolume(0);

        bgmController.onValueChanged?.AddListener(SetVolume);
        vfxController.onValueChanged?.AddListener(SetVolume);
    }
    private void SetVolume(float volume)
    {
        bgmVolume = bgmController.value;
        bgmPlayer.volume = bgmVolume;

        sfxVolum = vfxController.value;
        for (int i = 0; i < _sfxPlayers.Length; i++)
        {
            _sfxPlayers[i].volume = sfxVolum;
        }

        vfxVolumeTxt.text = $"SFX Sound : {(int)(sfxVolum * 100)}%";
        bgmVolumeTxt.text = $"BGM Sound : {(int)(bgmVolume * 100)}%";
    }

    private void Init()
    {
        //����� �ʱ�ȭ
        GameObject bgmObject = new GameObject("BgmPlayers");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;
        bgmPlayer.Play();

        //ȿ���� �÷��̾� �ʱ�ȭ
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        _sfxPlayers = new AudioSource[channels];

        for (int i = 0; i < _sfxPlayers.Length; i++)
        {
            _sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            _sfxPlayers[i].playOnAwake = false;
            _sfxPlayers[i].volume = sfxVolum;
        }
    }

    public void PlayerSFX(SfxType sfxType)
    {
        for (int i = 0; i < _sfxPlayers.Length; i++)
        {
            int loopIndex = (i + _channelIndex) % _sfxPlayers.Length;

            if (_sfxPlayers[loopIndex].isPlaying)
            {
                continue;
            }
            _channelIndex = loopIndex;

            foreach (var item in sfxClipSO)
            {
                if (item.sfxType == sfxType) _sfxPlayers[loopIndex].clip = item.audioClip;
                _sfxPlayers[loopIndex].outputAudioMixerGroup = audioMixer.outputAudioMixerGroup;

                if (sfxType == SfxType.BGM)
                {
                    _sfxPlayers[loopIndex].outputAudioMixerGroup = BGMGroup;
                }
                else
                {
                    _sfxPlayers[loopIndex].outputAudioMixerGroup = SFXGroup;
                }

            }
            //Debug.Log(_sfxPlayers[loopIndex]);
            _sfxPlayers[loopIndex].Play();
            break;
        }
    }
}
