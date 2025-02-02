﻿// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using Azure.Core;
using Azure.ResourceManager.AppService.Models;
using Azure.ResourceManager.AppService;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager;
using Azure;
using System.Threading.Tasks;

namespace InternTrack.Portal.Web.Infrastructure.Provision.Brokers.Clouds
{
    public partial class CloudBroker
    {
        public async ValueTask<WebSiteResource> CreateWebAppAsync(
            string webAppName,
            string databaseConnectionString,
            AppServicePlanResource plan,
            ResourceGroupResource resourceGroup)
        {
            var webSiteData =
                new WebSiteData(AzureLocation.WestUS3)
                {
                    AppServicePlanId = plan.Id,

                    SiteConfig = new SiteConfigProperties()
                    {
                        ConnectionStrings =
                        {
                            new ConnStringInfo()
                            {
                                Name = "DefaultConnection",

                                ConnectionString =
                                    databaseConnectionString,

                                ConnectionStringType =
                                    ConnectionStringType.SqlAzure
                            }
                        },

                        NetFrameworkVersion = "v7.0"
                    }
                };

            ArmOperation<WebSiteResource> webApp =
                await resourceGroup.GetWebSites()
                    .CreateOrUpdateAsync(
                        WaitUntil.Completed,
                            webAppName,
                                webSiteData);

            return webApp.Value;
        }
    }
}
