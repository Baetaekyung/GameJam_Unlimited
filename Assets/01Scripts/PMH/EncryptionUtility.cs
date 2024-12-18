using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class EncryptionUtility
{
    private static readonly string Key = "1234567890123456"; //16�ڸ� �Ǵ� 24�ڸ� �Ǵ� 32�ڸ��� ���� �� ����

    //��ȣȭ
    public static string Encrypt(string plainText)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(Key);
        using Aes aes = Aes.Create(); //Aes�� ��ȣȭ �˰��� �� �ϳ� 16, 24, 32����Ʈ �� �ϳ��� Ű ������ ��ȣȭ�Ѵ�.
        aes.Key = keyBytes; //Key
        aes.IV = new byte[16]; //�ߺ����� ���ڿ� ���� ����

        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV); //��ȣȭ�ϴ°� ����
        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText); //����Ʈ ���·� ����
        //��ȣȭ
        byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

        return Convert.ToBase64String(encryptedBytes); //���ڿ� ���·� �ٽ� ��ȯ
    }

    //��ȣȭ ����
    public static string Decrypt(string encryptedText)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(Key); //���� ����
        using Aes aes = Aes.Create();
        aes.Key = keyBytes;
        aes.IV = new byte[16];

        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV); //��ȣ �ؼ��� ����
        byte[] encryptedBytes = Convert.FromBase64String(encryptedText); //���ڿ� ���·� �ޱ�
        //��ȣ �ؼ�
        byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

        return Encoding.UTF8.GetString(decryptedBytes); //���ڿ����·� ����
    }
}