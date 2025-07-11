//-----------------------------------------------------------------------
// <copyright file="IdName.cs" company="Home">
//     Author: Horus Ra
//     Copyright (c) Home. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace WebDocMobile.Models
{
    public class IdName<T, R> where T : IConvertible, IComparable<T>, IEquatable<T>
    {
        public T Id { get; set; }

        public R Name { get; set; }

        public override int GetHashCode() => Id.GetHashCode();

        public override bool Equals(object obj) => obj == this || obj is IdName<T, R> idObj && Id.Equals(idObj.Id);
    }
}
