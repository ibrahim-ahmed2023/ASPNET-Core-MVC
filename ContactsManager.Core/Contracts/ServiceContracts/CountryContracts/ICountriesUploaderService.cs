﻿
using Microsoft.AspNetCore.Http;

namespace ContactsManager.Core.Contracts.ServiceContracts.CountryContracts
{

    /// <summary>
    /// Represents business logic (bulk insert) for manipulating Country entity
    /// </summary>
    public interface ICountriesUploaderService
    {
        /// <summary>
        /// Uploads countries from excel file into database
        /// </summary>
        /// <param name="formFile">Excel file with list of countries</param>
        /// <returns>Returns number of countries added</returns>
        Task<int> UploadCountriesFromExcelFile(IFormFile formFile);
    }
}
