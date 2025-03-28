﻿
using ContactsManager.Core.Contracts.ServiceContracts.CountryContracts;
using ContactsManager.Core.Domain;
using ContactsManager.Core.DTOs;
using static ContactsManager.Core.Contracts.RepoContracts.IcountriesRepository;

namespace ContactsManager.Core.Services.CountryServices
{
    public class CountriesAdderService : ICountriesAdderService
    {
        //private field
        private readonly ICountriesRepository _countriesRepository;

        //constructor
        public CountriesAdderService(ICountriesRepository countriesRepository)
        {
            _countriesRepository = countriesRepository;
        }

        public async Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest)
        {
            //Validation: countryAddRequest parameter can't be null
            ArgumentNullException.ThrowIfNull(countryAddRequest);

            //Validation: CountryName can't be null
            if (countryAddRequest.CountryName == null)
            {
                throw new ArgumentException(nameof(countryAddRequest.CountryName));
            }

            //Validation: CountryName can't be duplicate
            if (await _countriesRepository.GetCountryByCountryName(countryAddRequest.CountryName) != null)
            {
                throw new ArgumentException("Given country name already exists");
            }

            //Convert object from CountryAddRequest to Country type
            Country country = countryAddRequest.ToCountry();

            //generate CountryID
            country.CountryId = Guid.NewGuid();

            //Add country object into _countries
            await _countriesRepository.AddCountry(country);

            return country.ToCountryResponse();
        }
    }
}
