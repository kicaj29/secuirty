// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

Console.WriteLine("Hello, World!");

StringBuilder sb = new();
for (int index = 0; index < 100; index++)
{
    sb.Append(Guid.NewGuid().ToString());
    sb.Append("CounterClockwise");
}
string textToHash = sb.ToString();
byte[] sourceBytes = Encoding.UTF8.GetBytes(textToHash);

Stopwatch sw = new Stopwatch();
sw.Start();
byte[] hashBytes = SHA256.HashData(sourceBytes);
string checksumSha256 = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
sw.Stop();

Console.WriteLine("----SHA256----");
Console.WriteLine(sw.Elapsed.ToString());
Console.WriteLine(checksumSha256);

sw.Start();
hashBytes = SHA1.HashData(sourceBytes);
string checksumSha1 = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
sw.Stop();


Console.WriteLine("----SHA1----");
Console.WriteLine(sw.Elapsed.ToString());
Console.WriteLine(checksumSha1);

Console.ReadKey();