using System;

namespace FilamentFrontier.DarkWebSim
{
    public sealed class CryptoWallet
    {
        public string WalletId { get; }
        public decimal Balance { get; private set; }

        public CryptoWallet(string walletId)
        {
            WalletId = walletId;
            Balance = 0m;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0m)
            {
                return;
            }

            Balance += amount;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0m || amount > Balance)
            {
                return false;
            }

            Balance -= amount;
            return true;
        }

        public bool PurchaseBlueprint(decimal cost)
        {
            return Withdraw(cost);
        }

        public void ApplyNetworkFee(decimal fee)
        {
            if (fee <= 0m)
            {
                return;
            }

            Balance = Math.Max(0m, Balance - fee);
        }
    }
}
