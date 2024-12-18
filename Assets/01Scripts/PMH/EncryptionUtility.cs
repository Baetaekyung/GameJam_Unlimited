using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class EncryptionUtility
{
    private static readonly string Key = "1234567890123456"; //16자리 또는 24자리 또는 32자리를 가질 수 있음

    //암호화
    public static string Encrypt(string plainText)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(Key);
        using Aes aes = Aes.Create(); //Aes란 암호화 알고리즘 중 하나 16, 24, 32바이트 중 하나의 키 값으로 암호화한다.
        aes.Key = keyBytes; //Key
        aes.IV = new byte[16]; //중복적인 문자열 방지 벡터

        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV); //암호화하는거 생성
        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText); //바이트 형태로 저장
        //암호화
        byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

        return Convert.ToBase64String(encryptedBytes); //문자열 형태로 다시 전환
    }

    //암호화 해제
    public static string Decrypt(string encryptedText)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(Key); //위와 같음
        using Aes aes = Aes.Create();
        aes.Key = keyBytes;
        aes.IV = new byte[16];

        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV); //암호 해석자 생성
        byte[] encryptedBytes = Convert.FromBase64String(encryptedText); //문자열 형태로 받기
        //암호 해석
        byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

        return Encoding.UTF8.GetString(decryptedBytes); //문자열형태로 받음
    }
}