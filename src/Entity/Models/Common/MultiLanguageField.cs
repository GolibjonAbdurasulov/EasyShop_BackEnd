using System;
using Newtonsoft.Json;

namespace Entity.Models.Common
{
    public class MultiLanguageField
    {
        
        protected bool Equals(MultiLanguageField other)
        {
            return uz == other.uz && ru == other.ru && kr == other.kr ;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MultiLanguageField)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(uz, ru, kr);
        }

        /// <summary>
        /// O`zbek (Lotin) tilida
        /// </summary>
        public string uz { get; set; }

        /// <summary>
        /// Rus tilida
        /// </summary>
        public string ru { get; set; }

        /// <summary>
        /// kiril alifbosi
        /// </summary>
        public string kr { get; set; }
        
 

        public static implicit operator MultiLanguageField(string data) => new MultiLanguageField()
        {
            ru = data,
            uz = data,
            kr = data,
        };

        public static bool operator ==(MultiLanguageField a, string b)
        {
            return a.ru == b || a.kr == b || a.uz == b ;
        }

        public static bool operator !=(MultiLanguageField a, string b)
        {
            return a.ru != b && a.kr != b && a.uz != b;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}