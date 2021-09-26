using System;
using System.Linq;
using NBitcoin;

namespace NBitcoinConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            GenerateSimpleKeys();
        }

        private static void GenerateSimpleKeys()
        {
            var keys = new[]
            {
                new Key(fCompressedIn: true),
                new Key(fCompressedIn: false),
            };

            var networks = new[]
            {
                Network.Main,
                Network.TestNet,
            };

            var keysWithNetworks =
            from k in keys
            from n in networks
            select new
            {
                Key = k,
                Network = n,
                Wif = k.GetWif(n),
            };

            foreach (var knw in keysWithNetworks)
            {
                Console.WriteLine($"Network: {knw.Network}");
                Console.WriteLine($"Is Compressed: {knw.Key.IsCompressed}");
                Console.WriteLine($"Private Key: {knw.Wif}");
                Console.WriteLine($"Public Address (Legacy): {knw.Wif.GetAddress(ScriptPubKeyType.Legacy)}");
                Console.WriteLine($"Public Address (Segwit): {knw.Wif.GetAddress(ScriptPubKeyType.Segwit)}");
                Console.WriteLine($"Public Address (SegwitP2SH): {knw.Wif.GetAddress(ScriptPubKeyType.SegwitP2SH)}");
                Console.WriteLine();
            }
        }
    }
}
