using System;
using System.Collections.Generic;
using System.Linq;

namespace CRZ.Framework.Blockchain
{
    public class Chain<T>
        where T : class
    {
        readonly List<Block<T>> _blocks = new List<Block<T>>();

        public IReadOnlyCollection<Block<T>> Blocks => _blocks;

        public Chain()
        {
            InitializeChain();
        }

        public Block<T> GetLatest()
        {
            return _blocks[_blocks.Count - 1];
        }

        public Block<T> GetBlock(Func<Block<T>, bool> predicate)
        {
            return _blocks.SingleOrDefault(predicate);
        }

        public bool Validate()
        {
            if (_blocks.Count > 1)
            {
                for (int i = 1; i < _blocks.Count; i++)
                {
                    Block<T> currBlock = _blocks[i];
                    Block<T> prevBlock = _blocks[i - 1];

                    if (currBlock.Hash != currBlock.CalculateHash())
                        return false;

                    if (currBlock.Hash != prevBlock.Hash)
                        return false;
                }
            }

            return true;
        }

        public void AddBlock(Block<T> block)
        {
            if (!block.Validate()) throw new InvalidOperationException("invalid block");

            var latestBlock = GetLatest();

            block.Index = latestBlock.Index + 1;
            block.PreviousHash = latestBlock.Hash;

            _blocks.Add(block);
        }

        public void AddBlock(Block<T> block, int difficulty)
        {
            block.Mine(difficulty);

            AddBlock(block);
        }

        void InitializeChain()
        {
            var block = new Block<T>(DateTimeOffset.Now, null);

            _blocks.Add(block);
        }
    }
}
