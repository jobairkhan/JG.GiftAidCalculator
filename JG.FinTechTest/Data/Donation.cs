using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace JG.FinTechTest.Data
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class Donation : IEquatable<Donation>
    {
        [Key]
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string PostCode { get; private set; }
        public decimal Amount { get; private set; }
        public decimal GiftAid { get; private set; }

        private Donation(string name, string postCode, decimal amount, decimal giftAid)
        {
            Name = name;
            PostCode = postCode;
            Amount = amount;
            GiftAid = giftAid;
        }

        public static Donation MakeNew(string name, string postCode, decimal amount, decimal giftAid)
        {
            return new Donation(name, postCode, amount, giftAid);
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