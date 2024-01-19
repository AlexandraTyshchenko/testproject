using BLL.Exceptions;
using BLL.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using DAL.Context;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using BLL.Exceptions;
using BLL.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using DAL.Context;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BLL.Services
{
    public class CSVUploader : ICSVUploader
    {
        private readonly ApplicationContext _context;

        public CSVUploader(ApplicationContext context)
        {
            _context = context;
        }

        public class Person
        {
            public int Id { get; set; }

            [Required]
            public string Name { get; set; }

            [Required]
            [DataType(DataType.Date)]
            public DateTime DateOfBirth { get; set; }

            public bool IsMarried { get; set; }

            [StringLength(10, ErrorMessage = "Phone number cannot be longer than 10 characters.")]
            public string Phone { get; set; }

            [Required]
            public decimal Salary { get; set; }
        }

        public async Task UploadScv<T, K>(IFormFile file) where T : class where K : ClassMap<T>
        {
            var allowedExtensions = new[] { ".csv" };

            if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLowerInvariant()))
            {
                throw new BadRequestException("Incorrect format");
            }

            var conf = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };

            using (var streamReader = new StreamReader(file.OpenReadStream()))
            using (var csvReader = new CsvReader(streamReader, conf))
            {
                csvReader.Context.RegisterClassMap<K>();
                await csvReader.ReadAsync();

                await foreach (var record in csvReader.GetRecordsAsync<T>())
                {
                    // Perform validation before adding to the context
                    var validationContext = new ValidationContext(record, serviceProvider: null, items: null);
                    var validationResults = new List<ValidationResult>();
                    var isValid = Validator.TryValidateObject(record, validationContext, validationResults, validateAllProperties: true);

                    if (!isValid)
                    {
                        // Validation failed, throw an exception or handle accordingly
                        var errorMessage = string.Join(Environment.NewLine, validationResults.Select(vr => vr.ErrorMessage));
                        throw new BadRequestException(errorMessage);
                    }

                    await _context.Set<T>().AddAsync(record);
                }

                await _context.SaveChangesAsync();
            }
        }

    }
}
