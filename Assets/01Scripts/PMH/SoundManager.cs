using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public enum SfxType
{
    BGM = 0, EFFECT = 1
}

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Slider vfxController;
    [SerializeField] private Slider bgmController;

    public AudioMixer audioMixer;
    public AudioMixerGroup MasterGroup;
    public AudioMixerGroup BGMGroup;
    public AudioMixerGroup SFXGroup;
    //�ϴ���

    private readonly string sound_master = "Master";
    private readonly string sound_bgm = "BGM";
    private readonly string sound_sfx = "SFX";

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

    private void Awake()
    {
        Init();
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    Debug.Log("�־ȵ�?");
        //    _sfxPlayers[0].Play();
        //    PlayerSFX(SfxType.EFFECT);
        //}
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
            Debug.Log(_sfxPlayers[loopIndex]);
            _sfxPlayers[loopIndex].Play();
            break;
        }
    }
}
