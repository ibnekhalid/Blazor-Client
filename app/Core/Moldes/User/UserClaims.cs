﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace app.Core.Moldes.User
{
	public class UserClaims
	{
		public string Id { get; private set; }
		//public string CompanyId { get; private set; }
		//public string Email { get; private set; }
		public string Username { get; private set; }
		//public string CompanyName { get; private set; }
		public UserClaims()
		{

		}
		public UserClaims(string token)
		{
			var identity = string.IsNullOrEmpty(token)
			? new ClaimsIdentity()
			: new ClaimsIdentity(ServiceExtensions.ParseClaimsFromJwt(token), "jwt");
			Id = identity.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;
			Username = identity.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.UniqueName).Value;
		}
	}
	public static class ServiceExtensions
	{
		public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
		{
			var payload = jwt.Split('.')[1];
			var jsonBytes = ParseBase64WithoutPadding(payload);
			var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
			return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
		}

		private static byte[] ParseBase64WithoutPadding(string base64)
		{
			switch (base64.Length % 4)
			{
				case 2: base64 += "=="; break;
				case 3: base64 += "="; break;
			}
			return Convert.FromBase64String(base64);
		}
	}
}
