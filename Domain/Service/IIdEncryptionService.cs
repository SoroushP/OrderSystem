namespace Domain.Service
{
  public interface IIdEncryptionService
  {
    string Encrypt(int id);
    int Decrypt(string encryptedId);
  }
}
