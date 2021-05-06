using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.Models;
using Microsoft.IdentityModel.Tokens;

namespace api.Business_Logic
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        public readonly string _key;
      
        public JwtAuthenticationManager(string key) => _key = key;

        string IJwtAuthenticationManager.Key { get => _key;  }
    }
}