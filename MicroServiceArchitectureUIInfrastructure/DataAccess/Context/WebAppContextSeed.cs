﻿using MicroServiceArchitectureUIDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MicroServiceArchitectureUIInfrastructure.DataAccess.Context
{
    public class WebAppContextSeed
    {
        public static async Task SeedAsync(WebAppContext webAppContext, ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            try
            {
                webAppContext.Database.Migrate();
                if (!webAppContext.Users.Any())
                {
                    webAppContext.Users.AddRange(GetPreconfiguredUsers());
                    await webAppContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 50)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<WebAppContextSeed>();
                    log.LogError(ex.Message);
                    Thread.Sleep(2000);
                    await SeedAsync(webAppContext, loggerFactory, retryForAvailability);
                }
            }
        }

        private static IEnumerable<User> GetPreconfiguredUsers()
        {
            return new List<User>()
            {
                new User
                {
                    FirstName ="Kurt",
                    LastName = "Gödel",
                    IsSeller = true,
                    IsBuyer = false
                }
            };
        }
    }
}
