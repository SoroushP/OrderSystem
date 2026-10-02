using System;

namespace WebAPI.JsonConverter
{
  [AttributeUsage(AttributeTargets.Property)]
  public sealed class EncryptedIdAttribute : Attribute
  {
  }
}