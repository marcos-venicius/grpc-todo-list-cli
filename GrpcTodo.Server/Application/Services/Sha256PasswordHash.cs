using System.Security.Cryptography;
using System.Text;
using GrpcTodo.Server.Domain.Services;
namespace GrpcTodo.Server.Application.Services;

public sealed class Sha256PasswordHash : IPasswordHashingService
{
    public string Hash(string password)
    {
        var bytes = Encoding.UTF8.GetBytes(password);

        var computedHash = SHA256.HashData(bytes);

        StringBuilder hash = new();

        foreach (var singleByte in computedHash)
        {
            hash.Append(singleByte.ToString("x2"));
        }

        return hash.ToString().ToLower();
    }
}