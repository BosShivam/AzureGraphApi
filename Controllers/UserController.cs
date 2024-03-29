﻿using Azure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace AzureGraphApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly GraphServiceClient _graphServiceClient;

        public UserController()
        {
            var tenantId = "fcdc0681-bfca-461b-8b19-a0d53054abfa";
            var clientId = "85fc1ee0-e915-4afd-bfbb-e74bfa74f93d";
            var clientSecret = "x_.8Q~6iQmI2a5-LqG9DabJ2Hi1urgbB6IGpqdrS";
            var clientSecretCredential = new ClientSecretCredential(
            tenantId, clientId, clientSecret, new TokenCredentialOptions { AuthorityHost = AzureAuthorityHosts.AzurePublicCloud });


            _graphServiceClient = new GraphServiceClient(clientSecretCredential);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var userCollection = await _graphServiceClient.Users.GetAsync();

            return Ok(userCollection.Value);
        }


        [HttpGet("userId")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var user = await _graphServiceClient.Users[userId].GetAsync();

            return Ok(user);
        }
    }
}
