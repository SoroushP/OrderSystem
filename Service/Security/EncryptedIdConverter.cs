using Domain;
using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Service.Security
{
  public class EncryptedIdConverter : JsonConverter
  {
    public override bool CanConvert(Type objectType)
    {
      return objectType == typeof(int);
    }

    public override void WriteJson(
        JsonWriter writer,
        object value,
        JsonSerializer serializer)
    {
      if (value == null)
      {
        writer.WriteNull();
        return;
      }
      var id = (int)value;
      var encryptedId = Encrypt(id);
      writer.WriteValue(encryptedId);
    }

    public override object ReadJson(
        JsonReader reader,
        Type objectType,
        object existingValue,
        JsonSerializer serializer)
    {
      if (reader.TokenType == JsonToken.Null)
      {
        return 0;
      }
      if (reader.TokenType != JsonToken.String)
      {
        throw new JsonSerializationException(
            "Encrypted ID must be a string.");
      }
      try
      {
        return Decrypt(reader.Value.ToString());
      }
      catch (Exception ex)
      {
        throw new JsonSerializationException(
            "Invalid encrypted ID.",
            ex);
      }
    }

    public override bool CanWrite => true;

    public override bool CanRead => true;

    private static string Encrypt(int id)
    {
      var key = Encoding.UTF8.GetBytes(
          Constants.EncryptionKey);

      using (var aes = Aes.Create())
      {
        aes.Key = key;
        aes.GenerateIV();

        var plainBytes = Encoding.UTF8.GetBytes(
            id.ToString());

        using (var encryptor = aes.CreateEncryptor())
        {
          var cipherBytes = encryptor.TransformFinalBlock(
              plainBytes,
              0,
              plainBytes.Length);

          // IV + CipherText
          var result = new byte[
              aes.IV.Length + cipherBytes.Length];

          Buffer.BlockCopy(
              aes.IV,
              0,
              result,
              0,
              aes.IV.Length);

          Buffer.BlockCopy(
              cipherBytes,
              0,
              result,
              aes.IV.Length,
              cipherBytes.Length);

          return Convert.ToBase64String(result)
              .Replace("+", "-")
              .Replace("/", "_")
              .TrimEnd('=');
        }
      }
    }

    private static int Decrypt(string encryptedId)
    {
      var base64 = encryptedId
          .Replace("-", "+")
          .Replace("_", "/");

      switch (base64.Length % 4)
      {
        case 2:
          base64 += "==";
          break;

        case 3:
          base64 += "=";
          break;
      }

      var data = Convert.FromBase64String(base64);

      var key = Encoding.UTF8.GetBytes(
          Constants.EncryptionKey);

      using (var aes = Aes.Create())
      {
        aes.Key = key;

        var ivLength = aes.BlockSize / 8;

        if (data.Length <= ivLength)
          throw new CryptographicException(
              "Invalid encrypted ID.");

        var iv = new byte[ivLength];

        var cipherBytes = new byte[
            data.Length - ivLength];

        Buffer.BlockCopy(
            data,
            0,
            iv,
            0,
            ivLength);

        Buffer.BlockCopy(
            data,
            ivLength,
            cipherBytes,
            0,
            cipherBytes.Length);

        aes.IV = iv;

        using (var decryptor = aes.CreateDecryptor())
        {
          var plainBytes = decryptor.TransformFinalBlock(
              cipherBytes,
              0,
              cipherBytes.Length);

          var plainText =
              Encoding.UTF8.GetString(plainBytes);

          return int.Parse(plainText);
        }
      }
    }
  }
}