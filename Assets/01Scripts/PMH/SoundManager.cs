using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public enum SfxType
{
    BGM = 0,
    EFFECT,
    BALLDETECT,
    BALLJUMP,
    TAKEITEM,
    LASERACTIVE,
    TURRETACTIVE,
    BULLETHIT,
    LASERHIT,
    JUMPPAD,
    GameClear
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

    private AudioSource bgmPlayer;

    [Header("SFX")]
    public SfxSO[] sfxClipSO;
    
    public int channels;

    public struct VolumeValues
    {
        public float sfxVolum;
        public float bgmVolume;
    }

    private VolumeValues vV;

    private AudioSource[] _sfxPlayers;
    private int _channelIndex;

    protected override void Awake()
    {
        base.Awake();
        Init();

        if (SaveManager.Exist("Sounds.json"))
        {
            vV = SaveManager.Load<VolumeValues>("Sounds.json");
        }
        else
        {
            vV = new VolumeValues();
            vV.bgmVolume = 0.1f;
            vV.sfxVolum = 0.1f;
            SaveManager.Save(vV, "Sounds.json");
        }

        bgmController.value = vV.bgmVolume;
        vfxController.value = vV.sfxVolum;
        vV.bgmVolume = bgmController.value;
        vV.sfxVolum = vfxController.value;
        SetVolume(0);

        bgmController.onValueChanged?.AddListener(SetVolume);
        vfxController.onValueChanged?.AddListener(SetVolume);
    }
    private void SetVolume(float volume)
    {
        vV.bgmVolume = bgmController.value;
        bgmPlayer.volume = vV.bgmVolume;

        vV.sfxVolum = vfxController.value;
        for (int i = 0; i < _sfxPlayers.Length; i++)
        {
            _sfxPlayers[i].volume = vV.sfxVolum;
        }

        vfxVolumeTxt.text = $"SFX Sound : {(int)(vV.sfxVolum * 100)}%";
        bgmVolumeTxt.text = $"BGM Sound : {(int)(vV.bgmVolume * 100)}%";
    }

    public void SetVolumes()
    {
        SaveManager.Save(vV, "Sounds.json");
    }

    private void Init()
    {
        //����� �ʱ�ȭ
        GameObject bgmObject = new GameObject("BgmPlayers");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = vV.bgmVolume;
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
            _sfxPlayers[i].volume = vV.sfxVolum;
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
