using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace CRZ.Framework.Blockchain
{
    public class Block<T>
        where T : class
    {
        readonly List<T> _data = new List<T>();

        public int Index { get; internal set; }

        public DateTimeOffset Timestamp { get; internal set; }

        public string PreviousHash { get; internal set; }

        public string Hash { get; private set; }

        public int Nonce { get; private set; }

        public IReadOnlyCollection<T> Data => _data;

        public Block(DateTimeOffset timestamp, T data)
        {
            Index = 0;
            Timestamp = timestamp;
            PreviousHash = null;
            Nonce = 0;
            _data.Add(data);
            Hash = CalculateHash();
        }

        public bool Validate()
        {
            return Hash == CalculateHash();
        }

        public void AddData(T data)
        {
            _data.Add(data);
            Hash = CalculateHash();
        }

        protected internal string CalculateHash()
        {
            SHA256 sHA256 = SHA256.Create();
            byte[] input = Encoding.ASCII.GetBytes(
                $"{Timestamp}-{PreviousHash ?? ""}-{Nonce}-{JsonConvert.SerializeObject(_data)}");
            byte[] output = sHA256.ComputeHash(input);

            return Convert.ToBase64String(output);
        }

        protected internal void Mine(int difficulty)
        {
            var leadingZeros = new string('0', difficulty);

            while (Hash == null || Hash.Substring(0, difficulty) != leadingZeros)
            {
                Nonce++;
                Hash = CalculateHash();
            }
        }
    }
}
