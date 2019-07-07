using System;

namespace JG.FinTechTest.Data
{
    public class Donation : IEquatable<Donation>
    {
        public int Id { get; }
        public string Name { get; }
        public string PostCode { get; }
        public decimal Amount { get; }
        public decimal GiftAid { get; }

        private Donation()
        {
        }
        private Donation(int id, string name, string postCode, decimal amount, decimal giftAid)
        {
            Id = id;
            Name = name;
            PostCode = postCode;
            Amount = amount;
            GiftAid = giftAid;
        }

        public static Donation MakeNew(string name, string postCode, decimal amount, decimal giftAid)
        {
            return new Donation(0, name, postCode, amount, giftAid);
        }

        public bool Equals(Donation other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Donation) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}