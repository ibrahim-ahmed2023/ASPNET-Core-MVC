
using ContactsManager.Core.Domain;

namespace ContactsManager.Core.DTOs
{ /// <summary>
  /// DTO class for adding a new country as a request
  /// </summary>
    public class CountryAddRequest
    {
        public string? CountryName { get; set; }

        public Country ToCountry()
        {
            return new Country() { CountryName = this.CountryName };
        }

    }
}
